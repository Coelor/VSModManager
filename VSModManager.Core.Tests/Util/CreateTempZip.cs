using System.IO.Compression;

namespace VSModManager.Core.Tests.Util
{
    public class CreateTempZip(string tempPath)
    {
        public string TempPath { get; private set; } = tempPath;

        public async Task<string> Create(string json)
        {
            string tempZipPath = Path.Combine(TempPath, Path.GetRandomFileName() + ".zip");

            using (FileStream zipFile = File.Create(tempZipPath))
            using (ZipArchive archive = new ZipArchive(zipFile, ZipArchiveMode.Create))
            {
                ZipArchiveEntry entry = archive.CreateEntry("modinfo.json");
                await using (StreamWriter writer = new StreamWriter(entry.Open()))
                {
                    await writer.WriteAsync(json);
                }
            }

            return TempPath;
        }

        public void Delete()
        {
            if (Directory.Exists(TempPath))
            {
                {
                    Directory.Delete(TempPath, recursive: true);
                }
            }
        }
    }
}
