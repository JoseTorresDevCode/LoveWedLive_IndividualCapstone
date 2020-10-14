using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoveWedLive_Capstone.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        string StreetName { get; set; }
        string City { get; set; }
        string State { get; set; }
        int Zipcode { get; set; }

    }
}
