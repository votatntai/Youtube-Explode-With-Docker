namespace Explode.Models
{
    public class Resolution
    {
        public string Quality { get; set; } = null!;

        public string Type { get; set; } = null!;

        public string URL { get; set; } = null!;

        public bool OnlyVideo { get; set; } = false;

        public bool OnlyAudio { get; set; } = false;
    }
}
