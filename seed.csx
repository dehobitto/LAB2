using Microsoft.Data.Sqlite;

var dbPath = args[0];
using var conn = new SqliteConnection($"Data Source={dbPath}");
conn.Open();

var cmd = conn.CreateCommand();
cmd.CommandText = """
    DELETE FROM Airplanes WHERE RegistrationNumber = '2134';
    INSERT OR IGNORE INTO Airplanes (Model, RegistrationNumber, Capacity, YearManufactured, ManufacturerId, AirlineId) VALUES
        ('Boeing 737-800',     'UR-PSA', 189, 2015, 1, 1),
        ('Boeing 737-800',     'UR-PSB', 189, 2016, 1, 1),
        ('Boeing 767-300ER',   'UR-GEA', 280, 2011, 1, 1),
        ('Airbus A320neo',     'EI-FRI', 186, 2019, 2, 2),
        ('Airbus A320neo',     'EI-FRJ', 186, 2020, 2, 2),
        ('Airbus A321neo',     'EI-GXA', 220, 2021, 2, 2),
        ('Boeing 737 MAX 8',   'EI-HGM', 197, 2023, 1, 2),
        ('Airbus A320ceo',     'HA-LWG', 180, 2014, 2, 3),
        ('Airbus A320ceo',     'HA-LWH', 180, 2015, 2, 3),
        ('Airbus A321ceo',     'HA-LTX', 230, 2018, 2, 3),
        ('Boeing 737-700',     'UR-GBD', 149, 2009, 1, 1),
        ('Embraer E195',       'UR-EMB', 118, 2017, 3, 1),
        ('Airbus A319ceo',     'HA-LPD', 156, 2012, 2, 3),
        ('Boeing 737 MAX 200', 'EI-HGN', 197, 2024, 1, 2),
        ('Embraer E175',       'HA-LKF',  80, 2016, 3, 3);
    """;

int n = cmd.ExecuteNonQuery();
Console.WriteLine($"Inserted: {n} rows");
