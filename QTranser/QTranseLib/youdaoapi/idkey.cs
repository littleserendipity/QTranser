using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTranser.QTranseLib
{
    internal static class Idkey
    {
        private static string _id = "";
        private static string _key = "";
        public static string Id
        {
            get
            {
                if(_id == "") Getidkey.getidkey();
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        public static string Key
        {
            get
            {
                if (_key == "") Getidkey.getidkey();
                return _key;
            }
            set
            {
                _key = value;
            }
        }
    }
}
