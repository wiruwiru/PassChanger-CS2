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

| Parameter              | Description                                                                                       | Default              |
|------------------------|---------------------------------------------------------------------------------------------------|----------------------|
| `ChangePassCommand`    | The command used to change the server password.                                                         | `css_changepass`     |
| `ChangePassFlag`       | The permission flag required to use the change password command (**leave empty to allow all users**).   | `@css/generic`       |
| `CurrentPassCommand`   | The command used to display the current server password.                                                | `css_currentpass`    |
| `CurrentPassFlag`      | The permission flag required to use the current password command (**leave empty to allow all users**).  | `@css/generic`       |
| `AllowPasswordRemoval` | If enabled (`true`), allows removing the server password.                                               | `true`               |
| `PasswordApplyDelay`   | The delay in seconds before applying the password.                                                      | `2.0 (recommended)`  |
| `EnableDebug`          | If enabled (`true`), it will display debug messages in the server console.                              | `false`              |

---

## Usage

The plugin provides the following commands by default (these can be changed in the configuration):

1. To change the server password:
```css
css_changepass [Password]
```
2. To remove the password from the server (simply execute the command without any arguments):
```css
css_changepass
```
3. To check the current server password:
```css
css_currentpass
```

Note: The actual commands available on your server depend on your configuration. If you change the command names in the config file, you'll need to use those instead of the default ones.