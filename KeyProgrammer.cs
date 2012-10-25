using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DX1Utility
{
    public class KeyProgrammer
    {
        public const int kNotActive = -1;
        
        Byte[] mKeyMap;
       String[] mMacroMap;

       public KeyProgrammer(ref List<KeyMap> KeyMapTest, ref String[] macros)
        {
            mKeyMap = GetKeyMap(KeyMapTest);
            mMacroMap = macros;
            InitKeyPairConversionTable();
        }

        public byte[] GetKeyMap(List<KeyMap> KeyMapTest)
        {
            //Return the Byte array of the key map fromt he Profiles
            byte[] TempKeymap = new Byte[3 * 50];
            int Offset = 0;

            foreach (KeyMap DxKey in KeyMapTest)
            {
                DxKey.KeyMapByteArray().CopyTo(TempKeymap, Offset);
                Offset = Offset + 3;
            }

            return TempKeymap;
        }

        private bool active = false;
        private int keyToProgram = 0;
        public int KeyToProgram
        {
            get 
            {
                if (active)
                    return keyToProgram;
                else 
                    return kNotActive;
            }
        }
        public bool Active
        {
            get { return active; }
            set 
            {
                active = value; 
                if (active == false)
                {
                    keyToProgram = 0;
                }
            }
        }

        Byte[][] KeyPairConversionTable;

        private void InitKeyPairConversionTable()
        {
            Byte[] oddMappings = { (Byte)'0', 0x0d, 0x1b, 0x08,
                                   0x09, 0x20, 0xbd, 0xbb,
                                   0xdb, 0xdd, 0xdc, 0x00,
                                   0xba, 0xde, 0xc0, 0xbc,
                                   0xbe, 0xbf, 0x14, 0x70,
                                   0x71, 0x72, 0x73, 0x74,
                                   0x75, 0x76, 0x77, 0x78,
                                   0x79, 0x7a, 0x7b, 0x00 /* PrintScreen */,
                                   0x91, 0x13, 0x2d, 0x24,
                                   0x21, 0x2e, 0x23, 0x22,
                                   0x27, 0x25, 0x28, 0x26,
                                   0x90, 0x6f, 0x6a, 0x6d,
                                   0x6b, 0x0d, 0x61, 0x62, /* note numEnter doesn't return a different keycode (to investigate) */
                                   0x63, 0x64, 0x65, 0x66,
                                   0x67, 0x68, 0x69, 0x60,
                                   0x6e
                                 };

            Byte[] specialMappings = { 0xa2, 0xa0, 0xa4, 0x5b,
                                       0xa3, 0xa1, 0xa5, 0x5c
                                     };

            // Alloc the table
            KeyPairConversionTable = new Byte[256][];

            // A-Z
            for (int i = 'A'; i <= 'Z'; i++)
            {
                KeyPairConversionTable[i] = new Byte[2];
                KeyPairConversionTable[i][0] = 1;
                KeyPairConversionTable[i][1] = (Byte)(4 + (i - 'A'));
            }
            for (int i = '1'; i<='9'; i++)
            {
                KeyPairConversionTable[i] = new Byte[2];
                KeyPairConversionTable[i][0] = 1;
                KeyPairConversionTable[i][1] = (Byte)(0x1e + (i - '1'));
            }

            for (int i = 0; i < oddMappings.Length; i++)
            {
                int index = (int)oddMappings[i];
                if (index != 0)
                {
                    KeyPairConversionTable[index] = new Byte[2];
                    KeyPairConversionTable[index][0] = 1;
                    KeyPairConversionTable[index][1] = (Byte)(0x27 + i);
                }
            }

            for (int i = 0; i < specialMappings.Length; i++)
            {
                int index = (int)specialMappings[i];
                if (index != 0)
                {
                    KeyPairConversionTable[index] = new Byte[2];
                    KeyPairConversionTable[index][0] = 2;
                    KeyPairConversionTable[index][1] = (Byte)(1<<i);
                }
            }

        }


        public Byte[] ConvertToKeyTuple(int inputCode)
        {
            return KeyPairConversionTable[inputCode];
        }


        //public bool KeyDown(int e)
        //{
        //    if (active && keyToProgram != 0)
        //    {
        //        Byte[] keyTuple = ConvertToKeyTuple(e);
        //        if (keyTuple != null)
        //        {
        //            int offset = (keyToProgram - 1) * 3;
        //            mKeyMap[offset++] = (Byte)keyToProgram;
        //            mKeyMap[offset++] = keyTuple[0];
        //            mKeyMap[offset++] = keyTuple[1];
        //            keyToProgram = 0;
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        public bool AssignMacro(String macroName, ref List<KeyMap> KeyMaps)
        {
            if (active && keyToProgram != 0)
            {
                KeyMaps[keyToProgram].Action = 0;
                KeyMaps[keyToProgram].Type = 0x3;
                KeyMaps[keyToProgram].Description = macroName;
                //int offset = (keyToProgram - 1) * 3;
                //mKeyMap[offset++] = (Byte)keyToProgram;
                //mKeyMap[offset++] = 3;
                //mKeyMap[offset++] = 0;

                mMacroMap[keyToProgram - 1] = macroName;
                keyToProgram = 0;
                return true;
            }
            return false;
        }

        public bool DX1KeyDown(int key)
        {
            if (active && key != 0)
            {
                //Code to Quick Program
                keyToProgram = key;
                // allow unbinds
                //if (keyToProgram == key)
                //{
                //    int offset = (keyToProgram - 1) * 3;
                //    mKeyMap[offset++] = 0;
                //    mKeyMap[offset++] = 0;
                //    mKeyMap[offset++] = 0;
                //    keyToProgram = 0;
                //}
                //else
                //    keyToProgram = key;

                return true;
            }
            return false;
        }

    }
}
