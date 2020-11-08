using System;
using MvvmCross.Core;
using MvvmCross.Forms.Platforms.Uap.Core;
using MvvmCross.Forms.Platforms.Uap.Views;
using MvvmCross.Platforms.Uap.Core;
using ExampleApp.Forms.UI;
using ExampleApp.Forms.UWP;

namespace ExampleApp.Forms.UWP
{
    sealed partial class App
    {
        public App()
        {
            InitializeComponent();
        }
    }

    public abstract class ExampleAppApp : MvxWindowsApplication<Setup, ExampleApp.Core.App, UI.FormsApp, MainPage>
    {
    }
}