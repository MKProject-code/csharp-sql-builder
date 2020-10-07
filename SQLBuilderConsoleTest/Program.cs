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
                    .Limit(5)
                    .Table(
                        new Select()
                        .Table("Customers", "c")
                        .Column("c.ContactName", "Contact")
                        .Column("c.Country")
                        .Where("Country", "=", "Germany") // not work now!
                        .Union()
                        .Table("Suppliers", "s")
                        .Column("s.ContactName", "Contact")
                        .Column("s.Country")
                        )
                    )
                .GetQuery(true)
                );

                // more example
                //new Select()
                //.Table("customers", "c")
                //.Columns(new Dictionary<string, string> {
                //    {"c.FirstName", null},
                //    {"c.LastName", null},
                //    {"a.ProductName", null}
                //})

                //new Select()
                //.Table("Orders", "o")
                //.Column("o.CustomerId", "c_id")
                //.Column("o.ProductId", "p_id")
                //.Limit(1)
                //.Union()
                //.Column("so.c_id", "CustomerId")
                //.Column("so.p_id", "ProductId")
                //.Table(
                //    new Select()
                //    .Table("Orders", "o")
                //    .Column("o.CustomerId", "c_id")
                //    .Column("o.ProductId", "p_id")
                //    .Limit(1)
                //    , "so")
                //)
        }
    }
}
