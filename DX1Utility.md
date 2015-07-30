# DX1Utility #

The DX1Utility is the interface to the DX1 [Driver](Driver.md) and allows you to customize what each key on the DX1 will do when depressed.

At the current version, these [Keymap](Keymap.md)s are profile based, meaning you can have multiple different assignments per DX1 key depending on what Profile is currently active.  These profiles can be manually selected, selected from a Quick Menu, or selected automatically by using the [Application switch detection](switching.md).

_If you have previously created Keymaps for Rob's DX1Utility see [Updating from Rob's Utility](UpdateFromRob.md) for instructions on how to not have to recreate those Keymaps._

# Create a Profile #
You must first create a profile for the keymap to be assigned to.
  * Click the Combobox Pulldown for Profiles
  * Select "(Create New Profile)"
![http://ew-ergodex-dx1-driver.googlecode.com/files/Profiles14.jpg](http://ew-ergodex-dx1-driver.googlecode.com/files/Profiles14.jpg)
  * The New profile Dialog will appear
http://ew-ergodex-dx1-driver.googlecode.com/files/NewProfile14.JPG
  * Name the Profile (New is not allowed as a Profile name)
  * If you want this profile to be automatically selected using the Application Switch detection, click the "..." button to select the path to the executable you want this profile associated with.
  * "Profile Enabled" allows the profile to be Switched to through Application Switch Detection and allows it to showup in the Quick Menu if that option is also checked.
  * Select "Show in Quick Menu" if you want to be able to manually select this profile from the Right-Click [Quick Menu](QuickMenu.md).
  * By default the new profile will have whatever keys programmed that the Currently Selected profile had before you opened the new Profile window.  Selecting "Create Blank Profile" will create the new Profile with all keys Unassigned.
  * Click OK when you are done naming the profile and selecting other options.

# Simple Programming #

Once a Profile has been created, you can program the DX1 Keys.  To assign a single keyboard key to a single DX1 key follow these steps:
  1. Press the large "Quick Program" button in the center of the application
  1. It will change to "Programming"
  1. Press the DX1 key you wish to assign a keystroke to
  1. Then press the keyboard key you want programmed.
  1. Notice the Keymap list on the left updates to show the key is now programmed.
  1. The description will be the Key name Windows gives the specific key
  1. Repeat from step 3 for each DX1 key you wish to program
  1. If you wish to change the description of the key, Right-Click on the key list and select Properties, Once you have edited the description click Finish.

# Macro Assignment #

You can assign a timed Macro to keys by selecting the macro in the right-hand pane instead of pressing a key on the keyboard.  Doing so will show the name of the macro following the key, instead of the HID Key Binding.

See [Editing Macros](Macros.md) for instructions on creating and editing timed macros.