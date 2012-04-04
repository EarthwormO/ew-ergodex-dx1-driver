using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace DX1Utility
{
    public class MacroPlayer
    {

        const int INPUT_MOUSE = 0;
        const int INPUT_KEYBOARD = 1;
        const int INPUT_HARDWARE = 2;
        const uint KEYEVENTF_EXTENDEDKEY = 0x0001;
        const uint KEYEVENTF_KEYUP = 0x0002;
        const uint KEYEVENTF_UNICODE = 0x0004;
        const uint KEYEVENTF_SCANCODE = 0x0008;
        const uint XBUTTON1 = 0x0001;
        const uint XBUTTON2 = 0x0002;
        const uint MOUSEEVENTF_MOVE = 0x0001;
        const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        const uint MOUSEEVENTF_LEFTUP = 0x0004;
        const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        const uint MOUSEEVENTF_RIGHTUP = 0x0010;
        const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        const uint MOUSEEVENTF_MIDDLEUP = 0x0040;
        const uint MOUSEEVENTF_XDOWN = 0x0080;
        const uint MOUSEEVENTF_XUP = 0x0100;
        const uint MOUSEEVENTF_WHEEL = 0x0800;
        const uint MOUSEEVENTF_VIRTUALDESK = 0x4000;
        const uint MOUSEEVENTF_ABSOLUTE = 0x8000;

        List<Macro> mMacros = new List<Macro>();

        public void Add(Macro macro)
        {
            mMacros.Add(macro);
        }

        public const UInt64 kForever = 0xffffffffffffffff;
        public UInt64 Tick(UInt64 curTime)
        {

            UInt64 minTime = kForever;
            foreach(Macro macro in mMacros)
            {
                UInt64 next = macro.ProcessMacroToTime(curTime);
                if (next < minTime)
                    minTime = next;
            }
            return minTime;
        }




        [Serializable]
        public class MacroDefinition
        {
            public enum MacroType
            {
//              kMacroTimed =0,
                kMacroMultiKey = 0x1,
                kMacroUseScanCodes = 0x02
            }
            static int VERSION = 1;


            public String name = "";
            public MacroType macroType = 0 | MacroType.kMacroUseScanCodes;
            public KeySequenceEntry[] sequence;

            public MacroDefinition(String n, KeySequenceEntry[] keys, MacroType type)
            {
                name = n;
                sequence = keys;
                macroType = type;
            }
            public MacroDefinition(KeySequenceEntry[] keys)
            {
                sequence = keys;
            }


            public static MacroDefinition Read(System.IO.Stream stream)
            {
                IFormatter formatter = new BinaryFormatter();
                int version = (Int32)formatter.Deserialize(stream);
                MacroDefinition macro = (MacroDefinition)formatter.Deserialize(stream);
                return macro;
            }

            public static void Write(System.IO.Stream stream, MacroDefinition macro)
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, VERSION);
                formatter.Serialize(stream, macro);
            }



            private List<INPUT> CreateKeyDownInputList()
            {
                List<INPUT> keyActions = new List<INPUT>();
                for (int i = 0; i < sequence.Length; i++)
                {
                    KeySequenceEntry entry = sequence[i];
                    INPUT temp = new INPUT();
                    temp.type = INPUT_KEYBOARD;
                    if ((macroType & MacroDefinition.MacroType.kMacroUseScanCodes) != 0)
                    {
                        temp.ki.wScan = entry.keyCode;
                        temp.ki.dwFlags = KEYEVENTF_SCANCODE;
                    }
                    else
                    {
                        temp.ki.wVk = entry.keyCode;
                        temp.ki.dwFlags = 0;
                    }

                    keyActions.Add(temp);
                }
                return keyActions;
            }

            // Invert order for Key up
            private List<INPUT> CreateKeyUpInputList()
            {
                List<INPUT> keyActions = new List<INPUT>();
                for (int i = sequence.Length-1; i >= 0 ; --i)
                {
                    KeySequenceEntry entry = sequence[i];
                    INPUT temp = new INPUT();
                    temp.type = INPUT_KEYBOARD;
                    if ((macroType & MacroDefinition.MacroType.kMacroUseScanCodes) != 0)
                    {
                        // Note scan codes send a different value for key up
                        temp.ki.wScan = (ushort) entry.keyCode;
                        temp.ki.dwFlags = KEYEVENTF_SCANCODE | KEYEVENTF_KEYUP;
                    }
                    else
                    {
                        temp.ki.wVk = entry.keyCode;
                        temp.ki.dwFlags = KEYEVENTF_KEYUP;
                    }
                    keyActions.Add(temp);
                }
                return keyActions;
            }

            private void AllMacroKeys(bool KeyUp)
            {
                INPUT[] keyArray = (KeyUp ? CreateKeyUpInputList() : CreateKeyDownInputList()).ToArray();
                if (keyArray.Length > 0)
                {
                    int size = Marshal.SizeOf(keyArray[0]);
                    SendInput((uint)keyArray.Length, keyArray, size);
                }
            }

            public void AllMacroKeysDown()
            {
                AllMacroKeys(false);
            }

            public void AllMacroKeysUp()
            {
                AllMacroKeys(true);
            }
        
        
        }


        [Serializable]
        public class KeySequenceEntry 
        {
            public UInt32 time;
            public bool keyUp;
            public byte keyCode;

            public KeySequenceEntry(UInt32 t, bool up, byte key)
            {
                time = t;
                keyUp = up;
                keyCode = key;
            }

        }



        public class Macro
        {
            UInt64 baseTime;
            MacroDefinition macroKeys;
            int IP = 0;

            public Macro(MacroDefinition keys)
            {
                macroKeys = keys;
            }

            public UInt64 ProcessMacroToTime(UInt64 time)
            {
                List<INPUT> keyCommands = new List<INPUT>();

                for (; IP < macroKeys.sequence.Length; IP++)
                {
                    KeySequenceEntry entry = macroKeys.sequence[IP];
                    if (baseTime + entry.time > time)
                        break;

                    INPUT temp = new INPUT();
                    temp.type = INPUT_KEYBOARD;
                    if (((macroKeys.macroType & MacroDefinition.MacroType.kMacroUseScanCodes) != 0))
                    {
                        temp.ki.wScan = (ushort)entry.keyCode;
                        temp.ki.dwFlags = KEYEVENTF_SCANCODE |  (entry.keyUp ? KEYEVENTF_KEYUP : 0);
                    }
                    else
                    {
                        temp.ki.wVk = entry.keyCode;
                        temp.ki.dwFlags = entry.keyUp ? KEYEVENTF_KEYUP : 0;
                    }
                    keyCommands.Add(temp);
                }
                INPUT[] keyArray = keyCommands.ToArray();
                if (keyArray.Length > 0)
                {
                    int size = Marshal.SizeOf(keyArray[0]);
                    SendInput((uint)keyArray.Length, keyArray, size);
                }

                return IP < macroKeys.sequence.Length ? macroKeys.sequence[IP].time + baseTime : kForever;
            }

            // Returns time of next Event or -1 if done
            public UInt64 InitMacroAtTime(UInt64 time)
            {
                if ((macroKeys.macroType & MacroDefinition.MacroType.kMacroMultiKey) == 0)
                {
                    baseTime  = time;
                    return ProcessMacroToTime(time);
                }
                return kForever;
            }



        }


        [StructLayout(LayoutKind.Sequential)]
        struct MOUSEINPUT
        {
            int dx;
            int dy;
            uint mouseData;
            uint dwFlags;
            uint time;
            IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct HARDWAREINPUT
        {
            uint uMsg;
            ushort wParamL;
            ushort wParamH;
        }

        [StructLayout(LayoutKind.Explicit)]
        struct INPUT
        {
            [FieldOffset(0)]
            public int type;
            [FieldOffset(4)] //*
            MOUSEINPUT mi;
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
