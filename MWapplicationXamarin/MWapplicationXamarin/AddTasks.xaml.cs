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
    public partial class AddTasks : ContentPage
    {
        public AddTasks()
        {
            InitializeComponent();
            List<string> tempList = new List<string>() { "Low", "Medium", "High", "Critical" };
            pickerStatus.ItemsSource = tempList;
            pickerStatus.SelectedIndex = 0;
        }

        private async void btnConfirm_Clicked(object sender, EventArgs e)
        {
            if (await Firebase.AddTasks(new Task(null, txtTaskDescription.Text, pickerStatus.SelectedItem.ToString(), Convert.ToDouble(txtTaskValue.Text))))
            {
                await DisplayAlert("Alert", "Task Added", "OK");
            }
            else
            {
                await DisplayAlert("Alert", "Error to add the Task", "OK");
            }
            await Navigation.PopAsync();
        }

        private void btnCancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}