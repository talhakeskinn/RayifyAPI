namespace Rayify.Application.Abstractions.Storage.Main
{
    public interface IStorageService : IStorage
    {
        public string StorageName { get; }
    }
}
