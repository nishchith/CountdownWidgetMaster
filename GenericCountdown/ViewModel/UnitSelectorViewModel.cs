using GalaSoft.MvvmLight;
using GenericCountdown.Model;

namespace GenericCountdown.ViewModel
{
    public class UnitSelectorViewModel : ViewModelBase
    {
        public const string CurrentCountdownPropertyName = "CurrentCountdown";
        private CountdownItem _currentCountdown;
        public CountdownItem CurrentCountdown
        {
            get
            {
                return _currentCountdown;
            }

            set
            {
                if (_currentCountdown == value)
                {
                    return;
                }

                RaisePropertyChanging(CurrentCountdownPropertyName);
                _currentCountdown = value;
                RaisePropertyChanged(CurrentCountdownPropertyName);
            }
        }

        public UnitSelectorViewModel()
        {
            CurrentCountdown = ViewModelLocator.GetCurrentCounterItem();

        }
    }
}