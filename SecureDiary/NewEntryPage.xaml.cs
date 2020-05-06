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
    public partial class NewEntryPage : ContentPage
    {
        string _dbPath;
        public NewEntryPage()
        {
            InitializeComponent();
            
           
        }

     async private void SaveButton_Clicked(object sender, EventArgs e)
        {
            _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "DiarySecure.db3");
            var db = new SQLiteConnection(_dbPath);
            db.CreateTable<Account>();
            db.CreateTable<Diary>();
            var LogedInUsername = Application.Current.Properties["UserName"].ToString();
            var MaximumPrimaryKey = db.Table<Account>().OrderByDescending(zt => zt.id).FirstOrDefault();
            var toSaveDIary = new Diary()
            {
                Id = (MaximumPrimaryKey == null ? 1 : MaximumPrimaryKey.id + 1),
                Content = ContentEntry.Text,
                Title = TitleEntry.Text,
                Username = LogedInUsername,
                DateOEntry = DateTime.Now

            };
            db.Insert(toSaveDIary);
            await DisplayAlert("message", "Entry saved", "Ok");
            await Navigation.PopAsync();

        }


        private async void NewEntryImageButton_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("message", "You are already in New Entry Page", "Ok");
        }

        private async void HomeImageButtom_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePage());
        }

        private async void OldEntryImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EntryListPage());
        }

        private async void ProfileImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfilePage());
        }
    }
}