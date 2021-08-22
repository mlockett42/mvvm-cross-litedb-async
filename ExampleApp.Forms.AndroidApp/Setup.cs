﻿using Android.App;
//using Microsoft.Extensions.Logging;
using MvvmCross.Forms.Platforms.Android.Core;
//using Serilog;
//using Serilog.Extensions.Logging;

#if DEBUG
[assembly: Application(Debuggable = true)]
#else
[assembly: Application(Debuggable = false)]
#endif

namespace ExampleApp.Forms.AndroidApp
{
    public class Setup : MvxFormsAndroidSetup<Core.App, UI.FormsApp>
    {
        //protected override ILoggerProvider CreateLogProvider() => new SerilogLoggerProvider();

        //protected override ILoggerFactory CreateLogFactory()
        //{
        //    Log.Logger = new LoggerConfiguration()
        //        .MinimumLevel.Debug()
        //        .WriteTo.AndroidLog()
        //        .CreateLogger();

        //    return new SerilogLoggerFactory();
        //}
        public override void InitializeSecondary()
        {
            base.InitializeSecondary();
        }
    }
}