using Explode.Helpers;
using Explode.Service.Interfaces;
using Explode.Services.Implementations;

class Program
{
    static async Task Main(string[] args)
    {
        IVideoService videoService = new VideoService();

        try
        {
            string? urlInput = args.Length > 0 ? args[0] : null;

            if (urlInput != null && Validators.IsYouTubeUrl(urlInput))
            {
                var model = await videoService.GetVideoInfo(urlInput);

                var jsonStringModel = Newtonsoft.Json.JsonConvert.SerializeObject(model);

                Console.WriteLine(jsonStringModel);

                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Invalid Youtube URL");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}
