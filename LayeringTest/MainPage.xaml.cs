using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Input;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace LayeringTest
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void nvSample_PaneOpening(Microsoft.UI.Xaml.Controls.NavigationView sender, object args)
        {
            //Change the opacity on the navview compact mode flyout so Acrylic is visible when flyout is open


            ((AcrylicBrush)Application.Current.Resources["NavPaneOpen"]).Opacity = 1;
        }

        private void nvSample_PaneClosing(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewPaneClosingEventArgs args)
        {
            //Change the opacity on the navview compact mode flyout so that mica (to be) shows through when collapsed
            ((AcrylicBrush)Application.Current.Resources["NavPaneOpen"]).Opacity = 0;
        }

        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            // Prototype override to test two side-panel navview modes
            if (displayToggle.IsOn)
            {
                nvSample.PaneDisplayMode = Microsoft.UI.Xaml.Controls.NavigationViewPaneDisplayMode.LeftCompact;
            }
            else
            {
                nvSample.PaneDisplayMode = Microsoft.UI.Xaml.Controls.NavigationViewPaneDisplayMode.Left;
            }
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            titleHeader.Text = ((Frame)Window.Current.Content).ActualWidth.ToString();
        }

        private void ToggleSwitch_Toggled_1(object sender, RoutedEventArgs e)
        {
            LocalTitleBarControl.ShowSearch = searchToggle.IsOn;
        }

        private void backBtnToggle_Toggled(object sender, RoutedEventArgs e)
        {
            LocalTitleBarControl.ShowBackButton = backBtnToggle.IsOn;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LocalTitleBarControl.Mode = (ModeBox.SelectedItem as ComboBoxItem).Content as string;
        }
    }
}
