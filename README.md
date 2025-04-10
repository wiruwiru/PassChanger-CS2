# PassChanger

This plugin allows server administrators to set a password for the server from a command, without the need to access or edit the `server.cfg` file manually.

---

## Installation

1. Install [CounterStrike Sharp](https://github.com/roflmuffin/CounterStrikeSharp) and [Metamod:Source](https://www.sourcemm.net/downloads.php/?branch=master).

2. Download [PassChanger.zip](https://github.com/wiruwiru/PassChanger-CS2/releases) from the releases section.

3. Unzip the archive and upload it to your CS2 game server.

4. Start the server. The plugin will be loaded and ready to use.

---

## Configuration

The `PassChanger.json` configuration file will be automatically generated when the plugin is first loaded. Below are the available configuration options:

| Parameter      | Description                                                                                              |
|---------------|----------------------------------------------------------------------------------------------------------|
| `PermissionFlag` | The permission flag required to use the command. Default is `@css/generic`. |
| `AllowPasswordRemoval` | If enabled (`true`), allows removing the password. Default is `true`. |
| `PasswordApplyDelay` | The delay in seconds before applying the password. Default is `2.0`. |
| `EnableDebug` | If enabled (`true`), it will display debug messages in the server console.                               |

---

## Usage

Use the following command in the server console or in-game (as admin) to set the server password:

```css
css_changepass [Password]
```

This will immediately apply the password to the server without the need to execute commands in the console or manually edit the server.cfg.

---