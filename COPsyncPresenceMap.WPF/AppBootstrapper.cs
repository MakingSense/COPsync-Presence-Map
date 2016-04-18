using System;
using System.Collections.Generic;
using Caliburn.Micro;
using System.Reflection;
using COPsyncPresenceMap.WPF.ViewModels;
using COPsyncPresenceMap.WPF;
using Ninject;
using COPsyncPresenceMap.WPF.NinjectModules;
using System.Windows;
using COPsyncPresenceMap.WPF.Helpers;
using System.IO;
using COPsyncPresenceMap.WPF.Properties;

namespace COPsyncPresenceMap.WPF
{
    public class AppBootstrapper : BootstrapperBase
    {
        private IKernel _container;

        public AppBootstrapper()
        {
            PrepareCOPsyncMapsFolder();
            Initialize();
        }

        /// <summary>
        /// It creates the folder COPsyncMaps and create a folder for each state.
        /// If the state folder already exists, it is not overriden.
        /// </summary>
        private void PrepareCOPsyncMapsFolder()
        {
            try
            {
                var outputPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    Settings.Default.COPsyncMapsFolderName);
                var inputDir = new DirectoryInfo("COPsyncMaps");

                foreach (var dir in inputDir.GetDirectories())
                {
                    var targetDirPath = Path.Combine(outputPath, dir.Name);
                    if (!Directory.Exists(targetDirPath))
                    {
                        Directory.CreateDirectory(targetDirPath);
                        foreach (var file in dir.GetFiles())
                        {
                            file.CopyTo(Path.Combine(targetDirPath, file.Name));
                        }
                    }
                }
            }
            catch
            {
                //Do not break the system on unexpected error
            }
        }

        protected override void Configure()
        {
            _container = new StandardKernel(new ServicesModule());

            _container.Bind<Application>().ToConstant(Application.Current);
            _container.Bind<IWindowManager>().To<WindowManager>().InSingletonScope();
            _container.Bind<IEventAggregator>().To<EventAggregator>().InSingletonScope();

            _container.Bind<IShell>().To<ShellViewModel>().InSingletonScope();

            var config = new TypeMappingConfiguration
            {
                DefaultSubNamespaceForViews = "COPsyncPresenceMap.WPF.Views",
                DefaultSubNamespaceForViewModels = "COPsyncPresenceMap.WPF.ViewModels"
            };

            ViewLocator.ConfigureTypeMappings(config);
            ViewModelLocator.ConfigureTypeMappings(config);
        }

        protected override object GetInstance(Type service, string key)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return _container.Get(service);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAll(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.Inject(instance);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<IShell>();
        }

        protected override void OnExit(object sender, EventArgs e)
        {
            Settings.Default.Save();
            base.OnExit(sender, e);
        }

    }
}
