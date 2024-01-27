using System.Text.RegularExpressions;

namespace Explode.Helpers
{
    public class Validators
    {
        public static bool IsYouTubeUrl(string url)
        {
            // Biểu thức chính quy để kiểm tra URL của YouTube
            string youtubePattern = @"^(https?://)?(www\.)?(youtube\.com/watch\?v=|youtu\.be/|youtube\.com/embed/|youtube\.com/v/|youtube\.com/user/[\w/]+#.*?/)?([\w\-]{11})(\?\S+)?$";

            // Kiểm tra chuỗi đầu vào với biểu thức chính quy
            Regex regex = new Regex(youtubePattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(url);
        }
    }
}
