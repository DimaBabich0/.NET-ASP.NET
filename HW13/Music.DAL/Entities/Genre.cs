﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.DAL.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Song>? Songs { get; set; }
    }
}
