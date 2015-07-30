# Introduction #

Please follow these instructions to install the new 32bit or 64bit drivers for your DX1 on Windows Vista or Windows 7, or Windows 8.

# Windows 8 #
If you are using Vista or Windows 7, skip to the Details section below.  _Instructions for Windows 8 installation based off instructions from here: http://www.everything-microsoft.com/2012/08/29/install-unsigned-drivers-windows-8/_

  * Sign into Windows 8
  * Press the Windows Key + i
  * Click the Power icon, while holding Shift select Restart
  * This should bring up a troubleshooting page
  * Select the Startup Settings
  * Select the option "Disable driver signature enforcement"
  * After the system reboots, continue on with the steps below.

# Details #

Driver installation instructions are based off of the original install instructions from Rob Povey.

  * Start up Device Manager
  * Uninstall any previous DX1 driver and delete the drivers from the system to prevent Windows from attempting to re-install them.
  * Scan for new hardware, and you should see the DX1 Pad listed in "Other Devices"
  * Right-Click the DX1 Pad entry and select "Update Driver Software..."
  * Select "Browse my computer for driver software"
  * Click the Browse button and navigate to the Driver directory from the download (Select the proper 32/64 folder)
  * Select Next
  * Select "Install this driver software anyway" if prompted.
  * Click close when the complete dialog comes up.
  * You should now see the DX1 listed under the "Sound, video and game controllers" as "Robs DX1 Driver based on UMDF...."