namespace App.Services.DataModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class MakeGuessModel
    {
        [Range(1000, 9999)]
        public int Number { get; set; }
    }
}