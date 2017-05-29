using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CueStacker.Network.APIManagers;
using Xamarin.Forms;
using CueStacker.Network.Models.Response;
using System.Collections.Generic;

namespace CueStacker
{
    public class ItemsViewModel : BaseViewModel
    {
        public List<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public Action<List<Item>> ItemsCallBack;

        public ItemsViewModel()
        {
            Title = "Cue Stackers";
            Items = new List<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            /*MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var _item = item as Item;
                Items.Add(_item);
                await DataStore.AddItemAsync(_item);
            });
            */
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();

				UsersAPI userAPI = new UsersAPI();
				userAPI.GetStackUserIds((stackIds, error) =>
				{
					userAPI.getSODetails(stackIds, (allUserDetails, soError) =>
					{
                        //Todo:Display Data On List Here.
                        //Console.WriteLine(allUserDetails);
                        Items = allUserDetails;
                        ItemsCallBack(allUserDetails);
					});
				});

                //var items = await DataStore.GetItemsAsync(true);
                //Items.ReplaceRange(items);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load items.",
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
