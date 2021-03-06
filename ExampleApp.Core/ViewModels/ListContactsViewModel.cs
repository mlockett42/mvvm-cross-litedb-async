﻿using ExampleApp.Core.Models;
using ExampleApp.Data.Models;
using ExampleApp.Data.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ExampleApp.Core.ViewModels
{
    public class ListContactsViewModel : MvxViewModel
    {
        #region PrivateData
        private readonly IContactService _contactService;
        private readonly IMvxNavigationService _navigationService;
        #endregion

        #region Initialization
        public ListContactsViewModel(IContactService contactService, IMvxNavigationService navigationService)
        {
            _contactService = contactService;
            _navigationService = navigationService;

            AddContact = new MvxAsyncCommand(AddContactAsync);
            ItemTappedCommand = new MvxAsyncCommand<Contact>(ItemTappedAsync);
        }
        public override async Task Initialize()
        {
            await base.Initialize();

            IsBusy = true;
            await RaisePropertyChanged(() => IsBusy);

            var contacts = new ObservableCollection<Contact>(await _contactService.GetAllContactsAsync().ConfigureAwait(false));

            IsBusy = false;
            await RaisePropertyChanged(() => IsBusy);

            if (contacts.Count == 0)
            {
                _ = _navigationService.Navigate<ConfirmViewModel, ConfirmPromptModel, ConfirmResultModel>(
                    new ConfirmPromptModel() { Prompt = "We could not find any contacts do you want to create some?" }
                    ).ContinueWith(async (resultTask) =>
                    {
                        if (resultTask.Result.Choice == true)
                        {
                            await InitializeDemoContacts();
                        }
                        else
                        {
                            Contacts = contacts;
                        }
                    });
            }
            else
            {
                Contacts = contacts;
            }
        }

        public async Task InitializeDemoContacts()
        {
            var contacts = new List<Contact>()
            {
                new Contact()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Kelsey",
                    LastName = "Garza",
                    Email = "Kelly@suscipit.edu",
                    AddressLine1 = "62702 West Bosnia and Herzegovina Way",
                    AddressLine2 = "",
                    City ="Wheaton",
                    State = "MO"
                },
                new Contact()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Brendan",
                    LastName = "Bennett",
                    Email = "Donna@cursus.net",
                    AddressLine1 = "8674 West Venezuela Ct.",
                    AddressLine2 = "",
                    City = "DuBois",
                    State = "MI"
                },
                new Contact()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Davis",
                    LastName = "Clay",
                    Email = "Rhea@ac.net",
                    AddressLine1 = "53699 West Dunkirk Blvd.",
                    AddressLine2 = "",
                    City = "Dixon",
                    State =  "MS"
                },
                new Contact()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Cairo",
                    LastName = "Holloway",
                    Email = "Iona@massa.us",
                    AddressLine1 = "21197 South Bosnia and Herzegovina Ct.",
                    AddressLine2 = "",
                    City =  "Worcester",
                    State = "TX"
                },
                new Contact()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Hillary",
                    LastName = "Rich",
                    Email = "Gabriel@malesuada.org",
                    AddressLine1 = "35115 Bolivia Way",
                    AddressLine2 = "",
                    City = "Laughlin",
                    State = "IA"
                },
            };

            await Task.WhenAll(contacts.Select(c => _contactService.SaveContactAsync(c)));

            contacts = await _contactService.GetAllContactsAsync().ConfigureAwait(false);

            Contacts = new ObservableCollection<Contact>(contacts);
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

        public bool IsBusy { get; set; }

        private string _search;
        public string Search {
            get => _search;
            set
            {
                _search = value;
                _ = FilterContacts();
            }
        }

        #endregion

        #region Functions
        private async Task FilterContacts()
        {
            IsBusy = true;
            await RaisePropertyChanged(() => IsBusy);

            Contacts = new ObservableCollection<Contact>(
                await _contactService
                .GetFilteredContactsAsync(_search)
                .ConfigureAwait(false)
                );
            await RaisePropertyChanged(() => Contacts);
            IsBusy = false;
            await RaisePropertyChanged(() => IsBusy);
        }
        #endregion

        #region Commands
        public ICommand AddContact { get; set; }

        private async Task AddContactAsync()
        {
            var result = await _navigationService.Navigate<ContactDetailViewModel, ContactDetailEditModel, ContactDetailResultModel>(
                new ContactDetailEditModel()
                {
                    AddNew = true,
                    ContactId = Guid.NewGuid(),
                    ParentViewModel = this
                }
                );
        }

        public ICommand ItemTappedCommand { get; set; }

        private async Task ItemTappedAsync(Contact contact)
        {
            var result = await _navigationService.Navigate<ContactDetailViewModel, ContactDetailEditModel, ContactDetailResultModel>(
                new ContactDetailEditModel()
                {
                    ContactId = contact.Id,
                    ParentViewModel = this
                }
                );
        }

        public void DeleteContact(Contact contact)
        {
            // From https://stackoverflow.com/a/4075374
            var item = Contacts.Select((item, index) => new { Item = item, Index = index })
                           .Where(pair => pair.Item.Id == contact.Id)
                           .Select(result => result.Index)
                           .First();

            Contacts.RemoveAt(item);
        }

        public async Task SaveContact(bool wasAdded, Contact contact)
        {
            if (wasAdded)
            {
                Contacts.Add(contact);
            }
            else
            {
                // From https://stackoverflow.com/a/4075374
                var item = Contacts.Select((item, index) => new { Item = item, Index = index })
                               .Where(pair => pair.Item.Id == contact.Id)
                               .Select(result => result.Index)
                               .First();

                var newContact = await _contactService.GetContactAsync(contact.Id);
                Contacts.RemoveAt(item);
                Contacts.Insert(item, newContact);
            }
        }
        #endregion

    }
}
