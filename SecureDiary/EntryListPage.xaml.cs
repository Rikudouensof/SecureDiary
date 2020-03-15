using SecureDiary.Model;
using SecureDiary.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace SecureDiary
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntryListPage : ContentPage
    {
        private string _dbPath;

        public EntryListPage()
        {
            InitializeComponent();
            _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "DiarySecure.db3");
            var db = new SQLiteConnection(_dbPath);
            db.CreateTable<Account>();
            db.CreateTable<Diary>();
            var Username = Application.Current.Properties["UserName"].ToString();
            Account Livecontext = db.Table<Account>().Where(m => m.UserName.Equals(Username)).FirstOrDefault();
            string dispUername = "@" + Livecontext.UserName;
            

            var quantico = db.Table<Diary>().Where(m => m.Username.Equals(Username)).ToList();
            ObservableCollection<Diary> Diaries = new ObservableCollection<Diary>(db.Table<Diary>().Where(m => m.Username.Equals(Username)).ToList());


            EntriesListview.ItemsSource = null;
            EntriesListview.ItemsSource = Diaries;

        }

        private void FilterListSerchbar_TextChanged(object sender, TextChangedEventArgs e)
        {
            _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "DiarySecure.db3");
            var db = new SQLiteConnection(_dbPath);
            db.CreateTable<Account>();
            db.CreateTable<Diary>();
            var Username = Application.Current.Properties["UserName"].ToString();
            Account Livecontext = db.Table<Account>().Where(m => m.UserName.Equals(Username)).FirstOrDefault();
            string dispUername = "@" + Livecontext.UserName;


            var quantico = db.Table<Diary>().Where(m => m.Username.Equals(Username)).ToList();
            var filtrate = quantico.Where(z => z.Title.Contains(e.NewTextValue) || z.Content.Contains(e.NewTextValue));
            EntriesListview.ItemsSource = filtrate;

        }

        private async void EntriesListview_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var parser = e.SelectedItem as Diary;
            await Navigation.PushAsync(new EntryDetails(parser));
        }

        private void EntriesListview_OnRefreshing(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dbPath);
            db.CreateTable<Account>();
            db.CreateTable<Diary>();
            var Username = Application.Current.Properties["UserName"].ToString();
            var quantico = db.Table<Diary>().Where(m => m.Username.Equals(Username)).Take(8).ToList();

            EntriesListview.ItemsSource = null;
            EntriesListview.ItemsSource = quantico;
            EntriesListview.EndRefresh();
        }

        private async void NewEntryImageButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewEntryPage());
        }

        private void CalenderImageButton_OnClicked(object sender, EventArgs e)
        {
        }

        private void OldEntryImageButton_OnClicked(object sender, EventArgs e)
        {
        }

        private void MoodImageButton_OnClicked(object sender, EventArgs e)
        {
        }

        private void ProfileImageButton_OnClicked(object sender, EventArgs e)
        {
        }
    }
}