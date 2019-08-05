using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Table
    {
        public List<Column> columns;
        public List<object> rows;
        public string type = "table";
    }
}
