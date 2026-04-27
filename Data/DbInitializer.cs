using Dapper;

namespace AirlineApp.Data;

public static class DbInitializer
{
    public static void Initialize(Database db)
    {
        using var conn = db.CreateConnection();
        conn.Open();

        conn.Execute("""
            CREATE TABLE IF NOT EXISTS Manufacturers (
                Id      INTEGER PRIMARY KEY AUTOINCREMENT,
                Name    TEXT NOT NULL,
                Country TEXT NOT NULL
            );
            """);

        conn.Execute("""
            CREATE TABLE IF NOT EXISTS Airlines (
                Id      INTEGER PRIMARY KEY AUTOINCREMENT,
                Name    TEXT NOT NULL,
                Country TEXT NOT NULL
            );
            """);

        conn.Execute("""
            CREATE TABLE IF NOT EXISTS Airplanes (
                Id                 INTEGER PRIMARY KEY AUTOINCREMENT,
                Model              TEXT    NOT NULL,
                RegistrationNumber TEXT    NOT NULL UNIQUE,
                Capacity           INTEGER NOT NULL,
                YearManufactured   INTEGER NOT NULL,
                ManufacturerId     INTEGER NOT NULL REFERENCES Manufacturers(Id),
                AirlineId          INTEGER NOT NULL REFERENCES Airlines(Id)
            );
            """);

        // seed only if empty
        if (conn.ExecuteScalar<int>("SELECT COUNT(*) FROM Manufacturers") == 0)
        {
            conn.Execute("""
                INSERT INTO Manufacturers (Name, Country) VALUES
                    ('Boeing',  'USA'),
                    ('Airbus',  'France'),
                    ('Embraer', 'Brazil');
                """);
        }

        if (conn.ExecuteScalar<int>("SELECT COUNT(*) FROM Airlines") == 0)
        {
            conn.Execute("""
                INSERT INTO Airlines (Name, Country) VALUES
                    ('Ukraine International Airlines', 'Ukraine'),
                    ('Ryanair',  'Ireland'),
                    ('Wizz Air', 'Hungary');
                """);
        }
    }
}
