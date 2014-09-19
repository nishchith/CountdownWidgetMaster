using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using GenericCountdown.ViewModel;
using System.Collections;
using GenericCountdown.Model;
using System.Windows.Media.Imaging;
using Microsoft.Xna.Framework.Media;
using System.Collections.ObjectModel;
using System.Text;

namespace GenericCountdown.View
{
    public partial class Setting : PhoneApplicationPage
    {
        PhotoChooserTask photoChooserTask = null;

        SettingViewModel viewModel = null;

        private BitmapImage selectedImage;
        public BitmapImage SelectedImage
        {
            get { return selectedImage; }
            set { selectedImage = value; }
        }

        public Setting()
        {
            InitializeComponent();
            photoChooserTask = new PhotoChooserTask();
            photoChooserTask.Completed += photoChooserTask_Completed;

            viewModel = this.DataContext as SettingViewModel;

            // Get the full collection of items:
            //List<CountdownType> allTypes = TypePicker.ItemsSource as List<CountdownType>;

            //int id;
            //if (viewModel.SelectedCountdownType.Name == "Countdown")
            //    id = 0;
            //else
            //    id = 1;

            //TypePicker.SelectedItems = new ObservableCollection<object>() { 
            //    allTypes[id] 
            //};

            unitListPicker.SummaryForSelectedItemsDelegate = SummarizeItems;
            //TypePicker.SummaryForSelectedItemsDelegate = SummarizeTypes;
        }

        void photoChooserTask_Completed(object sender, PhotoResult e)
        {
            try
            {
                if (e.TaskResult == TaskResult.OK)
                {
                    BitmapImage image = new BitmapImage();
                    image.SetSource(e.ChosenPhoto);


                    //LibrImage.Source = image;


                    ViewModelLocator.SelectedImage = image;
                    viewModel.SelectedCountdown.PhotoFile = ViewModelLocator.SelectedImage.UriSource.ToString();

                    //LibrImage.Source = new BitmapImage(e.OriginalFileName);

                    

                    //ViewModelLocator.SelectedImage = image;
                    ////ViewModelLocator.SelectedImage = new BitmapImage(new Uri(e.OriginalFileName, UriKind.RelativeOrAbsolute));
                    ////ViewModelLocator.SelectedImage.SetSource(e.ChosenPhoto);
                    //ViewModelLocator.CurrentCountdownItem.PhotoFile = e.OriginalFileName;
                    //PhotoLibraryPicker.Text = e.OriginalFileName;
                }
            }
            catch (Exception)
            {
                //Common.ShowMessageBox("Error occured while saving pic.");
            }
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
        private void unitListPicker_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            StringBuilder msg = new StringBuilder();
            msg.Append("Selected Teams:");

            if (unitListPicker.SelectedItems != null)
            {
                foreach (var item in unitListPicker.SelectedItems)
                {
                    var team = item as Units;
                    msg.Append(" " + team.Name);
                }
            }

            System.Diagnostics.Debug.WriteLine(msg.ToString());
        }

        //private void unitListPicker_SetSelectedItem(IList items)
        //{
            

        //}
        //private void PhotoLibraryPicker_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        //{
        //    try
        //    {
        //        photoChooserTask.Show();
        //    }
        //    catch (Exception)
        //    {

        //    }
        //}

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            //if (unitListPicker.SelectedItems.Count != null && unitListPicker.SelectedItems.Count > 0)
            //{
            //    viewModel.CountdownItem.YearFlag = false;
            //    viewModel.CountdownItem.YearFlag = false;
            //    viewModel.CountdownItem.MonthFlag = false;
            //    viewModel.CountdownItem.WeekFlag = false;
            //    viewModel.CountdownItem.DayFlag = false;
            //    viewModel.CountdownItem.HourFlag = false;
            //    viewModel.CountdownItem.MinuteFlag = false;
            //    viewModel.CountdownItem.SecondFlag = false;
            //    viewModel.CountdownItem.HeartbeatsFlag = false;

            //    for (int i = 0; i < unitListPicker.SelectedItems.Count; i++)
            //    {
            //        switch (((Units)unitListPicker.SelectedItems[i]).Name.ToString())
            //        {
            //            case "Random":
            //                viewModel.CountdownItem.YearFlag = true;
            //                break;
            //            case "Years":
            //                viewModel.CountdownItem.YearFlag = true;
            //                break;
            //            case "Months":
            //                viewModel.CountdownItem.MonthFlag = true;
            //                break;
            //            case "Weeks":
            //                viewModel.CountdownItem.WeekFlag = true;
            //                break;
            //            case "Days":
            //                viewModel.CountdownItem.DayFlag = true;
            //                break;
            //            case "Hours":
            //                viewModel.CountdownItem.HourFlag = true;
            //                break;
            //            case "Minutes":
            //                viewModel.CountdownItem.MinuteFlag = true;
            //                break;
            //            case "Seconds":
            //                viewModel.CountdownItem.SecondFlag = true;
            //                break;
            //            case "Heartbeats":
            //                viewModel.CountdownItem.HeartbeatsFlag = true;
            //                break;
            //            default:
            //                break;
            //        }
            //    }
            //}

            UpdateChangesToDatabase();
            base.OnNavigatedFrom(e);
        }
        
