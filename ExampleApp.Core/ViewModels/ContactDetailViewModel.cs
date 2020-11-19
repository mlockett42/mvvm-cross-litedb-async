using ExampleApp.Data.Models;
using ExampleApp.Data.Services;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;

namespace ExampleApp.Core.ViewModels
{
    public class ContactDetailViewModel : MvxViewModel<Guid>
    {
        #region PrivateData
        private readonly IContactService _contactService;
        private Guid _contactId;
        private Contact _contact;
        #endregion

        #region Initialization
        public ContactDetailViewModel(IContactService contactService)
        {
            _contactService = contactService;
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

            _ = RaisePropertyChanged(() => FirstName);
            _ = RaisePropertyChanged(() => LastName);
        }

        #endregion

        #region BindableData
        public string FirstName => _contact?.FirstName;
        public string LastName => _contact?.LastName;
        #endregion
    }
}
