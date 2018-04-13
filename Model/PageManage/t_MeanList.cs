using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class t_MeanList
    {
        private string GuIDValue= "9D2512E9-6FF4-4E7E-BBB8-23DE83755D18";
        public string GuID { get => GuIDValue; set => GuIDValue = value; }

        private string TitleValue = "菜单管理";
        public string Title { get => TitleValue; set => TitleValue = value; }

        private string SQLValue = "SELECT top(10000) * FROM  [t_Mean]";
        public string SQL { get => SQLValue; set => SQLValue = value; }

        private string TableNameValue= "[t_Mean]";
        public string TableName { get => TableNameValue; set => TableNameValue = value; }

        private string FileNameValue = "MeanList";
        public string FileName { get => FileNameValue; set => FileNameValue = value; }

        private string NoteValue = "";
        public string Note { get => NoteValue; set => NoteValue = ""; }

        private string ChoiceValue = "1";
        public string Choice { get => ChoiceValue; set => ChoiceValue = value; }

        private string InsertValue = "1";
        public string Insert { get => InsertValue; set => InsertValue = value; }

        private string UpdateValue = "1";
        public string Update { get => UpdateValue; set => UpdateValue = value; }

        private string DeleteValue = "1";
        public string Delete { get => DeleteValue; set => DeleteValue = value; }

        private string StrwhereValue = "1";
        public string Strwhere { get => StrwhereValue; set => StrwhereValue = value; }

        private string CountDataValue = "'记录总条数：'+ convert(varchar(20),count(guid))+'（条）'";
        public string CountData { get => CountDataValue; set => CountDataValue = value; }

        private string PlusValue = "1";
        public string Plus { get => PlusValue; set => PlusValue = value; }
    }
}
