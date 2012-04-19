using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DX1Utility
{
    public class KeyMap
    {
        private byte _Dx1Key;
        private byte _Type;
        private byte _Action;
        private string _Description;

        public byte Dx1Key
        {
            get { return _Dx1Key; }
            set { _Dx1Key = value; }
        }

        public byte Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        public byte Action
        {
            get { return _Action; }
            set { _Action = value; }
        }

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public byte[] KeyMapByteArray()
        {
            //Used to return the Byte Array that the DX1 expects for programming
            byte[] tempbyte = new byte[3];

            tempbyte[0] = _Dx1Key;
            tempbyte[1] = _Type;
            tempbyte[2] = _Action;

            return tempbyte;
        }

    }

}
