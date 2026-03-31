using System;
using System.Reflection;
using System.Windows.Media.Imaging;
using Autodesk.AutoCAD.Runtime;
using Autodesk.Windows;

[assembly: ExtensionApplication(typeof(SitMasterPro.RibbonHandler))]

namespace SitMasterPro
{
    /// <summary>
    /// Handles the creation and management of the SitMaster Pro Ribbon interface.
    /// </summary>
    public class RibbonHandler : IExtensionApplication
    {
        private const string TabTitle = "SITMASTER PRO";
        private const string PanelTitle = "SURVEY TOOLS";

        public void Initialize()
        {
            RibbonControl ribbon = ComponentManager.Ribbon;
            if (ribbon != null)
            {
                RibbonTab tab = ribbon.FindTab(TabTitle);
                if (tab == null)
                {
                    tab = new RibbonTab();
                    tab.Title = TabTitle;
                    tab.Id = TabTitle;
                    ribbon.Tabs.Add(tab);
                }

                AddSurveyPanel(tab);
                tab.IsActive = true;
            }
        }

        private void AddSurveyPanel(RibbonTab tab)
        {
            RibbonPanelSource source = new RibbonPanelSource { Title = PanelTitle };
            RibbonPanel panel = new RibbonPanel { Source = source };
            tab.Panels.Add(panel);

            RibbonButton btnImport = new RibbonButton
            {
                Text = "Import\nPoints",
                Name = "SMP_BtnImport",
                ShowText = true,
                ShowImage = true,
                Size = RibbonItemSize.Large,
                Orientation = System.Windows.Controls.Orientation.Vertical,
                CommandHandler = new RibbonCommandHandler(),
                CommandParameter = "._SMP_IMPORTPOINTS ",
                ToolTip = "Professional Point Import Tool",
                Description = "Import points from text/CSV files with various format options."
            };

            // Loading embedded resources for high-quality branding
            btnImport.LargeImage = LoadImage("SitMasterPro.Resources.icon32.png");
            btnImport.Image = LoadImage("SitMasterPro.Resources.icon16.png");

            source.Items.Add(btnImport);
        }

        public void Terminate() { }

        private BitmapImage? LoadImage(string resourceName)
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                using (var stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream == null) return null;

                    BitmapImage bmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.StreamSource = stream;
                    bmp.CacheOption = BitmapCacheOption.OnLoad;
                    bmp.EndInit();
                    return bmp;
                }
            }
            catch
            {
                return null;
            }
        }
    }

    public class RibbonCommandHandler : System.Windows.Input.ICommand
    {
        public bool CanExecute(object? parameter) => true;
        public event EventHandler? CanExecuteChanged;

        public void Execute(object? parameter)
        {
            if (parameter is string cmd)
            {
                Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.SendStringToExecute(cmd, true, false, true);
            }
        }
    }
}
