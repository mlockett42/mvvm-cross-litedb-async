using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExampleApp.Core.Services
{
    public interface IDialogs
    {
        Task<bool> ConfirmAsync(string message, string title = null, string okText = null, string cancelText = null);
    }
}
