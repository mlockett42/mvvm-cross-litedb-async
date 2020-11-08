using ExampleApp.Core.ViewModels;
using ExampleApp.Data.Services;
using MvvmCross;
using MvvmCross.ViewModels;

namespace ExampleApp.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            Mvx.IoCProvider.RegisterType<IContactService, ContactService>();

            RegisterAppStart<ListContactsViewModel>();
        }
    }
}