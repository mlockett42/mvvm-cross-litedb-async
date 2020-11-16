using System.Threading.Tasks;
using Acr.UserDialogs;
using ExampleApp.Core.Services;

namespace ExampleApp.Forms.UWP.Controls
{
    class Dialogs : IDialogs
    {
        private readonly IUserDialogs _userDialogs;

        public Dialogs(IUserDialogs userDialogs)
        {
            _userDialogs = userDialogs;
        }

        public async Task<bool> ConfirmAsync(string message, string title = null, string okText = null, string cancelText = null)
        {
            return await _userDialogs.ConfirmAsync(message, title, okText, cancelText);
        }
    }
}
