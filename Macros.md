# Timed Macros #

Timed Macros are a series of keystrokes to be performed when you press a DX1 key.  Instead of sending a single keystroke as a Keyboard input, a Timed Macro will call the DX1Utility to playback the macro you specified.


# Create a Macro #

To create a new Timed Macro, select the "New Macro" entry from the Right-Hand list and select the Edit Macro button.  This will open the MacroEditor.

http://ew-ergodex-dx1-driver.googlecode.com/files/MacroEditor.JPG

Give the Macro a name

The List ont he left hand side of the window is a list of timed events.  To insert a new event click int he box and press the "Insert" key on the keyboard.

You will see the following added to the list.

http://ew-ergodex-dx1-driver.googlecode.com/files/MacroEditor-Insert.JPG

Double-Click the new entry to edit it.  The first number is the milliseconds after the start of the macro for the action to take place.  The second is the action (KeyDown or KeyUp).  The Last is the Windows virtual key code to submit.

You can delete an entry by selecting it in the list on the left and pressing the "Delete" key.

Some applications are going to want corresponding key up commands, it is usually OK to have the KeyDown and KeyUp events occur at the same time.

# Use Scancode #
There is also a checkbox for the macro to use Scancodes.  Some applications will not accept the Windows Virtual Key Code and require the Hardware Scancode of the key pressed.  If this is selected, you will need to provide the [Scancodes](Scancodes.md) of the keys instead of the Virtual Key Code.

# Use Mouse Buttons #
At version 1.31 Mouse buttons (Left, Right and Middle) can be added to Timed macros.  _They will not work with Multi-key Macros_

To add a Mouse button, change the KeyDown or KeyUp to "Mouse"
Then for the Keycode use one of the following:

  * Mouse Left Down = 2
  * Mouse Left Up   = 4
  * Mouse Right Down = 8
  * Mouse Right Up = 16
  * Mouse Middle Down = 32
  * Mouse Middle Up = 64

You must have a corresponding button up code for each button down.  Notice that the codes for button down and up are different for the same button.