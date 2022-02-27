using System;
using System.Collections.Generic;

namespace EagleApp.Models
{
    public partial class Ocawa
    {
        public int Id { get; set; }
        public string Ocawaname { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
