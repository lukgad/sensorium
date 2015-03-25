# Introduction #

The text config file is a placeholder with only the most basic functionality until I get around to writing an xml plugin.


# Details #
A default text file will be generated - `settings\TextFileSettings\config.ini` (under the directory the main executable is in) - when you run Sensorium2 for the second time (after log4net generates its own config). It will look like this:

```
Plugin|TextFileSettings
	Enabled|True
EndPlugin

Plugin|libSensorsPlugin
	Enabled|False
EndPlugin

Plugin|SpeedFan
	Enabled|True
EndPlugin

Plugin|UDP Plugin
	Enabled|True
EndPlugin

Plugin|Console Control Plugin
EndPlugin
```

The Plugin/EndPlugin tags encapsulate a specific plugin's settings. A pipe - "|" - is used to separate tag names from the data they contain. For example, "Plugin|SpeedFan" means that the following settings are applicable to the SpeedFan plugin (until the next "EndPlugin").

## Plugin-specific settings ##
Most plugins support the "Enabled" setting, setting this to "false" will disable the plugin. All comm plugins also support the "Mode" setting, which can be either "client" or "server".

Currently, only the UDP plugin has customisable settings beyond these

### UDP Plugin ###
|**Name**|**Possible values**|**Example**|
|:-------|:------------------|:----------|
|Server|IP address and port to bind to. Space separated. Can be used multiple times.|`Server|0.0.0.0 12345`<br><code>Server|192.168.1.2 8924</code>
<tr><td>Client</td><td>IP address and port to get data from. Space separated. Can be used multiple times. Will work regardless of Mode</td><td><code>Client|127.0.0.1 12345</code><br><code>Client|192.168.1.2 8924</code></td></tr>