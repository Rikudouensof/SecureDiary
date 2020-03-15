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
    public partial class SignUp : ContentPage
    {
        string _dbPath;
        public SignUp()
        {
            InitializeComponent();
            _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "DiarySecure.db3");

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            bool validate = IsSignupValidated();
            if (validate == false)
            {

            }
            else
            {
                var db = new SQLiteConnection(_dbPath);
                db.CreateTable<Account>();
                var MaximumPrimaryKey = db.Table<Account>().OrderByDescending(zt => zt.id).FirstOrDefault();
                int? UsernameExistError = db.Table<Account>().Where(we => we.UserName == UsernameEntry.Text).Count();
                if (UsernameExistError == null)
                {
                    ErrorLabel.IsVisible = true;
                    ErrorLabel.Text = "Username already Exist!";
                }
                else
                {
                    ErrorLabel.IsVisible = false;

                    Account account = new Account()
                    {
                        id = (MaximumPrimaryKey == null ? 1 : MaximumPrimaryKey.id + 1),
                        FirstName = FirstnameEntry.Text,
                        LastName = LastnameEntry.Text,
                        UserName = UsernameEntry.Text,
                        Hint = HintEntry.Text,
                        Password = PasswordEntry.Text
                    };
                    db.Insert(account);
                    await Navigation.PushAsync(new MainPage());
                }
            }
        }

        private bool IsSignupValidated()
        {
            bool NullCheck = Nullchecker();
            bool RePasswordCheck = Passwordmatch();
            if (NullCheck == true || RePasswordCheck == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void RePasswordEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool livechecker = Passwordmatch();
            if (livechecker.Equals(false))
            {
                ErrorLabel.Text = "Passwords do not match";
            }
            else
            {
                ErrorLabel.IsVisible = false;
            }
        }

        private bool Passwordmatch()
        {
            if (RePasswordEntry.Text == PasswordEntry.Text)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Nullchecker()
        {
            if (LastnameEntry.Text == null && LastnameEntry.Text.Equals(null))
            {
                ErrorLabel.IsVisible = true;
                NullError();
                return true;
            }
            else
            {
                ErrorLabel.IsVisible = false;

            }

            if (FirstnameEntry.Text == null && FirstnameEntry.Text.Equals(null))
            {
                ErrorLabel.IsVisible = true;
                NullError();
                return true;
            }
            else
            {
                ErrorLabel.IsVisible = false;

            }

            if (UsernameEntry.Text == null && ErrorLabel.Text.Equals(null))
            {
                ErrorLabel.IsVisible = true;
                NullError();
                return true;
            }
            else
            {
                ErrorLabel.IsVisible = false;
            }

            if (PasswordEntry.Text == null || PasswordEntry.Text.Equals(null))
            {
                ErrorLabel.IsVisible = true;
                NullError();
                return true;

            }
            else
            {
                ErrorLabel.IsVisible = false;
            }

            if (RePasswordEntry.Text == null || RePasswordEntry.Text.Equals(null))
            {
                ErrorLabel.IsVisible = true;
                NullError();
                return true;
            }
            else
            {
                ErrorLabel.IsVisible = false;
            }

            if (HintEntry.Text == null || HintEntry.Text.Equals(null))
            {
                ErrorLabel.IsVisible = true;
                NullError();
                return true;
            }
            else
            {
                ErrorLabel.IsVisible = false;
                return false;
            }



        }


        private void NullError()
        {
            ErrorLabel.Text = "Please fill all fields";
        }

        private void HintEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            ErrorLabel.Text = "Note: This your only window to password recovery";
        }

       
    }
}