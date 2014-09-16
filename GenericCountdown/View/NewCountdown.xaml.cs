using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using GenericCountdown.ViewModel;
using GenericCountdown.Model;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Tasks;
using System.Collections;

namespace GenericCountdown.View
{
    public partial class NewCountdown : PhoneApplicationPage
    {
        PhotoChooserTask photoChooserTask = null;

        NewCountdownViewModel viewModel = null;

        public NewCountdown()
        {
            InitializeComponent();

            viewModel = this.DataContext as NewCountdownViewModel;

            unitListPicker.SummaryForSelectedItemsDelegate = SummarizeItems;
        }

        private string SummarizeItems(IList items)
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
        private void appBarAddButton_Click(object sender, EventArgs e)
        {
            UpdateChangesToDatabase();
            viewModel.AddCountdownItem();

            // Return to the main page.
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void appBarCancelButton_Click(object sender, EventArgs e)
        {
            // Return to the main page.
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void TypePicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TypePicker != null && TypePicker.SelectedItem != null)
            {
                var item = TypePicker.SelectedItem as CountdownType;

                viewModel.NewCountdownItem.Type = item.Name;
                viewModel.NewCountdownItemTypeIndex = TypePicker.SelectedIndex;
            }

        }
        private void PhotoPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != null && PhotoPicker.SelectedItem != null)
            {
                viewModel.NewCountdownItem.PhotoFile = ViewModelLocator.ImageCollection.ElementAt(PhotoPicker.SelectedIndex).ToString(); 
            }
        }
        private void PhotoLibraryPicker_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                photoChooserTask.Show();
            }
            catch (Exception)
            {

            }
        }
        private void MusicLibraryPicker_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                Uri MusicItem = new Uri("../Assets/Music/Sleep_Away.wma", UriKind.Relative);
                viewModel.NewCountdownItem.MusicFile = MusicItem.ToString();
            }
            catch (Exception exe)
            {

            }
        }
        private void UpdateChangesToDatabase()
        {
            // Get the full collection of items:
            if (viewModel.SelectedUnits != null)
            {
                //viewModel.SelectedCountdown.RandomFlag	= false;
                viewModel.NewCountdownItem.YearFlag = false;
                viewModel.NewCountdownItem.MonthFlag = false;
                viewModel.NewCountdownItem.WeekFlag = false;
                viewModel.NewCountdownItem.DayFlag = false;
                viewModel.NewCountdownItem.HourFlag = false;
                viewModel.NewCountdownItem.MinuteFlag = false;
                viewModel.NewCountdownItem.SecondFlag = false;
                //viewModel.SelectedCountdown.HearbeatFlag 	= false;

                foreach (var item in viewModel.SelectedUnits)
                {
                    var unit = item as Units;

                    switch (unit.Name)
                    {
                        case "Random":
                            //viewModel.SelectedCountdown.RandomFlag = true;
                            break;
                        case "Years":
                            viewModel.NewCountdownItem.YearFlag = true;
                            break;
                        case "Months":
                            viewModel.NewCountdownItem.MonthFlag = true;
                            break;
                        case "Weeks":
                            viewModel.NewCountdownItem.WeekFlag = true;
                            break;
                        case "Days":
                            viewModel.NewCountdownItem.DayFlag = true;
                            break;
                        case "Hours":
                            viewModel.NewCountdownItem.HourFlag = true;
                            break;
                        case "Minutes":
                            viewModel.NewCountdownItem.MinuteFlag = true;
                            break;
                        case "Seconds":
                            viewModel.NewCountdownItem.SecondFlag = true;
                            break;
                        case "Hearbeats":
                            viewModel.NewCountdownItem.YearFlag = true;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}