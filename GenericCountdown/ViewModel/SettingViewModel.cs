using GalaSoft.MvvmLight;
using GenericCountdown.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace GenericCountdown.ViewModel
{
    public class SettingViewModel : ViewModelBase
    {
        private CountdownItem _countdownItem;
        public CountdownItem CountdownItem
        {
            get { return _countdownItem; }
            set
            {
                _countdownItem = value;
                RaisePropertyChanged("CountdownItem");
            }
        }

        public const string AllUnitsPropertyName = "AllUnits";
        private ObservableCollection<Units> _allUnits = new ObservableCollection<Units>();
        public ObservableCollection<Units> AllUnits
        {
            get
            {
                return _allUnits;
            }

            set
            {
                if (_allUnits == value)
                {
                    return;
                }

                RaisePropertyChanging(AllUnitsPropertyName);
                _allUnits = value;
                RaisePropertyChanged(AllUnitsPropertyName);
            }
        }

        private CountdownType selectedCountdownType;
        public CountdownType SelectedCountdownType
        {
            get { return selectedCountdownType; }
            set
            {
                selectedCountdownType = value;
                RaisePropertyChanged("SelectedCountdownType");
            }
        }

        //private string[] countdownTypes = new string[] { "Countdown", "Anniversary" };
        //public string[] CountdownTypes
        //{
        //    get { return countdownTypes; }
        //}

        public const string CountdownTypesPropertyName = "CountdownTypes";
        private ObservableCollection<CountdownType> _countdownTypes;
        public ObservableCollection<CountdownType> CountdownTypes
        {
            get
            {
                return _countdownTypes  ;
            }

            set
            {
                if (_countdownTypes  == value)
                {
                    return;
                }

                RaisePropertyChanging(CountdownTypesPropertyName);
                _countdownTypes= value;
                RaisePropertyChanged(CountdownTypesPropertyName);
            }
        }

        //DashboardViewModel dashboardViewModel = new DashboardViewModel();
        public IEnumerable<Uri> Images { get; set; }


        public SettingViewModel()
        {
            CountdownItem = ViewModelLocator.CurrentCountdownItem;
            PopulateUnits();
            PopulateType();
            PopulateImages();

            SelectedCountdownType = new CountdownType() { Name = CountdownItem.Type };
        }

        public void PopulateUnits()
        {
            AllUnits = new ObservableCollection<Units>();
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
        
        public void PopulateType()
        {
            CountdownTypes = new ObservableCollection<CountdownType>();
            CountdownTypes.Add(new CountdownType() { Name = "Countdown" });
            CountdownTypes.Add(new CountdownType() { Name = "Anniversary" });
        }
        public void PopulateImages()
        {
            Images = new List<Uri>
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
        //public void MyRaisePorpertyChanged(string ppt)
        //{
        //    RaisePropertyChanged(ppt);
        //}
    }
}