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
using System.Windows.Media.Imaging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using GoogleAds;
using System.Windows.Media;
using System.Windows.Input;

namespace GenericCountdown.View
{
    public partial class Dashboard : PhoneApplicationPage
    {
        DashboardViewModel viewModel = null;


        public Dashboard()
        {
            InitializeComponent();
            
            viewModel = this.DataContext as DashboardViewModel;

            SetupAdMob();

            this.ManipulationStarted += MainPage_ManipulationStarted; 
            this.ManipulationDelta += MainPage_ManipulationDelta; 
            this.ManipulationCompleted += MainPage_ManipulationCompleted;
        }

        // user has started manipulation, change outline to red 
        private void MainPage_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            Grid tupple = e.ManipulationContainer as Grid;
        }
        // element is being dragged, provide new translate coordinates 
        void MainPage_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            Grid tupple = e.ManipulationContainer as Grid;
            if (tupple != null)
            {
                TranslateTransform transform = tupple.RenderTransform as TranslateTransform;
                transform.Y += e.DeltaManipulation.Translation.Y;
            }
        }
        // element is dropped, make the outline transparent 
        void MainPage_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            Grid tupple = e.ManipulationContainer as Grid;
            //if (tupple != null)
            //{
            //    TranslateTransform transform = tupple.RenderTransform as TranslateTransform;
            //    viewModel.TransformPortraitY = transform.Y;
            //}
        }
        private void SetupAdMob()
        {
            AdRequest adRequest = new AdRequest();
            adRequest.ForceTesting = true;
            adModHome.LoadAd(adRequest);
        }

        private void History_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/History.xaml", UriKind.Relative));
        }

        private void Setting_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/Setting.xaml", UriKind.Relative));
        }

        private void AddNewCountdown_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/NewCountdown.xaml", UriKind.Relative));
        }

        private void FacebookShare_Click(object sender, EventArgs e)
        {

        }

        private void EmailShare_Click(object sender, EventArgs e)
        {

        }

        private void RateAndReview_Click(object sender, EventArgs e)
        {

        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            var journal = ((Microsoft.Phone.Controls.PhoneApplicationFrame)(ViewModelLocator.Navigation));
            if (journal != null)
            {
                while (journal.RemoveBackEntry() != null)
                { }
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            viewModel.LoadCurrentItems();
            viewModel.LoadTickerImage();
            viewModel.AsyncTicker();

            //if (ViewModelLocator.SelectedImage == null)
            //{
            //    ViewModelLocator.SelectedImage = new BitmapImage(new Uri("../Assets/Images/default_portrait_01.png", UriKind.Relative));
            //}

            //Uri MusicItem = new Uri("/Assets/Music/Sleep_Away.wma", UriKind.Relative);
            //viewModel.MyCurrentCountdownItem.MusicFile = MusicItem.ToString();
            //MusicMediaElement.Source = MusicItem;
            //MusicMediaElement.AutoPlay=true;

            //if (ViewModelLocator.CurrentCountdownItem.Music && ViewModelLocator.CurrentCountdownItem.MusicFile != "")
            //{
            //    MusicMediaElement.Source = new Uri(ViewModelLocator.CurrentCountdownItem.MusicFile, UriKind.Relative);
            //    MusicMediaElement.Play();
            //    if (CanPlay())
            //    {
            //        //MoveRobot.Begin();
            //        //MusicMediaElement.Stop();
            //        //MusicMediaElement.Source = new System.Uri("sound26.wma", System.UriKind.Relative); 
            //        MusicMediaElement.Play();
            //    }
            //}

            //working code below

            //if(1)
            //{
            //    //
            //}
            //else
            //{
            //    //
            //}
            //this.BackgroundImage.Source = ViewModelLocator.SelectedImage;

            //viewModel.BgImagePath = new Uri(viewModel.MyCurrentCountdownItem.PhotoFile, UriKind.RelativeOrAbsolute);

            //this.BackgroundImage.Source = new BitmapImage(viewModel.BgImagePath);

            base.OnNavigatedFrom(e);
        }

        //private bool CanPlay()
        //{
        //    bool canPlay = false;
        //    FrameworkDispatcher.Update();
        //    if (MediaPlayer.GameHasControl)
        //    {
        //        canPlay = true;
        //    }
        //    else
        //    {
        //        if (MessageBox.Show("Is it ok to stop currently playing music?", "Can stop music?",
        //            MessageBoxButton.OKCancel) == MessageBoxResult.OK)
        //        {
        //            canPlay = true;
        //            MediaPlayer.Pause();

        //        }
        //        else
        //        {
        //            canPlay = false;
        //        }
        //    }
        //    return canPlay;
        //}

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            viewModel.KillAsyncTickerTask();

            //MusicMediaElement.Stop();
            //MusicMediaElement.Position = System.TimeSpan.FromSeconds(0); 

            //viewModel.CurrentCountdownItem.PortraitY = viewModel.TransformPortraitY;
            ViewModelLocator.SaveCountdown();
            base.OnNavigatedFrom(e);
        }

        private void beep_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show("There was an error: " + e.ErrorException.Message);
        }

        private void beep_MediaOpened(object sender, RoutedEventArgs e)
        {
            //MusicMediaElement.Play();
            //MessageBox.Show("Media opened");
        }

    }
}