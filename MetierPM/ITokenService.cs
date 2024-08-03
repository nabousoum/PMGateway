using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MetierPM
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITokenService" in both code and config file together.
    [ServiceContract]
    public interface ITokenService
    {
        [OperationContract]
        string GenerateAccessToken(ClaimData[] claims);

        [OperationContract]
        string GenerateRefreshToken();

        [OperationContract]
        ClaimsPrincipalData GetPrincipalFromExpiredToken(string token);
    }

    [DataContract]
    public class ClaimData
    {
        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public string Value { get; set; }
    }

    [DataContract]
    public class ClaimsPrincipalData
    {
        [DataMember]
        public ClaimData[] Claims { get; set; }
    }
}
