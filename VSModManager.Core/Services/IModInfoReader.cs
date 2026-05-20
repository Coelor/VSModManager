using VSModManager.Core.Models;

namespace VSModManager.Core.Services
{
    public interface IModInfoReader
    {
        Task<ModInfo?> ReadFromFolderAsync(string folderPath, CancellationToken ct = default);
    }
}
