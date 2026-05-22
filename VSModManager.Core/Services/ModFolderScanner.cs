using VSModManager.Core.Models;

namespace VSModManager.Core.Services
{
    public class ModFolderScanner : IModFolderScanner
    {
        public async Task<IReadOnlyList<InstalledMod>> ScanAsync(string modsFolderPath, CancellationToken ct)
        {
            List<InstalledMod> installedMods = new();
            ModInfoReader modInfoReader = new();

            foreach(string modPath in Directory.EnumerateFiles(modsFolderPath, "*.zip"))
            {
                ModInfo? modInfo = await modInfoReader.ReadFromZipAsync(modPath, ct);

                if(modInfo != null)
                {
                    InstalledMod installedMod = new InstalledMod(modInfo, modPath);
                    installedMods.Add(installedMod);
                }
            }

            return installedMods;
        }
    }
}
