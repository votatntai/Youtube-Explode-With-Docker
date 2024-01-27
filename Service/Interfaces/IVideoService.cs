using Explode.Models;

namespace Explode.Service.Interfaces
{
    public interface IVideoService
    {
        Task<VideoInfoViewModel?> GetVideoInfo(string link);
    }
}
