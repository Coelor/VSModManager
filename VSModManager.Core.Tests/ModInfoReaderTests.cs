using VSModManager.Core.Services;
using VSModManager.Core.Models;
using Xunit;
using System.IO;
using System.Text.Json;
using System.IO.Compression;

namespace VSModManager.Core.Tests
{
    public class ModInfoReaderTests
    {
        [Fact]
        public void ReadFromFolderAsync_NullFolder()
        {
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken ct = source.Token;

            ModInfoReader reader = new ModInfoReader();

            DirectoryInfo tempDirInfo = Directory.CreateTempSubdirectory(prefix:"VSModManager");
           
            string zipPath = Path.Combine(tempDirInfo.FullName, "test.zip");

            string jsonText = JsonSerializer.Serialize(
                new ModInfo { Type = "code", Version = "0.1.0", Name = "test-mod" }, 
                new JsonSerializerOptions { WriteIndented = true }
            );

            using var archive = ZipFile.Open(zipPath, ZipArchiveMode.Create);
            var zipEntry = archive.CreateEntry("modinfo.json");
            using var writer = new StreamWriter(zipEntry.Open());
            writer.Write(jsonText);

            Task<List<ModInfo?>?> results = reader.ReadFromFolderAsync(tempDirInfo.ToString(), ct:ct);

            results.Start();

            Assert.Single(results);

            tempDirInfo.Delete(recursive: true);
        }
    }
}
