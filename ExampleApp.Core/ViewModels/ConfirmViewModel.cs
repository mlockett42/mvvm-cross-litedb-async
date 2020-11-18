using MvvmCross.ViewModels;
using ExampleApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Navigation;
using System.Windows.Input;
using MvvmCross.Commands;

namespace ExampleApp.Core.ViewModels
{
    public class ConfirmViewModel : MvxViewModel<ConfirmPromptModel, ConfirmResultModel>
    {
        #region Private data
        private readonly IMvxNavigationService _navigationService;
        private ConfirmPromptModel _confirmPromptModel;
        #endregion

        #region Initialization
        public ConfirmViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            YesCommand = new MvxAsyncCommand(YesAsync);
            NoCommand = new MvxAsyncCommand(NoAsync);
        }

        public override void Prepare()
        {
            // first callback. Initialize parameter-agnostic stuff here
        }

        public override void Prepare(ConfirmPromptModel parameter)
        {
            // receive and store the parameter here
            _confirmPromptModel = parameter;
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            //Do heavy work and data loading here
        }
        #endregion

        #region Bindable Data
        public string Prompt { get => _confirmPromptModel.Prompt; }
        public string YesLabel { get => _confirmPromptModel.YesLabel ?? "Yes"; }
        public string NoLabel { get => _confirmPromptModel.NoLabel ?? "No"; }
        #endregion

        #region Commands
        public ICommand YesCommand { get; set; }

        private Task YesAsync()
        {
            return SomeMethodToClose(true);
        }

        public ICommand NoCommand { get; set; }

        private Task NoAsync()
        {
            return SomeMethodToClose(false);
        }

        public Task SomeMethodToClose(bool result)
        {
            return _navigationService.Close(this, new ConfirmResultModel() { Result = result });
        }
        #endregion
    }
}
