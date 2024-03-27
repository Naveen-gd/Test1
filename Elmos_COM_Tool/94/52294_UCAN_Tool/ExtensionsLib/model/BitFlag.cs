using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Xml.Linq;
using System.Xml;
using System.IO;

namespace Extensions
{
    public class BitFlag
    {
        bool _value = false;
        bool _was0 = false;
        bool _was1 = false;

        public BitFlag(bool initValue = false)
        {
            _value = initValue;
        }

        public bool value
        {
            get { return _value; }
        }

        public void SetValue(bool value)
        {
            if (value && !_value) _was1 = true;
            if (!value && _value) _was0 = true;
            this._value = value;
        }

        public bool GetClearWasIs0()
        {
            bool ret = _was0 || !_value;
            _was0 = false;
            return ret;
        }

        public bool GetClearWasIs1()
        {
            bool ret = _was1 || _value;
            _was1 = false;
            return ret;
        }

        public void Clear()
        {
            _was0 = false;
            _was1 = false;
        }

    }
}
