using Microsoft.Phone.Shell;
using GenericCountdown.Model;
using GenericCountdown.ViewModel;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GenericCountdown.Commons
{
    public static class Common
    {

        private static ProgressIndicator progress = null;

        public static ProgressIndicator GetProgressIndicator()
        {
            if (progress == null)
            {
                progress = new ProgressIndicator();
                progress.IsVisible = true;
                progress.IsIndeterminate = true;

            }

            return progress;
        }

        //public static FacebookLogin GetFacebookHelperInstance()
        //{
        //    return new FacebookLogin("", "");
        //}

        public static void SaveDataIntoSettings(string key, object value)
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            if (GetDataFromSettings(key) != null)
            {
                settings.Remove(key);
            }
            settings.Add(key, value);
            settings.Save();
        }
        public static void deleteDataIntoSettings(string key)
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            if (GetDataFromSettings(key) != null)
            {
                // clear user details
                settings.Remove(key);
            }
            settings.Save();
        }

        public static object GetDataFromSettings(string key)
        {
            try
            {
                return IsolatedStorageSettings.ApplicationSettings[key];
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static void ClearNavigationStack()
        {
            var journal = ((Microsoft.Phone.Controls.PhoneApplicationFrame)(ViewModelLocator.Navigation));
            if (journal != null)
            {
                while (journal.RemoveBackEntry() != null)
                { }
            }
        }

        //public static string ConvertUTCDate(string date)
        //{
        //    string sdate = "";
        //    var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        //    sdate = epoch.AddSeconds(long.Parse(date)).AddHours(5.5).ToString("dd MMM, yyyy");

        //    return sdate;
        //}

        public static void ShowMessageBox(string Message)
        {
            System.Windows.MessageBox.Show(Message, "PEPSI DEALS", System.Windows.MessageBoxButton.OK);
        }

        public static void LogError(string ModuleName, string MethodName, Exception Data)
        {
            // ShowMessageBox(Data.ToString());
        }

        //public static async Task<string> GetProfileImage(string UserId, string Size)
        //{
        //    string profileImage = "";

        //    try
        //    {

        //        WebRequest req = WebRequest.Create(string.Format("https://graph.facebook.com/{0}/picture?type={1}", UserId, Size));
        //        WebResponse response = await req.GetResponseAsync();
        //        profileImage = response.ResponseUri.ToString();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //    return profileImage;
        //}

        public static void ShowToastMessage(string message)
        {
            // The toast notification will not be shown if the foreground app is running.
            ShellToast toast = new ShellToast();
            toast.Title = "PEPSI DEALS";
            toast.Content = message;
            toast.Show();
        }

        //public static bool CheckEmailAddress(string EmailAddress)
        //{
        //    if (EmailAddress == null)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return System.Text.RegularExpressions.Regex.IsMatch(EmailAddress, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");
        //    }
        //}

        public static void LogCustomError(string Error)
        {
        }

    }
}
