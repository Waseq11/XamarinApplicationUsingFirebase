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
    public partial class UserPage : ContentPage
    {
        int index;

        public UserPage()
        {
            InitializeComponent();
            listTasks.ItemsSource = Firebase.myTasks;
        }

        protected override void OnAppearing()
        {
            listTasks.ItemsSource = Firebase.myTasks;
            btnEdit.IsVisible = false;
            btnDelete.IsVisible = false;
        }

        private void listTasks_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            index = e.SelectedItemIndex;
            btnEdit.IsVisible = true;
            btnDelete.IsVisible = true;
        }

        private void btnAdd_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddTasks());
        }

        private void btnEdit_Clicked(object sender, EventArgs e)
        {
            var editTasks = new EditTasks();
            editTasks.BindingContext = Firebase.myTasks[index];
            Navigation.PushAsync(editTasks);
        }

        private async void btnDelete_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Alert", "Are you sure?", "Yes", "No");
            if (answer)
            {
                await Firebase.DeleteTasks(Firebase.myTasks[index].Id);
            }
            listTasks.ItemsSource = Firebase.myTasks;
        }
    }
}