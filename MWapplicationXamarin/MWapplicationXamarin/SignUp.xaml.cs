using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MWapplicationXamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUp : ContentPage
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void btnCancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void btnCreateAccount_Clicked(object sender, EventArgs e)
        {
            if (await Firebase.SignUp(txtUsername.Text, txtPassword.Text))
            {
                await Navigation.PopAsync();
            }
        }
    }
}