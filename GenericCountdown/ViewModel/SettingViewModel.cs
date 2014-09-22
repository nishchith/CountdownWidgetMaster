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

        // set this binding property of type object
        public const string SelectedUnitsPropertyName = "SelectedUnits";
        private ObservableCollection<object> _selectedUnits;
        public ObservableCollection<object> SelectedUnits
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

        public const string AllUnitsPropertyName = "AllUnits";
        private List<Units> _allUnits = new List<Units>();
        public List<Units> AllUnits
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

        public SettingViewModel()
        {
            LoadSelectedItemAsCurrent();

            PopulateUnits();
            PopulateType();
            PopulateImages();
        }

        private void LoadSelectedItemAsCurrent()
        {
            SelectedCountdown = ViewModelLocator.GetCurrentCounterItem(); 
            SelectedCountdownTypeIndex = (SelectedCountdown.Type == "Countdown") ? 0 : 1;
            //PopulateSelectedUnits();
        }

        public void PopulateUnits()
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

        public void PopulateImages()
        {
            Images = new List<Uri>
            {
                new Uri("/Assets/Images/default_portrait_01.png", UriKind.Relative),
                new Uri("/Assets/Images/default_portrait_02.png", UriKind.Relative),
                new Uri("/Assets/Images/default_portrait_03.png", UriKind.Relative),
                new Uri("/Assets/Images/default_portrait_04.png", UriKind.Relative),
                new Uri("/Assets/Images/default_portrait_05.png", UriKind.Relative),
                new Uri("/Assets/Images/default_landscape_01.png", UriKind.Relative),
                new Uri("/Assets/Images/default_landscape_02.png", UriKind.Relative),
                new Uri("/Assets/Images/default_landscape_03.png", UriKind.Relative)
            };
        }

        public void PopulateType()
        {
            CountdownTypes = new ObservableCollection<CountdownType>();
            CountdownTypes.Add(new CountdownType() { Name = "Countdown" });
            CountdownTypes.Add(new CountdownType() { Name = "Anniversary" });
        }

        //private CountdownItem _countdownItem;
        //public CountdownItem CountdownItem
        //{
        //    get { return _countdownItem; }
        //    set
        //    {
        //        _countdownItem = value;
        //        RaisePropertyChanged("CountdownItem");
        //    }
        //}
    }
}