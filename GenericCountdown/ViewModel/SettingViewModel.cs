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

        public const string SelectedCountdownPropertyName = "SelectedCountdown";
        private CountdownItem _selectedCountdown;
        public CountdownItem SelectedCountdown
        {
            get
            {
                return _selectedCountdown;
            }

            set
            {
                if (_selectedCountdown == value)
                {
                    return;
                }

                RaisePropertyChanging(SelectedCountdownPropertyName);
                _selectedCountdown = value;
                RaisePropertyChanged(SelectedCountdownPropertyName);
            }
        }

        //public const string CountdownTypeListPropertyName = "CountdownTypeList";
        //private string[] _countdownTypeList;
        //public string[] CountdownTypeList
        //{
        //    get
        //    {
        //        return _countdownTypeList;
        //    }

        //    set
        //    {
        //        if (_countdownTypeList == value)
        //        {
        //            return;
        //        }

        //        RaisePropertyChanging(CountdownTypeListPropertyName);
        //        _countdownTypeList = value;
        //        RaisePropertyChanged(CountdownTypeListPropertyName);
        //    }
        //}


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

        public IEnumerable<Uri> Images { get; set; }

        private int selectedCountdownTypeIndex;
        public int SelectedCountdownTypeIndex
        {
            get { return selectedCountdownTypeIndex; }
            set
            {
                selectedCountdownTypeIndex = value;
                RaisePropertyChanged("SelectedCountdownTypeIndex");
            }
        }

        public SettingViewModel()
        {
            SelectedCountdown = ViewModelLocator.GetCurrentCounterItem();

            PopulateUnits();
            PopulateType();
            PopulateImages();

            SelectedCountdownTypeIndex = (SelectedCountdown.Type == "Countdown")?0:1;

            //SelectedCountdownTypes = new ObservableCollection<CountdownType>();
            //SelectedCountdownTypes.Add(SelectedCountdownType);
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


        public void PopulateType()
        {
            //CountdownTypeList = new string[] { "Countdown", "Anniversary" };
            CountdownTypes = new ObservableCollection<CountdownType>();
            CountdownTypes.Add(new CountdownType() { Name = "Countdown" });
            CountdownTypes.Add(new CountdownType() { Name = "Anniversary" });
        }








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
                return _countdownTypes;
            }

            set
            {
                if (_countdownTypes == value)
                {
                    return;
                }

                RaisePropertyChanging(CountdownTypesPropertyName);
                _countdownTypes = value;
                RaisePropertyChanged(CountdownTypesPropertyName);
            }
        }

        //public const string SelectedCountdownTypesPropertyName = "SelectedCountdownTypes";
        //private ObservableCollection<CountdownType> _selectedCountdownTypes;
        //public ObservableCollection<CountdownType> SelectedCountdownTypes
        //{
        //    get
        //    {
        //        return _selectedCountdownTypes;
        //    }

        //    set
        //    {
        //        if (_selectedCountdownTypes == value)
        //        {
        //            return;
        //        }

        //        RaisePropertyChanging(SelectedCountdownTypesPropertyName);
        //        _selectedCountdownTypes = value;
        //        RaisePropertyChanged(SelectedCountdownTypesPropertyName);
        //    }
        //}

        //DashboardViewModel dashboardViewModel = new DashboardViewModel();






        
        //public void MyRaisePorpertyChanged(string ppt)
        //{
        //    RaisePropertyChanged(ppt);
        //}
    }
}