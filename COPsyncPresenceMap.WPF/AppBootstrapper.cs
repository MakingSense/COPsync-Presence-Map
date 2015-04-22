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

namespace COPsyncPresenceMap.WPF
{
    public class AppBootstrapper : BootstrapperBase
    {
        private IKernel _container;

        public AppBootstrapper()
        {
            PrepareDefaultSpreadsheet();
            Initialize();
        }

        private void PrepareDefaultSpreadsheet()
        {
            try
            {
                var folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var filename = "COPsyncPresence.xlsx";
                var outputFilename = Path.Combine(folder, filename);
                if (File.Exists(filename) && !File.Exists(outputFilename))
                {
                    File.Copy(filename, outputFilename, false);
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

    }
}
