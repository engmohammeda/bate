using System;
using System.Reflection;
using System.Windows.Media.Imaging;
using Autodesk.AutoCAD.Runtime;
using Autodesk.Windows;

[assembly: ExtensionApplication(typeof(SitMasterPro.RibbonHandler))]

namespace SitMasterPro
{
    public class RibbonHandler : IExtensionApplication
    {
        private const string TabTitle = "SITMASTER PRO";
        private const string PanelTitle = "Points Management";

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

                AddPanel(tab);
                tab.IsActive = true;
            }
        }

        private void AddPanel(RibbonTab tab)
        {
            RibbonPanelSource source = new RibbonPanelSource();
            source.Title = PanelTitle;
            RibbonPanel panel = new RibbonPanel();
            panel.Source = source;
            tab.Panels.Add(panel);

            RibbonButton btnImport = new RibbonButton();
            btnImport.Text = "Import Points";
            btnImport.ShowText = true;
            btnImport.ShowImage = true;
            btnImport.Size = RibbonItemSize.Large;
            btnImport.Orientation = System.Windows.Controls.Orientation.Vertical;
            btnImport.CommandHandler = new RibbonCommandHandler();
            btnImport.CommandParameter = "._SMP_IMPORTPOINTS ";

            // Placeholder for icons
            // btnImport.LargeImage = LoadImage("SitMasterPro.Resources.icon32.png");
            // btnImport.Image = LoadImage("SitMasterPro.Resources.icon16.png");

            source.Items.Add(btnImport);
        }

        public void Terminate()
        {
        }

        private BitmapImage? LoadImage(string name)
        {
            try
            {
                var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name);
                if (stream == null) return null;

                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = stream;
                bmp.EndInit();
                return bmp;
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
