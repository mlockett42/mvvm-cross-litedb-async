using LiteDB.Async;
using System;
using System.Collections.Generic;
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

        public LiteDbAsyncService(ILiteDatabaseAsync liteDatabaseAsync)
        {
            _liteDatabaseAsync = liteDatabaseAsync;
        }

        public ILiteDatabaseAsync LiteDatabaseAsync => _liteDatabaseAsync;
    }
}
