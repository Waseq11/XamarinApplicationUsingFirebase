using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace MWapplicationXamarin
{
    public static class Firebase
    {
        static private string apiKey = "AIzaSyDHUdq_Q8VoKQ4JWBUPJkhlTtooEuzIE9U";
        static private FirebaseClient firebaseClient = new FirebaseClient("https://finalexammu6-9fce7-default-rtdb.firebaseio.com/");
        static public ObservableCollection<Task> myTasks = GetAllTasks();

        static public async Task<bool> SignUp (string username, string password)
        {
            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(username, password);
                await App.Current.MainPage.DisplayAlert("Alert", "User Created!", "OK");
                return true;
            }
            catch (Exception ex)
            {
                FirebaseAuthException error = (FirebaseAuthException)ex;
                await App.Current.MainPage.DisplayAlert("Alert",error.Reason.ToString(), "OK");
                return false;
            }
        }

        static public async Task<bool> Login (string username, string password)
        {
            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(username, password);
                await App.Current.MainPage.DisplayAlert("Alert", "Logged in successfully!", "OK");
                return true;
            }
            catch(Exception ex)
            {
                FirebaseAuthException error = (FirebaseAuthException)(ex);
                await App.Current.MainPage.DisplayAlert("Alert",error.Reason.ToString(),"OK");
                return false;
            }
        }
        static public async Task<bool> AddTasks(Task task)
        {
            try
            {
                await firebaseClient.Child("Task").PostAsync(task);
                return true;
            }
            catch
            {
                return false;
            }
        }
        static public async Task<bool> DeleteTasks(String id)
        {
            try
            {
                await firebaseClient.Child("Task").Child(id).DeleteAsync();
                myTasks.Clear();
                myTasks = GetAllTasks();
                return true;
            }
            catch
            {
                return false;
            }
        }

        static public async Task<bool> EditTasks(Task task)
        {
            try
            {
                await firebaseClient.Child("Task").Child(task.Id).PutAsync(task);
                myTasks.Clear();
                myTasks = GetAllTasks();
                return true;
            }
            catch
            {
                return false;
            }
        }

        static public ObservableCollection<Task> GetAllTasks()
        {
            ObservableCollection<Task> tempTasks = new ObservableCollection<Task>();

            var col = firebaseClient.Child("Task").AsObservable<Task>().Subscribe((item) =>
            {
                if (item.Object != null)
                {
                    bool flag = true;
                    foreach (Task task in myTasks)
                    {

                        if (task.Id == item.Key)
                        {
                            flag = false;
                        }
                    }
                    if (flag)
                        tempTasks.Add(new Task(item.Key, item.Object.Description, item.Object.Priority, Convert.ToDouble(item.Object.Value)));
                }
            });

            return tempTasks;
        }

    }
}
