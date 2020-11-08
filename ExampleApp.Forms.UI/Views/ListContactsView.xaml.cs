using ExampleApp.Core.ViewModels;
using MvvmCross.Forms.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExampleApp.Forms.UI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListContactsView : MvxContentPage<ListContactsViewModel>
    {
        public ListContactsView()
        {
            InitializeComponent();
        }
    }
}