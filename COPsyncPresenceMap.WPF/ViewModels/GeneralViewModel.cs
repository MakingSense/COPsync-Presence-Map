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
using SpreadsheetUtilities;
using System.Linq;
using System.Collections.Generic;
using SvgUtilities;

namespace COPsyncPresenceMap.WPF.ViewModels
{
    public class GeneralViewModel : Screen
    {
        private readonly IPainterService _painterService;
        private readonly ISpreadsheetParsingService _spreadsheetParsingService;

        private Spreadsheet _spreadsheet;
        public Spreadsheet Spreadsheet
        {
            get { return _spreadsheet; }
            set
            {
                if (_spreadsheet != value)
                {
                    _spreadsheet = value;
                    ReadyToProcess = _spreadsheet != null;
                    NotifyOfPropertyChange();
                }
            }
        }

        private bool _readyToProcess;
        public bool ReadyToProcess
        {
            get { return _readyToProcess; }
            private set
            {
                if (_readyToProcess != value)
                {
                    _readyToProcess = value;
                    NotifyOfPropertyChange();
                }
            }
        }

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

        private bool _includeCOPsyncEnterprise = true;
        public bool IncludeCOPsyncEnterprise
        {
            get { return _includeCOPsyncEnterprise;}
            set
            {
                if (_includeCOPsyncEnterprise != value)
                {
                    _includeCOPsyncEnterprise = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        private bool _includeCOPsync911 = true;
        public bool IncludeCOPsync911
        {
            get { return _includeCOPsync911;}
            set
            {
                if (_includeCOPsync911 != value)
                {
                    _includeCOPsync911 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        private bool _includeWarrantsync = true;
        public bool IncludeWarrantsync
        {
            get { return _includeWarrantsync; }
            set
            {
                if (_includeWarrantsync != value)
                {
                    _includeWarrantsync = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        private string[] GetSelectedProducts()
        {
            var list = new List<string>();
            if (IncludeCOPsyncEnterprise)
            {
                list.Add(PresenceSpreadsheetHelpers.CHECKCOLUMN_COPSYNC_ENTERPRISE);
            }
            if (IncludeCOPsyncEnterprise)
            {
                list.Add(PresenceSpreadsheetHelpers.CHECKCOLUMN_COPSYNC911);
            }
            if (IncludeWarrantsync)
            {
                list.Add(PresenceSpreadsheetHelpers.CHECKCOLUMN_WARRANTSYNC);
            }
            return list.ToArray();
        }

        private Color GetSelectedColor()
        {
            return Color.FromArgb(SelectedFillColor.R, SelectedFillColor.G, SelectedFillColor.B);
        }

        public GeneralViewModel(IPainterService painterService, ISpreadsheetParsingService spreadsheetParsingService)
        {
            _painterService = painterService;
            _spreadsheetParsingService = spreadsheetParsingService;
        }


        public void Process()
        {
            var selectedProducts = GetSelectedProducts();

            if (selectedProducts.Length == 0)
            {
                throw new ApplicationException("Select a product before continue.");
            }

            var ids = Spreadsheet.GetIdsToFill(selectedProducts);

            var color = GetSelectedColor();

            var converter = new SvgToPngConverter();
            //var converter = new SvgToWmfInkscapeConverter();

            try
            {
                var resultPath = _painterService.Process("base-map.svg", converter, OutputFolder, color, ids);
                OpenExplorerWindowAndSelectFile(resultPath);
            }
            catch (ApplicationException e)
            {
                MessageBox.Show(e.Message, "Error");
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception message: " + e.Message, "Unexpected Error");
            }

        }

        private void OpenExplorerWindowAndSelectFile(string resultPath)
        {
            ProcessStartInfo l_psi = new ProcessStartInfo();
            l_psi.FileName = "explorer";
            l_psi.Arguments = string.Format("{0},/select", resultPath);
            l_psi.UseShellExecute = true;
            Process l_newProcess = new Process();
            l_newProcess.StartInfo = l_psi;
            l_newProcess.Start();
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
                    var fileName = openFileDialog.FileName;

                    var spreadsheet = _spreadsheetParsingService.Process(fileName);

                    if (!spreadsheet.HasAllRequiredColumns())
                    {
                        throw new ApplicationException("Excel file format is not valid.\nIt requires the columns 'ElementId', 'COPsync Enterprise', 'COPsync911' and 'Warrantsync'.");
                    }

                    SpreadsheetPath = fileName;
                    Spreadsheet = spreadsheet;
                }
                catch (ApplicationException e)
                {
                    MessageBox.Show(e.Message, "Error");
                }
                catch (Exception e)
                {
                    MessageBox.Show("Exception message: " + e.Message, "Unexpected Error");
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
