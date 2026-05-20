using VSModManager.Core.Services;

namespace VSModManager.Core.Tests
{
    public class ModInfoReaderTests : IDisposable
    {
        DirectoryInfo? _tempDir;

        [Fact]
        public async Task ReadFromFolderAsync_ValidJson_ReturnsCorrectModInfo()
        {
            _tempDir = Directory.CreateTempSubdirectory(prefix: "VSModManager");

            string filePath = Path.Combine(_tempDir.FullName, "modinfo.json");

            File.WriteAllText(filePath, """
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
                """);

            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken ct = source.Token;

            var reader = new ModInfoReader();
            var result = await reader.ReadFromFolderAsync(folderPath: _tempDir.FullName, ct: ct);

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


        }

        public void Dispose()
        {
            _tempDir?.Delete(recursive: true);
        }
    }
}