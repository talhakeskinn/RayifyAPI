namespace Rayify.Application.Abstractions.ConvertVideo
{
    public interface IConvertVideo
    {
        string ConvertToMp3(string inputPath, string outputPath, string musicId);
    }
}
