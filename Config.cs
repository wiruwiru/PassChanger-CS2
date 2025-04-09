using CounterStrikeSharp.API.Core;
using System.Text.Json.Serialization;

namespace PassChanger;

public class BaseConfigs : BasePluginConfig
{

    [JsonPropertyName("PermissionFlag")]
    public string PermissionFlag { get; set; } = "@css/generic";

    [JsonPropertyName("EnableDebug")]
    public bool EnableDebug { get; set; } = false;
    
}