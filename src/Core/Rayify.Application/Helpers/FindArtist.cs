using System.Text.RegularExpressions;

namespace Rayify.Application.Helpers
{
    public class FindArtist
    {
        public static string FromTitle(string title)
        {
            string regex = @"^(.*?)\s*-\s*(.*?)\s*\((.*?)\)$";

            Match eslesme = Regex.Match(title, regex);
            string singerName = eslesme.Groups[1].Value.Trim();

            return singerName;
        }
    }
}
