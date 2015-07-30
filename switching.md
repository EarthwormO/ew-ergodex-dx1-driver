# Application Switch Detection #

The DX1Utility has the ability to detect when another application has the focus of Windows.  It can then compare the Path of the executable that has focus against the Profile List in the DX1Utility and automatically apply the Profile's keymap.

# Run as Administrator #

~~For the Application Switch detection to operate, you must be running the DX1Utility as an Administrator.~~

This does not appear to be needed at Windows 7.  I am running Windows 7 Ultimate x64 with Default UAC settings and DX1Utility correctly identifies Application Switching.  I need more details from user experience to confirm this.  Please see [Issue 9](https://code.google.com/p/ew-ergodex-dx1-driver/issues/detail?id=9) to post your experience.

# Finding the Executable #

Some applications can be confusing as to the actual executable that gains focus when running.  If you are having problems getting the right executable assigned to the profile, check the "Debug" box in the upper right of the DX1Utility Window.

While Debug is checked, the DX1Utility will log all application Event switches it detects to Dx1Degug.txt in the My Documents\DX1Profiles folder.  You can then switch to the application you are having problems with, then switch back and review the log file.  It should contain the executable path you need to specify in the Profile.

# Issues with x64 apps #

The DX1Utility is currently coded to run as an x85 exe even on x64 OSes, this is causing a problem in detecting the Path of native x64 executables.  When changing the DX1Utility to run as a Native x64 app, it looses communication with the DX1 pad due to the interface coding.  Unfortunately at this time, this is a much larger project than I can dedicate time to, and there are not many native x64 applications out right now.  It is on the list as [Issue 16](https://code.google.com/p/ew-ergodex-dx1-driver/issues/detail?id=16) and will be dealt with, but not right now.  Please let me know if this is becoming a significant problem with your use of the DX1.