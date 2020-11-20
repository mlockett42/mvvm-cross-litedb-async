using ExampleApp.Core.Models;
using ExampleApp.Data.Models;
using ExampleApp.Data.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ExampleApp.Core.ViewModels
{
    public class ContactDetailViewModel : MvxViewModel<Guid, ContactDetailResultModel>
    {
        #region PrivateData
        private readonly IContactService _contactService;
        private Guid _contactId;
        private Contact _contact;
        private readonly IMvxNavigationService _navigationService;
        #endregion

        #region Initialization
        public ContactDetailViewModel(IContactService contactService, IMvxNavigationService navigationService)
        {
            _contactService = contactService;
            _navigationService = navigationService;

            CancelCommand = new MvxAsyncCommand(CancelAsync);
            SaveCommand = new MvxAsyncCommand(SaveAsync);
        }

        public override void Prepare()
        {
            // first callback. Initialize parameter-agnostic stuff here
        }

        public override void Prepare(Guid parameter)
        {
            // receive and store the parameter here
            _contactId = parameter;
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            _contact = await _contactService.GetContactAsync(_contactId).ConfigureAwait(false);

            //_ = RaisePropertyChanged(() => FirstName);
            //_ = RaisePropertyChanged(() => LastName);
            _ = RaiseAllPropertiesChanged();
        }

        #endregion

        #region BindableData
        public string FirstName { get => _contact?.FirstName; set => _contact.FirstName = value; }
        public string LastName  { get => _contact?.LastName; set => _contact.LastName = value; }
        public string Email { get => _contact?.Email; set => _contact.Email = value; }
        public string AddressLine1 { get => _contact?.AddressLine1; set => _contact.AddressLine1 = value; }
        public string AddressLine2 { get => _contact?.AddressLine2; set => _contact.AddressLine2 = value; }
        public string City { get => _contact?.City; set => _contact.City = value; }
        public string State { get => _contact?.State; set => _contact.State = value; }
        #endregion

        #region Commands
        public ICommand CancelCommand { get; set; }

        private Task CancelAsync()
        {
            return _navigationService.Close(this, new ContactDetailResultModel() { WasSaved = false });
        }

        public ICommand SaveCommand { get; set; }

        private async Task SaveAsync()
        {
            _isBusy = true;
            _ = RaisePropertyChanged(() => CanDo);
            await _contactService.SaveContactAsync(_contact);
            await _navigationService.Close(this, new ContactDetailResultModel() { WasSaved = true, ContactId = _contactId });
            _isBusy = false;
            _ = RaisePropertyChanged(() => CanDo);
        }

        private bool _isBusy;
        public bool CanDo => !_isBusy;
        #endregion
    }
}
