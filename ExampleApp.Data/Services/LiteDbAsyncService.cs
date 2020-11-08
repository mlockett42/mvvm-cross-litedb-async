using LiteDB.Async;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ExampleApp.Data.Services
{
    public interface ILiteDbAsyncService
    {
        ILiteDatabaseAsync LiteDatabaseAsync { get; }
    }

    public class LiteDbAsyncService : ILiteDbAsyncService
    {
        private readonly ILiteDatabaseAsync _liteDatabaseAsync;

        public LiteDbAsyncService()
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var databasePath = Path.Combine(folder, "hello.db");
            _liteDatabaseAsync = new LiteDatabaseAsync(databasePath);
        }

        public ILiteDatabaseAsync LiteDatabaseAsync => _liteDatabaseAsync;
    }
}
