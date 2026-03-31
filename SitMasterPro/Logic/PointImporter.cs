using System;
using System.IO;
using System.Collections.Generic;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using Autodesk.Civil.ApplicationServices;
using Autodesk.Civil.DatabaseServices;
using SitMasterPro.UI;

namespace SitMasterPro.Logic
{
    public class PointImporter
    {
        [CommandMethod("_SMP_IMPORTPOINTS")]
        public static void ImportPoints()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Editor ed = doc.Editor;
            CivilDocument civilDoc = CivilApplication.ActiveDocument;

            ImportPointsWindow window = new ImportPointsWindow();
            if (Application.ShowModalWindow(window) != true) return;

            string? filePath = window.SelectedFilePath;
            string? format = window.SelectedFormat;

            if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(format)) return;

            try
            {
                using (Transaction tr = doc.Database.TransactionManager.StartTransaction())
                {
                    CogoPointCollection points = civilDoc.CogoPoints;
                    int count = 0;

                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] parts = line.Split(',', ';', ' ', '\t');
                            if (parts.Length < 3) continue;

                            double n = 0, e = 0, z = 0;
                            string desc = string.Empty;

                            // Dynamic parsing based on selected format
                            if (format.Contains("PNEZD")) { e = Parse(parts, 2); n = Parse(parts, 1); z = Parse(parts, 3); desc = GetDesc(parts, 4); }
                            else if (format.Contains("PENZD")) { e = Parse(parts, 1); n = Parse(parts, 2); z = Parse(parts, 3); desc = GetDesc(parts, 4); }
                            else if (format.Contains("PNEZ")) { e = Parse(parts, 2); n = Parse(parts, 1); z = Parse(parts, 3); }
                            else if (format.Contains("ENZ")) { e = Parse(parts, 0); n = Parse(parts, 1); z = Parse(parts, 2); }

                            Autodesk.AutoCAD.Geometry.Point3d p3d = new Autodesk.AutoCAD.Geometry.Point3d(e, n, z);
                            ObjectId pointId = points.Add(p3d, false);
                            CogoPoint point = (CogoPoint)tr.GetObject(pointId, OpenMode.ForWrite);

                            if (!string.IsNullOrEmpty(desc)) point.RawDescription = desc;
                            count++;
                        }
                    }

                    tr.Commit();
                    ed.WriteMessage($"\nSitMaster Pro: Successfully imported {count} points from {Path.GetFileName(filePath)}.");
                }
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage($"\nSitMaster Pro Error: {ex.Message}");
            }
        }

        private static double Parse(string[] parts, int index)
        {
            if (index < parts.Length && double.TryParse(parts[index], out double val)) return val;
            return 0;
        }

        private static string GetDesc(string[] parts, int index)
        {
            if (index < parts.Length) return parts[index];
            return string.Empty;
        }
    }
}
