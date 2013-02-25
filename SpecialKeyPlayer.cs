using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace DX1Utility
{
    public class SpecialKeyPlayer
    {
        const uint MOUSEEVENTF_MOVE = 0x0001;
        const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        const uint MOUSEEVENTF_LEFTUP = 0x0004;
        const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        const uint MOUSEEVENTF_RIGHTUP = 0x0010;
        const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        const uint MOUSEEVENTF_MIDDLEUP = 0x0040;
        const uint MOUSEEVENTF_XDOWN = 0x0080;
        const uint MOUSEEVENTF_XUP = 0x0100;
        const uint MOUSEEVENTF_VWHEEL = 0x0800;
        const uint MOUSEEVENTF_HWHEEL = 0x1000;

        
        private List<SpecialKey> _SpecialKeys = new List<SpecialKey>();

        
        public List<SpecialKey> SpecialKeys
        {
            get { return _SpecialKeys; }
            set { _SpecialKeys = value; }
        }

        public void InitPlayer()
        {
            //Initializes the Special Keys and Ability to play the keys
            //Since Special Keys are a hard-Coded List, add Each Special Key
            SpecialKey TempSpecial = new SpecialKey();
            
            //SpecialKey[0] = Left Mouse Button
            TempSpecial.SpecialID = 0;
            TempSpecial.SpecialName = "Left Mouse Button";
            _SpecialKeys.Add(TempSpecial);

            TempSpecial = new SpecialKey();
            //SpecialKey[1] = Right Mouse Button
            TempSpecial.SpecialID = 1;
            TempSpecial.SpecialName = "Right Mouse Button";
            _SpecialKeys.Add(TempSpecial);

            TempSpecial = new SpecialKey();
            //SpecialKey[2] = Middle Mouse Button
            TempSpecial.SpecialID = 2;
            TempSpecial.SpecialName = "Middle Mouse Button";
            _SpecialKeys.Add(TempSpecial);

            TempSpecial = new SpecialKey();
            //SpecialKey[3] = Scroll Wheel
            TempSpecial.SpecialID = 3;
            TempSpecial.SpecialName = "Mouse Vertical Scroll";
            TempSpecial.ReqData = true;
            TempSpecial.ExtraDataType = SpecialKey.SpecialKeysExtraData.Boolean;
            TempSpecial.ExtraDataParams.Add("True", "Up");
            TempSpecial.ExtraDataParams.Add("False", "Down");
            _SpecialKeys.Add(TempSpecial);

            TempSpecial = new SpecialKey();
            //SpecialKey[3] = Scroll Wheel
            TempSpecial.SpecialID = 4;
            TempSpecial.SpecialName = "Mouse Horizontal Scroll";
            TempSpecial.ReqData = true;
            TempSpecial.ExtraDataType = SpecialKey.SpecialKeysExtraData.Boolean;
            TempSpecial.ExtraDataParams.Add("True", "Right");
            TempSpecial.ExtraDataParams.Add("False", "Left");
            _SpecialKeys.Add(TempSpecial);
        }

        public string GetCustomData(int SpecialID, string InputData)
        {
            //Turn the input Data into a CustomData String that the Player understands later for this specific Special Key
            string TempString = "";

            switch (_SpecialKeys[SpecialID].ExtraDataType)
            {
                case SpecialKey.SpecialKeysExtraData.Boolean:
                    {
                        if (InputData == "True")
                        {
                            TempString = "1";
                        }
                        else
                        {
                            TempString = "0";
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            return TempString;
        }


        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);


        public void KeyDown(KeyMap CurrentKey, string ExtraData = "")
        {
            //A special key has been pressed
            switch (CurrentKey.Action)
            {
                case 0:
                    {
                        //Left Mouse Button
                        //Old Code - Mostly Worked
                        //mouse_event(0x02, 0, 0, 0, 0);
                        INPUT input_down = new INPUT();
                        input_down.mi.dx = 0;
                        input_down.mi.dy = 0;
                        input_down.mi.mouseData = 0;
                        input_down.mi.dwFlags = (int)MOUSEEVENTF_LEFTDOWN;

                        INPUT[] input = { input_down };

                        SendInput(1, input, Marshal.SizeOf(input_down));
                        break;
                    }
                case 1:
                    {
                        //Right Mouse Button
                        INPUT input_down = new INPUT();
                        input_down.mi.dx = 0;
                        input_down.mi.dy = 0;
                        input_down.mi.mouseData = 0;
                        input_down.mi.dwFlags = (int)MOUSEEVENTF_RIGHTDOWN;

                        INPUT[] input = { input_down };

                        SendInput(1, input, Marshal.SizeOf(input_down));
                        break;
                    }
                case 2:
                    {
                        //Middle Mouse Button
                        INPUT input_down = new INPUT();
                        input_down.mi.dx = 0;
                        input_down.mi.dy = 0;
                        input_down.mi.mouseData = 0;
                        input_down.mi.dwFlags = (int)MOUSEEVENTF_MIDDLEDOWN;

                        INPUT[] input = { input_down };

                        SendInput(1, input, Marshal.SizeOf(input_down));
                        break;
                    }
                case 3:
                    {
                        //Mouse Vert Scroll
                        INPUT input_down = new INPUT();
                        input_down.mi.dx = 0;
                        input_down.mi.dy = 0;

                        //Check to see if scrolling Up or down
                        if (CurrentKey.CustomData == "1")
                            input_down.mi.mouseData = 100;
                        else
                            input_down.mi.mouseData = -100;

                        input_down.mi.dwFlags = (int)MOUSEEVENTF_VWHEEL;

                        INPUT[] input = { input_down };

                        SendInput(1, input, Marshal.SizeOf(input_down));
                        break;
                    }

                case 4:
                    {
                        //Mouse Horz Scroll
                        INPUT input_down = new INPUT();
                        input_down.mi.dx = 0;
                        input_down.mi.dy = 0;

                        //Check to see if scrolling Up or down
                        if (CurrentKey.CustomData == "1")
                            input_down.mi.mouseData = 100;
                        else
                            input_down.mi.mouseData = -100;

                        input_down.mi.dwFlags = (int)MOUSEEVENTF_HWHEEL;

                        INPUT[] input = { input_down };

                        SendInput(1, input, Marshal.SizeOf(input_down));
                        break;
                    }

                default:
                    {
                        break;
                    }
            }
        }

        public void KeyUp(KeyMap CurrentKey, string ExtraData = "")
        {
            //A special key has been released
            switch (CurrentKey.Action)
            {
                case 0:
                    {
                        //Left Mouse Button
                        //mouse_event(0x04, 0, 0, 0, 0);
                        INPUT input_up = new INPUT();
                        input_up.mi.dx = 0;
                        input_up.mi.dy = 0;
                        input_up.mi.mouseData = 0;
                        input_up.mi.dwFlags = (int)MOUSEEVENTF_LEFTUP;

                        INPUT[] input = { input_up };

                        SendInput(1, input, Marshal.SizeOf(input_up));
                        break;
                    }
                case 1:
                    {
                        //Right Mouse Button
                        INPUT input_up = new INPUT();
                        input_up.mi.dx = 0;
                        input_up.mi.dy = 0;
                        input_up.mi.mouseData = 0;
                        input_up.mi.dwFlags = (int)MOUSEEVENTF_RIGHTUP;

                        INPUT[] input = { input_up };

                        SendInput(1, input, Marshal.SizeOf(input_up));
                        break;
                    }
                case 2:
                    {
                        //Middle Mouse Button
                        INPUT input_up = new INPUT();
                        input_up.mi.dx = 0;
                        input_up.mi.dy = 0;
                        input_up.mi.mouseData = 0;
                        input_up.mi.dwFlags = (int)MOUSEEVENTF_MIDDLEUP;

                        INPUT[] input = { input_up };

                        SendInput(1, input, Marshal.SizeOf(input_up));
                        break;
                    }

                default:
                    {
                        break;
                    }
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public int mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct HARDWAREINPUT
        {
            uint uMsg;
            ushort wParamL;
            ushort wParamH;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct INPUT
        {
            [FieldOffset(0)]
            public int type;
            [FieldOffset(4)] //*
            public MOUSEINPUT mi;
            [FieldOffset(4)] //*
            public KEYBDINPUT ki;
            [FieldOffset(4)] //*
            HARDWAREINPUT hi;
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint SendInput(uint nInputs,
        INPUT[] pInputs,
        int cbSize);

    }
}
