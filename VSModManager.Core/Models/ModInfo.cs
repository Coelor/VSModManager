namespace VSModManager.Core.Models
{
    public record ModInfo
    {
        public required string Type { get; init; }
        public required string Name { get; init; }        
        public required string Version { get; init; }

        public string? ModId { get; init; } 
        public string? Description { get; init; }        
        public List<string>? Authors { get; init; }
        public List<string>? Contributors { get; init; }
        public string? Website { get; init; }
        public string? Side { get; init; }
        public bool? RequiredOnClient { get; init; }
        public bool? RequiredOnServer { get; init; }
        public Dictionary<string, string>? Dependencies { get; init; } // ModId -> Version

    }
}
