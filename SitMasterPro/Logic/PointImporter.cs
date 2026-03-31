using System;
using System.IO;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using Autodesk.Civil.ApplicationServices;
using Autodesk.Civil.DatabaseServices;
using SitMasterPro.UI;
using SitMasterPro.Models;

#if NET8_0_OR_GREATER
using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;
#else
using Application = Autodesk.AutoCAD.ApplicationServices.Application;
#endif

namespace SitMasterPro.Logic
{
    public class PointImporter
    {
        [CommandMethod("_SMP_IMPORTPOINTS")]
        public static void ImportPoints()
        {
            var doc = Application.DocumentManager.MdiActiveDocument;
            Editor ed = doc.Editor;
            CivilDocument civilDoc = CivilApplication.ActiveDocument;

            ImportPointsWindow window = new ImportPointsWindow();
            if (Application.ShowModalWindow(window) != true) return;

            string? filePath = window.ViewModel.FilePath;
            PointFormat? formatModel = window.ViewModel.SelectedFormat;

            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath) || formatModel == null)
            {
                ed.WriteMessage("\nSitMaster Pro: Invalid file or format selection.");
                return;
            }

            try
            {
                using (Transaction tr = doc.Database.TransactionManager.StartTransaction())
                {
                    CogoPointCollection points = civilDoc.CogoPoints;
                    int count = 0;
                    int skipped = 0;
                    string format = formatModel.Format;

                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string? line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (string.IsNullOrWhiteSpace(line)) continue;

                            // Using normal split to preserve empty columns (crucial for CSV stability)
                            string[] parts = line.Split(',', ';', ' ', '\t');
                            if (parts.Length < 3) { skipped++; continue; }

                            try
                            {
                                double n = 0, e = 0, z = 0;
                                string desc = string.Empty;

                                switch (format)
                                {
                                    case "PNEZD":
                                        n = Parse(parts, 1); e = Parse(parts, 2); z = Parse(parts, 3); desc = GetDesc(parts, 4);
                                        break;
                                    case "PENZD":
                                        e = Parse(parts, 1); n = Parse(parts, 2); z = Parse(parts, 3); desc = GetDesc(parts, 4);
                                        break;
                                    case "PNEZ":
                                        n = Parse(parts, 1); e = Parse(parts, 2); z = Parse(parts, 3);
                                        break;
                                    case "ENZ":
                                        e = Parse(parts, 0); n = Parse(parts, 1); z = Parse(parts, 2);
                                        break;
                                }

                                Autodesk.AutoCAD.Geometry.Point3d p3d = new Autodesk.AutoCAD.Geometry.Point3d(e, n, z);
                                ObjectId pointId = points.Add(p3d, false);
                                CogoPoint point = (CogoPoint)tr.GetObject(pointId, OpenMode.ForWrite);

                                if (!string.IsNullOrEmpty(desc)) point.RawDescription = desc;
                                count++;
                            }
                            catch { skipped++; }
                        }
                    }

                    tr.Commit();
                    ed.WriteMessage($"\nSitMaster Pro: Successfully imported {count} points.");
                    if (skipped > 0) ed.WriteMessage($"\nSitMaster Pro: Skipped {skipped} invalid/empty lines.");
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
            // Throwing exception to be caught and count as a skipped line
            throw new FormatException();
        }

        private static string GetDesc(string[] parts, int index)
        {
            if (index < parts.Length) return parts[index];
            return string.Empty;
        }
    }
}
