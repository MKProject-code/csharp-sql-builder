using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLBuilderConsoleTest.SQLBuilder
{
    public abstract class SQLController
    {
        private string query;
        private int openedBrackets;
        private static readonly List<string> beautyQueryWordsNewLine = new List<string>()
                    {
                        "UNION",
                        "SELECT",
                        "FROM",
                        "WHERE",
                    };

        public SQLController()
        {
            this.query = "";
            this.openedBrackets = 0;
        }

        public string GetQuery(bool beauty = false)
        {
            if (beauty)
            {
                this.query += " ";

                this.query = this.query.Replace("(", "(" + Environment.NewLine);
                this.query = this.query.Replace(")", Environment.NewLine + ")");

                foreach (string word in beautyQueryWordsNewLine)
                {
                    this.query = this.query.Replace(" " + word + " ", Environment.NewLine + word + " ");
                }

                string[] queryLines = this.query.Split(Environment.NewLine);
                this.query = "";

                int tabs = 0;

                foreach (string line in queryLines)
                {
                    string lineEdited = line.Trim();

                    if (lineEdited.StartsWith(')'))
                        tabs--;

                    string beforeTabsStr = "";
                    for (int i = 0; i < tabs; i++)
                    {
                        beforeTabsStr += "\t";
                    }

                    if (lineEdited.Length > 1 && lineEdited.EndsWith('('))
                    {
                        lineEdited = lineEdited.Substring(0, lineEdited.Length - 1) + Environment.NewLine + beforeTabsStr + "(";
                        tabs++;
                    }

                    this.query += beforeTabsStr + lineEdited + Environment.NewLine;

                    if (lineEdited.StartsWith('('))
                        tabs++;
                }

                this.query = this.query.TrimEnd();
            }

            return this.query+";";
        }

        protected void AddToQuery(string query, bool brackets = false)
        {
            this.query += brackets ? "(" + query + ")" : query;
        }

        protected void AddToQuerySafe(string query, bool brackets = false)
        {
            if (this.query.Length > 0 && !this.query.EndsWith(' ') && !this.query.EndsWith('('))
                this.query += " ";
            this.query += brackets ? "(" + query + ")" : query;
        }

        protected void OpenBracket()
        {
            this.AddToQuerySafe("(");
            this.openedBrackets++;
        }
        protected void CloseBracket()
        {
            if(this.openedBrackets == 0)
                throw new ArgumentException("Must use OpenBracket() before CloseBracket()");
            this.AddToQuery(")");
            this.openedBrackets--;
        }
    }
}
