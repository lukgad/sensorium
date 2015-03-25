# Requirements #

## Windows ##

You will need:
  * Windows XP or later.
  * .NET framework 3.5

If you want to access local sensors, you will also need [SpeedFan](http://www.almico.com/sfdownload.php), which must be running whenever you run Sensorium2. If you forget, the plugin will disable itself and you'll have to enable it in the [config file](TextConfigFile.md).

## Linux ##

You will need:
  * mono 2.0

If you want to access local sensors you will also need [lm-sensors](http://www.lm-sensors.org/) v3.x and a kernel that supports it. If you don't need this, you will need to disable the libsensors plugin in the [config file](TextConfigFile.md) (or add -c on the command line, then re-enable the plugins you want)