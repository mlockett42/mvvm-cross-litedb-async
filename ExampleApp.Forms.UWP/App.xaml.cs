using System;
using MvvmCross.Core;
using MvvmCross.Forms.Platforms.Uap.Core;
using MvvmCross.Forms.Platforms.Uap.Views;
using MvvmCross.Platforms.Uap.Core;
using ExampleApp.Forms.UI;
using ExampleApp.Forms.UWP;
using Windows.ApplicationModel.Activation;

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
        protected override void OnLaunched(LaunchActivatedEventArgs activationArgs)
        {
            try
            {
                var assemblies = new[]
                {
                    typeof(Xamarin.Forms.Xaml.Extensions).Assembly
                };

                Xamarin.Forms.Forms.Init(activationArgs, assemblies);

                Core.App.PlatformAssembly = typeof(ExampleAppApp).Assembly;

                base.OnLaunched(activationArgs);
            }
            catch (Exception ex)
            {
                if (!ex.Message.StartsWith("More data is available."))
                {
                    throw;
                }
            }
        }
    }
}