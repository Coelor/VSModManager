using VSModManager.Core.Models;

namespace VSModManager.Core.Services
{
    public class ModFolderScanner : IModFolderScanner
    {
        public async Task<IReadOnlyList<InstalledMod?>> ScanAsync(string modsFolderPath, CancellationToken ct)
        {
            List<InstalledMod> installedMods = new();
            ModInfoReader modInfoReader = new();

            foreach(string mod in Directory.EnumerateFiles(modsFolderPath, ".zip"))
            {
                InstalledMod? installedMod = (InstalledMod?)await modInfoReader.ReadFromZipAsync(mod, ct);

                if(installedMod != null)
                    installedMods.Add(installedMod);
            }

            return installedMods;
        }
    }
}
