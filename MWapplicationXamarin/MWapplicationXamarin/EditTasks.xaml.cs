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
    public partial class EditTasks : ContentPage
    {
        public EditTasks()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            List<string> tempList = new List<string>() { "Low", "Medium", "High", "Critical" };
            pickerStatus.ItemsSource = tempList;
            Task task = (Task)this.BindingContext;
            int index = tempList.FindIndex(item => item == task.Priority);
            pickerStatus.SelectedIndex = index;
        }

        private async void btnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void btnConfirm_Clicked(object sender, EventArgs e)
        {
            if (await Firebase.EditTasks(new Task(txtTaskId.Text, txtTaskDescription.Text, pickerStatus.SelectedItem.ToString(), Convert.ToDouble(txtTaskValue.Text))))
            {
                await DisplayAlert("Alert", "Task Edited!", "OK");
            }
            else
            {
                await DisplayAlert("Alert", "Error on Trying to Edit Task!", "OK");
            }

            await Navigation.PopAsync();
        }
    }
}