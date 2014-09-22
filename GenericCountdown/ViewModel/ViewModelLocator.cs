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
using System.Collections;
using GenericCountdown.Commons;
using System.IO.IsolatedStorage;
using System.IO;
using Microsoft.Phone.Tasks;
using Windows.Storage;
using System.Text;

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

        public static CountdownItem TempCountdownItem { get; set; }

        public static int CountdownItemIndex { get; set; }

        public static ObservableCollection<CountdownItem> AllCountdownItems { get; set; }

        public static BitmapImage SelectedImage { get; set; }

        public static Uri SelectedMusic { get; set; }

        public static IEnumerable<Uri> ImageCollection { get; set; }

        public static ObservableCollection<CountdownType> CountdownTypes { get; set; }

        public static List<Units> AllUnits { get; set; }

        public static string SelectedUnitSummary { get; set; }

        public string PhotoFileName { get; set; }

        public static int ImageCount { get; set; }

        public static string FileDir { get; set; }

        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            CountdownItemIndex = 0;
                    
            ImageCount = 1;
            FileDir = "/SavedImages";

            PopulateImages();
            PopulateType();
            PopulateUnits();

            SelectedUnitSummary = "Choose Units";
            // Register those ViewModels which should be setup asa the app launch
            // this also delays the launch
            //SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<DashboardViewModel>();
            SimpleIoc.Default.Register<SettingViewModel>();
            SimpleIoc.Default.Register<HistoryViewModel>();
            SimpleIoc.Default.Register<NewCountdownViewModel>();
            SimpleIoc.Default.Register<UnitSelectorViewModel>();
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

        public UnitSelectorViewModel UnitSelector
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UnitSelectorViewModel>();
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
            
            LocalDateTime fromDate = new LocalDateTime();
            LocalDateTime toDate = new LocalDateTime();

            //Period timePeriod = null;

            PeriodBuilder pendingPeriod = new PeriodBuilder();

            //TimeSpan timeElapsed = timePeriod;

            switch (item.Type)
            {
                case "Countdown":
                    if (eventDate > DateTime.Now)
                    {
                        // Future Date
                        //timeElapsed = new TimeSpan(eventDate.Subtract(DateTime.Now).Ticks);
                        fromDate    = GetLocalDateTimeFromDateTime(DateTime.Now);
                        toDate      = GetLocalDateTimeFromDateTime(eventDate);

                        myTicker.Phrase = "Until " + item.EventName;
                    }
                    else
                    {
                        // Past Date
                        //timeElapsed = new TimeSpan(DateTime.Now.Subtract(eventDate).Ticks);
                        //timePeriod = Period.Between(GetLocalDateTimeFromDateTime(DateTime.Now), GetLocalDateTimeFromDateTime(eventDate));

                        fromDate    = GetLocalDateTimeFromDateTime(DateTime.Now);
                        toDate      = GetLocalDateTimeFromDateTime(eventDate);

                        myTicker.Phrase = "Since " + item.EventName;
                    }
                    break;
                case "Anniversary":
                    if (eventDate > DateTime.Now)
                    {
                        // Future Date
                        //timeElapsed = new TimeSpan(eventDate.AddYears(1).Subtract(DateTime.Now).Ticks);
                        //timePeriod = Period.Between(GetLocalDateTimeFromDateTime(DateTime.Now), GetLocalDateTimeFromDateTime(eventDate.AddYears(1)));
                        
                        fromDate    = GetLocalDateTimeFromDateTime(DateTime.Now);
                        toDate      = GetLocalDateTimeFromDateTime(eventDate.AddYears(1));
                        
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
                        //timePeriod = Period.Between(GetLocalDateTimeFromDateTime(DateTime.Now), GetLocalDateTimeFromDateTime(eventDate.AddYears(n)));

                        fromDate    = GetLocalDateTimeFromDateTime(DateTime.Now);
                        toDate      = GetLocalDateTimeFromDateTime(eventDate.AddYears(n));

                        myTicker.Phrase = "Until " + item.EventName + " " + n + "th Anniversary";
                    }
                    break;
            }

            

            //timePeriod.
            //timePeriod = Period.Between(fromDate, toDate);
            //timePeriod.

            //    (item.YearFlag)?PeriodUnits.Years|: ""
            //    (item.YearFlag)?PeriodUnits.Months:| 
            //    (item.YearFlag)?PeriodUnits.Weeks:| 
            //    (item.YearFlag)?PeriodUnits.Days:| 
            //    (item.YearFlag)?PeriodUnits.Hours:| 
            //    (item.YearFlag)?PeriodUnits.Minutes:| 
            //    (item.YearFlag)?PeriodUnits.Seconds:| 
            //    PeriodUnits.None
            //    );

            //timePeriod = 

            pendingPeriod = new PeriodBuilder(Period.Between(fromDate, toDate,
                ((item.YearFlag) ? PeriodUnits.Years : PeriodUnits.None) |
                ((item.MonthFlag) ? PeriodUnits.Months : PeriodUnits.None) |
                ((item.WeekFlag) ? PeriodUnits.Weeks : PeriodUnits.None) |
                ((item.DayFlag) ? PeriodUnits.Days : PeriodUnits.None) |
                ((item.HourFlag) ? PeriodUnits.Hours : PeriodUnits.None) |
                ((item.MinuteFlag) ? PeriodUnits.Minutes : PeriodUnits.None) |
                ((item.SecondFlag || item.HeartbeatFlag) ? PeriodUnits.Seconds : PeriodUnits.None) |
                PeriodUnits.None
                ));

            if (item.YearFlag)
            {
                ////int yy = Period.Between(fromDate, toDate, PeriodUnits.Years).Years;

                myTicker.Year = pendingPeriod.Years.ToString("00");
                //pendingPeriod.Years = 0;
                //fromDate.PlusYears(1);
            }

            if (item.MonthFlag)
            {
                //pendingPeriod = new PeriodBuilder(Period.Between(fromDate, toDate, PeriodUnits.Months));

                myTicker.Month = pendingPeriod.Months.ToString("00");
                //pendingPeriod.Months = 0;
            }

            if (item.WeekFlag)
            {
                //pendingPeriod = new PeriodBuilder(Period.Between(fromDate, toDate, PeriodUnits.Years));

                myTicker.Week = pendingPeriod.Weeks.ToString("00");
                //pendingPeriod.Weeks = 0;
            }

            if (item.DayFlag)
            {
                //pendingPeriod = new PeriodBuilder(Period.Between(fromDate, toDate, PeriodUnits.Years));

                myTicker.Day = pendingPeriod.Days.ToString("00");
                //pendingPeriod.Days = 0;
            }

            if (item.HourFlag)
            {
                //pendingPeriod = new PeriodBuilder(Period.Between(fromDate, toDate, PeriodUnits.Years));

                myTicker.Hour = pendingPeriod.Hours.ToString("00");
                //pendingPeriod.Hours = 0;
            }

            if (item.MinuteFlag)
            {
                //pendingPeriod = new PeriodBuilder(Period.Between(fromDate, toDate, PeriodUnits.Years));

                myTicker.Minute = pendingPeriod.Minutes.ToString("00");
                //pendingPeriod.Minutes = 0;
            }

            if (item.SecondFlag)
            {
                //pendingPeriod = new PeriodBuilder(Period.Between(fromDate, toDate, PeriodUnits.Years));

                myTicker.Second = pendingPeriod.Seconds.ToString("00");
            }
            if (item.HeartbeatFlag)
            {
                //pendingPeriod = new PeriodBuilder(Period.Between(fromDate, toDate, PeriodUnits.Years));

                myTicker.Heartbeat = (pendingPeriod.Seconds * item.HeartbeatCount/60).ToString("00");
            }

            myTicker.YearFlag = item.YearFlag;
            myTicker.MonthFlag = item.MonthFlag;
            myTicker.WeekFlag = item.WeekFlag;
            myTicker.DayFlag = item.DayFlag;
            myTicker.HourFlag = item.HourFlag;
            myTicker.MinuteFlag = item.MinuteFlag;
            myTicker.SecondFlag = item.SecondFlag;
            myTicker.HeartbeatFlag = item.HeartbeatFlag;

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
                new Uri("/Assets/Images/default_portrait_01.png", UriKind.RelativeOrAbsolute),
                new Uri("/Assets/Images/default_portrait_02.png", UriKind.RelativeOrAbsolute),
                new Uri("/Assets/Images/default_portrait_03.png", UriKind.RelativeOrAbsolute),
                new Uri("/Assets/Images/default_portrait_04.png", UriKind.RelativeOrAbsolute),
                new Uri("/Assets/Images/default_portrait_05.png", UriKind.RelativeOrAbsolute),
                new Uri("/Assets/Images/default_landscape_01.png", UriKind.RelativeOrAbsolute),
                new Uri("/Assets/Images/default_landscape_02.png", UriKind.RelativeOrAbsolute),
                new Uri("/Assets/Images/default_landscape_03.png", UriKind.RelativeOrAbsolute)
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
            AllUnits.Add(new Units() { Name = "Heartbeats" });
        }

        public static string SummerizeSelection(IList items)
        {
            if (items != null && items.Count > 0)
            {
                string summarizedString = "";
                for (int i = 0; i < items.Count; i++)
                {
                    summarizedString += ((Units)items[i]).Name.ToString();

                    if (i != items.Count - 1)
                        summarizedString += ", ";
                }

                return summarizedString;
            }
            else
                return "Select Unit";
        }

        //public void photoChooserTask_Completed()
        //{
        //    try
        //    {
        //        //if (e.TaskResult == TaskResult.OK)
        //        //{
        //        //    BitmapImage image = new BitmapImage();
        //        //    image.SetSource(e.ChosenPhoto);

        //        //    ViewModelLocator.SelectedImage = image;
        //        //    viewModel.SelectedCountdown.PhotoFile = ViewModelLocator.SelectedImage.UriSource.ToString();

        //        //    //LibrImage.Source = new BitmapImage(e.OriginalFileName);

        //        //    //ViewModelLocator.SelectedImage = image;
        //        //    ////ViewModelLocator.SelectedImage = new BitmapImage(new Uri(e.OriginalFileName, UriKind.RelativeOrAbsolute));
        //        //    ////ViewModelLocator.SelectedImage.SetSource(e.ChosenPhoto);
        //        //    //ViewModelLocator.CurrentCountdownItem.PhotoFile = e.OriginalFileName;
        //        //    //PhotoLibraryPicker.Text = e.OriginalFileName;
        //        //}

        //        if (e.TaskResult == TaskResult.OK)
        //        {
        //            PhotoFileName = "GallaryImage.jpg";
        //            string FileName = Path.GetFileName(e.OriginalFileName);

        //            StorageFolder tmpfolder = await ApplicationData.Current.LocalFolder.GetFolderAsync("temp");
        //            StorageFile file = await tmpfolder.CreateFileAsync(PhotoFileName, CreationCollisionOption.ReplaceExisting);
        //            using (Stream current = await file.OpenStreamForWriteAsync())
        //            {
        //                await e.ChosenPhoto.CopyToAsync(current);
        //            }

        //            SelectedImage = GetImageFromIsolatedStorage(PhotoFileName);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        //Common.ShowMessageBox("Error occured while saving pic.");
        //    }
        //}

        public static string SaveImageToIsolatedStorage(PhotoResult e)
        {
            //string fname = null;

            //try
            //{
            //    if (bitImg != null)
            //    {
            //        fname = GetImageName();

            //        IsolatedStorageFile isoStorage = IsolatedStorageFile.GetUserStoreForApplication();

            //        // if (isf.FileExists(fname)) { isf.DeleteFile(fname); }

            //        if (!isoStorage.FileExists(fname))
            //        {
            //            IsolatedStorageFileStream fileStream = isoStorage.CreateFile(fname);

            //            WriteableBitmap wb = new WriteableBitmap(bitImg);
            //            wb.SaveJpeg(fileStream, bitImg.PixelWidth, bitImg.PixelHeight, 0, 100);
            //            fileStream.Close();
            //        }
            //    }
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            
            //return fname;


            //string fname = GetImageName();

            string fname = "";

            try
            {
                if (e.TaskResult == TaskResult.OK)
                {
                    IsolatedStorageFile isoStorage = IsolatedStorageFile.GetUserStoreForApplication();

                    Stream photoStream = e.ChosenPhoto;
                    fname = Path.GetFileName(e.OriginalFileName);

                    if (!isoStorage.FileExists(fname))
                    {
                        IsolatedStorageFileStream fileStream = isoStorage.CreateFile(fname);

                        BitmapImage bitmap = new BitmapImage();
                        bitmap.SetSource(e.ChosenPhoto);

                        WriteableBitmap wb = new WriteableBitmap(bitmap);
                        wb.SaveJpeg(fileStream, 480, 800, 0, 85);
                        fileStream.Close();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return fname;
        }

        public static string GetImageName()
        {
            //return string.Format(
            //    "{0}/GalleryImage{1}.jpg",
            //    FileDir,
            //    ImageCount++);

            ImageCount = ImageCount + 1;

            return string.Format(
                "GalleryImage{0}.jpg",
                ImageCount);
        }

        public static BitmapImage GetImageFromIsolatedStorage(string fname)
        {
            BitmapImage img = new BitmapImage();
            try
            {
                using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (IsolatedStorageFileStream fileStream = isf.OpenFile(fname, FileMode.Open, FileAccess.Read))
                    {
                        img.SetSource(fileStream);
                        fileStream.Close();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return img;
        }


        //internal static void UnitSelectionFilter(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        //{
        //    if (sender == null) 
        //        return;

        //    try
        //    {
        //        if (((SmartListPicker)sender).SelectedItem == "")
        //            return;

        //        Units selectedUnit = (sender as SmartListPicker).SelectedItem as Units;

        //        switch (selectedUnit.Name)
        //        {
        //            case "Random":
        //                break;
        //            case "Heartbeats":
        //                break;
        //            default:
        //                break;
        //        }
                
        //    }
        //    catch (Exception)
        //    {
                
        //        throw;
        //    }
            


        //}


    }
}