# C# SQL Builder

### Project on hold...

## Example usage

### Build SQL query:
```
SQLBuilder.SQLBuilder builder = new SQLBuilder.SQLBuilder();

string sql = builder
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
        .GetQuery(true); // true = return beautiful formated SQL

Console.WriteLine(sql);
```
### Result:
```
(
        SELECT *
        FROM
        (
                SELECT `c`.`ContactName` AS `Contact`,`c`.`Country`
                FROM `Customers` AS `c` WHERE `Country`='Germany'
                UNION
                SELECT `s`.`ContactName` AS `Contact`,`s`.`Country`
                FROM `Suppliers` AS `s`
        ) LIMIT 5
);
```
