using VSModManager.Core.Models;
using VSModManager.Core.Services;
using VSModManager.Core.Tests.Util;

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
        public async Task ScanAsync_ValidJson_1ZippedMod_ReturnsCorrectModInfo()
        {
            CreateTempZip tempZipCreater = new(Directory.CreateTempSubdirectory().FullName);

            try
            {
                await tempZipCreater.Create(validJson);

                ModFolderScanner scanner = new ModFolderScanner();
                IReadOnlyList<InstalledMod> result = await scanner.ScanAsync(modsFolderPath: tempZipCreater.TempPath, ct: CancellationToken.None);

                Assert.NotNull(result);
                Assert.Single(result);

                InstalledMod mod = result[0];
                Assert.NotNull(mod);
                Assert.Equal("Test Mod", mod.Info.Name);
                Assert.Equal("1.0.0", mod.Info.Version);
                Assert.Equal("code", mod.Info.Type);
                Assert.Equal("test-mod", mod.Info.ModId);
                Assert.Equal(["Test Author"], mod.Info.Authors);
                Assert.Equal("A test mod for unit testing.", mod.Info.Description);
                Assert.Equal("universal", mod.Info.Side);
                Assert.True(mod.Info.RequiredOnClient);
                Assert.True(mod.Info.RequiredOnServer);
                Assert.NotNull(mod.Info.Dependencies);
                Assert.Equal("1.22.2", mod.Info.Dependencies?["game"]);
                Assert.Equal("https://github.com/Coelor/vsmodmanager", mod.Info.Website);
                Assert.Equal("modicon.png", mod.Info.IconPath);
            }

            finally
            {
                tempZipCreater.Delete();
            }
        }
    }
}
