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
            get { return DataFolder != null; }
        }

        public string DataFolder
        {
            get
            {
                var path = Settings.Default.DataFolder;
                if (!string.IsNullOrEmpty(path) && !Directory.Exists(path))
                {
                    path = Settings.Default.DataFolder = null;
                }
                return path;
            }
            private set
            {
                Settings.Default.DataFolder = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(() => this.ReadyToProcess);
            }
        }

        public string OutputFolder
        {
            get
            {
                return string.IsNullOrEmpty(Settings.Default.OutputFolder)
                    ? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                    : Settings.Default.OutputFolder;
            }
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
                var xlsx = LoadXlsx(DataFolder);
                var svg = LoadSvg(DataFolder);
                var resultFileNames = _presenceMapGenerator.FullProcess(xlsx, svg, _converters, OutputFolder, colors, selectedProducts);
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

        public void SelectDataFolder()
        {
            var dialogPath = DataFolder;
            if (string.IsNullOrEmpty(dialogPath) || !Directory.Exists(dialogPath))
            {
                dialogPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Settings.Default.COPsyncMapsFolderName);
            }
            if (string.IsNullOrEmpty(dialogPath) || !Directory.Exists(dialogPath))
            {
                dialogPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }

            var dialog = new WinForms.FolderBrowserDialog();
            dialog.SelectedPath = dialogPath;
            if (dialog.ShowDialog() == WinForms.DialogResult.OK)
            {
                try
                {
                    // To validate folder content
                    LoadXlsx(dialog.SelectedPath);
                    LoadSvg(dialog.SelectedPath);

                    DataFolder = dialog.SelectedPath;
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

        private Spreadsheet.ISpreadsheet LoadXlsx(string dataFolder)
        {
            var path = GetSingleFilePath(dataFolder, ".xlsx");
            return _presenceMapGenerator.ParseSpreadsheet(path);
        }

        private IMapGraphic LoadSvg(string dataFolder)
        {
            var path = GetSingleFilePath(dataFolder, ".svg");
            return _presenceMapGenerator.ParseSvg(path);
        }

        private string GetSingleFilePath(string dataFolder, string extension)
        {
            if (!Directory.Exists(dataFolder))
            {
                throw new ApplicationException("Specified folder (" + dataFolder + ") not found.");
            }
            var files = Directory.EnumerateFiles(dataFolder)
                .Where(x => extension.Equals(Path.GetExtension(x), StringComparison.OrdinalIgnoreCase))
                .Take(2)
                .ToArray();
            if (files.Length == 0)
            {
                throw new ApplicationException(extension + " file not found in specified folder (" + dataFolder + ").");
            }
            if (files.Length > 1)
            {
                throw new ApplicationException("There are more than a " + extension + " file in specified folder (" + dataFolder + ").");
            }
            return files[0];
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
