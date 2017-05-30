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
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
				UsersAPI userAPI = new UsersAPI();
				userAPI.GetStackUserIds((stackIds, error) =>
				{
					userAPI.getSODetails(stackIds, (allUserDetails, soError) =>
					{
                        if (soError != null) 
                        {
							//Error
							MessagingCenter.Send(new MessagingCenterAlert
							{
								Title = "Error",
                                Message = soError.Message,
								Cancel = "OK"
							}, "message");
                            ItemsCallBack(null);
                        } else if (allUserDetails.Count == 0) {
							MessagingCenter.Send(new MessagingCenterAlert
							{
								Title = "Error",
								Message = "No data available.",
								Cancel = "OK"
							}, "message");
                            ItemsCallBack(null);
                        } else {
							Items = allUserDetails;
							ItemsCallBack(allUserDetails);
                        }
					});
				});
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
