using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoveWedLive_Capstone.Models
{
    public class Admin
    {
        [Key]
        int Id { get; set; }
        string AdminName { get; set; }
    }
}
