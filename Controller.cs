using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ConsoleTableExt;

public class Controller {

    static string connectionString = @"Server=localhost,1433; Database=flashcards; User=sa; Password=someThingComplicated1234";
    public static void GenerateTables() {
       using(var connection = new SqlConnection(connectionString)) {
            connection.Open();

            var createTables = connection.CreateCommand();

            createTables.CommandText = @"
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'stacks')
            CREATE TABLE stacks (id INTEGER PRIMARY KEY IDENTITY, name TEXT);
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'cards')
            CREATE TABLE cards (id INTEGER PRIMARY KEY IDENTITY, question TEXT, answer TEXT, stackID INT, FOREIGN KEY (stackID) REFERENCES stacks(id) ON DELETE CASCADE);
            ";

            createTables.ExecuteNonQuery();

            connection.Close();
        }      
    } 

    public static void createStack(string stackName) {
        using(var connection = new SqlConnection(connectionString)) {
            connection.Open();

            var sendStack = connection.CreateCommand();

            sendStack.CommandText = $"INSERT INTO stacks(name) VALUES('{stackName}')";

            sendStack.ExecuteNonQuery();
        }
    }

    public static List<Records> returnStack() {

        using(var connection = new SqlConnection(connectionString)) {
            connection.Open();

            var getData = connection.CreateCommand();

            getData.CommandText = $"SELECT * FROM stacks";

            List<Records> tableData = new();

            SqlDataReader reader = getData.ExecuteReader();

            if (reader.HasRows) {
                while (reader.Read()) {
                    tableData.Add(
                        new Records {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        }); 
                }
            } else {
                Console.WriteLine("No Stacks Exist");
            }

            connection.Close();

            return tableData;
        } 
    }

    public class Records { 
        public int Id {get; set;}
        public string Name {get; set;}
    }

    public static void addCards(string question, string answer, int foundID) {
        using(var connection = new SqlConnection(connectionString)) {
            connection.Open();
        
            var addData = connection.CreateCommand();

            addData.CommandText = $"INSERT INTO cards (question, answer, stackID) VALUES ('{question}', '{answer}', '{foundID}')";

            addData.ExecuteNonQuery();
        }
    }

    public static void deleteStack(int foundID) {
        using(var connection = new SqlConnection(connectionString)) {
            connection.Open();
        
            var deleteData = connection.CreateCommand();

            deleteData.CommandText = $"DELETE FROM stacks WHERE id={foundID}";

            deleteData.ExecuteNonQuery();
        }
    }

    public static void renameStack(int foundID, string newname) {
        using(var connection = new SqlConnection(connectionString)) {
            connection.Open();
        
            var renameStack = connection.CreateCommand();

            renameStack.CommandText = $"UPDATE stacks SET name = '{newname}' WHERE id = {foundID}";

            renameStack.ExecuteNonQuery();
        }
    }
}

    