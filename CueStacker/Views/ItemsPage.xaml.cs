using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestDemo;
using CueStacker.Network.APIManagers;

using Xamarin.Forms;

namespace CueStacker
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();

            GetStackIds();

        }

        void GetStackIds() 
        {
            UsersAPI userAPI = new UsersAPI();
            userAPI.GetStackUserIds((stackIds, error) =>
            {
                userAPI.getSODetails(stackIds, (allUserDetails, soError) =>
                {
                    //Todo:Display Data On List Here.
                    Console.WriteLine(allUserDetails);
                });
            });
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            // Manually deselect item
            ItemsListView.SelectedItem = null;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)            
                viewModel.LoadItemsCommand.Execute(null);

            viewModel.ItemsCallBack = (items) => {
                ItemsListView.ItemsSource = items;                  
            };

        }
    }
}
