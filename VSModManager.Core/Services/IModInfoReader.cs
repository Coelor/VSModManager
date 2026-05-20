using VSModManager.Core.Models;

namespace VSModManager.Core.Services
{
    interface IModInfoReader
    {
        Task<ModInfo?> ReadFromFolderAsync(string folderPath, CancellationToken ct = default);
    }
}
