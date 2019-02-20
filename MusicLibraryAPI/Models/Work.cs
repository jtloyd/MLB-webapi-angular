using System;
using System.Collections.Generic;

namespace MusicLibraryAPI.Models
{
    public partial class Work
    {
        public Work()
        {
            WorkVersion = new HashSet<WorkVersion>();
        }

        public int WorkId { get; set; }
        public int ArtistId { get; set; }
        public string WorkName { get; set; }
        public int? ImageId { get; set; }
        public int? LyricId { get; set; }

        public Artist Artist { get; set; }
        public ICollection<WorkVersion> WorkVersion { get; set; }
    }
}
