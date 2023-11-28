using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Rayify.Infrastructure.Helpers
{
    public class RenameHelper
    {
        public static string Rename(IFormFile file)
        {
            Regex regex = new("[*'\",+-._&#^@|/<>~]");
            string regexedName = regex.Replace(Path.GetFileNameWithoutExtension(file.FileName), string.Empty);

            DateTime stamp = DateTime.UtcNow;
            return $"{regexedName}-{stamp.ToString("yyyyMMddHHmmssff")}{Path.GetExtension(file.FileName)}";

        }
    }
}
