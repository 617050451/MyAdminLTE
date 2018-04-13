using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class t_Tables
    {
        private Guid GuIDValue;
        public Guid Guid { get => GuIDValue; set => GuIDValue = value; }

        private string TitleValue = null;
        public string Title { get => TitleValue; set => TitleValue = value; }

        private string SQLValue = null;
        public string SQL { get => SQLValue; set => SQLValue = value; }

        private string TableNameValue = null;
        public string TableName { get => TableNameValue; set => TableNameValue = value; }

        private string FileNameValue = null;
        public string FileName { get => FileNameValue; set => FileNameValue = value; }

        private string NoteValue = null;
        public string Note { get => NoteValue; set => NoteValue = value; }

        private int ChoiceValue = -1;
        public int Choice { get => ChoiceValue; set => ChoiceValue = value; }

        public int Insert { get; set; }
        public int Update { get; set; }
        public int StrWhere { get; set; }
        public string CountData { get; set; }
        public int Plus { get; set; }
    }
}
