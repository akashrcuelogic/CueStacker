using System;
using System.Collections.Generic;

namespace CueStacker.Network.Models.Response
{
    public class UserDetails
    {
        public UserDetails()
        {
        }

        public List<Item> items { get; set; }
		public bool has_more { get; set; }
		public int quota_max { get; set; }
		public int quota_remaining { get; set; }
    }
}
