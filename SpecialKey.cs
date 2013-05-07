using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DX1Utility
{
    public class SpecialKey
    {
        public enum SpecialKeysExtraData
        {
            None = 0,
            Boolean = 1,
            TextBox = 2,
            Slider = 4,
            Byte = 5
        }

        private int _SpecialID;                             //Holds the ID of the special key for reerence and search
        private string _SpecialName;                        //Holds the Name of the special key for display
        private ushort _SpecialValue;                       //Holds the Value of the key press if this special key can be used as a Value
        private bool _ReqData = false;                      //Does this Special key require Additional Settings
        private SpecialKeysExtraData _ExtraDataType =  0;   //What type of Extra Data is required 1=Boolean, 2=Textbox, 4=Slider
        private Dictionary<string, string> _ExtraDataParams = new Dictionary<string,string>();                //Holds the parameters for what to display when extra data is required

        public int SpecialID
        {
            get { return _SpecialID; }
            set { _SpecialID = value; }
        }
        
        public string SpecialName
        {
            get { return _SpecialName; }
            set { _SpecialName = value; }
        }

        public ushort SpecialValue
        {
            get { return _SpecialValue; }
            set { _SpecialValue = value; }
        }

        public bool ReqData
        {
            get { return _ReqData; }
            set { _ReqData = value; }
        }

        public SpecialKeysExtraData ExtraDataType
        {
            get { return _ExtraDataType; }
            set { _ExtraDataType = value; }
        }

        public Dictionary<string, string> ExtraDataParams
        {
            get { return _ExtraDataParams; }
            set { _ExtraDataParams = value; }
        }

    }
}
