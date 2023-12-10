using MediaToolkit;
using MediaToolkit.Model;
using Microsoft.AspNetCore.Hosting;
using Rayify.Application.Abstractions.ConvertVideo;
using Rayify.Domain.Entities;


namespace Rayify.Infrastructure.Services.ConvertVideo
{
    public class ConvertVideo : IConvertVideo
    {
        private IWebHostEnvironment _webHostEnvironment;
        public ConvertVideo(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public string ConvertToMp3(string inputPath, string outputPath, string musicId)
        {
            string outputFileName = $"{outputPath}/{musicId}.mp3";
            var outputFile = new MediaFile(Path.Combine(_webHostEnvironment.WebRootPath, outputFileName));
            var inputFile = new MediaFile(inputPath);

            using (var engine = new Engine())
            {
                engine.Convert(inputFile, outputFile);
            }

            return outputFileName;

        }
    }
}
