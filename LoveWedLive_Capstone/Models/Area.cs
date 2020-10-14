using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoveWedLive_Capstone.Models
{
    public class Area
    {
        [Key]
        int Id { get; set; }
        string AreaName { get; set; }
    }
}
