using SecureDiary.Model;
using SecureDiary.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SecureDiary
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
       
        string _dbPath;
        public HomePage()
        {
            InitializeComponent();
            _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "DiarySecure.db3");
            var db = new SQLiteConnection(_dbPath);
            db.CreateTable<Account>();
            db.CreateTable<Diary>();
            var Username = Application.Current.Properties["UserName"].ToString();
            Account Livecontext = db.Table<Account>().Where(m => m.UserName.Equals(Username)).FirstOrDefault();
            string dispUername = "@" + Livecontext.UserName;
            LastNameLabel.Text = Livecontext.LastName;
            FirstnameLabel.Text = Livecontext.FirstName;

            ObservableCollection<Diary> Diaries = new ObservableCollection<Diary>(db.Table<Diary>().Where(m => m.Username.Equals(Username)).OrderByDescending(m => m.DateOEntry).ToList());

            var quantico = db.Table<Diary>().Where(m =>m.Username.Equals(Username)).Take(8).ToList();

            TheOldPostSLider.ItemsSource = null;
            TheOldPostSLider.ItemsSource = Diaries;


            //quantico.Where(m =>m.Content.Length.Equals(30))
            //  TheOldPostSLider.SetBinding(ItemsView.ItemsSourceProperty, "quantico");


            DayDateLabel.Text = DateTime.Now.ToString("dddd dd MMMM yyyy");

            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                // do something every 1 second
                Device.BeginInvokeOnMainThread(() =>
                {
                    // interact with UI elements
                    TimeDateLabel.Text = DateTime.Now.ToString("HH:mm:ss");

                  

                });
                return true; // runs again, or false to stop
            });




            var random = new Random();
            var questions = new List<string>{
                "image_part_001.png",
                "image_part_002.png",
                "image_part_003.png",
                "image_part_004.png",
                "image_part_005.png",
                "image_part_006.png",
                "image_part_007.png",
                "image_part_008.png",
                "image_part_009.png",
                "image_part_010.png",
                "image_part_011.png",
                "image_part_012.png",
                "image_part_013.png",
                "image_part_014.png",
                "image_part_015.png",
                "image_part_016.png",
                "image_part_017.png",
                "image_part_018.png",
                "image_part_019.png",
                "image_part_020.png",
                "image_part_021.png",
                "image_part_022.png",
                "image_part_023.png",
                "image_part_024.png",
                "image_part_025.png"


            };
            int meeindex = random.Next(questions.Count);
            BarnerImage.Source = questions[meeindex];





        }

        private async void NewEntryImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewEntryPage());
        }

        private  async void OldEntryImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EntryListPage());
        }

        private async void TheOldPostSLider_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var parser = e.SelectedItem as Diary;
            await Navigation.PushAsync(new EntryDetails(parser));
        }

        private async void ProfileImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfilePage());
        }

        private void TheOldPostSLider_OnRefreshing(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dbPath);
            db.CreateTable<Account>();
            db.CreateTable<Diary>();
            var Username = Application.Current.Properties["UserName"].ToString();
            var quantico = db.Table<Diary>().Where(m => m.Username.Equals(Username)).Take(8).ToList();

            TheOldPostSLider.ItemsSource = null;
            TheOldPostSLider.ItemsSource = quantico;
            TheOldPostSLider.EndRefresh();
        }

        private void CalenderImageButton_OnClicked(object sender, EventArgs e)
        {
        }

        private void MoodImageButton_OnClicked(object sender, EventArgs e)
        {
            
        }
    }
}