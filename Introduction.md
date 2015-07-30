# Introduction #

Due to no forthcoming Vista or above or 64bit drivers for the Ergodex DX1, some people have created their own versions of the drivers.  The best Windows based driver I have found was at: http://polygonalhell.blogspot.com/2009/01/new-32-and-64-bit-ergodex-dx1-drivers.html

This driver worked great, but the interface to recording new keymaps was a bit rough, and with his permission I have started smoothing the interface application a bit further.  This is my attempt to make the driver more user friendly.


# Details #

New features added at Version 1
  * Made some minor enhancements to [App switching detection](switching.md), should be a bit smoother for 64bit systems.
  * Moved all Keymap files (.pgm) to a central location (My Documents\DX1Profiles)
  * Created Profiles for keymap detail for easier understanding
  * Added [Right-Click menu](QuickMenu.md) to the NotifyIcon to quickly select profiles, and added Exit option
  * Began process of splitting App switch detection code from user interface.  Will completely switch to 2 executables at a later version.
  * Now displays all keys in list instead of only programmed keys, making it easier to spot un-used keys.

I am using the Issues tracking to track and display my own features I will be working towards in future versions.