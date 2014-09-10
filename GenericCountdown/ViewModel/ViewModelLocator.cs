using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using GenericCountdown.Model;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

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

        public static ObservableCollection<CountdownItem> AllCountdownItems { get; set; }

        public static BitmapImage SelectedImage { get; set; }


        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            // Register those ViewModels which should be setup asa the app launch
            // this also delays the launch
            //SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<DashboardViewModel>();
            SimpleIoc.Default.Register<SettingViewModel>();
            SimpleIoc.Default.Register<CountdownListViewModel>();
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
        public CountdownListViewModel CountdownList
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CountdownListViewModel>();
            }
        }

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }


        public static void LoadCollectionsFromDatabase()
        {
            var countdownItemsInDB = from CountdownItem countdown in countdownDB.CountdownTable
                                     select countdown;

            AllCountdownItems = new ObservableCollection<CountdownItem>(countdownItemsInDB);

            //LoadCurrentItems();
            //this.LoadTickerItems();
        }

        // Write changes in the data context to the database.
        public static void SaveCountdown()
        {
            countdownDB.SubmitChanges();
        }

        
    }
}