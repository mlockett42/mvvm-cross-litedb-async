using Microsoft.Extensions.Logging;
using MvvmCross.Forms.Platforms.Uap.Core;
using Serilog.Extensions.Logging;
using Serilog;

namespace ExampleApp.Forms.UWP
{
    public class Setup : MvxFormsWindowsSetup<Core.App, UI.FormsApp>
    {
        public override void InitializeSecondary()
        {
            base.InitializeSecondary();
        }

        protected override ILoggerProvider CreateLogProvider()
        {
            return new SerilogLoggerProvider();
        }

        protected override ILoggerFactory CreateLogFactory()
        {
            // serilog configuration
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                // add more sinks here
                .CreateLogger();

            return new SerilogLoggerFactory();
        }
    }
}
