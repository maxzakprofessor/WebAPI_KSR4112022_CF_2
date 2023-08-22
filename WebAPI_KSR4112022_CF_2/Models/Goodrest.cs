using System;
using System.Collections.Generic;

namespace WebAPI_KSR4112022_CF_2.Models
{
    public partial class Goodrest
    {
        public int Id { get; set; }
        public string NameStock { get; set; } = null!;
        public string NameGood { get; set; } = null!;
        public int Qty { get; set; }
    }
}
