using System;
public class StackManager {

    public static void Populate() {
        Console.Clear();

        Console.WriteLine("Stack Populate\n");

        var returnStack = Controller.returnStack();

        Console.WriteLine("\nEnter the name of a stack below..\n");

        foreach (Controller.Records item in returnStack)
        {
            Console.WriteLine($"Stack Name: {item.Name}");
        }

        Console.WriteLine("\n");

        string stackChoice = Console.ReadLine();

        var findId = returnStack.Find(item => item.Name == (stackChoice));

        int foundID = findId.Id;

        bool closeApp = false;
        while(closeApp == false) {
            Console.Clear();
            Console.WriteLine($"Add to Stack: {stackChoice}");

            Console.WriteLine("\nPress 1 to add a card. Press 0 to quit\n");

            string questionApp = Console.ReadLine();

            switch(questionApp) {
                case "0":
                closeApp = true;
                break;
                case "1":
                    cardGenerator(stackChoice, foundID);
                break;
            }
        }
    }

    public static void Delete() {
        Console.Clear();

        Console.WriteLine("Stack Delete\n");

        Console.WriteLine("Enter the ID of a stack from the list below to delete..\n");

        var returnStack = Controller.ReindexedStack();

        foreach (Controller.Records item in returnStack)
        {
            Console.WriteLine($"{item.Id}: {item.Name}");
        }

        Console.WriteLine("\n");

        int deletionChoice = Convert.ToInt32(Console.ReadLine());

        var findName = returnStack.Find(item => item.Id == (deletionChoice));

        string foundName = findName.Name;

        Controller.deleteStack(foundName);
    }

    public static void Rename() {
        Console.Clear();

        Console.WriteLine("Stack Delete\n");

        Console.WriteLine("Enter the name of a stack from the list below to Rename..\n");

        var returnStack = Controller.returnStack();

        foreach (Controller.Records item in returnStack)
        {
            Console.WriteLine($"Stack Name: {item.Name}");
        }

        Console.WriteLine("\n");

        string renameChoice = Console.ReadLine();

        var findId = returnStack.Find(item => item.Name == (renameChoice));

        int foundID = findId.Id;

        Console.WriteLine("\nEnter New Name...\n");

        string newName = Console.ReadLine();

       Controller.renameStack(foundID, newName);

       Console.WriteLine("\nStack Renamed. Press any key to RETURN to Main Menu");

       Console.ReadLine();
    }

    public static void cardGenerator(string stackChoice, int foundID) {
        Console.WriteLine($"\nAdd to Stack: {stackChoice}");

        Console.WriteLine("Add a Question to your Card:\n");

        string cardQuestion = Console.ReadLine();

        Console.WriteLine("Add an Answer to your Card:\n");

        string cardAnswer = Console.ReadLine();

        Controller.addCards(cardQuestion, cardAnswer, foundID);

        Console.WriteLine("Card Created. Press Any key to return.");
    }
}