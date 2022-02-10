using System;
public class StackManager {

    public static void Populate() {
        Console.Clear();

        Console.WriteLine("Stack Populate\n");

        var returnStack = AccessDB.returnStack();

        Console.WriteLine("\nEnter the name of a stack below..\n");

        foreach (AccessDB.Records item in returnStack)
        {
            Console.WriteLine($"Stack Name: {item.Name}");
        }

        Console.WriteLine("\n");

        string stackChoice = Console.ReadLine();

        var findId = returnStack.Find(item => item.Name == (stackChoice));

        Console.ReadLine();
    }

        public static void Delete() {

    }

        public static void Rename() {

    }
}

    public class Records { 

        public int Id {get; set;}
        public string Name {get; set;}
    }