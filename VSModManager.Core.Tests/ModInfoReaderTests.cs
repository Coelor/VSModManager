using VSModManager.Core.Services;
using VSModManager.Core.Models;
using Xunit;
using System.IO;
using System.Text.Json;
using System.IO.Compression;

namespace VSModManager.Core.Tests
{
    public class ModInfoReaderTests : IDisposable
    {
        DirectoryInfo? _tempDir;

        [Fact]
        public async Task ReadFromFolderAsync_NullFolder()
        {
            _tempDir = Directory.CreateTempSubdirectory(prefix: "VSModManager");

            string filePath = Path.Combine(_tempDir.ToString(), "modinfo.json");

            File.WriteAllText(filePath, """
                {                
                    "name": "Test Mod",
                    "version": "1.0.0",
                    "author": "Test Author",
                    "description": "A test mod for unit testing."
                }
                """);

            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken ct = source.Token;

            var reader = new ModInfoReader();
            var result = await reader.ReadFromFolderAsync(folderPath: _tempDir.ToString(), ct: ct);

            Assert.NotNull(result);
            Assert.Equal("Test Mod", result.Name);
            Assert.Equal("1.0.0", result.Version);

        }

        public void Dispose()
        {
            _tempDir?.Delete(recursive: true);
        }
    }
}