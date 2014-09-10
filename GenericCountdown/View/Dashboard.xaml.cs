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

namespace GenericCountdown.View
{
    public partial class Dashboard : PhoneApplicationPage
    {
        DashboardViewModel viewModel = null;


        // Constructor
        public Dashboard()
        {
            InitializeComponent();
            viewModel = this.DataContext as DashboardViewModel;

        }

        private void History_Click(object sender, EventArgs e)
        {

        }

        private void Setting_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/Setting.xaml", UriKind.Relative));
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
            viewModel.AsyncTicker();

            if (ViewModelLocator.SelectedImage == null)
            {
                ViewModelLocator.SelectedImage = new BitmapImage(new Uri("../Assets/Images/default_portrait_01.png", UriKind.Relative));
            }

            if (ViewModelLocator.CurrentCountdownItem.Music && ViewModelLocator.CurrentCountdownItem.MusicFile != "")
            {
                //MusicMediaElement.Source = new Uri(ViewModelLocator.CurrentCountdownItem.MusicFile, UriKind.Relative);
                //MusicMediaElement.Play();
                if (CanPlay())
                {
                    //MoveRobot.Begin();
                    //MusicMediaElement.Stop();
                    //MusicMediaElement.Source = new System.Uri("sound26.wma", System.UriKind.Relative); 
                    MusicMediaElement.Play();
                }   
            }

            this.BackgroundImage.Source = ViewModelLocator.SelectedImage;

            base.OnNavigatedFrom(e);
        }

        private bool CanPlay()
        {
            bool canPlay = false;
            FrameworkDispatcher.Update();
            if (MediaPlayer.GameHasControl)
            {
                canPlay = true;
            }
            else
            {
                if (MessageBox.Show("Is it ok to stop currently playing music?", "Can stop music?",
                    MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    canPlay = true;
                    MediaPlayer.Pause();

                }
                else
                {
                    canPlay = false;
                }
            }
            return canPlay;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            viewModel.KillAsyncTickerTask();

            MusicMediaElement.Stop();
            MusicMediaElement.Position = System.TimeSpan.FromSeconds(0); 

            base.OnNavigatedFrom(e);
        }
    }
}