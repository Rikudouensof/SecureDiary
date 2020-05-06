using SecureDiary.Model;
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
    public partial class SigninPage : ContentPage
    {

        string _dbPath;
        public SigninPage()
        {
            InitializeComponent();
            _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "DiarySecure.db3");
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                var db = new SQLiteConnection(_dbPath);
                db.CreateTable<Account>();
                
                int? UserNameError = db.Table<Account>().OrderByDescending(c => c.id).Where(c => c.UserName == UsernameEntry.Text).Count();
                if (UserNameError == null)
                {
                    ErrorLabel.IsVisible = true;
                    ErrorLabel.Text = "Please Enter Username";
                    return;
                }
                else
                {
                    ErrorLabel.IsVisible = false;

                    bool UsernameExists = UserNameExists();

                    if (UsernameExists.Equals(true))
                    {
                        var MaxPk = db.Table<Account>().OrderByDescending(c => c.id).Where(c => c.UserName == UsernameEntry.Text).FirstOrDefault();
                        string hint = db.Table<Account>().Where(rd => rd.UserName == UsernameEntry.Text).Select(ss => ss.Hint).ToString();
                        if (UsernameEntry.Text == MaxPk.UserName && PasswordEntry.Text != MaxPk.Password)
                        {
                            ErrorLabel.Text = "Hint: " + MaxPk.Hint;
                            ErrorLabel.IsVisible = true;
                        }
                        else
                        {
                            ErrorLabel.IsVisible = false;
                            Application.Current.Properties["AccountID"] = MaxPk.id;
                            Application.Current.Properties["UserName"] = MaxPk.UserName;
                            Application.Current.Properties["F"] = MaxPk.FirstName;
                            Application.Current.Properties["L"] = MaxPk.LastName;
                            await Navigation.PushAsync(new HomePage());
                        }
                    }
                    else
                    {
                        ErrorLabel.Text = "Username does not exist";
                    }
                    
                }
            }
            catch (Exception)
            {


            }

        }

        private bool UserNameExists()
        {
            var db = new SQLiteConnection(_dbPath);
            db.CreateTable<Account>();
            try
            {
                int? UsernameExixtance = db.Table<Account>().Where(m => m.UserName.Equals(UsernameEntry.Text)).Count();
                if (UsernameExixtance != null || UsernameExixtance > 0)
                {
                    ErrorLabel.Text = "Username does not exist";
                    ErrorLabel.IsVisible = false;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private async void ImageButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}