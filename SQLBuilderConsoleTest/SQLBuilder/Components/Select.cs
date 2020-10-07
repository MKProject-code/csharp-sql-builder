using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLBuilderConsoleTest.SQLBuilder
{
    public class Select
    {
        private readonly Dictionary<string, string> dictionaryColumns;
        private readonly Dictionary<string, string> dictionaryTables;
        private readonly List<string> listWheres;
        private int limitStart;
        private int limitMax;
        private string queryBeforeUnion;

        internal Select()
        {
            this.dictionaryColumns = new Dictionary<string, string>();
            this.dictionaryTables = new Dictionary<string, string>();
            this.listWheres = new List<string>();
            this.limitStart = -1;
            this.limitMax = -1;
            this.queryBeforeUnion = "";
        }
        public Select Where(string column, string operator_, string value)
        {
            try
            {
                this.listWheres.Add("`" + column + "` " + operator_ + " '" + value.Replace(@"\", @"\\").Replace("'", @"\'") + "'");
            }
            catch(ArgumentException)
            {
                throw new ArgumentException(String.Format("Wheres are repeated -> Where(\"{0}\", \"{1}\", \"{2}\")", column, operator_, value));
            }
            return this;
        }
        public Select Column(string column)
        {
            try
            {
                this.dictionaryColumns.Add("`" + column + "`", null);
            }
            catch(ArgumentException)
            {
                throw new ArgumentException(String.Format("Columns are repeated -> Column(\"{0}\")", column));
            }
            return this;
        }
        public Select Columns(string[] columns)
        {
            foreach (string column in columns)
                this.Column(column);
            return this;
        }
        public Select Columns(List<string> columns)
        {
            foreach (string column in columns)
                this.Column(column);
            return this;
        }

        public Select Columns(Dictionary<string,string> columnsWithAsColumn)
        {
            foreach (KeyValuePair<string, string> entry in columnsWithAsColumn)
                this.Column(entry.Key, entry.Value);
            return this;
        }

        public Select Column(string column, string asColumn)
        {
            string[] columns = column.Split('.');
            column = String.Join("`.`",columns);
            try
            {
                this.dictionaryColumns.Add("`" + column + "`", asColumn == null ? null : "`" + asColumn + "`");
            }
            catch (ArgumentException)
            {
                throw new ArgumentException(String.Format("Columns are repeated -> Column(\"{0}\", \"{1}\")", column, asColumn));
            }
            return this;
        }

        public Select Table(string table, string asTable = null)
        {
            this.dictionaryTables.Add("`" + table + "`", asTable == null ? null : "`" + asTable + "`");
            return this;
        }
        public Select Table(Select select, string asTable = null)
        {
            this.dictionaryTables.Add("(" + select.Build() + ")", asTable == null ? null : "`" + asTable + "`");
            return this;
        }
        public Select Tables(string[] tables)
        {
            foreach (string table in tables)
                this.Table(table);
            return this;
        }
        public Select Tables(List<string> tables)
        {
            foreach (string table in tables)
                this.Table(table);
            return this;
        }
        public Select Tables(Dictionary<string, string> tablesWithAsTable)
        {
            foreach (KeyValuePair<string, string> entry in tablesWithAsTable)
                this.Table(entry.Key, entry.Value);
            return this;
        }


        public Select Union()
        {
            this.queryBeforeUnion += this.Build() + " UNION ";
            this.dictionaryColumns.Clear();
            this.dictionaryTables.Clear();
            this.limitStart = -1;
            this.limitMax = -1;
            return this;
        }

        public Select Limit(int limitMax)
        {
            this.limitMax = limitMax;
            return this;
        }
        public Select Limit(int limitStart, int limitMax)
        {
            this.limitStart = limitStart;
            this.limitMax = limitMax;
            return this;
        }

        public string Build()
        {
            string dataTables = "";
            foreach (KeyValuePair<string, string> entry in this.dictionaryTables)
            {
                if (entry.Key.Length > 0)
                {
                    if (dataTables.Length == 0)
                        dataTables = entry.Key;
                    else
                        dataTables += "," + entry.Key;//for beauty: ", "

                    if (entry.Value != null && entry.Value.Length > 0)
                        dataTables += " AS " + entry.Value;
                }
            }

            if (dataTables.Length == 0)
            {
                throw new ArgumentException("Select error -> Tables be null");
            }

            string dataColumns = "";
            foreach (KeyValuePair<string, string> entry in this.dictionaryColumns)
            {
                if(entry.Key.Length > 0)
                {
                    if(dataColumns.Length == 0)
                        dataColumns = entry.Key;
                    else
                        dataColumns += "," + entry.Key;//for beauty: ", "

                    if (entry.Value != null && entry.Value.Length > 0)
                        dataColumns += " AS " + entry.Value;
                }
            }

            if (dataColumns.Length == 0)
            {
                dataColumns = "*";
            }

            string result = this.queryBeforeUnion + "SELECT " + dataColumns + " FROM " + dataTables;

            if (this.limitStart > 0 && this.limitMax > 0)
                return result + " LIMIT " + this.limitStart + "," + this.limitMax;
            else if (this.limitMax > 0)
                return result + " LIMIT " + this.limitMax;
            else
                return result;
        }
    }
}
