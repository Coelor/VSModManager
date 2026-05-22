using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;
using VSModManager.Core.Services;

namespace VSModManager.Core.Tests
{
    public class ModFolderScannerTests
    {
        private readonly string validJson = """
                {  
                    "type": "code",
                    "name": "Test Mod",
                    "version": "1.0.0",
                    "modid": "test-mod",
                    "authors": ["Test Author"],
                    "description": "A test mod for unit testing.",
                    "side": "universal",
                    "requiredOnClient": true,
                    "requiredOnServer": true,
                    "dependencies": { "game": "1.22.2" },
                    "website": "https://github.com/Coelor/vsmodmanager",
                    "iconpath": "modicon.png"
                }
                """;


        [Fact]
        public async Task ScanAsync_ValidJson_2ZippedMods_ReturnsCorrectModInfo()
        {
            string tempPath = Directory.CreateTempSubdirectory().FullName;
            string tempZipPath = Path.Combine(tempPath, Path.GetRandomFileName() + ".zip");

            try
            {
                using (FileStream zipFile = File.Create(tempZipPath))
                using (ZipArchive archive = new ZipArchive(zipFile, ZipArchiveMode.Create))
                {
                    ZipArchiveEntry entry = archive.CreateEntry("modinfo.json");
                    await using (StreamWriter writer = new StreamWriter(entry.Open()))
                    {
                        await writer.WriteAsync(validJson);
                    }
                }

                var scanner = new ModFolderScanner();
                var result = await scanner.ScanAsync(modsFolderPath: tempPath, ct: CancellationToken.None);

                Assert.NotNull(result);
                Assert.Equal(1, result?.Count);
                Assert.Equal("Test Mod", result[0]?.Name);
                Assert.Equal("1.0.0", result[0]?.Version);
                Assert.Equal("code", result[0]?.Type);
                Assert.Equal("test-mod", result[0]?.ModId);
                Assert.Equal(["Test Author"], result[0]?.Authors);
                Assert.Equal("A test mod for unit testing.", result[0]?.Description);
                Assert.Equal("universal", result[0]?.Side);
                Assert.True(result[0]?.RequiredOnClient);
                Assert.True(result[0]?.RequiredOnServer);
                Assert.NotNull(result[0] ?.Dependencies);
                Assert.Equal("1.22.2", result[0] ?.Dependencies?["game"]);
                Assert.Equal("https://github.com/Coelor/vsmodmanager", result[0]?.Website);
                Assert.Equal("modicon.png", result[0]?.IconPath);
            }

            finally
            {
                if(Directory.Exists(tempPath)) {
                {
                    Directory.Delete(tempPath, recursive: true);
                }
            }
        }
    }
}
