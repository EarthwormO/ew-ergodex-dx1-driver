# 1.51 Update #
  * Fixed Mouse and macro issues with 1.5.0 when running under 64bit introduced with 1.50

# 1.50 Update #
  * Fixed Driver Communication problem when running under 64bit CPU mode.  DX1Utility will no longer run as an x86 process when running on 64bit OS
  * DX1Utility can now detect 64bit Applications properly and switch profiles correctly.


# 1.41 Update #
  * Had recreated the Quick Menu Bug that was fixed in 1.21, fixed it again, now Quick Menu selections will actually apply the keyset right then without having to open and close the Utility.
  * Fixed bug that Right-Clicking on an already programmed Special Key and selecting Properties caused the utility to Crash
  * Fixed Bug in assigning Macros using Quick Program actually assigning the macro to the next Higher DX1 Key, not the key actually pressed


# 1.4 Update #
  * Added Ability to create Blank Profiles, or to Blank a current profile through the profile properties
  * Centralized the code for Selecting a specific Profile, removing redundant code located in 5 different locations
  * Fixed Deleting of a Profile not selecting a valid profile when it was done, and not reassigning the keys properly.  Now deleting a profile will default back to the Global profile.
  * Fixed a problem with attempting to do something else while Quick programming could cause the programmed keys to not be saved, and cause the DX1utility to get stuck in a state where Quick Program would not work again until app was deactivated and activated again.

# 1.32 Update #
  * Added feature that when the DX1Utility is open and you press a DX1 Key, that key will be highlighted, making programming keys easier.
  * Fixed ArgumentOutofRange Exception when in Windows TEST Mode
  * Added new Special Keys for the Media Keys (Play, Stop, Next and Previous Tracks) and Volume keys. (Up, Down and Mute)

# 1.31 Update #
  * Added Mouse buttons to Macros (Left, Middle, Right)  See [Macros](Macros.md) page for instructions on adding Mouse buttons to the Macros.

# 1.3 Update #
  * Added Left, Right and Middle Mouse Clicks and mouse scroll to the [Special Keys](SpecialKeys.md) options
  * Fixed the Right-Click -> Clear so that it updates the grid right away, instead of waiting for a new key to be selected.

# 1.21 Update #
  * The Quick Select menu was not actually applying the new profile's keys correctly, fixed now
  * Right-Clicking and selecting Clear on the new Dataview of the Keys wasn't doing anything.  Also fixed.

# 1.2 Update #

  * Fixed bug that prevented recording the Right Windows Key as a single key.
  * Removed Application Switch detection enhancement as it made communication with DX1 unstable.  Need to re-implement in a new fashion.  Right now, app switch detection works fine as long as the executable is 32bit, if it is native x64, DX1Utility won't detect it properly.
  * [(Global) Profile](GlobalProfile.md) Added.  DX1Utility will default to this profile when a detected app has no associated Profile and a profile has not been manually selected.
  * DX1Utility won't detect itself for App Switching, thus preventing the New Global Profile from continuously overriding manually selected profiles
  * Dx1Utility places the NotifyIcon on Open, and it stays visible at all times now, instead of hiding when opened and unhiding when minimized
  * Changed Minimize button to only minimize, not hide the application from the menu bar
  * Re-enabled Close control box, which minimizes the application and hides it from the Start bar.  To truely close the application, use the right-click menu on the Notifyicon
  * Profiles are now Alphabetically sorted in both the Combobox and the Quick Menu
  * Show in Quick menu check box updates on-screen in Profile edit window properly now.
  * Added Open to Quick Menu to re-open the Dx1Utility (Same as double-clicking on the NotifyIcon itself)
  * Added code to prevent multiple copies of the Dx1utility from running
  * When Selecting (Create new Profile) from the profile ComboBox will now automatically open the New Profile Window
  * Replaced List for programmed keys with DatagridView allowing Descriptions for programmed keys.  This will eventually allow Importing keys between profiles, and knowing what keys are what.  Key codes are no longer shown.
  * Created a [Key Programming Wizard](KeyWizard.md) tool for programming individual keys to Macros, and Single Keys (Future version to incorporate MultiKey, and Special keys)
  * Changed name of Program key to be "Quick Program", and allows pressing a DX1 key then a Keyboard key to program.


# 1.1 Update #

  * [Quick Menu](QuickMenu.md) is now dynamic, does not require closing and reopening app to update Profile List.
  * Deleting a Profile now properly cleans up related Keymap file (.pgm)
  * Unchecking "Profile Enabled" on a Profile will not prevent the profile from being selected by [App Switch detection](switching.md), and will remove it from the Quick menu.
  * This release corresponds to the master code upload.

# 1.0 Initial Release #

Initial Release