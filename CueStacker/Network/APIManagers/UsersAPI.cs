using System;
using TestDemo;
using System.Collections.Generic;
using Newtonsoft.Json;
using CueStacker.Network.Models.Response;

namespace CueStacker.Network.APIManagers
{
    public class UsersAPI
    {
        private string GET_STACK_USERS = "http://ec2-52-41-237-177.us-west-2.compute.amazonaws.com/api/users/json";
        private string GET_USER_SO_DETAILS = "http://api.stackexchange.com/2.2/users/$USER_IDS$?order=desc&sort=reputation&site=stackoverflow";
        public UsersAPI()
        {
        }

        public async void GetStackUserIds(Action<String, GPError> callBack)
        {
            APIResult result = await NetworkRequestManager.Sharedmanager.sendGetRequest(GET_STACK_USERS);
            UserIdList userIds = new UserIdList();
            if (result.Error == null) 
            {
                userIds = JsonConvert.DeserializeObject<UserIdList>(result.ResponseJSON);
            }

            callBack(userIds.response, result.Error);
        }

        public async void getSODetails(String userIds, Action<List<CueStacker.Network.Models.Response.Item>, GPError> callBack)
        {
            string finalUrl = GET_USER_SO_DETAILS.Replace("$USER_IDS$", userIds);

			APIResult result = await NetworkRequestManager.Sharedmanager.sendGetRequest(finalUrl);
            UserDetails usersDetails = new UserDetails();

            if (result.Error == null) 
            {
                usersDetails = JsonConvert.DeserializeObject<UserDetails>(result.ResponseJSON);
            }

            callBack(usersDetails.items, result.Error);
        }
    }
}
