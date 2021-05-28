using languageinterpretor1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace languageinterpretor1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DictionaryPage : ContentPage
    {
        public DictionaryPage()
        {
            // constructor of DictionaryPage xaml
            BindingContext = new DictionaryViewModel();
            //InitializeComponent();
        }
    }
}