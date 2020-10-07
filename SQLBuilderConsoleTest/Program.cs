using SQLBuilderConsoleTest.SQLBuilder;
using System;
using System.Collections.Generic;

namespace SQLBuilderConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            SQLBuilder.SQLBuilder builder = new SQLBuilder.SQLBuilder();
            Console.WriteLine(
                builder
                .Select(
                    new Select()
                    .Table("Customers", "c")
                    .Table("Suppliers", "s")
                    .Columns(new Dictionary<string, string> {
                        {"c.City", "CC"},
                        {"s.City", "SC"}
                    })
                    .Union()
                    .Table("Customers", "c")
                    .Table("Suppliers", "s")
                    .Column("c.City", "CC")
                    .Column("s.City", "SC")
                    .Where("xd", "=", "xd")
                    .Where("xd", "=", "xd")
                    .Where("xd", "=", "xd")
                    .Table(
                        new Select()
                        .Table("Customers", "c")
                        .Table("Suppliers", "s")
                        .Column("c.City", "CC")
                        .Column("s.City", "SC")
                        .Where(
                        , "t")
                    , "a")
                .GetQuery(true)
                );
        }
    }
}
