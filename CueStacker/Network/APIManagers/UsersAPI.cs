using System;
using TestDemo;
using Newtonsoft.Json;
using CueStacker.Network.Models.Response;

namespace CueStacker.Network.APIManagers
{
    public class UsersAPI
    {
        private string GET_STACK_USERS = "http://ec2-52-41-237-177.us-west-2.compute.amazonaws.com/api/users/json";
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
    }
}
