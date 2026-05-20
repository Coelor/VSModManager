using System;
using System.Collections.Generic;
using System.Text;

namespace VSModManager.Core.Models
{
    public record ModInfo
    {
        public required string Type { get; init; }
        public required string Name { get; init; }        
        public required string Version { get; init; }

        public string? ModId { get; init; }    
        public string? Description { get; init; }        
        public IEnumerable<string>? Authors { get; init; }
        public IEnumerable<string>? Contributors { get; init; }
        public string? Website { get; init; }
        public EnumAppSide? Side { get; init; }
        public bool? RequiredOnClient { get; init; }
        public bool? RequiredOnServer { get; init; }
        public IEnumerable<ModDependency>? Dependencies { get; init; }

    }

    public record ModDependency
    {
        public required string ModId { get; init; }
        public required string Version { get; init; }
    }

    public enum EnumAppSide
    {
        Client = 2,
        Server = 1,
        Universal = Server | Client
    }
}
