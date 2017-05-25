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
                Console.WriteLine(stackIds);
            });
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewItemPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}
