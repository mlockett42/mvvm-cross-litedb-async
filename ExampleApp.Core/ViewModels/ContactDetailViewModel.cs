using ExampleApp.Data.Models;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExampleApp.Core.ViewModels
{
    public class ContactDetailViewModel : MvxViewModel<Contact>
    {
        private Contact _contact;

        public override void Prepare()
        {
            // first callback. Initialize parameter-agnostic stuff here
        }

        public override void Prepare(Contact parameter)
        {
            // receive and store the parameter here
            _contact = parameter;
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            // do the heavy work here
        }

        public string FirstName => _contact?.FirstName;
        public string LastName => _contact?.LastName;
    }
}
