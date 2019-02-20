﻿using System;
using System.Collections.Generic;

namespace MusicLibraryAPI.Models
{
    public partial class Genre
    {
        public Genre()
        {
            Artist = new HashSet<Artist>();
        }

        public int GenreId { get; set; }
        public string GenreName { get; set; }

        public ICollection<Artist> Artist { get; set; }
    }
}
