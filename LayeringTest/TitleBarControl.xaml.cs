using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace LayeringTest
{
    public sealed partial class TitleBarControl : UserControl
    {
        private bool searchVisibility = false;
        private bool appTitleVisibility = true;
        private bool appIconVisibility = true;
        private bool appBackButtonVisibility = false;

        public static readonly DependencyProperty showSearchProperty =
            DependencyProperty.RegisterAttached(
              "ShowSearch",
              typeof(Boolean),
              typeof(TitleBarControl),
              null
            );

        public static readonly DependencyProperty showBackButtonProperty =
            DependencyProperty.RegisterAttached(
              "ShowBackButton",
              typeof(Boolean),
              typeof(TitleBarControl),
              null
            );

        public bool ShowSearch
        {
            get { return searchVisibility; }
            set 
            {
                searchVisibility = value;

                if (searchVisibility)
                {
                    SearchBoxControl.Visibility = Visibility.Visible;
                }
                else
                {
                    SearchBoxControl.Visibility = Visibility.Collapsed;
                }
            }
        }

        public bool ShowBackButton
        {
            get { return appBackButtonVisibility; }
            set
            {
                appBackButtonVisibility = value;

                if (appBackButtonVisibility)
                {
                    BackButton.Visibility = Visibility.Visible;
                }
                else
                {
                    BackButton.Visibility = Visibility.Collapsed;
                }
            }
        }

        public TitleBarControl()
        {
            this.InitializeComponent();
            OverrideTitleBarContent();
        }

        private void OverrideTitleBarContent()
        {
            // Hide default title bar.
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;

            var titleBar = ApplicationView.GetForCurrentView().TitleBar;

            // Set active window colors
            titleBar.ButtonForegroundColor = new Windows.UI.Color() { A = 255, R = 191, G = 191, B = 191 };
            titleBar.ButtonBackgroundColor = Windows.UI.Colors.Transparent;

            titleBar.ButtonHoverForegroundColor = new Windows.UI.Color() { A = 255, R = 255, G = 255, B = 255 };
            titleBar.ButtonHoverBackgroundColor = Windows.UI.Colors.Transparent;

            titleBar.ButtonPressedForegroundColor = new Windows.UI.Color() { A = 145, R = 145, G = 145, B = 145 };
            titleBar.ButtonPressedBackgroundColor = Windows.UI.Colors.Transparent;

            // Set inactive window colors
            titleBar.ButtonInactiveForegroundColor = new Windows.UI.Color() { A = 92, R = 145, G = 145, B = 145 };
            titleBar.ButtonInactiveBackgroundColor = Windows.UI.Colors.Transparent;

            coreTitleBar.ExtendViewIntoTitleBar = true;
            UpdateTitleBarLayout(coreTitleBar);

            // Set XAML element as a draggable region.
            Window.Current.SetTitleBar(AppTitleBar);

            // Register a handler for when the size of the overlaid caption control changes.
            // For example, when the app moves to a screen with a different DPI.
            coreTitleBar.LayoutMetricsChanged += CoreTitleBar_LayoutMetricsChanged;

            // Register a handler for when the title bar visibility changes.
            // For example, when the title bar is invoked in full screen mode.
            coreTitleBar.IsVisibleChanged += CoreTitleBar_IsVisibleChanged;
        }

        private void CoreTitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
        {
            UpdateTitleBarLayout(sender);
        }

        private void CoreTitleBar_IsVisibleChanged(CoreApplicationViewTitleBar sender, object args)
        {
            if (sender.IsVisible)
            {
                AppTitleBar.Visibility = Visibility.Visible;
            }
            else
            {
                AppTitleBar.Visibility = Visibility.Collapsed;
            }
        }

        private void UpdateTitleBarLayout(CoreApplicationViewTitleBar coreTitleBar)
        {
            LeftPaddingColumn.Width = new GridLength(coreTitleBar.SystemOverlayLeftInset);
            RightPaddingColumn.Width = new GridLength(coreTitleBar.SystemOverlayRightInset);

            AppTitleBar.Height = coreTitleBar.Height;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (((Frame)Window.Current.Content).ActualWidth <= 1100)
            {
                SearchBoxControl.MaxWidth = ((Frame)Window.Current.Content).ActualWidth * 0.43;
            }
            else
            {
                SearchBoxControl.MaxWidth = 480;
            }
        }
    }
}
