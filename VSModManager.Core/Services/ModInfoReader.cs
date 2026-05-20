using VSModManager.Core.Models;
using System.Text.Json;

namespace VSModManager.Core.Services
{
    public class ModInfoReader : IModInfoReader
    {
        private static readonly JsonSerializerOptions jso = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public async Task<ModInfo?> ReadFromFolderAsync(string folderPath, CancellationToken ct)
        {
            ModInfo? modInfo;

            var jsonPath = Path.Combine(folderPath, "modinfo.json");
            await using (var json = File.OpenRead(jsonPath))
            {
                modInfo = await JsonSerializer.DeserializeAsync<ModInfo>(json, jso, ct);
            }

            return modInfo;
        }
    }
}
