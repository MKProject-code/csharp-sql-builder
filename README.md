# C# SQL Builder

### Project on hold...

## Example usage

### For build query: 
```
(
        SELECT *
        FROM
        (
                SELECT `ContactName` AS `Contact`,`Country`
                FROM `Customers` AS `c` WHERE `Country`='Germany'
                UNION
                SELECT `ContactName` AS `Contact`,`Country`
                FROM `Suppliers` AS `s`
        ) LIMIT 5
);
```
Use:
```
            SQLBuilder.SQLBuilder builder = new SQLBuilder.SQLBuilder();
            Console.WriteLine(
                builder
                .Select(
                    new Select()
                    .Limit(5)
                    .Table(
                        new Select()
                        .Table("Customers", "c")
                        .Column("ContactName", "Contact")
                        .Column("Country")
                        .Where("Country", "=", "Germany") // not work now!
                        .Union()
                        .Table("Suppliers", "s")
                        .Column("ContactName", "Contact")
                        .Column("Country")
                        )
                    )
                .GetQuery(true) // true = print beautiful formated SQL
                );
```
