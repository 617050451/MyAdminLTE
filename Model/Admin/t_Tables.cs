using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    public class t_Tables
    {

        Dictionary<string, bool> IsDicMyProperty = new Dictionary<string, bool>();
        /// <summary>
        /// GUID
        /// </summary>		

        private Guid GUIDValue;
        public Guid GUID
        {
            get { return GUIDValue; }
            set
            {
                GUIDValue = value;
                IsDicMyProperty.Add(nameof(GUID), true);
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
        /// <summary>
        /// CountData
        /// </summary>		

        private string CountDataValue;
        public string CountData
        {
            get { return CountDataValue; }
            set
            {
                CountDataValue = value;
                IsDicMyProperty.Add(nameof(CountData), true);
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