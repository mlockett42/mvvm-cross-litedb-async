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
    public class ContactDetailViewModel : MvxViewModel<ContactDetailEditModel, ContactDetailResultModel>
    {
        #region PrivateData
        private readonly IContactService _contactService;
        private Guid _contactId;
        private Contact _contact;
        private readonly IMvxNavigationService _navigationService;
        private bool _addNew;
        private ListContactsViewModel _parent;
        private bool _triedSaving = false;
        #endregion

        #region Initialization
        public ContactDetailViewModel(IContactService contactService, IMvxNavigationService navigationService)
        {
            _contactService = contactService;
            _navigationService = navigationService;

            CancelCommand = new MvxAsyncCommand(CancelAsync);
            SaveCommand = new MvxAsyncCommand(SaveAsync);
            DeleteCommand = new MvxAsyncCommand(DeleteAsync);
        }

        public override void Prepare()
        {
            // first callback. Initialize parameter-agnostic stuff here
        }

        public override void Prepare(ContactDetailEditModel parameter)
        {
            // receive and store the parameter here
            _contactId = parameter.ContactId;
            _addNew = parameter.AddNew;
            _parent = parameter.ParentViewModel;
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            if (_addNew)
            {
                _contact = new Contact()
                {
                    Id = _contactId
                };
            }
            else
            {
                _contact = await _contactService.GetContactAsync(_contactId).ConfigureAwait(false);

                _ = RaiseAllPropertiesChanged();
            }
        }

        #endregion

        #region BindableData
        public string FirstName
        {
            get => _contact?.FirstName ?? "";
            set
            {
                if (_contact != null)
                {
                    _contact.FirstName = value;
                }
                RaisePropertyChanged(() => IsFirstNameError);
            }
        }
        public string LastName
        {
            get => _contact?.LastName ?? "";
            set
            {
                if (_contact != null)
                {
                    _contact.LastName = value;
                }
                RaisePropertyChanged(() => IsLastNameError);
            }
        }
        public string Email { get => _contact?.Email; set => _contact.Email = value; }
        public string AddressLine1 { get => _contact?.AddressLine1; set => _contact.AddressLine1 = value; }
        public string AddressLine2 { get => _contact?.AddressLine2; set => _contact.AddressLine2 = value; }
        public string City { get => _contact?.City; set => _contact.City = value; }
        public string State { get => _contact?.State; set => _contact.State = value; }

        public bool IsFirstNameError => _triedSaving && FirstName == "";
        public string FirstNameErrorMessage => "You must enter a First Name";

        public bool IsLastNameError => _triedSaving && LastName == "";
        public string LastNameErrorMessage => "You must enter a Last Name";
        #endregion

        #region Commands
        public ICommand CancelCommand { get; set; }

        private Task CancelAsync()
        {
            return _navigationService.Close(this, new ContactDetailResultModel());
        }

        public ICommand SaveCommand { get; set; }

        private async Task SaveAsync()
        {
            _triedSaving = true;
            if (FirstName == "" || LastName == "")
            {
                _ = RaisePropertyChanged(() => IsFirstNameError);
                _ = RaisePropertyChanged(() => IsLastNameError);
                return;
            }
            _isBusy = true;
            _ = RaisePropertyChanged(() => CanDo);
            _ = RaisePropertyChanged(() => CanDelete);
            await _contactService.SaveContactAsync(_contact);
            await _navigationService.Close(this, new ContactDetailResultModel() { ContactId = _contactId });
            _isBusy = false;
            _ = RaisePropertyChanged(() => CanDo);
            _ = RaisePropertyChanged(() => CanDelete);

            // Have to do this nonsense because MvvmCross navigation is not a stack. What a mediocre platform.
            await _parent.SaveContact(_addNew, _contact);
        }

        public ICommand DeleteCommand { get; set; }

        private async Task DeleteAsync()
        {
            var result = await _navigationService.Navigate<ConfirmViewModel, ConfirmPromptModel, ConfirmResultModel>(
                new ConfirmPromptModel() { Prompt = $"Are you sure you want to delete {FirstName} {LastName}?" }
                );
            if (result.Choice)
            {
                await _contactService.DeleteContactAsync(_contact);
                await _navigationService.Close(this, new ContactDetailResultModel() { ContactId = _contactId });

                // Have to do this nonsense because MvvmCross navigation is not a stack. What a mediocre platform.
                _parent.DeleteContact(_contact);
            }
            _ = RaisePropertyChanged(() => CanDelete);
        }

        private bool _isBusy;
        public bool CanDo => !_isBusy;
        public bool CanDelete => CanDo && !_addNew;
        #endregion
    }
}
