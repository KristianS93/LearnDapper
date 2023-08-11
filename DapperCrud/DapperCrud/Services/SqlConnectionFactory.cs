using System.Data.SQLite;

namespace DapperCrud.Services;

public class SqlConnectionFactory
{
    private readonly string _connectionString;

    public SqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public SQLiteConnection Create()
    {
        return new SQLiteConnection(_connectionString);
    }
}