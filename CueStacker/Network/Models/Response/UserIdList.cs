using System;
namespace CueStacker.Network.Models.Response
{
    public class UserIdList
    {
        public UserIdList()
        {
        }


        /* Unmerged change from project 'CueStacker.Droid'
        Before:
                public string response = String.Empty;
        After:
                private string response = String.Empty;

                public string response = String.Empty;
        */
        public string response { get; set; }
    }
}
