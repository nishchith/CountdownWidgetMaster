using GalaSoft.MvvmLight;
using GenericCountdown.Model;
using System;
using System.Collections.ObjectModel;

namespace GenericCountdown.ViewModel
{
    public class NewCountdownViewModel : ViewModelBase
    {
        public const string NewCountdownItemPropertyName = "NewCountdownItem";
        private CountdownItem _newCountdownItem;
        public CountdownItem NewCountdownItem
        {
            get
            {
                return _newCountdownItem;
            }

            set
            {
                if (_newCountdownItem == value)
                {
                    return;
                }

                RaisePropertyChanging(NewCountdownItemPropertyName);
                _newCountdownItem = value;
                RaisePropertyChanged(NewCountdownItemPropertyName);
            }
        }

        private int newCountdownItemTypeIndex;
        public int NewCountdownItemTypeIndex
        {
            get { return newCountdownItemTypeIndex; }
            set
            {
                newCountdownItemTypeIndex = value;
                RaisePropertyChanged("NewCountdownItemTypeIndex");
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

        public NewCountdownViewModel()
        {
            InitNewCounter();
            SetupPageControls();
        }

        public void InitNewCounter()
        {
            NewCountdownItem = new CountdownItem
            {
                EventName = "My New Event",
                EventDateTime = DateTime.Now.AddYears(1),
                Type = "Countdown",
                YearFlag = true,
                MonthFlag = true,
                MinuteFlag = true,
                SecondFlag = true,
                PhotoFile = "/Assets/Images/default_portrait_05.png"
            };
            
        }

        private void SetupPageControls()
        {
            NewCountdownItemTypeIndex = (NewCountdownItem.Type == "Countdown") ? 0 : 1;
        }

        public void AddCountdownItem()
        {
            ViewModelLocator.AddCountdownItem(NewCountdownItem);
        }
    }
}