        private void UpdateChangesToDatabase()
        {
            //ViewModelLocator.CurrentCountdownItem.Type = viewModel.SelectedCountdownType.Name;
            //viewModel.MyRaisePorpertyChanged("CurrentCountdownItem");

            // Get the full collection of items:
            if (viewModel.SelectedUnits != null)
            {
                //viewModel.SelectedCountdown.YearFlag = viewModel.SelectedUnits.Contains()

                //viewModel.SelectedCountdown.RandomFlag	= false;
                viewModel.SelectedCountdown.YearFlag = false;
                viewModel.SelectedCountdown.MonthFlag = false;
                viewModel.SelectedCountdown.WeekFlag = false;
                viewModel.SelectedCountdown.DayFlag = false;
                viewModel.SelectedCountdown.HourFlag = false;
                viewModel.SelectedCountdown.MinuteFlag = false;
                viewModel.SelectedCountdown.SecondFlag = false;
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
                            viewModel.SelectedCountdown.YearFlag = true;
                            break;
                        case "Months":
                            viewModel.SelectedCountdown.MonthFlag = true;
                            break;
                        case "Weeks":
                            viewModel.SelectedCountdown.WeekFlag = true;
                            break;
                        case "Days":
                            viewModel.SelectedCountdown.DayFlag = true;
                            break;
                        case "Hours":
                            viewModel.SelectedCountdown.HourFlag = true;
                            break;
                        case "Minutes":
                            viewModel.SelectedCountdown.MinuteFlag = true;
                            break;
                        case "Seconds":
                            viewModel.SelectedCountdown.SecondFlag = true;
                            break;
                        case "Hearbeats":
                            viewModel.SelectedCountdown.YearFlag = true;
                            break;
                        default:
                            break;
                    }
                }
            }

            //List<Units> allItems =  as List<Units>;

            //// Set the SelectedItems to the 5th and 6th teams
            //unitListPicker.SelectedItems = new ObservableCollection<object>();

            //if (viewModel.SelectedCountdown.YearFlag)
            //    unitListPicker.SelectedItems.Add(allItems[0]);
            //if (viewModel.SelectedCountdown.YearFlag)
            //    unitListPicker.SelectedItems.Add(allItems[1]);
            //if (viewModel.SelectedCountdown.MonthFlag)
            //    unitListPicker.SelectedItems.Add(allItems[2]);
            //if (viewModel.SelectedCountdown.WeekFlag)
            //    unitListPicker.SelectedItems.Add(allItems[3]);
            //if (viewModel.SelectedCountdown.DayFlag)
            //    unitListPicker.SelectedItems.Add(allItems[4]);
            //if (viewModel.SelectedCountdown.HourFlag)
            //    unitListPicker.SelectedItems.Add(allItems[5]);
            //if (viewModel.SelectedCountdown.MinuteFlag)
            //    unitListPicker.SelectedItems.Add(allItems[6]);
            //if (viewModel.SelectedCountdown.SecondFlag)
            //    unitListPicker.SelectedItems.Add(allItems[7]);
            //if (viewModel.SelectedCountdown.YearFlag)
            //    unitListPicker.SelectedItems.Add(allItems[8]);


            ViewModelLocator.SaveCountdown();
            ViewModelLocator.LoadCollectionsFromDatabase();
        }


        private void MusicLibraryPicker_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                //using (MediaLibrary library = new MediaLibrary())
                //{
                //    SongCollection songs = library.Songs;
                //    Song song = songs[0];
                //    MediaPlayer.Play(song);
                //}
                Uri MusicItem = new Uri("../Assets/Music/Sleep_Away.wma", UriKind.Relative);
                viewModel.SelectedCountdown.MusicFile = MusicItem.ToString();
            }
            catch (Exception exe)
            {

            }
        }

        private void TypePicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TypePicker != null && TypePicker.SelectedItem != null)
            {
                var item = TypePicker.SelectedItem as CountdownType;

                viewModel.SelectedCountdown.Type = item.Name;
                viewModel.SelectedCountdownTypeIndex = TypePicker.SelectedIndex;
            }

            //StringBuilder msg = new StringBuilder();
            //msg.Append("Selected Teams:");
            //if (TypePicker.SelectedItems != null)
            //{
            //    foreach (var item in TypePicker.SelectedItems)
            //    {
            //        var team = item as CountdownType;
            //        msg.Append(" " + team.Name);
            //    }
            //}
            //System.Diagnostics.Debug.WriteLine(msg.ToString());
        }
        private void PhotoPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != null && PhotoPicker.SelectedItem != null)
            {
                ViewModelLocator.SelectedImage = new BitmapImage(viewModel.Images.ElementAt(PhotoPicker.SelectedIndex));
                viewModel.SelectedCountdown.PhotoFile = ViewModelLocator.SelectedImage.UriSource.ToString();

                //viewModel.CountdownItem.PhotoFile = viewModel.Images.ElementAt(PhotoPicker.SelectedIndex).ToString();
                ////viewModel.MyRaisePorpertyChanged("CurrentCountdownItem");
            }
        }

        private void PhotoLibraryPicker_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                photoChooserTask.Show();
            }
            catch (Exception)
            {

            }
        }
    }
}