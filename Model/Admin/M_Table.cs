using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
     public class M_Table
    {
        Dictionary<string, bool> IsDicMyProperty = new Dictionary<string, bool>();
        /// <summary>
        /// GUID
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
        /// Title
        /// </summary>		

        private string TitleValue;
        public string Title
        {
            get { return TitleValue; }
            set
            {
                TitleValue = value;
                IsDicMyProperty.Add(nameof(Title), true);
            }
        }
        /// <summary>
        /// SQL
        /// </summary>		
        private string SQLValue;
        public string SQL
        {
            get { return SQLValue; }
            set
            {
                SQLValue = value;
                IsDicMyProperty.Add(nameof(SQL), true);
            }
        }
        /// <summary>
        /// Predefined
        /// </summary>		
        private string PredefinedSQLdValue;
        public string PredefinedSQL
        {
            get { return PredefinedSQLdValue; }
            set
            {
                PredefinedSQLdValue = value;
                IsDicMyProperty.Add(nameof(PredefinedSQL), true);
            }
        }
        /// <summary>
        /// TableType
        /// </summary>		
        private int TableTypeValue;
        public int TableType
        {
            get { return TableTypeValue; }
            set
            {
                TableTypeValue = value;
                IsDicMyProperty.Add(nameof(TableType), true);
            }
        }
        /// <summary>
        /// TableName 
        /// </summary>		
        private string TableNameValue;
        public string TableName
        {
            get { return TableNameValue; }
            set
            {
                TableNameValue = value;
                IsDicMyProperty.Add(nameof(TableName), true);
            }
        }
        private string PrimaryKeyValue;
        public string PrimaryKey
        {
            get { return PrimaryKeyValue; }
            set
            {
                PrimaryKeyValue = value;
                IsDicMyProperty.Add(nameof(PrimaryKey), true);
            }
        }

        /// <summary>
        /// FileName
        /// </summary>		

        private string FileNameValue;
        public string FileName
        {
            get { return FileNameValue; }
            set
            {
                FileNameValue = value;
                IsDicMyProperty.Add(nameof(FileName), true);
            }
        }
        /// <summary>
        /// Note
        /// </summary>		

        private string NoteValue;
        public string Note
        {
            get { return NoteValue; }
            set
            {
                NoteValue = value;
                IsDicMyProperty.Add(nameof(Note), true);
            }
        }

        /// <summary>
        /// insert
        /// </summary>		

        private int IsInsertValue;
        public int IsInsert
        {
            get { return IsInsertValue; }
            set
            {
                IsInsertValue = value;
                IsDicMyProperty.Add(nameof(IsInsert), true);
            }
        }
        /// <summary>
        /// update
        /// </summary>		

        private int IsUpdateValue;
        public int IsUpdate
        {
            get { return IsUpdateValue; }
            set
            {
                IsUpdateValue = value;
                IsDicMyProperty.Add(nameof(IsUpdate), true);
            }
        }
        /// <summary>
        /// delete
        /// </summary>		

        private int IsDeleteValue;
        public int IsDelete
        {
            get { return IsDeleteValue; }
            set
            {
                IsDeleteValue = value;
                IsDicMyProperty.Add(nameof(IsDelete), true);
            }
        }
        /// <summary>
        /// Plus
        /// </summary>		

        private int IsPlusValue;
        public int IsPlus
        {
            get { return IsPlusValue; }
            set
            {
                IsPlusValue = value;
                IsDicMyProperty.Add(nameof(IsPlus), true);
            }
        }
        /// <summary>
        /// strwhere
        /// </summary>		

        private int IsWhereValue;
        public int IsWhere
        {
            get { return IsWhereValue; }
            set
            {
                IsWhereValue = value;
                IsDicMyProperty.Add(nameof(IsWhere), true);
            }
        }
        /// <summary>
        /// choice
        /// </summary>		

        private int IsChoiceValue;
        public int IsChoice
        {
            get { return IsChoiceValue; }
            set
            {
                IsChoiceValue = value;
                IsDicMyProperty.Add(nameof(IsChoice), true);
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
