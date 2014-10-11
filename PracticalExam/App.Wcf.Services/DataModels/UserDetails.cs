namespace App.Wcf.Services
{
    using App.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;

    [DataContractAttribute]
    public class UserDetails
    {
        // user's id, username, number of won games, number of lost games and rank
        public UserDetails(User user)
        {
            this.UserId = user.Id;
            this.Username = user.UserName;
            this.WonGames = user.WinsCount;
            this.LostGames = user.LossesCount;
            this.Rank = 100 * user.WinsCount + 15 * user.LossesCount;
        }

        [DataMember]
        public string UserId { get; set; }

        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public int? WonGames { get; set; }

        [DataMember]
        public int? LostGames { get; set; }

        [DataMember]        
        public int? Rank { get; set; }
    }
}