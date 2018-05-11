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
        /// choice
        /// </summary>		

        private int ChoiceValue;
        public int Choice
        {
            get { return ChoiceValue; }
            set
            {
                ChoiceValue = value;
                IsDicMyProperty.Add(nameof(Choice), true);
            }
        }
        /// <summary>
        /// insert
        /// </summary>		

        private int InsertValue;
        public int Insert
        {
            get { return InsertValue; }
            set
            {
                InsertValue = value;
                IsDicMyProperty.Add(nameof(Insert), true);
            }
        }
        /// <summary>
        /// update
        /// </summary>		

        private int UpdateValue;
        public int Update
        {
            get { return UpdateValue; }
            set
            {
                UpdateValue = value;
                IsDicMyProperty.Add(nameof(Update), true);
            }
        }
        /// <summary>
        /// delete
        /// </summary>		

        private int DeleteValue;
        public int Delete
        {
            get { return DeleteValue; }
            set
            {
                DeleteValue = value;
                IsDicMyProperty.Add(nameof(Delete), true);
            }
        }
        /// <summary>
        /// strwhere
        /// </summary>		

        private int StrwhereValue;
        public int Strwhere
        {
            get { return StrwhereValue; }
            set
            {
                StrwhereValue = value;
                IsDicMyProperty.Add(nameof(Strwhere), true);
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
        /// <summary>
        /// Plus
        /// </summary>		

        private int PlusValue;
        public int Plus
        {
            get { return PlusValue; }
            set
            {
                PlusValue = value;
                IsDicMyProperty.Add(nameof(Plus), true);
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