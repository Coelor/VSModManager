using VSModManager.Core.Models;

namespace VSModManager.Core.Services
{
    public interface IModInfoReader
    {
        Task<ModInfo?> ReadFromFolderAsync(string folderPath, CancellationToken ct = default);
        Task<ModInfo?> ReadFromZipAsync(string zipPath, CancellationToken ct = default);
        Task<ModInfo?> ReadAsync(string path, CancellationToken ct = default);
    }
}
