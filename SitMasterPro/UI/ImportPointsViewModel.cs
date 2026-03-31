using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Win32;
using SitMasterPro.Models;

namespace SitMasterPro.UI
{
    public class ImportPointsViewModel : INotifyPropertyChanged
    {
        private string? _filePath;
        private PointFormat? _selectedFormat;

        public string? FilePath
        {
            get => _filePath;
            set { _filePath = value; OnPropertyChanged(); }
        }

        public PointFormat? SelectedFormat
        {
            get => _selectedFormat;
            set { _selectedFormat = value; OnPropertyChanged(); }
        }

        public ObservableCollection<PointFormat> Formats { get; }

        public ICommand BrowseCommand { get; }

        public ImportPointsViewModel()
        {
            Formats = new ObservableCollection<PointFormat>
            {
                new PointFormat { Name = "PNEZD (comma delimited)", Format = "PNEZD", Description = "Point, Northing, Easting, Elevation, Description" },
                new PointFormat { Name = "PENZD (comma delimited)", Format = "PENZD", Description = "Point, Easting, Northing, Elevation, Description" },
                new PointFormat { Name = "PNEZ (comma delimited)", Format = "PNEZ", Description = "Point, Northing, Easting, Elevation" },
                new PointFormat { Name = "ENZ (space delimited)", Format = "ENZ", Description = "Easting, Northing, Elevation" }
            };

            BrowseCommand = new RelayCommand(BrowseFile);
        }

        private void BrowseFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt;*.csv)|*.txt;*.csv|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool>? _canExecute;

        public RelayCommand(Action execute, Func<bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter) => _canExecute == null || _canExecute();
        public void Execute(object? parameter) => _execute();
        public event EventHandler? CanExecuteChanged;
    }
}
