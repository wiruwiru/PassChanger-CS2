using CounterStrikeSharp.API.Core;
using System.Text.Json.Serialization;

namespace PassChanger;

public class BaseConfigs : BasePluginConfig
{

    [JsonPropertyName("PermissionFlag")]
    public string PermissionFlag { get; set; } = "@css/generic";

    [JsonPropertyName("AllowPasswordRemoval")]
    public bool AllowPasswordRemoval { get; set; } = true;

    [JsonPropertyName("PasswordApplyDelay")]
    public float PasswordApplyDelay { get; set; } = 2.0f;

    [JsonPropertyName("EnableDebug")]
    public bool EnableDebug { get; set; } = false;
    
}