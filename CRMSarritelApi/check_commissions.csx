#r "nuget: Npgsql, 8.0.2"

using Npgsql;
using System;

string connStr = "Host=94.143.137.115;Port=5178;Database=sarritel;Username=postgres;Password=lsaknpnacpnoxp;";
using (var conn = new NpgsqlConnection(connStr)) {
    conn.Open();

    using (var cmd = new NpgsqlCommand(@"
        SELECT c.""Id"", c.""VentaId"", c.""EmpleadoId"", u.""Nombre"" as Empleado, c.""MontoComision""
        FROM ""Comisiones"" c
        JOIN ""Usuarios"" u ON c.""EmpleadoId"" = u.""Id""
        ORDER BY c.""Id"" DESC
        LIMIT 10
    ", conn)) {
        using (var reader = cmd.ExecuteReader()) {
            Console.WriteLine("Últimas Comisiones:");
            while (reader.Read())
            {
                var ventaIdStr = reader.IsDBNull(1) ? "NULL" : reader.GetInt32(1).ToString();
                Console.WriteLine($"- ID {reader.GetInt32(0)}, Venta {ventaIdStr}, Empleado: {reader.GetString(3)} (ID {reader.GetInt32(2)}), Monto: {reader.GetDecimal(4)}");
            }
        }
    }
}
