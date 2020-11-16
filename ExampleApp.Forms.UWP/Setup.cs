using Acr.UserDialogs;
using ExampleApp.Core.Services;
using ExampleApp.Forms.UWP.Controls;
using MvvmCross;
using MvvmCross.Forms.Platforms.Uap.Core;

namespace ExampleApp.Forms.UWP
{
    public class Setup : MvxFormsWindowsSetup<Core.App, UI.FormsApp>
    {
        public override void InitializeSecondary()
        {
            base.InitializeSecondary();
            Mvx.IoCProvider.RegisterSingleton(() => (IDialogs)new Dialogs(UserDialogs.Instance));
        }
    }
}
