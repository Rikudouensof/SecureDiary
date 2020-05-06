using SecureDiary.Model;
using SecureDiary.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SecureDiary
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateEntryPage : ContentPage
    {
        public string _dbPath;
        Diary quantadiary;
        public UpdateEntryPage( Diary diary )
        {
            InitializeComponent();
            quantadiary = diary;
            BindingContext = diary;
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
                Id = quantadiary.Id,
                Content = ContentEntry.Text,
                Title = TitleEntry.Text,
                Username = LogedInUsername,
                DateOEntry = DateTime.Now

            };
            db.Update(toSaveDIary);
            await DisplayAlert("message", "Entry updated", "Ok");
            await Navigation.PopAsync();

        }

        private async void ImageButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}