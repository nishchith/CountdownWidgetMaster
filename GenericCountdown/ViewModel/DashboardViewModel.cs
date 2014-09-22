using GalaSoft.MvvmLight;
using GenericCountdown.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NodaTime;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;
using System.Threading;
using System.Windows.Media.Imaging;
using Windows.Storage;
using System.IO;

namespace GenericCountdown.ViewModel
{
    public class DashboardViewModel : ViewModelBase
    {

        CancellationTokenSource taskSource = new CancellationTokenSource();

        string DBConnectionString = "Data Source=isostore:/CountdownDB.sdf";

        public const string CurrentCountdownItemPropertyName = "CurrentCountdownItem";
        private CountdownItem _currentCountdownItem;
        public CountdownItem CurrentCountdownItem
        {
            get
            {
                return _currentCountdownItem;
            }

            set
            {
                if (_currentCountdownItem == value)
                {
                    return;
                }

                RaisePropertyChanging(CurrentCountdownItemPropertyName);
                _currentCountdownItem = value;
                RaisePropertyChanged(CurrentCountdownItemPropertyName);
            }
        }

        private Ticker _myCurrentTickerItem;
        public Ticker MyCurrentTickerItem
        {
            get { return _myCurrentTickerItem; }
            set
            {
                _myCurrentTickerItem = value;
                RaisePropertyChanged("MyCurrentTickerItem");
            }
        }

        public double TransformPortraitY { get; set; }

        public DashboardViewModel()
        {
            PopulateDatabase();

            CurrentCountdownItem = ViewModelLocator.AllCountdownItems.ElementAtOrDefault(ViewModelLocator.CountdownItemIndex);

            LoadCurrentItems();

        }

        public void PopulateDatabase()
        {
            // Create the database if it does not exist.
            using (CountdownDataContext db = new CountdownDataContext(DBConnectionString))
            {
                //if database structure changed
                //db.DeleteDatabase();

                if (db.DatabaseExists() == false)
                {
                    db.CreateDatabase();

                    CountdownItem defaultItem = new CountdownItem
                    {
                        EventName = "My Event",
                        EventDateTime = DateTime.Now.AddYears(1),
                        Type = "Countdown",
                        YearFlag = true,
                        MonthFlag = true,
                        MinuteFlag = true,
                        SecondFlag = true,
                        PhotoFile = ViewModelLocator.ImageCollection.ElementAt(4).ToString()
                    };

                    db.CountdownTable.InsertOnSubmit(defaultItem);
                    db.SubmitChanges();
                }
            }

            ViewModelLocator.countdownDB = new CountdownDataContext(DBConnectionString);
            ViewModelLocator.LoadCollectionsFromDatabase();
        }

        public void LoadCurrentItems(int index = 0)
        {
            MyCurrentTickerItem = ViewModelLocator.BuildTicker(ViewModelLocator.AllCountdownItems.ElementAtOrDefault(ViewModelLocator.CountdownItemIndex));
            LoadTickerImage();
        }

        public void LoadTickerImage()
        {

            //MyCurrentTickerItem.ImagePath = new Uri(CurrentCountdownItem.PhotoFile, UriKind.RelativeOrAbsolute);

            if (MyCurrentTickerItem.ImagePath == null) 
                return;

            //if (ViewModelLocator.ImageCollection.Contains(MyCurrentTickerItem.ImagePath))
            //{
            //    MyCurrentTickerItem.BitImage = new BitmapImage(MyCurrentTickerItem.ImagePath);
            //}
            //else
            //{

            //    MyCurrentTickerItem.BitImage = ViewModelLocator.GetImageFromIsolatedStorage(MyCurrentTickerItem.ImagePath.ToString());

            //    //// how to read the data later
            //    //StorageFile file2 = await ApplicationData.Current.LocalFolder.GetFileAsync(MyCurrentTickerItem.ImagePath.ToString());
            //    //Stream imageStream = await file2.OpenStreamForReadAsync();

            //    //// display the file as image
            //    //BitmapImage bi = new BitmapImage();
            //    //bi.SetSource(imageStream);
            //    //MyCurrentTickerItem.BitImage = bi;
            //    //// assign the bitmap to Image in XAML: <Image x:Name="img"/>
            //    ////LibrImage.Source = bi;
            //}
            //= new Uri(item.PhotoFile, UriKind.RelativeOrAbsolute);
        }

        public void AsyncTicker()
        {
            var ts = new CancellationTokenSource();
            taskSource = ts;
            CancellationToken ct = taskSource.Token;

            IProgress<object> progress = new Progress<object>(_ => LoadCurrentItems());
            Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(1000);
                    progress.Report(null);
                }
            }, ct);
        }

        public void KillAsyncTickerTask()
        {
            taskSource.Cancel();
        }
    }
}