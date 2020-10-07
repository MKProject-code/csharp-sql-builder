# C# SQL Builder

### Project on hold...

## Example usage

### For build query: 
`
(
        SELECT *
        FROM
        (
                SELECT `ContactName` AS `Contact`,`Country`
                FROM `Customers` AS `c`
                UNION
                SELECT `ContactName` AS `Contact`,`Country`
                FROM `Suppliers` AS `s`
        ) LIMIT 5
);
`

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
