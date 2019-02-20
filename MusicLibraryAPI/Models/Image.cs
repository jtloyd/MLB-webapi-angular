using System;
using System.Collections.Generic;

namespace MusicLibraryAPI.Models
{
    public partial class Image
    {
        public int ImageId { get; set; }
        public byte[] Data { get; set; }
    }
}
