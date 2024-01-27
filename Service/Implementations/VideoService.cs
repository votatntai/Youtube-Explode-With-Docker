using Explode.Constants;
using Explode.Models;
using Explode.Service.Interfaces;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using Resolution = Explode.Models.Resolution;

namespace Explode.Services.Implementations
{
    public class VideoService : IVideoService
    {
        public VideoService()
        {
        }

        public async Task<VideoInfoViewModel?> GetVideoInfo(string link)
        {
            try
            {
                var model = link switch
                {
                    _ when link.Contains(SocialConstants.YoutubeContainLink) => await GetInfoVideoYoutubeAsync(link),
                    _ => throw new ArgumentOutOfRangeException(nameof(link), link, null)
                };

                return model ?? null!;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static async Task<VideoInfoViewModel?> GetInfoVideoYoutubeAsync(string link)
        {
            var youtubeClient = new YoutubeClient();

            var videoUrl = link;
            var video = await youtubeClient.Videos.GetAsync(videoUrl);
            var title = video.Title;
            var author = video.Author.ChannelTitle;
            var duration = video.Duration;

            var streamManifest = await youtubeClient.Videos.Streams.GetManifestAsync(videoUrl);
            var thumbnailUrl = video.Thumbnails.MaxBy(x => x.Resolution.Width)?.Url;

            var muxedStreamInfos = streamManifest.GetMuxedStreams();
            var videoOnlyStreamInfos = streamManifest.GetVideoOnlyStreams();
            var audioOnlyStreamInfos = streamManifest.GetAudioOnlyStreams();

            var muxedResolutions = muxedStreamInfos.Select(x => new Resolution()
            {
                Type = x.Container.Name,
                Quality = x.VideoQuality.Label,
                URL = x.Url + "&title=" + title
            }).ToList();

            var videoOnlyResolutions = videoOnlyStreamInfos.Select(x => new Explode.Models.Resolution()
            {
                Type = x.Container.Name,
                Quality = x.VideoQuality.Label,
                URL = x.Url + "&title=" + title,
                OnlyVideo = true
            }).ToList();

            var audioOnlyResolutions = new List<Resolution>
            {
                audioOnlyStreamInfos.Select(x => new Resolution()
                {
                    Type = x.Container.Name,
                    URL = x.Url + "&title=" + title,
                    OnlyAudio = true
                }).FirstOrDefault() ?? null!
            };

            var resolutions = muxedResolutions.Concat(videoOnlyResolutions).Concat(audioOnlyResolutions).ToList();


            var streamInfo = streamManifest
                .GetMuxedStreams()
                .Where(s => s.Container == Container.Mp4)
                .GetWithHighestVideoQuality();

            var urlReturn = $"{streamInfo.Url}&title={title}";

            return new VideoInfoViewModel
            {
                Title = title,
                URL = urlReturn,
                ThumbnailUrl = thumbnailUrl,
                Resolutions = resolutions,
                Type = SocialConstants.YoutubeContainLink
            };
        }
    }
}