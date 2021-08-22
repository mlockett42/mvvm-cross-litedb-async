using Microsoft.Extensions.Logging;
using MvvmCross.Forms.Platforms.Android.Core;
using Serilog;
using Serilog.Extensions.Logging;

namespace ExampleApp.Forms.AndroidApp
{
    public class Setup : MvxFormsAndroidSetup<Core.App, UI.FormsApp>
    {
        protected override ILoggerProvider CreateLogProvider()
        {
            return new SerilogLoggerProvider();
        }

        protected override ILoggerFactory CreateLogFactory()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .CreateLogger();

            return new SerilogLoggerFactory();
        }
    }
}