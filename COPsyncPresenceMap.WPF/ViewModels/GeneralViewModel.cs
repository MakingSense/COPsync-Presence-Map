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
using System.Drawing;
using COPsyncPresenceMap.Graphics;
using COPsyncPresenceMap.Graphics.Converters;
using COPsyncPresenceMap.WPF.Properties;

namespace COPsyncPresenceMap.WPF.ViewModels
{
    public class GeneralViewModel : Screen
    {
        readonly IMapGraphicConverter[] _converters = new IMapGraphicConverter[] { new MapPngConverter(scale: 10), new MapSvgConverter() };
        public readonly ICOPsyncPresenceMapGenerator _presenceMapGenerator;
        public bool ReadyToProcess
        {
            get { return SpreadsheetPath != null; }
        }

        public string SpreadsheetPath
        {
            get { return Settings.Default.SpreadsheetPath; }
            private set
            {
                Settings.Default.SpreadsheetPath = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(() => this.ReadyToProcess);
            }
        }

        public string OutputFolder
        {
            get { return string.IsNullOrEmpty(Settings.Default.OutputFolder) ? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) : Settings.Default.OutputFolder; }
            private set
            {
                Settings.Default.OutputFolder = value;
                NotifyOfPropertyChange();
            }
        }

        public MediaColor SelectedFillColor
        {
            get { return Settings.Default.SelectedFillColor; }
            set
            {
                Settings.Default.SelectedFillColor = value;
                NotifyOfPropertyChange();
            }
        }

        public MediaColor DefaultFillColor
        {
            get { return Settings.Default.DefaultFillColor; }
            set
            {
                Settings.Default.DefaultFillColor = value;
                NotifyOfPropertyChange();
            }
        }

        public MediaColor LineColor
        {
            get { return Settings.Default.LineColor; }
            set
            {
                Settings.Default.LineColor = value;
                NotifyOfPropertyChange();
            }
        }

        public bool IncludeCOPsyncEnterprise
        {
            get { return Settings.Default.IncludeCOPsyncEnterprise; }
            set
            {
                Settings.Default.IncludeCOPsyncEnterprise = value;
                NotifyOfPropertyChange();
            }
        }

        public bool IncludeCOPsync911
        {
            get { return Settings.Default.IncludeCOPsync911; }
            set
            {
                Settings.Default.IncludeCOPsync911 = value;
                NotifyOfPropertyChange();
            }
        }

        public bool IncludeWarrantsync
        {
            get { return Settings.Default.IncludeWarrantsync; }
            set
            {
                Settings.Default.IncludeWarrantsync = value;
                NotifyOfPropertyChange();
            }
        }

        public bool ShowCountyLines
        {
            get { return Settings.Default.ShowCountyLines; }
            set
            {
                Settings.Default.ShowCountyLines = value;
                NotifyOfPropertyChange();
            }
        }

        public bool ShowCountyNames
        {
            get { return Settings.Default.ShowCountyNames; }
            set
            {
                Settings.Default.ShowCountyNames = value;
                NotifyOfPropertyChange();
            }
        }

        private RenderPreferences GetPreferences()
        {
            return new RenderPreferences(
                DefaultFillColor.ToDrawingColor(),
                LineColor.ToDrawingColor(),
                SelectedFillColor.ToDrawingColor(),
                ShowCountyLines,
                ShowCountyNames);
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

        public GeneralViewModel(ICOPsyncPresenceMapGenerator presenceMapGenerator)
        {
            _presenceMapGenerator = presenceMapGenerator;
        }

        public void Process()
        {
            try
            {
                var colors = GetPreferences();
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
