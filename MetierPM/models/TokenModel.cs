using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetierPM.models
{
    public class TokenModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}