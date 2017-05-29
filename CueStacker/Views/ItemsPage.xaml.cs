using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestDemo;
using CueStacker.Network.APIManagers;

using Xamarin.Forms;
using System.Threading;

namespace CueStacker
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;
        public static bool first;
        public ItemsPage()
        {

#if __IOS__
            first = false;
#else
            first = true;
#endif
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();

        }


        void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            // Manually deselect item
            ItemsListView.SelectedItem = null;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!first)
            {




                if (!NetworkReachabilityManager.isInternetAvailable())
                {
                    DisplayAlert("Cue Stackers", "Internet connection not available.", "Ok");
                    LblNoData.Text = "Internet connection not available.";
                    return;
                }
                LblNoData.IsVisible = false;

                viewModel.LoadItemsCommand.Execute(null);

                LoadingIndicator.IsRunning = true;
                LoadingIndicator.IsVisible = true;
                //ItemsListView.SeparatorVisibility = SeparatorVisibility.None;

                viewModel.ItemsCallBack = (items) =>
                {
                    if (items != null)
                    {
                        ItemsListView.SeparatorVisibility = SeparatorVisibility.Default;
                        LblNoData.IsVisible = false;
                        ItemsListView.ItemsSource = items;
                    }
                    else
                    {
                        LblNoData.IsVisible = true;
                        LblNoData.Text = "Data not available.";
                        DisplayAlert("Cue Stackers", "Data not available", "Ok");
                    }
                    LoadingIndicator.IsRunning = false;
                    LoadingIndicator.IsVisible = false;

                };
            }
            first = false;

        }
    }
}
