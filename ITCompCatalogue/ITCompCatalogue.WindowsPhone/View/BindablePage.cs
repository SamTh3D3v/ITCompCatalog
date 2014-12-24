using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using ITCompCatalogue.Helper;

namespace ITCompCatalogue.View
{
    public class BindablePage : Page
    {
        public BindablePage():base()
        {
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var navigableViewModel = this.DataContext as INavigable;
            if (navigableViewModel != null)
                navigableViewModel.Activate(e.Parameter);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            var navigableViewModel = this.DataContext as INavigable;
            if (navigableViewModel != null)
                navigableViewModel.Deactivate(e.Parameter);
        }


        protected void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            var rootFrame = Window.Current.Content as Frame;
            if (rootFrame != null && rootFrame.CanGoBack)
            {
                rootFrame.GoBack();
                e.Handled = true;
            }

        }
        

    }
}
