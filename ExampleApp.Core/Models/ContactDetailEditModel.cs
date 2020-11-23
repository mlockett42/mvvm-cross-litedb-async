using ExampleApp.Core.ViewModels;
using System;

namespace ExampleApp.Core.Models
{
    public class ContactDetailEditModel
    {
        public bool AddNew { get; set; }
        public Guid ContactId { get; set; }
        public ListContactsViewModel ParentViewModel { get; set; }
    }
}
