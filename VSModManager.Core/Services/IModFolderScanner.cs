using VSModManager.Core.Models;

namespace VSModManager.Core.Services
{
    public interface IModFolderScanner
    {
        Task<IReadOnlyList<InstalledMod?>> ScanAsync(string modsFolderPath, CancellationToken ct = default);
    }
}
