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
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'study')
            CREATE TABLE study (id INTEGER PRIMARY KEY IDENTITY, stackID INT, score DECIMAL(4,2), date TEXT);
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

            List<Records> stackData = new();

            SqlDataReader reader = getData.ExecuteReader();

            if (reader.HasRows) {
                while (reader.Read()) {
                    stackData.Add(
                        new Records {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        }); 
                }
            } else {
                Console.WriteLine("No Stacks Exist");
            }

            connection.Close();

            return stackData;
        } 
    }

    public static List<Records> ReindexedStack() {

        List<Records> stackData = returnStack();

        List<Records> ReindexedStack = new();

        for(int i = 0; i < stackData.Count; i++) {
                ReindexedStack.Add(
                    new Records {
                        Id = i + 1,
                        Name = stackData[i].Name
                    });
        }

        return ReindexedStack;
    }

    public static List<Cards> returnCards() {
        using(var connection = new SqlConnection(connectionString)) {
            connection.Open();

            var getData = connection.CreateCommand();

            getData.CommandText = $"SELECT * FROM cards";

            List<Cards> cardData = new();

            SqlDataReader reader = getData.ExecuteReader();

            if(reader.HasRows) {
                while (reader.Read()) {
                    cardData.Add(
                        new Cards {
                            Id = reader.GetInt32(0),
                            Question = reader.GetString(1),
                            Answer = reader.GetString(2),
                            StackId = reader.GetInt32(3)
                        }); 
                }
            } else {
                Console.WriteLine("No Cards Exist");
            }

            connection.Close();

            return cardData;
        }
    }

    public static void saveSession(int foundID, decimal Score, string Date) {
        using(var connection = new SqlConnection(connectionString)) {
            connection.Open();

            var sendScore = connection.CreateCommand();

            sendScore.CommandText = $"INSERT INTO study (stackID, score, date) VALUES('{foundID}', '{Score}', '{Date}')";

            sendScore.ExecuteNonQuery();
        }
    }

    public static void addCards(string question, string answer, int foundID) {
        using(var connection = new SqlConnection(connectionString)) {
            connection.Open();
        
            var addData = connection.CreateCommand();

            addData.CommandText = $"INSERT INTO cards (question, answer, stackID) VALUES ('{question}', '{answer}', '{foundID}')";

            addData.ExecuteNonQuery();
        }
    }

    public static void deleteStack(string deletionChoice) {
        using(var connection = new SqlConnection(connectionString)) {
            connection.Open();
        
            var deleteData = connection.CreateCommand();

            List<Records> stackData = returnStack();

            var findItem = stackData.Find(item => item.Name == (deletionChoice));

            int itemId = findItem.Id;

            deleteData.CommandText = $"DELETE FROM stacks WHERE ID='{itemId}'";

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

    // OBJECTS

    public class Records { 
        public int Id {get; set;}
        public string Name {get; set;}
    }

    public class Cards {
        public int Id {get; set;}
        public string Question {get; set;}
        public string Answer {get; set;}
        public int StackId {get; set;}        
    }
}

    