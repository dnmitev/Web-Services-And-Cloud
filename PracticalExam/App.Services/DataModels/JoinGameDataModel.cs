namespace App.Services.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    public class JoinGameDataModel
    {
        [Range(1000,9999)]
        public int Number { get; set; }
    }
}