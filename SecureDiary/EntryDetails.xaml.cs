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

        List<FrontAndBackground> FrontAndBackgroundz = new List<FrontAndBackground>();
        List<FontsandStyles> FontsandStylesz = new List<FontsandStyles>();




        public EntryDetails(Diary diary)
        {
            InitializeComponent();
            _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "DiarySecure.db3");
            var db = new SQLiteConnection(_dbPath);
            db.CreateTable<Account>();
            db.CreateTable<Diary>();
            var Username = Application.Current.Properties["UserName"].ToString();
            DiaryObject = diary;
            BindingContext = db.Table<Diary>().Where(m => m.Id.Equals(diary.Id)).FirstOrDefault();
            FrontAndBackgroundz.Add(new FrontAndBackground { Text = "Day", Textcolor = Color.FromHex("#2B335C"), Backgroundcolor = Color.LightGray });
            FrontAndBackgroundz.Add(new FrontAndBackground { Text = "Dark", Textcolor = Color.Black, Backgroundcolor = Color.DarkGray });
            FrontAndBackgroundz.Add(new FrontAndBackground { Text = "Night", Textcolor = Color.Green, Backgroundcolor = Color.FromHex("#292929") });
            FrontAndBackgroundz.Add(new FrontAndBackground { Text = "Vintage", Textcolor = Color.FromHex("#ebb37b"), Backgroundcolor = Color.FromHex("#539443") });
            FrontAndBackgroundz.Add(new FrontAndBackground { Text = "Pidgey", Textcolor = Color.FromHex("#f6edb4"), Backgroundcolor = Color.FromHex("#69482b") });
            FrontAndBackgroundz.Add(new FrontAndBackground { Text = "Hides", Textcolor = Color.FromHex("#fbf5f2"), Backgroundcolor = Color.FromHex("#e3b9a2") });
            FrontAndBackgroundz.Add(new FrontAndBackground { Text = "Spice of Life", Textcolor = Color.FromHex("#c8c26c"), Backgroundcolor = Color.FromHex("#b64135") });

            BackgroudDetailsCarouselView.ItemsSource = FrontAndBackgroundz;

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

        private void SelectFontButton_Clicked(object sender, EventArgs e)
        {
            var stringInThisCell = (FrontAndBackground)((Button)sender).BindingContext;

            ContentLabelCorasel.TextColor = stringInThisCell.Textcolor;
            ContentScrollView.BackgroundColor = stringInThisCell.Backgroundcolor;
            DateLabelCorasel.TextColor = stringInThisCell.Textcolor;
            TitleLabelCorasel.TextColor = stringInThisCell.Textcolor;

        }

        private void MoreMenuImageButton_png_Clicked(object sender, EventArgs e)
        {

            TextcontrolFrame.IsVisible = !TextcontrolFrame.IsVisible;
        }

        private void ChangefontButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}