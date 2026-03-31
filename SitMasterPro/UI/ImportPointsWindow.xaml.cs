using System;
using System.Windows;

namespace SitMasterPro.UI
{
    public partial class ImportPointsWindow : Window
    {
        public ImportPointsViewModel ViewModel { get; }

        public ImportPointsWindow()
        {
            InitializeComponent();
            ViewModel = new ImportPointsViewModel();
            DataContext = ViewModel;
        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ViewModel.FilePath))
            {
                MessageBox.Show("Please select a data source file first.", "SitMaster Pro", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (ViewModel.SelectedFormat == null)
            {
                MessageBox.Show("Please select a point format from the list.", "SitMaster Pro", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
