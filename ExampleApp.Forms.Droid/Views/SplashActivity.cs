using System.Threading.Tasks;
using Android.App;
using Android.OS;
using MvvmCross.Forms.Platforms.Android.Views;

namespace ExampleApp.Forms.Droid.Views
{
    [Activity(
       NoHistory = true,
       MainLauncher = true,
       Label = "@string/app_name",
       Theme = "@style/AppTheme.Splash",
       Icon = "@mipmap/ic_launcher",
       RoundIcon = "@mipmap/ic_launcher_round")]
    public class SplashActivity : MvxFormsSplashScreenActivity<Setup, Core.App, UI.FormsApp>
    {
        protected override Task RunAppStartAsync(Bundle bundle)
        {
            StartActivity(typeof(MainActivity));
            return Task.CompletedTask;
        }
    }
}
