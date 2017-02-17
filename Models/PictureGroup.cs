using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminNaturguiden.Models
{
    public class PictureGroup
    {
        public libraryNaturguiden.Picture Picture { get; set; }
        public libraryNaturguiden.PictureCategory[] Categories { get; set; }
        public string[] Formats { get; set; }
    }
}