using System;
using System.Windows;
using Microsoft.Win32;

namespace SitMasterPro.UI
{
    public partial class ImportPointsWindow : Window
    {
        public string? SelectedFilePath { get; private set; }
        public string? SelectedFormat { get; private set; }

        public ImportPointsWindow()
        {
            InitializeComponent();
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt;*.csv)|*.txt;*.csv|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                txtFilePath.Text = openFileDialog.FileName;
            }
        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilePath.Text))
            {
                MessageBox.Show("Please select a file.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (lstFormats.SelectedItem == null)
            {
                MessageBox.Show("Please select a point format.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SelectedFilePath = txtFilePath.Text;
            SelectedFormat = (lstFormats.SelectedItem as System.Windows.Controls.ListBoxItem)?.Content.ToString();
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
