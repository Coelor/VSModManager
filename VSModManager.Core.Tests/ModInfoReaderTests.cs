using System.IO.Compression;
using System.Text.Json;
using VSModManager.Core.Services;

namespace VSModManager.Core.Tests
{
    public class ModInfoReaderTests
    {
        string validJson = """
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

        private static readonly JsonSerializerOptions jso = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, WriteIndented = true };

        [Fact]
        public async Task ReadFromFolderAsync_ValidJson_ReturnsCorrectModInfo()
        {
            DirectoryInfo? _tempDir = Directory.CreateTempSubdirectory(prefix: "VSModManager");

            string filePath = Path.Combine(_tempDir.FullName, "modinfo.json");

            File.WriteAllText(filePath, validJson);

            var reader = new ModInfoReader();
            var result = await reader.ReadFromFolderAsync(folderPath: _tempDir.FullName, ct: CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal("Test Mod", result.Name);
            Assert.Equal("1.0.0", result.Version);
            Assert.Equal("code", result.Type);
            Assert.Equal("test-mod", result.ModId);
            Assert.Equal(["Test Author"], result.Authors);
            Assert.Equal("A test mod for unit testing.", result.Description);
            Assert.Equal("universal", result.Side);
            Assert.Equal(true, result.RequiredOnClient);
            Assert.Equal(true, result.RequiredOnServer);
            Assert.NotNull(result?.Dependencies);
            Assert.Equal("1.22.2", result?.Dependencies?["game"]);
            Assert.Equal("https://github.com/Coelor/vsmodmanager", result.Website);
            Assert.Equal("modicon.png", result.IconPath);

            _tempDir?.Delete(recursive: true);
        }

        [Fact]
        public async Task ReadFromZipAsync_ValidJson_ReturnsCorrectModInfo()
        {
            string tempZipPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".zip");

            try
            {
                await using (FileStream zipStream = new FileStream(tempZipPath, FileMode.Create))
                {
                    await using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
                    {
                        var entry = archive.CreateEntry("modinfo.json");
                        await using (var entryStream = entry.Open())
                        {
                            await JsonSerializer.SerializeAsync(entryStream, validJson, jso);
                        }
                    }
                }
            }

            finally
            {
                var reader = new ModInfoReader();
                var result = await reader.ReadFromZipAsync(zipPath: tempZipPath, ct: CancellationToken.None);

                Assert.NotNull(result);
                Assert.Equal("Test Mod", result.Name);
                Assert.Equal("1.0.0", result.Version);
                Assert.Equal("code", result.Type);
                Assert.Equal("test-mod", result.ModId);
                Assert.Equal(["Test Author"], result.Authors);
                Assert.Equal("A test mod for unit testing.", result.Description);
                Assert.Equal("universal", result.Side);
                Assert.Equal(true, result.RequiredOnClient);
                Assert.Equal(true, result.RequiredOnServer);
                Assert.NotNull(result?.Dependencies);
                Assert.Equal("1.22.2", result?.Dependencies?["game"]);
                Assert.Equal("https://github.com/Coelor/vsmodmanager", result.Website);
                Assert.Equal("modicon.png", result.IconPath);

                if (File.Exists(tempZipPath))
                {
                    File.Delete(tempZipPath);
                }
            }            
        }
    }
}