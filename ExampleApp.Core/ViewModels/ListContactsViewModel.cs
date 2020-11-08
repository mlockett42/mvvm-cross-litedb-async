using ExampleApp.Data.Models;
using ExampleApp.Data.Services;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ExampleApp.Core.ViewModels
{
    public class ListContactsViewModel : MvxViewModel
    {
        #region PrivateData
        private readonly IContactService _contactService;
        #endregion

        #region Initialization
        public ListContactsViewModel(IContactService contactService)
        {
            _contactService = contactService;

            AddContact = new MvxAsyncCommand(AddContactAsync);
            ItemTappedCommand = new MvxAsyncCommand(ItemTappedAsync);
        }
        public override async Task Initialize()
        {
            await base.Initialize();

            Contacts = new ObservableCollection<Contact>(await _contactService.GetAllContacts());
        }
        #endregion

        #region BindableData
        private ObservableCollection<Contact> _contacts;
        public ObservableCollection<Contact> Contacts
        {
            get => _contacts;
            set
            {
                _contacts = value;
                RaisePropertyChanged(() => Contacts);
            }
        }

        #endregion

        #region Commands
        public ICommand AddContact { get; set; }

        private Task AddContactAsync()
        {
            return Task.CompletedTask;
        }

        public ICommand ItemTappedCommand { get; set; }

        private Task ItemTappedAsync()
        {
            return Task.CompletedTask;
        }
        #endregion

    }
}
