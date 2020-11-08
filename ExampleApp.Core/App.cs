using ExampleApp.Core.ViewModels;
using ExampleApp.Data.Services;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using System;
using System.Linq;
using System.Reflection;

namespace ExampleApp.Core
{
    public class App : MvxApplication
    {
        public static Assembly PlatformAssembly { get; set; }
        public override void Initialize()
        {
            RegisterByConvention();
            //RegisterSettings();
            //RegisterDatabase();
            //RegisterSingletonViewModels();
            //RegisterAllTypesAsSingletonArray<IMigration>();
            //RegisterAllTypesAsSingletonArray<ITool>();
            Mvx.IoCProvider.RegisterType<IContactService, ContactService>();

            RegisterAppStart<ListContactsViewModel>();
        }

        private static void RegisterByConvention()
        {
            var assemblies = new[]
            {
                typeof(LiteDbAsyncService).Assembly, // ExampleApp.Data
                typeof(App).Assembly, // ExampleApp.Core
                //typeof(FormsApp).Assembly, // ExampleApp.Forms.UI
                PlatformAssembly, // ExampleApp.Ugs.UWP
            };

            foreach (var assembly in assemblies)
            {
                var types = assembly.CreatableTypes();

                types
                    .Where(HasInterfaceWithMatchingName)
                    .AsInterfaces()
                    .RegisterAsDynamic();

                types
                    .EndingWith("ViewModel")
                    .AsTypes()
                    .RegisterAsDynamic();

                types
                    .EndingWith("Service")
                    .AsInterfaces()
                    .RegisterAsLazySingleton();
            }
        }

        private static bool HasInterfaceWithMatchingName(Type type)
        {
            string interfaceName = string.Concat("I", type.Name);
            return type.GetInterface(interfaceName) != null;
        }
    }
}