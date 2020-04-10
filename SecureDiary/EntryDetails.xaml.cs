using SecureDiary.Model;
using SecureDiary.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SecureDiary
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntryDetails : ContentPage
    {
        public string _dbPath;
        Diary DiaryObject;

        List<string> FontList = new List<string>()
        {"Pacifico","Yanone","Lazy","None" };

        
        
        public EntryDetails( Diary diary)
        {
            InitializeComponent();
            _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "DiarySecure.db3");
            var db = new SQLiteConnection(_dbPath);
            db.CreateTable<Account>();
            db.CreateTable<Diary>();
            var Username = Application.Current.Properties["UserName"].ToString();
            DiaryObject = diary;
            BindingContext = db.Table<Diary>().Where(m => m.Id.Equals(diary.Id)).FirstOrDefault();
            FontPicker.ItemsSource = FontList;
        }

        protected override void OnAppearing()
        {
            _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "DiarySecure.db3");
            var db = new SQLiteConnection(_dbPath);
            db.CreateTable<Account>();
            db.CreateTable<Diary>();
            var Username = Application.Current.Properties["UserName"].ToString();

            BindingContext = db.Table<Diary>().Where(m => m.Id.Equals(DiaryObject.Id)).FirstOrDefault();
        
        }

        private async void UpdateEntryImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UpdateEntryPage(DiaryObject));
        }

        private async void DeleteEntryImageButton_Clicked(object sender, EventArgs e)
        {
            _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "DiarySecure.db3");
            var db = new SQLiteConnection(_dbPath);
            db.CreateTable<Account>();
            db.CreateTable<Diary>();
            var Username = Application.Current.Properties["UserName"].ToString();
            db.Delete(DiaryObject);
            await Navigation.PopAsync();
        }

        private async void ImageButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void FontPicker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            TextcontrolFrame.IsVisible = false;
        }
    }
}