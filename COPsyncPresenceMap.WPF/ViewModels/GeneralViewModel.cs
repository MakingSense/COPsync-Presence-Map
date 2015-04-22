using System;
using Caliburn.Micro;
using COPsyncPresenceMap.WPF.Helpers;
using MediaColor = System.Windows.Media.Color;
using DrawingColor = System.Drawing.Color;
using Microsoft.Win32;
using WinForms = System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Linq;
using System.Collections.Generic;
using System.Xml;
using COPsyncPresenceMap.SvgImplementation;
using System.Drawing;

namespace COPsyncPresenceMap.WPF.ViewModels
{
    public class GeneralViewModel : Screen
    {
        readonly ISvgConverter[] _converters = new ISvgConverter[] { new SvgToPngConverter(10), new SvgToSvgConverter() };
        public readonly COPsyncPresenceMapGenerator _presenceMapGenerator;
        public bool ReadyToProcess
        {
            get { return SpreadsheetPath != null; }
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
                    NotifyOfPropertyChange(() => this.ReadyToProcess);
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

        private MediaColor _selectedFillColor = DrawingColor.MediumSeaGreen.ToMediaColor();
        public MediaColor SelectedFillColor
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

        private MediaColor _defaultFillColor = DrawingColor.White.ToMediaColor();
        public MediaColor DefaultFillColor
        {
            get { return _defaultFillColor; }
            set
            {
                if (_defaultFillColor != value)
                {
                    _defaultFillColor = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        private MediaColor _lineColor = DrawingColor.Black.ToMediaColor();
        public MediaColor LineColor
        {
            get { return _lineColor; }
            set
            {
                if (_lineColor != value)
                {
                    _lineColor = value;
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

        private ColorSet GetColors()
        {
            return new ColorSet(DefaultFillColor.ToDrawingColor(), LineColor.ToDrawingColor(), SelectedFillColor.ToDrawingColor());
        }

        private Products GetSelectedProducts()
        {
            var list = new HashSet<string>();
            if (IncludeCOPsyncEnterprise)
            {
                list.Add(Products.COPSYNC_ENTERPRISE);
            }
            if (IncludeCOPsync911)
            {
                list.Add(Products.COPSYNC911);
            }
            if (IncludeWarrantsync)
            {
                list.Add(Products.WARRANTSYNC);
            }
            return Products.AllProducts.FilterByProductName(list);
        }

        public GeneralViewModel(COPsyncPresenceMapGenerator presenceMapGenerator)
        {
            _presenceMapGenerator = presenceMapGenerator;
        }

        public void Process()
        {
            try
            {
                var colors = GetColors();
                var selectedProducts = GetSelectedProducts();
                var resultFileNames = _presenceMapGenerator.FullProcess(SpreadsheetPath, _converters, OutputFolder, colors, selectedProducts);
                var firstFileName = resultFileNames.FirstOrDefault();
                if (firstFileName != null)
                {
                    OpenExplorerWindowAndSelectFile(firstFileName);
                }
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
                    _presenceMapGenerator.ParseSpreadsheet(openFileDialog.FileName);
                    SpreadsheetPath = openFileDialog.FileName;
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
