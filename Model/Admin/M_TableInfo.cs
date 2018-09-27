using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class M_TableInfo
    {
        Dictionary<string, bool> IsDicMyProperty = new Dictionary<string, bool>();

        private int IDValue;
        public int ID
        {
            get { return IDValue; }
            set
            {
                IDValue = value;
                IsDicMyProperty.Add(nameof(ID), true);
            }
        }

        private int NameValue;
        public int Name
        {
            get { return NameValue; }
            set
            {
                NameValue = value;
                IsDicMyProperty.Add(nameof(Name), true);
            }
        }

        private int TypeValue;
        public int Type
        {
            get { return TypeValue; }
            set
            {
                TypeValue = value;
                IsDicMyProperty.Add(nameof(Type), true);
            }
        }

        public bool IsFieldAssign(string Fieldname)
        {
            if (IsDicMyProperty.ContainsKey(Fieldname))
                return IsDicMyProperty[Fieldname];
            else
                return false;
        }
    }
}
