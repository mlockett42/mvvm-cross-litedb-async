using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.Forms.Platforms.Android.Views;

namespace ExampleApp.Forms.AndroidApp
{
    //[Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    [Activity(
        Label = "LiteDB.Async Mvvmcross",
        Icon = "@mipmap/ic_launcher",
        Theme = "@style/AppTheme",
        // MainLauncher = true, // No Splash Screen: Uncomment this lines if removing splash screen
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        LaunchMode = LaunchMode.SingleTask)]
    //public class MainActivity : MvxFormsAppCompatActivity<MvxFormsAndroidSetup<Core.App, UI.FormsApp>, Core.App, UI.FormsApp>
    public class MainActivity : MvxFormsAppCompatActivity<Setup, Core.App, UI.FormsApp>
    {
        public MainActivity() : base()
        {
            
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            //SetContentView(Resource.Layout.activity_main);
        }
        //public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        //{
        //    Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        //    base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //}
    }
}