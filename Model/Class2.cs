using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    public class t_Tables1
    {

        Dictionary<string, bool> IsDicMyProperty = new Dictionary<string, bool>();
        /// <summary>
        /// GUID
        /// </summary>		
        private bool IsGUID = false;
        private Guid GUIDValue;
        public Guid GUID
        {
            get { return GUIDValue; }
            set
            {
                GUIDValue = value;
                IsGUID = true;
            }
        }
        /// <summary>
        /// Title
        /// </summary>		
        private bool IsTitle = false;
        private string TitleValue;
        public string Title
        {
            get { return TitleValue; }
            set
            {
                TitleValue = value;
                IsTitle = true;
            }
        }
        /// <summary>
        /// SQL
        /// </summary>		
        private bool IsSQL = false;
        private string SQLValue;
        public string SQL
        {
            get { return SQLValue; }
            set
            {
                SQLValue = value;
                IsSQL = true;
            }
        }
        /// <summary>
        /// TableName
        /// </summary>		
        private bool IsTableName = false;
        private string TableNameValue;
        public string TableName
        {
            get { return TableNameValue; }
            set
            {
                TableNameValue = value;
                IsTableName = true;
            }
        }
        /// <summary>
        /// FileName
        /// </summary>		
        private bool IsFileName = false;
        private string FileNameValue;
        public string FileName
        {
            get { return FileNameValue; }
            set
            {
                FileNameValue = value;
                IsFileName = true;
            }
        }
        /// <summary>
        /// Note
        /// </summary>		
        private bool IsNote = false;
        private string NoteValue;
        public string Note
        {
            get { return NoteValue; }
            set
            {
                NoteValue = value;
                IsNote = true;
            }
        }
        /// <summary>
        /// choice
        /// </summary>		
        private bool Ischoice = false;
        private int choiceValue;
        public int choice
        {
            get { return choiceValue; }
            set
            {
                choiceValue = value;
                Ischoice = true;
            }
        }
        /// <summary>
        /// insert
        /// </summary>		
        private bool Isinsert = false;
        private int insertValue;
        public int insert
        {
            get { return insertValue; }
            set
            {
                insertValue = value;
                Isinsert = true;
            }
        }
        /// <summary>
        /// update
        /// </summary>		
        private bool Isupdate = false;
        private int updateValue;
        public int update
        {
            get { return updateValue; }
            set
            {
                updateValue = value;
                Isupdate = true;
            }
        }
        /// <summary>
        /// delete
        /// </summary>		
        private bool Isdelete = false;
        private int deleteValue;
        public int delete
        {
            get { return deleteValue; }
            set
            {
                deleteValue = value;
                Isdelete = true;
            }
        }
        /// <summary>
        /// strwhere
        /// </summary>		
        private bool Isstrwhere = false;
        private int strwhereValue;
        public int strwhere
        {
            get { return strwhereValue; }
            set
            {
                strwhereValue = value;
                Isstrwhere = true;
            }
        }
        /// <summary>
        /// CountData
        /// </summary>		
        private bool IsCountData = false;
        private string CountDataValue;
        public string CountData
        {
            get { return CountDataValue; }
            set
            {
                CountDataValue = value;
                IsCountData = true;
            }
        }
        /// <summary>
        /// Plus
        /// </summary>		
        private bool IsPlus = false;
        private int PlusValue;
        public int Plus
        {
            get { return PlusValue; }
            set
            {
                PlusValue = value;
                IsPlus = true;
            }
        }
        public bool IsFieldAssign(string Fieldname)
        {
            return IsDicMyProperty[nameof(Fieldname)];
        }

    }
}