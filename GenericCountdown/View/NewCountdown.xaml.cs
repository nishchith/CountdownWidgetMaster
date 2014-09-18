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
using Windows.Storage.Pickers;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.Storage.Streams;
using GenericCountdown.Commons;

namespace GenericCountdown.View
{
    public partial class NewCountdown : PhoneApplicationPage
    {
        //PhotoChooserTask photoChooserTask = null;

        NewCountdownViewModel viewModel = null;

        public NewCountdown()
        {
            InitializeComponent();

            viewModel = this.DataContext as NewCountdownViewModel;

            // UI Control Events
            PhotoLibraryPicker.Click += new RoutedEventHandler(PhotoLibraryPicker_Click);
            unitListPicker.SummaryForSelectedItemsDelegate = SummarizeItems;
        }

        private void PhotoLibraryPicker_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");

            // Launch file open picker and caller app is suspended and may be terminated if required
            openPicker.PickSingleFileAndContinue();
        }

        //public void ContinueFileOpenPicker(FileOpenPickerContinuationEventArgs args)
        //{
        //    //WriteableBitmap _thumbnailImageBitmap;

        //    if (args.Files.Count > 0)
        //    {
        //        viewModel.NewCountdownItem.PhotoFile = args.Files[0].Path;

        //        GalleryPic.Source = new BitmapImage(new Uri(args.Files[0].Path, UriKind.RelativeOrAbsolute));


        //        //StorageFile file = args.Files[0];
        //        //IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read);

        //        //_thumbnailImageBitmap = new WriteableBitmap((int)GalleryPic.Width, (int)GalleryPic.Height); 

        //        //_thumbnailImageBitmap.SetSource(fileStream);
        //        //GalleryPic.Source = _thumbnailImageBitmap;




        //        //StorageFile file = args.Files[0];

        //        //BitmapImage image = new BitmapImage();

        //        //await image.SetSource(await file.OpenAsync(FileAccessMode.Read));

        //        //GalleryPic.Source = image;


                


        //        ////OutputTextBlock.Text = "Picked photo: " + args.Files[0].Path;
        //        //var stream = await args.Files[0].OpenAsync(Windows.Storage.FileAccessMode.Read);
        //        //var bitmapImage = new Windows.UI.Xaml.Media.Imaging.BitmapImage();
        //        //await bitmapImage.SetSourceAsync(stream);
        //        //GalleryPic.Source = bitmapImage;



        //    }
        //}

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
            UpdateChangesToObject();
            viewModel.AddCountdownItem();

            App.FileOpenPickerContinuationEventArgs = null;

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
                //photoChooserTask.Show();
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
        private void UpdateChangesToObject()
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            FileOpenPickerContinuationEventArgs args = App.FileOpenPickerContinuationEventArgs;

            if (args == null) return;

            if (args.Files.Count > 0)
            {
                //StorageFile file = args.Files[0];
                //BitmapImage image = new BitmapImage();
                //await image.SetSourceAsync(await file.OpenAsync(FileAccessMode.Read));
                //GalleryPic.Source = (BitmapImage)image;
                //viewModel.NewCountdownItem.PhotoFile = image.UriSource.ToString();


                StorageFile file = args.Files[0];
                BitmapImage image = new BitmapImage(new Uri(file.Path,UriKind.RelativeOrAbsolute));
                //image.SetSource(file.Path);
                GalleryPic.Source = image;
                viewModel.NewCountdownItem.PhotoFile = image.UriSource.ToString();



                //string filename = "Logo.png";
                //Windows.Storage.StorageFile sampleFile = await Windows.Storage.KnownFolders.DocumentsLibrary.GetFileAsync(filename);
                //// load file from a local folder
                ////Windows.Storage.StorageFile sampleFile = sampleFile = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync("Assets\\Logo.png");

                //BitmapImage img = new BitmapImage();
                //img = await LoadImage(sampleFile);
                //myImage.Source = img;

                //BitmapImage image = new BitmapImage();
                //image.SetSource(e.ChosenPhoto);
                //imgProfile.Source = image;

            }
            else
            {
                //OutputTextBlock.Text = "Operation cancelled.";
            }
        }
        //private static async Task<BitmapImage> LoadImage(StorageFile file)
        //{
        //    BitmapImage bitmapImage = new BitmapImage();
        //    FileRandomAccessStream stream = (FileRandomAccessStream)await file.OpenAsync(FileAccessMode.Read);

        //    bitmapImage.SetSource(stream);

        //    return bitmapImage;

        //}
    }
}