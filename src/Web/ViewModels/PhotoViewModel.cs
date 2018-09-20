using System;

namespace Web.ViewModels
{
    public class PhotoViewModel
    {
        public string PhotoTitle { get; set; }
        public string AlbumName { get; set; }
        public Uri ThumbnailUrl { get; set; }
        public Uri Url { get; set; }
    }
}
