using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Admin;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Cvars;
using CounterStrikeSharp.API.Modules.Timers;
using System.Text.Json;

namespace PassChanger;

public class PasswordData
{
	public string Password { get; set; } = "";
}

[MinimumApiVersion(300)]
public class PassChanger : BasePlugin, IPluginConfig<BaseConfigs>
{
	public override string ModuleName => "PassChanger";
	public override string ModuleVersion => "1.0.1";
	public override string ModuleAuthor => "luca.uy";
	public override string ModuleDescription => "Allows server administrators to set a password for the server from a command, without the need to access server.cfg";

	private string _configPath => Path.Combine(ModuleDirectory, "password_data.json");

	public required BaseConfigs Config { get; set; }

	public void OnConfigParsed(BaseConfigs config)
	{
		Config = config;
		Utils.Config = config;
	}

	public override void Load(bool hotReload)
	{
		if (hotReload)
		{
			Utils.DebugMessage("Reloading plugin...");
		}

		if (!File.Exists(_configPath))
		{
			var currentPassword = ConVar.Find("sv_password")?.StringValue ?? "";
			SavePassword(currentPassword);

			if (!string.IsNullOrEmpty(currentPassword))
			{
				Utils.DebugMessage("Created password_data.json with current server password");
			}
			else
			{
				Utils.DebugMessage("Created empty password_data.json (no server password set)");
			}
		}

		RegisterListener<Listeners.OnMapStart>(OnMapStart);
	}

	private void OnMapStart(string mapName)
	{
		float delaySeconds = Config?.PasswordApplyDelay ?? 3.0f;

		AddTimer(delaySeconds, () =>
		{
			var password = LoadPassword();
			if (!string.IsNullOrEmpty(password) || (Config != null && Config.AllowPasswordRemoval))
			{
				Server.ExecuteCommand(string.IsNullOrEmpty(password)
					? "sv_password \"\""
					: $"sv_password \"{password}\"");

				Utils.DebugMessage($"Password applied after {delaySeconds} seconds delay");

				AddTimer(1.0f, () =>
				{
					var currentPass = ConVar.Find("sv_password")?.StringValue;
					if (currentPass != password)
					{
						Utils.DebugMessage("Warning: Password was overwritten, reapplying...");
						Server.ExecuteCommand(string.IsNullOrEmpty(password)
							? "sv_password \"\""
							: $"sv_password \"{password}\"");
					}
				});
			}
		}, TimerFlags.STOP_ON_MAPCHANGE);
	}

	[ConsoleCommand("css_changepass", "Allows to change the server password")]
	[CommandHelper(minArgs: 1, usage: "[Password]", whoCanExecute: CommandUsage.CLIENT_AND_SERVER)]
	public void OnChangePasswordCommand(CCSPlayerController? player, CommandInfo commandInfo)
	{
		if (player != null && !AdminManager.PlayerHasPermissions(player, Config.PermissionFlag))
		{
			string noPermissionResponse = $"{Localizer["prefix"]} {Localizer["no.permission"]}";
			player.PrintToChat(noPermissionResponse);
			return;
		}

		var password = commandInfo.GetArg(1);

		if (string.IsNullOrEmpty(password) && !Config.AllowPasswordRemoval)
		{
			string errorResponse = $"{Localizer["prefix"]} {Localizer["password.removal.disabled"]}";
			if (player != null)
			{
				player.PrintToChat(errorResponse);
			}
			return;
		}

		SavePassword(password);

		if (string.IsNullOrEmpty(password))
		{
			Server.ExecuteCommand("sv_password \"\"");
		}
		else
		{
			Server.ExecuteCommand($"sv_password \"{password}\"");
		}

		string passwordResponse = string.IsNullOrEmpty(password) ? $"{Localizer["prefix"]} {Localizer["password.removed"]}" : $"{Localizer["prefix"]} {Localizer["password.changed", password]}";

		if (player != null)
		{
			player.PrintToChat(passwordResponse);
		}
	}

	private void SavePassword(string password)
	{
		try
		{
			var data = new PasswordData { Password = password };
			var json = JsonSerializer.Serialize(data);
			File.WriteAllText(_configPath, json);
		}
		catch (Exception ex)
		{
			Utils.DebugMessage($"Error saving password: {ex.Message}");
		}
	}

	private string LoadPassword()
	{
		try
		{
			if (File.Exists(_configPath))
			{
				var json = File.ReadAllText(_configPath);
				var data = JsonSerializer.Deserialize<PasswordData>(json);
				return data?.Password ?? "";
			}
			return "";
		}
		catch (Exception ex)
		{
			Utils.DebugMessage($"Error loading password: {ex.Message}");
			return "";
		}
	}

}