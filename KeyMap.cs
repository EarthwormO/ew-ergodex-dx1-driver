using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DX1Utility
{
    [Serializable]
    public class KeyMap
    {
        private byte _Dx1Key;           //DX1 Key being programmed
        private byte _Type;             //1=Single Key, 2=Modifier Key, 3=Macro, >3 custom
        private byte _Action;           //What scancode is sent when pressed
        private string _Description;    //Custom Description typed by user
        private string _KeyName;        //Single Key of Modifier Key Windows name
        private string _MacroName;      //Macro Name this Key is programmed to
        private string _CustomData;     //Unknown, reserved for later, probably "Special Key" commands
        
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

        public string KeyName
        {
            get { return _KeyName; }
            set { _KeyName = value; }
        }

        public string MacroName
        {
            get { return _MacroName; }
            set { _MacroName = value; }
        }

        public string CustomData
        {
            get { return _CustomData; }
            set { _CustomData = value; }
        }

        public bool AssignSingleKey(int KeyCode)
        {
            //Used to Set Type and Action from the KeyTuple
            byte[] KeyTuple;

            KeyTuple = KeyConversionTable.KeyPairConversionTable[KeyCode];

            if (KeyTuple != null)
            {
                _Type = KeyTuple[0];
                _Action = KeyTuple[1];
                return true;
            }
            return false;

        }

        public byte[] KeyMapByteArray()
        {
            //Used to return the Byte Array that the DX1 expects for programming
            byte[] tempbyte = new byte[3];

            tempbyte[0] = _Dx1Key;
            tempbyte[1] = Math.Min(_Type, (byte)3);
            tempbyte[2] = _Action;

            return tempbyte;
        }

    }

}
