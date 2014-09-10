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

            //unitListPicker.SummaryForSelectedItemsDelegate = SummarizeItems;
        }

        void photoChooserTask_Completed(object sender, PhotoResult e)
        {
            try
            {
                if (e.TaskResult == TaskResult.OK)
                {
                    BitmapImage image = new BitmapImage();
                    image.SetSource(e.ChosenPhoto);

                    ViewModelLocator.SelectedImage = image;
                    //ViewModelLocator.SelectedImage = new BitmapImage(new Uri(e.OriginalFileName, UriKind.RelativeOrAbsolute));
                    //ViewModelLocator.SelectedImage.SetSource(e.ChosenPhoto);
                    ViewModelLocator.CurrentCountdownItem.PhotoFile = e.OriginalFileName;
                    PhotoLibraryPicker.Text = e.OriginalFileName;
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
            ViewModelLocator.CurrentCountdownItem.Type = viewModel.SelectedCountdownType.Name;
            //viewModel.MyRaisePorpertyChanged("CurrentCountdownItem");

            ViewModelLocator.SaveCountdown();
            ViewModelLocator.LoadCollectionsFromDatabase();
        }

        private void PhotoPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != null && PhotoPicker.SelectedItem != null)
            {
                ViewModelLocator.SelectedImage = new BitmapImage(viewModel.Images.ElementAt(PhotoPicker.SelectedIndex));
                ViewModelLocator.CurrentCountdownItem.PhotoFile = ViewModelLocator.SelectedImage.UriSource.ToString();
                //viewModel.CountdownItem.PhotoFile = viewModel.Images.ElementAt(PhotoPicker.SelectedIndex).ToString();
                ////viewModel.MyRaisePorpertyChanged("CurrentCountdownItem");
            }
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

                ViewModelLocator.CurrentCountdownItem.MusicFile = "../Assets/Music/16-16000-mono-cbr.wma";

            }
            catch (Exception exe)
            {

            }
        }
    }
}