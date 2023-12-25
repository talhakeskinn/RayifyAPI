using System.Text.RegularExpressions;

namespace Rayify.Application.Helpers
{
    public class Slugify
    {
        public static string GenerateSlug(string text)
        {
            string str = text.ToLower();
            str = Regex.Replace(str, @"[^a-z0-9\s-]", ""); // - dışındaki karakterler
            str = Regex.Replace(str, @"\s+", " ").Trim(); // bir karakter uzunluğundan daha fazla uzunluktaki boşluklar
            str = Regex.Replace(str, @"\s", "-"); // boşlukları tireye
            return str;
        }
    }
}