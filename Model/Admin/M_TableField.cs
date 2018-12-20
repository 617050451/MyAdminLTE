using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class M_TableField
    {
        Dictionary<string, bool> IsDicMyProperty = new Dictionary<string, bool>();
        /// <summary>
        /// TableID
        /// </summary>		
        private int TableIDValue;
        public int TableID
        {
            get { return TableIDValue; }
            set
            {
                TableIDValue = value;
                IsDicMyProperty.Add(nameof(TableID), true);
            }
        }
        /// <summary>
        /// FieldKey
        /// </summary>		
        private string FieldKeyValue;
        public string FieldKey
        {
            get { return FieldKeyValue; }
            set
            {
                FieldKeyValue = value;
                IsDicMyProperty.Add(nameof(FieldKey), true);
            }
        }
        /// <summary>
        /// FieldValue
        /// </summary>		
        private string FieldTextValue;
        public string FieldText
        {
            get { return FieldTextValue; }
            set
            {
                FieldTextValue = value;
                IsDicMyProperty.Add(nameof(FieldText), true);
            }
        }
        /// <summary>
        /// FieldDataType
        /// </summary>		
        private int FieldDataTypeValue;
        public int FieldDataType
        {
            get { return FieldDataTypeValue; }
            set
            {
                FieldDataTypeValue = value;
                IsDicMyProperty.Add(nameof(FieldDataType), true);
            }
        }
        /// <summary>
        /// FieldData
        /// </summary>		
        private string FieldDataValue;
        public string FieldData
        {
            get { return FieldDataValue; }
            set
            {
                FieldDataValue = value;
                IsDicMyProperty.Add(nameof(FieldData), true);
            }
        }
        /// <summary>
        /// FieldStatusID
        /// </summary>		
        private int FieldStatusIDValue;
        public int FieldStatusID
        {
            get { return FieldStatusIDValue; }
            set
            {
                FieldStatusIDValue = value;
                IsDicMyProperty.Add(nameof(FieldStatusID), true);
            }
        }
        /// <summary>
        /// SelectType
        /// </summary>		
        private int SelectTypeValue;
        public int SelectType
        {
            get { return SelectTypeValue; }
            set
            {
                SelectTypeValue = value;
                IsDicMyProperty.Add(nameof(SelectType), true);
            }
        }
        /// <summary>
        /// SelectData
        /// </summary>		
        private string SelectDataValue;
        public string SelectData
        {
            get { return SelectDataValue; }
            set
            {
                SelectDataValue = value;
                IsDicMyProperty.Add(nameof(SelectData), true);
            }
        }
        /// <summary>
        /// FieldOrder
        /// </summary>		
        private int FieldOrderValue;
        public int FieldOrder
        {
            get { return FieldOrderValue; }
            set
            {
                FieldOrderValue = value;
                IsDicMyProperty.Add(nameof(FieldOrder), true);
            }
        }
        /// <summary>
        /// TextAlign
        /// </summary>		
        private string TextAlignValue;
        public string TextAlign
        {
            get { return TextAlignValue; }
            set
            {
                TextAlignValue = value;
                IsDicMyProperty.Add(nameof(TextAlign), true);
            }
        }
        /// <summary>
        /// Width
        /// </summary>		
        private string WidthValue;
        public string Width
        {
            get { return WidthValue; }
            set
            {
                WidthValue = value;
                IsDicMyProperty.Add(nameof(Width), true);
            }
        }
        /// <summary>
        /// OtherCSS
        /// </summary>		
        private string OtherCSSValue;
        public string OtherCSS
        {
            get { return OtherCSSValue; }
            set
            {
                OtherCSSValue = value;
                IsDicMyProperty.Add(nameof(OtherCSS), true);
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
