using SecureDiary.Model;
using SecureDiary.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SecureDiary
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        string _dbPath;
        public ProfilePage()
        {
            InitializeComponent();
            _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "DiarySecure.db3");
            var db = new SQLiteConnection(_dbPath);
            db.CreateTable<Account>();
            db.CreateTable<Diary>();
            var Username = Application.Current.Properties["UserName"].ToString();
            var accountcontext = db.Table<Account>().Where(m => m.UserName.Equals(Username)).FirstOrDefault();
            BindingContext = accountcontext;

            var TotalEntries = db.Table<Diary>().Where(m => m.Username.Equals(Username)).Count().ToString();
            TotalEntriesLabel.Text =  TotalEntries;

            var LastEntry = db.Table<Diary>().OrderByDescending(m => m.DateOEntry).Select(m => m.DateOEntry).FirstOrDefault().ToString();
            LastDateLabel.Text = LastEntry;

            var countBay = db.Table<Diary>().Where(m => m.Username.Equals(Username));
            int summer = 0;
            foreach (var item in countBay)
            {
                summer += item.Content.Split(' ').Length;
            }
            WordsLabel.Text = summer.ToString();

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var Username = Application.Current.Properties["UserName"].ToString();
            _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "DiarySecure.db3");
            var db = new SQLiteConnection(_dbPath);
            db.CreateTable<Account>();
            var wordica = db.Table<Account>().Where(m => m.UserName.Equals(Username)).FirstOrDefault();
            wordica.Password = ChangePasswordEntry.Text;
            wordica.Hint = ChangeHintEntry.Text;
            db.Update(wordica);
        }

        private void ChangePasswordEntry_PropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            try
            {
                if (ChangePasswordEntry.Text.Equals(null))
                {
                    UpdateButton.IsEnabled = false;
                }
                else
                {
                    UpdateButton.IsEnabled = true;
                }
            }
            catch (Exception)
            {

               
            }
           
          
        }

        private async void NewEntryImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewEntryPage());
        }

        private async void OldEntryImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EntryListPage());
        }

        private async void ProfileImageButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfilePage());
        }
    }
}