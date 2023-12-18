using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace SeaBaseAPI.IntegrationTests.ServiceTests;

public class TestUsingSqlite : IDisposable
{
    private const string ConnectionString = "Data Source=:memory:";
    private readonly SqliteConnection _connection;

    protected readonly SeaBaseContext Context;

    protected TestUsingSqlite()
    {
        _connection = new SqliteConnection(ConnectionString);
        _connection.Open();
        var options = new DbContextOptionsBuilder<SeaBaseContext>()
            .UseSqlite(_connection)
            .Options;
        Context = new SeaBaseContext(options);
        Context.Database.EnsureCreated();
    }

    public void Dispose()
    {
        _connection.Close();
        GC.SuppressFinalize(this);
    }
}