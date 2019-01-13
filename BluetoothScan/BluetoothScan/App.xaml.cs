using Prism;
using Prism.Ioc;
using BluetoothScan.ViewModels;
using BluetoothScan.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using Unity;
using Prism.Unity;
using Prism.Logging;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace BluetoothScan
{
    public partial class App: PrismApplication
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            var result = await NavigationService.NavigateAsync("NavigationPage/MainPage");

            if (!result.Success)
            {
                SetMainPageFromException(result.Exception);
            }

        }

        // It registers the pages for navigation in the app
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<DevicePage>();
        }

        // It gets an exception and sets up an exception page
        private void SetMainPageFromException(Exception e)
        {
            var exceptionLayout = new StackLayout
            {
                Padding = new Thickness(50)
            };
            // Exception Title
            exceptionLayout.Children.Add(new Label
            {
                Text = "Exception Title: " + e?.GetType()?.Name ?? "Unknown",
                HorizontalOptions = LayoutOptions.Center
            });
            // Exception Details
            exceptionLayout.Children.Add(new ScrollView
            {
                Content = new Label
                {
                    Text= $"{e}",
                    LineBreakMode= LineBreakMode.WordWrap
                }
            });
        }
    }
}
