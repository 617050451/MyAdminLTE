using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class Class1
    {
        Dictionary<string, bool> dic = new Dictionary<string, bool>();
        bool isMyProperty = false;
        int myval;
        public int MyProperty
        {
            get
            {
                return myval;
            }
            set
            {
                myval = value;
                isMyProperty = true;
                dic[nameof(MyProperty)] = true;
            }
        }

        public bool IsFieldAssign(string fieldname)
        {
            return dic[fieldname];



            switch (fieldname)
            {
                case nameof(MyProperty): return isMyProperty;
            }


            var type = this.GetType();
            var fields = type.GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

            var f = fields.FirstOrDefault(x => x.Name == "is" + fieldname);
            if (f != null)
            {
                return (bool)f.GetValue(this);
            }

            return false;
        }
    }
}
