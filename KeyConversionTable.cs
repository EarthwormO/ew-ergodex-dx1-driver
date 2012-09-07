using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DX1Utility
{
    static class KeyConversionTable
    {
        private static byte[][] KeyconversionTable;
        private static bool Initialized = false;

        public static Byte[][] KeyPairConversionTable
        {
            get{
                if (!Initialized)
                {
                    KeyconversionTable = InitTable();
                    Initialized = true;
                }
                return KeyconversionTable;
            }
        }

        private static byte[][] InitTable()
        {

            byte[][] TempKeyPairConversionTable;

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
            TempKeyPairConversionTable = new Byte[256][];

            // A-Z
            for (int i = 'A'; i <= 'Z'; i++)
            {
                TempKeyPairConversionTable[i] = new Byte[2];
                TempKeyPairConversionTable[i][0] = 1;
                TempKeyPairConversionTable[i][1] = (Byte)(4 + (i - 'A'));
            }
            for (int i = '1'; i <= '9'; i++)
            {
                TempKeyPairConversionTable[i] = new Byte[2];
                TempKeyPairConversionTable[i][0] = 1;
                TempKeyPairConversionTable[i][1] = (Byte)(0x1e + (i - '1'));
            }

            for (int i = 0; i < oddMappings.Length; i++)
            {
                int index = (int)oddMappings[i];
                if (index != 0)
                {
                    TempKeyPairConversionTable[index] = new Byte[2];
                    TempKeyPairConversionTable[index][0] = 1;
                    TempKeyPairConversionTable[index][1] = (Byte)(0x27 + i);
                }
            }

            for (int i = 0; i < specialMappings.Length; i++)
            {
                int index = (int)specialMappings[i];
                if (index != 0)
                {
                    TempKeyPairConversionTable[index] = new Byte[2];
                    TempKeyPairConversionTable[index][0] = 2;
                    TempKeyPairConversionTable[index][1] = (Byte)(1 << i);
                }
            }
            return TempKeyPairConversionTable;
        }

        
    }
}
