using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MWapplicationXamarin
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private void btnSignUp_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignUp());
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            if (await Firebase.Login(txtUsername.Text, txtPassword.Text))
            {
                await Navigation.PushAsync(new UserPage());
            }
        }

    }
}
