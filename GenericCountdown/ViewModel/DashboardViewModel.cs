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
        

        public const string SelectedUnitsPropertyName = "SelectedUnits";
        private ObservableCollection<Units> _selectedUnits = new ObservableCollection<Units>();
        public ObservableCollection<Units> SelectedUnits
        {
            get
            {
                return _selectedUnits;
            }

            set
            {
                if (_selectedUnits == value)
                {
                    return;
                }

                RaisePropertyChanging(SelectedUnitsPropertyName);
                _selectedUnits = value;
                RaisePropertyChanged(SelectedUnitsPropertyName);
            }
        }


        public const string TickerPhrasePropertyName = "TickerPhrase";
        private string _tickerPhrase = "";
        public string TickerPhrase
        {
            get
            {
                return _tickerPhrase;
            }

            set
            {
                if (_tickerPhrase == value)
                {
                    return;
                }

                RaisePropertyChanging(TickerPhrasePropertyName);
                _tickerPhrase = value;
                RaisePropertyChanged(TickerPhrasePropertyName);
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


        private CountdownItem _myCurrentCountdownItem;
        public CountdownItem MyCurrentCountdownItem
        {
            get { return _myCurrentCountdownItem; }
            set
            {
                _myCurrentCountdownItem = value;
                RaisePropertyChanged("MyCurrentCountdownItem");
            }
        }


        public DashboardViewModel()
        {
            PopulateDatabase();
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
            MyCurrentCountdownItem = ViewModelLocator.CurrentCountdownItem = ViewModelLocator.AllCountdownItems.ElementAtOrDefault(index);
            Period timePeriod = GetTimePeriod(ViewModelLocator.CurrentCountdownItem);

            MyCurrentTickerItem = BuildTicker(timePeriod, ViewModelLocator.CurrentCountdownItem);
        }

        //public void LoadTicker(int index = 0)
        //{
        //}

        public Ticker BuildTicker(Period timePeriod, CountdownItem item)
        {
            Ticker myTicker = new Ticker();
            PeriodBuilder pendingPeriod = new PeriodBuilder(timePeriod);

            if (item.YearFlag)
            {
                myTicker.Year = pendingPeriod.Years.ToString("00");
                pendingPeriod.Years = 0;
            }

            if (item.MonthFlag)
            {
                myTicker.Month = pendingPeriod.Months.ToString("00");
                pendingPeriod.Months = 0;
            }

            if (item.WeekFlag)
            {
                myTicker.Week = pendingPeriod.Weeks.ToString("00");
                pendingPeriod.Weeks = 0;
            }

            if (item.DayFlag)
            {
                myTicker.Day = pendingPeriod.Days.ToString("00");
                pendingPeriod.Days = 0;
            }

            if (item.HourFlag)
            {
                myTicker.Hour = pendingPeriod.Hours.ToString("00");
                pendingPeriod.Hours = 0;
            }

            if (item.MinuteFlag)
            {
                myTicker.Minute = pendingPeriod.Minutes.ToString("00");
                pendingPeriod.Minutes = 0;
            }

            if (item.SecondFlag)
            {
                myTicker.Second = pendingPeriod.Seconds.ToString("00");
            }

            myTicker.Phrase = TickerPhrase;

            return myTicker;
        }

        public Period GetTimePeriod(CountdownItem item)
        {
            DateTime eventDate = new DateTime(item.EventDateTime.Ticks, DateTimeKind.Local);

            //TimeSpan timeElapsed;
            Period timePeriod = null;

            switch (item.Type)
            {
                case "Countdown":
                    if (eventDate > DateTime.Now)
                    {
                        // Future Date
                        //timeElapsed = new TimeSpan(eventDate.Subtract(DateTime.Now).Ticks);
                        timePeriod = Period.Between(GetLocalDateTimeFromDateTime(DateTime.Now), GetLocalDateTimeFromDateTime(eventDate));
                        TickerPhrase = "Until " + item.EventName;
                    }
                    else
                    {
                        // Past Date
                        //timeElapsed = new TimeSpan(DateTime.Now.Subtract(eventDate).Ticks);
                        timePeriod = Period.Between(GetLocalDateTimeFromDateTime(DateTime.Now), GetLocalDateTimeFromDateTime(eventDate));
                        TickerPhrase = "Since " + item.EventName;
                    }
                    break;
                case "Anniversary":
                    if (eventDate > DateTime.Now)
                    {
                        // Future Date
                        //timeElapsed = new TimeSpan(eventDate.AddYears(1).Subtract(DateTime.Now).Ticks);
                        timePeriod = Period.Between(GetLocalDateTimeFromDateTime(DateTime.Now), GetLocalDateTimeFromDateTime(eventDate.AddYears(1)));
                        TickerPhrase = "Until " + item.EventName + " 1st Anniversary";
                    }
                    else
                    {
                        // Past Date
                        int n = 1;
                        while (eventDate.AddYears(n) < DateTime.Now)
                        {
                            n++;
                        }
                        //timeElapsed = new TimeSpan(eventDate.AddYears(n).Subtract(DateTime.Now).Ticks);
                        timePeriod = Period.Between(GetLocalDateTimeFromDateTime(DateTime.Now), GetLocalDateTimeFromDateTime(eventDate.AddYears(n)));
                        TickerPhrase = "Until " + item.EventName + " " + n + "th Anniversary";
                    }
                    break;
            }

            return timePeriod;
        }

        public static LocalDateTime GetLocalDateTimeFromDateTime(DateTime dt)
        {
            return new LocalDateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond);
        }


        public void AsyncTicker()
        {
            var ts = new CancellationTokenSource();

            taskSource = ts;

            CancellationToken ct = taskSource.Token;

            Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(1000);
                    Deployment.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        LoadCurrentItems();
                    }), null);
                }
            }, ct);
        }

        public void KillAsyncTickerTask()
        {
            taskSource.Cancel();
        }
    }
}