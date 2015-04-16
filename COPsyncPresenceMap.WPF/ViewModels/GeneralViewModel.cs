using System;
using Caliburn.Micro;
using COPsyncPresenceMap.WPF.Helpers;
using COPsyncPresenceMap.WPF.Services.Interfaces;
using System.Drawing;
using Microsoft.Win32;
using WinForms = System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace COPsyncPresenceMap.WPF.ViewModels
{
    public class GeneralViewModel : Screen
    {
        private readonly IPainterService _painterService;

        private string _spreadsheetPath;
        public string SpreadsheetPath
        {
            get { return _spreadsheetPath; }
            private set
            {
                if (_spreadsheetPath != value)
                {
                    _spreadsheetPath = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        private string _outputFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public string OutputFolder
        {
            get { return _outputFolder; }
            private set
            {
                if (_outputFolder != value)
                {
                    _outputFolder = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        private System.Windows.Media.Color _selectedFillColor = System.Windows.Media.Color.FromRgb(0, 100, 0);
        public System.Windows.Media.Color SelectedFillColor
        {
            get { return _selectedFillColor; }
            set
            {
                if (_selectedFillColor != value)
                {
                    _selectedFillColor = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public GeneralViewModel(IPainterService painterService)
        {
            _painterService = painterService;

            _painterService.Done += demoService_Done;
        }

        void demoService_Done(object sender, PainterServiceEventArgs e)
        {
            ProcessStartInfo l_psi = new ProcessStartInfo();
            l_psi.FileName = "explorer";
            l_psi.Arguments = string.Format("{0},/select", e.ResultPath);
            l_psi.UseShellExecute = true;
            Process l_newProcess = new Process();
            l_newProcess.StartInfo = l_psi;
            l_newProcess.Start();
        }

        public void Process()
        {
            var color = Color.FromArgb(SelectedFillColor.R, SelectedFillColor.G, SelectedFillColor.B);
            _painterService.Process("base-map.svg", OutputFolder, color, new[] { "TX_Bailey" });
        }

        public void SelectSpreadsheet()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XLSX files|*.xlsx";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    //TODO: parse spreadsheet
                    SpreadsheetPath = openFileDialog.FileName;
                }
                catch
                {
                    //TODO: show an error message
                }
            }
        }

        public void SelectOutputFolder()
        {
            var dialog = new WinForms.FolderBrowserDialog();
            dialog.SelectedPath = OutputFolder;
            if (dialog.ShowDialog() == WinForms.DialogResult.OK)
            {
                OutputFolder = dialog.SelectedPath;
            }
        }
    }
}
