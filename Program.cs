using System.Data;
using Microsoft.Data.SqlClient;
using System.Configuration;
using static System.Console;

WriteLine("Stored Procedures");

Write("Enter id: ");
int authorId = int.Parse(Console.ReadLine());

SqlConnection connection = null;

try
{
    var connectionString =
        ConfigurationManager.AppSettings.Get("connectionString");
    connection = new SqlConnection(connectionString);
    connection.Open();

    string procName =
        "getAuthorBookNumber";
    SqlCommand command =
        new SqlCommand(procName, connection);
    command.CommandType = CommandType.StoredProcedure;
    command.Parameters.AddWithValue("@AuthorId", authorId);
    command.Parameters.Add
        ("@BookCount", System.Data.SqlDbType.Int).Direction = 
        ParameterDirection.Output;
    command.ExecuteNonQuery();

    WriteLine($"Book Count = {command.Parameters[1].Value}");
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    if(connection is not null)
    {
        connection.Close();
    }
    ReadKey();
}
