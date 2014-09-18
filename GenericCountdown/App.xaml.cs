using System;
using System.Diagnostics;
using System.Resources;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Threading;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using GenericCountdown.Resources;
using GenericCountdown.ViewModel;
using GenericCountdown.Model;
using Windows.ApplicationModel.Activation;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace GenericCountdown
{
    public partial class App : Application
    {
        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        public static PhoneApplicationFrame RootFrame { get; private set; }

        public static FileOpenPickerContinuationEventArgs FileOpenPickerContinuationEventArgs { get; set; }

        //public static string FileOpenPickerContinuationArgsSelectedImagePath { get; set; }

        //ContinuationManager continuationManager;
        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            // Global handler for uncaught exceptions.
            UnhandledException += Application_UnhandledException;


            // Standard XAML initialization
            InitializeComponent();

            // Phone-specific initialization
            InitializePhoneApplication();

            // Language display initialization
            InitializeLanguage();

            // Show graphics profiling information while debugging.
            if (Debugger.IsAttached)
            {
                // Display the current frame rate counters.
                Application.Current.Host.Settings.EnableFrameRateCounter = true;

                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode,
                // which shows areas of a page that are handed off to GPU with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;

                // Prevent the screen from turning off while under the debugger by disabling
                // the application's idle detection.
                // Caution:- Use this under debug mode only. Application that disables user idle detection will continue to run
                // and consume battery power when the user is not using the phone.
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }

            Microsoft.Phone.Shell.PhoneApplicationService.Current.ContractActivated += Application_ContractActivated;
        }

        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
        }

        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
        }

        // this is custom code by Nish that replaces ContinuationManager.cs class logic
        private void Application_ContractActivated(object sender, IActivatedEventArgs args)
        {
            //FileOpenPickerContinuationEventArgs = args as FileOpenPickerContinuationEventArgs;
            //if (args != null)
            //{
            //    if (args.Files.Count > 0)
            //    {
            //        FileOpenPickerContinuationArgsSelectedImagePath = args.Files[0].Path;
            //        //FileOpenPickerContinuationArgsSelectedImage = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
            //    }
            //}

            if (args == null)
                return;

            switch (args.Kind)
            {
                case ActivationKind.PickFileContinuation:
                    FileOpenPickerContinuationEventArgs = args as FileOpenPickerContinuationEventArgs;
                    break;

                //case ActivationKind.PickSaveFileContinuation:
                //    var fileSavePickerPage = rootFrame.Content as IFileSavePickerContinuable;
                //    if (fileSavePickerPage != null)
                //    {
                //        fileSavePickerPage.ContinueFileSavePicker(args as FileSavePickerContinuationEventArgs);
                //    }
                //    break;

                //case ActivationKind.PickFolderContinuation:
                //    var folderPickerPage = rootFrame.Content as IFolderPickerContinuable;
                //    if (folderPickerPage != null)
                //    {
                //        folderPickerPage.ContinueFolderPicker(args as FolderPickerContinuationEventArgs);
                //    }
                //    break;

                //case ActivationKind.WebAuthenticationBrokerContinuation:
                //    var wabPage = rootFrame.Content as IWebAuthenticationContinuable;
                //    if (wabPage != null)
                //    {
                //        wabPage.ContinueWebAuthentication(args as WebAuthenticationBrokerContinuationEventArgs);
                //    }
                //    break;
            }



        }
        
        //protected override void OnActivated(IActivatedEventArgs args) 
        //{ 
        //    if( args is FileSavePickerContinuationEventArgs ) 
        //    { Frame rootFrame = Window.Current.Content as Frame; 
        //        // Do not have content contained in the window when repeated application initialization, 
        //        // Just make sure the window is active 
        //        if (rootFrame == null) 
        //        {
        //            // create a framework to act as navigation context, Navigate to the first page 
        //            rootFrame = new Frame(); 
        //            // TODO: Change this value to the cache size is appropriate for your application. 
        //            rootFrame.CacheSize = 1; 
        //            if( args.PreviousExecutionState == ApplicationExecutionState.Terminated ) 
        //            { 
        //                // TODO: From the application program loading state before hanging up 
        //            } 
        //            // The frame on the current window 
        //            Window.Current.Content = rootFrame; 
        //        } 
        //        if( rootFrame.Content == null ) 
        //        { 
        //            // Remove navigation start for the revolving door. 
        //            if( rootFrame.ContentTransitions != null ) 
        //            { 
        //                this.transitions = new TransitionCollection(); 
        //                foreach( var c in rootFrame.ContentTransitions ) 
        //                { 
        //                    this.transitions.Add(c); 
        //                } 
        //            } 
        //            rootFrame.ContentTransitions = null; 
        //            rootFrame.Navigated += this.RootFrame_FirstNavigated; 
        //            // When the navigation stack has not been reduced, Navigation to the first page, 
        //            // And through the required information as the navigation parameter to configure 
        //            // The new page 
        //            if( !rootFrame.Navigate(typeof(MainPage)) ) 
        //            { 
        //                throw new Exception("Failed to create initial page"); 
        //            } 
        //        } 
        //        if( !rootFrame.Navigate(typeof(MainPage)) ) 
        //        { throw new 
        //            Exception("Failed to create target page"); 
        //        } 
        //        MainPage targetPage = rootFrame.Content as MainPage; 
        //        targetPage.SavePickerArgs = (FileSavePickerContinuationEventArgs)args; 
        //        // To ensure that the current window is active 
        //        Window.Current.Activate(); 
        //    } 
        //}
        
            
            
            
            
        
        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing

        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
            ViewModelLocator.Cleanup();
        }

        // Code to execute if a navigation fails
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                Debugger.Break();
            }
        }

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                Debugger.Break();
            }
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized = false;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            DispatcherHelper.Initialize();

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Handle reset requests for clearing the backstack
            RootFrame.Navigated += CheckForResetNavigation;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;

            GenericCountdown.ViewModel.ViewModelLocator.Navigation = RootFrame;
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        private void CheckForResetNavigation(object sender, NavigationEventArgs e)
        {
            // If the app has received a 'reset' navigation, then we need to check
            // on the next navigation to see if the page stack should be reset
            if (e.NavigationMode == NavigationMode.Reset)
                RootFrame.Navigated += ClearBackStackAfterReset;
        }

        private void ClearBackStackAfterReset(object sender, NavigationEventArgs e)
        {
            // Unregister the event so it doesn't get called again
            RootFrame.Navigated -= ClearBackStackAfterReset;

            // Only clear the stack for 'new' (forward) and 'refresh' navigations
            if (e.NavigationMode != NavigationMode.New && e.NavigationMode != NavigationMode.Refresh)
                return;

            // For UI consistency, clear the entire page stack
            while (RootFrame.RemoveBackEntry() != null)
            {
                ; // do nothing
            }
        }

        #endregion

        // Initialize the app's font and flow direction as defined in its localized resource strings.
        //
        // To ensure that the font of your application is aligned with its supported languages and that the
        // FlowDirection for each of those languages follows its traditional direction, ResourceLanguage
        // and ResourceFlowDirection should be initialized in each resx file to match these values with that
        // file's culture. For example:
        //
        // AppResources.es-ES.resx
        //    ResourceLanguage's value should be "es-ES"
        //    ResourceFlowDirection's value should be "LeftToRight"
        //
        // AppResources.ar-SA.resx
        //     ResourceLanguage's value should be "ar-SA"
        //     ResourceFlowDirection's value should be "RightToLeft"
        //
        // For more info on localizing Windows Phone apps see http://go.microsoft.com/fwlink/?LinkId=262072.
        //
        private void InitializeLanguage()
        {
            try
            {
                // Set the font to match the display language defined by the
                // ResourceLanguage resource string for each supported language.
                //
                // Fall back to the font of the neutral language if the Display
                // language of the phone is not supported.
                //
                // If a compiler error is hit then ResourceLanguage is missing from
                // the resource file.
                RootFrame.Language = XmlLanguage.GetLanguage(AppResources.ResourceLanguage);

                // Set the FlowDirection of all elements under the root frame based
                // on the ResourceFlowDirection resource string for each
                // supported language.
                //
                // If a compiler error is hit then ResourceFlowDirection is missing from
                // the resource file.
                FlowDirection flow = (FlowDirection)Enum.Parse(typeof(FlowDirection), AppResources.ResourceFlowDirection);
                RootFrame.FlowDirection = flow;
            }
            catch
            {
                // If an exception is caught here it is most likely due to either
                // ResourceLangauge not being correctly set to a supported language
                // code or ResourceFlowDirection is set to a value other than LeftToRight
                // or RightToLeft.

                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }

                throw;
            }
        }
    }
}