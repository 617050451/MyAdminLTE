using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class t_Tables
    {
        public Guid Guid { get; set; }
        public string Title { get; set; }
        public string SQL { get; set; }
        public string TableName { get; set; }
        public string FileName { get; set; }
        public string Note { get; set; }
        public int Choice { get; set; }
        public int Insert { get; set; }
        public int Update { get; set; }
        public int StrWhere { get; set; }
        public string CountData { get; set; }
        public int Plus { get; set; }
    }
}
