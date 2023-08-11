using System.Data.SQLite;
using Dapper;
using DapperCrud.Models;
using DapperCrud.Services;

namespace DapperCrud.Endpoints;

public static class CustomerEndpoints
{
    public static void MapCustomerEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("customers", async (SqlConnectionFactory sqlConnectionFactory) =>
        {
            using var connection = sqlConnectionFactory.Create();

            const string sqlCommand = "SELECT * FROM Customers";

            var customers = await connection.QueryAsync<Customer>(sqlCommand);

            return Results.Ok(customers);
        });

        builder.MapGet("customers/{id}", async (int id, SqlConnectionFactory sqlConnectionFactory) =>
        {
            using var connection = sqlConnectionFactory.Create();

            const string sqlCommand = """
                                      SELECT * 
                                      FROM Customers
                                      WHERE Id = @CustomerId
                                      """;

            var customer = await connection.QuerySingleOrDefaultAsync<Customer>(sqlCommand, new {CustomerId = id});

            return customer != null ? Results.Ok(customer) : Results.NotFound();
        });

        builder.MapPost("customers", async (Customer customer, SqlConnectionFactory sqlConnectionFactory) =>
        {
            using var connection = sqlConnectionFactory.Create();

            const string sqlCommand = """
                                        INSERT INTO Customers
                                        (FirstName, LastName, Email, DateOfBirth)
                                        VALUES 
                                        (@FirstName, @LastName, @Email, @DateOfBirth)
                                      """;
            await connection.ExecuteAsync(sqlCommand, customer);

            return Results.Ok();
        });

        builder.MapPut("customers/{id}", async (Customer customer, SqlConnectionFactory sqlConnectionFactory, int id) =>
        {
            using var connection = sqlConnectionFactory.Create();

            customer.Id = id;
            
            const string sqlCommand = """
                                      UPDATE Customers
                                      SET 
                                          FirstName = @FirstName,
                                          LastName = @LastName,
                                          Email = @Email,
                                          DateOfBirth = @DateOfBirth
                                        WHERE Id = @Id
                                      """;
            await connection.ExecuteAsync(sqlCommand, customer);

            return Results.NoContent();
        });

        builder.MapDelete("customers/{id}", async (int id, SqlConnectionFactory sqlConnectionFactory) =>
        {
            using var connection = sqlConnectionFactory.Create();

            const string sqlCommand = """
                                      DELETE FROM Customers
                                      WHERE Id = @CustomerId
                                      """;
            
            await connection.ExecuteAsync(sqlCommand, new { CustomerId = id});

            return Results.NoContent();
        });
    }
}