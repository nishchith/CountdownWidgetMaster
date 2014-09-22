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
using GenericCountdown.Commons;
using System.IO;
using Windows.Storage;
using System.IO.IsolatedStorage;

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
            photoChooserTask.ShowCamera = true;

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

            //unitListPicker.SummaryForSelectedItemsDelegate = ViewModelLocator.SummerizeSelection;
            //unitListPicker.SelectionChanged += ViewModelLocator.UnitSelectionFilter;
            //TypePicker.SummaryForSelectedItemsDelegate = SummarizeTypes;
        }

        void photoChooserTask_Completed(object sender, PhotoResult e)
        {
            string fname = ViewModelLocator.GetImageName();

            //if (e.TaskResult == TaskResult.OK)
            //{

            //        BitmapImage bitmap = new BitmapImage();
            //        bitmap.SetSource(e.ChosenPhoto);

            fname = ViewModelLocator.SaveImageToIsolatedStorage(e);
            //}


            


            //IsolatedStorageFile isoStorage2 = IsolatedStorageFile.GetUserStoreForApplication();

            //Best practice is to check whether the file with a given name exists in isolated storage or not
            //if (isoStorage2.FileExists("existing.jpg"))
            //{
            //    using (IsolatedStorageFileStream fileStream = isoStorage2.OpenFile("existing.jpg", FileMode.Open, FileAccess.Read))
            //    {
            //        //read the saved image
            //        BitmapImage bitmap = new BitmapImage();
            //        bitmap.SetSource(fileStream);

            //        //display the image in the imagecontrol
            //        //LibrImage.Source = bitmap;

            //        //LibrImage.Source = new BitmapImage( new Uri("isostore:/existing.jpg", UriKind.Absolute));
            //    }
            //}


            //BitmapImage img = new BitmapImage();
            //try
            //{
            //    using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            //    {
            //        using (IsolatedStorageFileStream fileStream = isf.OpenFile(fname, FileMode.Open, FileAccess.Read))
            //        {
            //            img.SetSource(fileStream);
            //            fileStream.Close();
            //        }
            //    }
            //}
            //catch { }

            viewModel.SelectedCountdown.PhotoFile = fname;

            LibrImage.Source = ViewModelLocator.GetImageFromIsolatedStorage(fname);


            //// no photo selected
            //if (e.ChosenPhoto == null) return;

            //try
            //{
            //    if (e.TaskResult == TaskResult.OK)
            //    {
            //        //// get the file stream and file name
            //        //Stream photoStream = e.ChosenPhoto;
            //        //string fileName = Path.GetFileName(e.OriginalFileName);

            //        //// persist data into isolated storage
            //        //StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            //        //using (Stream current = await file.OpenStreamForWriteAsync())
            //        //{
            //        //    await photoStream.CopyToAsync(current);
            //        //}

            //        BitmapImage image = new BitmapImage();
            //        image.SetSource(e.ChosenPhoto);

            //        string fname = "/SavedImages/myImg.jpg";

            //        if (image != null)
            //        {
            //            WriteableBitmap wbmp = new WriteableBitmap(image);
            //            IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication();
            //            using (isf)
            //            {
            //                if (isf.FileExists(fname)) { isf.DeleteFile(fname); }

            //                using (var stream = isf.OpenFile(fname, System.IO.FileMode.OpenOrCreate))
            //                {
            //                    wbmp.SaveJpeg(stream, image.PixelWidth, image.PixelHeight, 0, 100);
            //                    stream.Close();
            //                }
            //            }
            //        }

            //        //LibrImage.Source = image;

            //        //ViewModelLocator.SelectedImage = image;

            //        //string fileName = ViewModelLocator.SaveImageToIsolatedStorage(image);


            //        //viewModel.SelectedCountdown.PhotoFile = fileName;

            //        //LibrImage.Source = new BitmapImage(new Uri("isostore:/SavedImages/GalleryImage2.png", UriKind.Absolute));

            //        //// how to read the data later
            //        //StorageFile file2 = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
            //        //Stream imageStream = await file2.OpenStreamForReadAsync();

            //        //// display the file as image
            //        //BitmapImage bi = new BitmapImage();
            //        //bi.SetSource(imageStream);

            //        // assign the bitmap to Image in XAML: <Image x:Name="img"/>
            //        //LibrImage.Source = bi;

            //        BitmapImage img = new BitmapImage();
            //        try
            //        {
            //            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            //            {
            //                using (IsolatedStorageFileStream fileStream = isf.OpenFile(fname, FileMode.Open, FileAccess.Read))
            //                {
            //                    img.SetSource(fileStream);
            //                    fileStream.Close();
            //                }
            //            }
            //        }
            //        catch { }

            //        LibrImage.Source = img;

            //        //LibrImage.Source = ViewModelLocator.GetImageFromIsolatedStorage(viewModel.SelectedCountdown.PhotoFile);
            //    }
            //}
            //catch (Exception exe)
            //{
            //    //Common.ShowMessageBox("Error occured while saving pic.");
            //}
        }

        //public string SaveImageToIsolatedStorage(PhotoResult e)
        //{
        //    string fname = null;

        //    try
        //    {
        //        if (e.TaskResult == TaskResult.OK)
        //        {
        //            IsolatedStorageFile isoStorage = IsolatedStorageFile.GetUserStoreForApplication();

        //            //Best practice is to check whether the file with same name doesn't exits then create a new file
        //            if (!isoStorage.FileExists(fname))
        //            {
        //                IsolatedStorageFileStream fileStream = isoStorage.CreateFile(fname);

        //                BitmapImage bitmap = new BitmapImage();
        //                bitmap.SetSource(e.ChosenPhoto);

        //                WriteableBitmap wb = new WriteableBitmap(bitmap);
        //                wb.SaveJpeg(fileStream, 480, 800, 0, 85);
        //                fileStream.Close();
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //    return fname;
        //}

        //private string SummarizeItems(IList items)
        //{

        //    if (items != null && items.Count > 0)
        //    {
        //        string summarizedString = "";
        //        for (int i = 0; i < items.Count; i++)
        //        {
        //            summarizedString += ((Units)items[i]).Name.ToString();

        //            if (i != items.Count - 1)
        //                summarizedString += ", ";
        //        }

        //        return summarizedString;
        //    }
        //    else
        //        return "Select Unit";
        //}

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            UnitListPicker.Content = ViewModelLocator.SelectedUnitSummary;

            base.OnNavigatedTo(e);
        }
        //private void unitListPicker_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        //{
        //    StringBuilder msg = new StringBuilder();
        //    msg.Append("Selected Teams:");

        //    if (unitListPicker.SelectedItems != null)
        //    {
        //        foreach (var item in unitListPicker.SelectedItems)
        //        {
        //            var team = item as Units;
        //            msg.Append(" " + team.Name);
        //        }
        //    }

        //    //System.Diagnostics.Debug.WriteLine(msg.ToString());
        //}

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
            //if (viewModel.SelectedUnits != null)
            //{
            //    //viewModel.SelectedCountdown.YearFlag = viewModel.SelectedUnits.Contains()

            //    //viewModel.SelectedCountdown.RandomFlag	= false;
            //    viewModel.SelectedCountdown.YearFlag = false;
            //    viewModel.SelectedCountdown.MonthFlag = false;
            //    viewModel.SelectedCountdown.WeekFlag = false;
            //    viewModel.SelectedCountdown.DayFlag = false;
            //    viewModel.SelectedCountdown.HourFlag = false;
            //    viewModel.SelectedCountdown.MinuteFlag = false;
            //    viewModel.SelectedCountdown.SecondFlag = false;
            //    //viewModel.SelectedCountdown.HearbeatFlag 	= false;

            //    foreach (var item in viewModel.SelectedUnits)
            //    {
            //        var unit = item as Units;

            //        switch (unit.Name)
            //        {
            //            case "Random":
            //                //viewModel.SelectedCountdown.RandomFlag = true;
            //                break;
            //            case "Years":
            //                viewModel.SelectedCountdown.YearFlag = true;
            //                break;
            //            case "Months":
            //                viewModel.SelectedCountdown.MonthFlag = true;
            //                break;
            //            case "Weeks":
            //                viewModel.SelectedCountdown.WeekFlag = true;
            //                break;
            //            case "Days":
            //                viewModel.SelectedCountdown.DayFlag = true;
            //                break;
            //            case "Hours":
            //                viewModel.SelectedCountdown.HourFlag = true;
            //                break;
            //            case "Minutes":
            //                viewModel.SelectedCountdown.MinuteFlag = true;
            //                break;
            //            case "Seconds":
            //                viewModel.SelectedCountdown.SecondFlag = true;
            //                break;
            //            case "Hearbeats":
            //                viewModel.SelectedCountdown.YearFlag = true;
            //                break;
            //            default:
            //                break;
            //        }
            //    }
            //}

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
                Uri MusicItem = new Uri("/Assets/Music/Sleep_Away.wma", UriKind.Relative);
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

        //private void unitListPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (sender == null)
        //        return;

        //    try
        //    {
        //        if (((SmartListPicker)sender).SelectedItem == "")
        //            return;

        //        Units selectedUnit = (sender as SmartListPicker).SelectedItem as Units;

        //        switch (selectedUnit.Name)
        //        {
        //            case "Random":
        //                break;
        //            case "Heartbeats":
        //                break;
        //            default:
        //                break;
        //        }

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        private void UnitItem_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (sender == null)
                return;

            try
            {
                if (((TextBox)sender).Text == "")
                    return;

                //Units selectedUnit =  as Units;

                switch ((sender as TextBox).Text)
                {
                    case "Random":
                        break;
                    case "Heartbeats":
                        break;
                    default:
                        break;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void UnitListPicker_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/Control/UnitSelector.xaml", UriKind.Relative));

        }

    }
}