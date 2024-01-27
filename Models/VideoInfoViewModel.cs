using YoutubeExplode.Common;

namespace Explode.Models
{
    public class VideoInfoViewModel
    {
        public string URL { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string? ThumbnailUrl { get; set; }

        public ICollection<Resolution>? Resolutions { get; set; }

        public string Type { get; set; } = null!;
    }
}
