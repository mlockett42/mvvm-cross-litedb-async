using MvvmCross;
using MvvmCross.Forms.Platforms.Uap.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleApp.Forms.UWP
{
    public class Setup : MvxFormsWindowsSetup<Core.App, UI.FormsApp>
    {
        public override void InitializeSecondary()
        {
            base.InitializeSecondary();
            //Mvx.IoCProvider.RegisterSingleton(() => (IDialogs)new Dialogs(UserDialogs.Instance));
        }
    }
}
