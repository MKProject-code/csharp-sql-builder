using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLBuilderConsoleTest.SQLBuilder
{
    public class SQLBuilder : SQLController
    {
        public SQLBuilder Select(Select select, string asTable = null) {
            this.AddToQuerySafe(select.Build(), true);
            if(asTable != null && asTable.Length > 0)
                this.AddToQuerySafe("AS `" + asTable + "`");
            return this;
        }

        public SQLBuilder BeginAs()
        {
            this.OpenBracket();
            return this;
        }
        public SQLBuilder EndAs(string asTable)
        {
            this.CloseBracket();
            this.AddToQuerySafe("AS `" + asTable + "`");
            return this;
        }
    }
}
