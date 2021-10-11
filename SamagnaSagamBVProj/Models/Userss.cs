using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace SamagnaSagamBVProj.Models
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class Userss
    {
        [JsonProperty("Users")]
        public List<UserInfo> Users {get;set;}
    }

    public class UserInfo
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("age")]
        public string age { get; set; }
    }
}

