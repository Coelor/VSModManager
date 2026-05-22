using VSModManager.Core.Models;

namespace VSModManager.Core.Services
{
    public interface IModInfoReader
    {
        Task<ModInfo?> ReadFromZipAsync(string zipPath, CancellationToken ct = default);
    }
}
