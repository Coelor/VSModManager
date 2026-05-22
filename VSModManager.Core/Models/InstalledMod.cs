namespace VSModManager.Core.Models
{
    public record InstalledMod : ModInfo
    {
        public required string ZipPath { get; init; }

        //public bool? active { get; set; }
    }
}
