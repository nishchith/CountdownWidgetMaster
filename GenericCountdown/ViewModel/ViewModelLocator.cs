using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using GenericCountdown.Model;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using NodaTime;
using System.Collections.Generic;

namespace GenericCountdown.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        public static System.Windows.Controls.Frame Navigation { get; set; }

        public static CountdownDataContext countdownDB;

        public static CountdownItem CurrentCountdownItem { get; set; }

        public static int CountdownItemIndex { get; set; }

        public static ObservableCollection<CountdownItem> AllCountdownItems { get; set; }

        public static BitmapImage SelectedImage { get; set; }

        public static Uri SelectedMusic { get; set; }

        public static IEnumerable<Uri> ImageCollection { get; set; }

        public static ObservableCollection<CountdownType> CountdownTypes { get; set; }

        public static List<Units> AllUnits { get; set; }

        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            CountdownItemIndex = 0;

            PopulateImages();
            PopulateType();
            PopulateUnits();

            // Register those ViewModels which should be setup asa the app launch
            // this also delays the launch
            //SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<DashboardViewModel>();
            SimpleIoc.Default.Register<SettingViewModel>();
            SimpleIoc.Default.Register<HistoryViewModel>();
            SimpleIoc.Default.Register<NewCountdownViewModel>();
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
         //   "CA1822:MarkMembersAsStatic",
         //   Justification = "This non-static member is needed for data binding purposes.")]
        public DashboardViewModel Dashboard
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DashboardViewModel>();
            }
        }

        public SettingViewModel Setting
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SettingViewModel>();
            }
        }

        public HistoryViewModel History
        {
            get
            {
                return ServiceLocator.Current.GetInstance<HistoryViewModel>();
            }
        }

        public NewCountdownViewModel NewCountdown
        {
            get
            {
                return ServiceLocator.Current.GetInstance<NewCountdownViewModel>();
            }
        }

        public static void Cleanup()
        {
        }

        public static void LoadCollectionsFromDatabase()
        {
            var countdownItemsInDB = from CountdownItem countdown in countdownDB.CountdownTable
                                     select countdown;

            AllCountdownItems = new ObservableCollection<CountdownItem>(countdownItemsInDB);
        }


        public static CountdownItem GetCurrentCounterItem()
        {
            return AllCountdownItems.ElementAtOrDefault(CountdownItemIndex);
        }

        public static void SaveCountdown()
        {
            countdownDB.SubmitChanges();
        }

        public static void AddCountdownItem(CountdownItem newCountdownItem)
        {
            countdownDB.CountdownTable.InsertOnSubmit(newCountdownItem);

            countdownDB.SubmitChanges();

            AllCountdownItems.Add(newCountdownItem);

        }

        public static Ticker BuildTicker(CountdownItem item)
        {
            DateTime eventDate = new DateTime(item.EventDateTime.Ticks, DateTimeKind.Local);
            Ticker myTicker = new Ticker();
            Period timePeriod = null;
            PeriodBuilder pendingPeriod = new PeriodBuilder();

            //TimeSpan timeElapsed;

            switch (item.Type)
            {
                case "Countdown":
                    if (eventDate > DateTime.Now)
                    {
                        // Future Date
                        //timeElapsed = new TimeSpan(eventDate.Subtract(DateTime.Now).Ticks);
                        timePeriod = Period.Between(GetLocalDateTimeFromDateTime(DateTime.Now), GetLocalDateTimeFromDateTime(eventDate));
                        myTicker.Phrase = "Until " + item.EventName;
                    }
                    else
                    {
                        // Past Date
                        //timeElapsed = new TimeSpan(DateTime.Now.Subtract(eventDate).Ticks);
                        timePeriod = Period.Between(GetLocalDateTimeFromDateTime(DateTime.Now), GetLocalDateTimeFromDateTime(eventDate));
                        myTicker.Phrase = "Since " + item.EventName;
                    }
                    break;
                case "Anniversary":
                    if (eventDate > DateTime.Now)
                    {
                        // Future Date
                        //timeElapsed = new TimeSpan(eventDate.AddYears(1).Subtract(DateTime.Now).Ticks);
                        timePeriod = Period.Between(GetLocalDateTimeFromDateTime(DateTime.Now), GetLocalDateTimeFromDateTime(eventDate.AddYears(1)));
                        myTicker.Phrase = "Until " + item.EventName + " 1st Anniversary";
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
                        myTicker.Phrase = "Until " + item.EventName + " " + n + "th Anniversary";
                    }
                    break;
            }



            pendingPeriod = new PeriodBuilder(timePeriod);

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

            myTicker.YearFlag = item.YearFlag;
            myTicker.MonthFlag = item.MonthFlag;
            myTicker.WeekFlag = item.WeekFlag;
            myTicker.DayFlag = item.DayFlag;
            myTicker.HourFlag = item.HourFlag;
            myTicker.MinuteFlag = item.MinuteFlag;
            myTicker.SecondFlag = item.SecondFlag;

            myTicker.ImagePath = new Uri(item.PhotoFile, UriKind.RelativeOrAbsolute);
            return myTicker;
        }

        public static LocalDateTime GetLocalDateTimeFromDateTime(DateTime dt)
        {
            return new LocalDateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond);
        }

        public static void PopulateImages()
        {
            ImageCollection = new List<Uri>
            {
                new Uri("../Assets/Images/default_portrait_01.png", UriKind.Relative),
                new Uri("../Assets/Images/default_portrait_02.png", UriKind.Relative),
                new Uri("../Assets/Images/default_portrait_03.png", UriKind.Relative),
                new Uri("../Assets/Images/default_portrait_04.png", UriKind.Relative),
                new Uri("../Assets/Images/default_portrait_05.png", UriKind.Relative),
                new Uri("../Assets/Images/default_landscape_01.png", UriKind.Relative),
                new Uri("../Assets/Images/default_landscape_02.png", UriKind.Relative),
                new Uri("../Assets/Images/default_landscape_03.png", UriKind.Relative)
            };
        }

        public static void PopulateType()
        {
            CountdownTypes = new ObservableCollection<CountdownType>();
            CountdownTypes.Add(new CountdownType() { Name = "Countdown" });
            CountdownTypes.Add(new CountdownType() { Name = "Anniversary" });
        }

        public static void PopulateUnits()
        {
            AllUnits = new List<Units>();
            AllUnits.Add(new Units() { Name = "Random" });
            AllUnits.Add(new Units() { Name = "Years" });
            AllUnits.Add(new Units() { Name = "Months" });
            AllUnits.Add(new Units() { Name = "Weeks" });
            AllUnits.Add(new Units() { Name = "Days" });
            AllUnits.Add(new Units() { Name = "Hours" });
            AllUnits.Add(new Units() { Name = "Minutes" });
            AllUnits.Add(new Units() { Name = "Seconds" });
            AllUnits.Add(new Units() { Name = "Hearbeats" });
        }

    }
}