namespace Trouvaille.Server.Common.Contracts
{
    public interface IFileProvider
    {
        string SavePhotoToFileSystem(string filePath, string path);
    }
}
