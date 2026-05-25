using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace VSModManager.Core.Tests.Util
{
    public class ValidJson(string type, string name, string version, string? modid, List<string>? authors, string? description, string? side, bool? requiredOnClient, bool? requiredOnServer, Dictionary<string, string>? dependencies, string? website, string? iconpath)
    {
        public string Json { get; private set; } = $$"""
                {
                    "type":"{{type}}",
                    "name": "{{name}}",
                    "version": "{{version}}",
                    "modid": "{{modid}}",
                    "authors": {{authors}},
                    "description": {{description}},
                    "side": {{side}},
                    "requiredOnClient": {{requiredOnClient}},
                    "requiredOnServer": {{requiredOnServer}},
                    "dependencies": {{dependencies}},
                    "website": {{website}},
                    "iconpath": {{iconpath}}
                }
                """;
    }
}
