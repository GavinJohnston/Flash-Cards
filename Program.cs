using System;
using System.Data.SqlClient;

namespace flashcards
{
    class Program
    {
        static string connectionString = @"Server=localhost,1433; User=sa; Password=someThingComplicated1234";
        static void Main(string[] args)
        {
            using(var connection = new SqlConnection(connectionString)) {
                connection.Open();

                var createDb = connection.CreateCommand();

                createDb.CommandText = @"
                IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Flashcards')
                BEGIN
                CREATE DATABASE Flashcards;
                END;";

                createDb.ExecuteNonQuery();

                connection.Close();

                AccessDB.GenerateTables();
            }      

            userInput.mainMenu();      
        }
    }
}
