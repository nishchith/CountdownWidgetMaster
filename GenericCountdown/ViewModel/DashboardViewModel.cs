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
                        PhotoFile = "/Assets/Images/default_portrait_05.png"
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