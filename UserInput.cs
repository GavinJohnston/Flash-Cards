using System;

public class userInput {

    public static void mainMenu() {

        bool closeApp = false;
        while(closeApp == false) {
            Console.Clear();
            Console.WriteLine("Welcome to Flashcards. Select an option below...");
            Console.WriteLine("\nPress 1 To Create a new stack");
            Console.WriteLine("\nPress 2 To Manage a stack");
            Console.WriteLine("\nPress 0 To Close\n");

            string userInput = Console.ReadLine();

            switch(userInput) {
                case "0":
                    Console.Clear();  
                    Console.WriteLine("Goodbye..");
                    closeApp = true;
                break;
                case "1":
                    GenerateStacks();
                break;
                case "2":
                    ManageStacks();
                break;
            }
        }
    }

    public static void GenerateStacks() {
        Console.Clear();

        Console.WriteLine("Stack Creator");

        Console.WriteLine("\nWhat would you like to call your stack?\n");

        string stackName = Console.ReadLine();

        Controller.createStack(stackName);

        Console.WriteLine("\nStack Created. Press ENTER to manage your stack\n");

        // Console.ReadLine();
    }

    public static void ManageStacks() {
        Console.Clear();

        Console.WriteLine("Stack Manager");

        Console.WriteLine("\nPress 1 to populate a Stack");

        Console.WriteLine("\nPress 2 to delete a Stack");

        Console.WriteLine("\nPress 3 to rename a Stack");

        Console.WriteLine("\nPress 4 to Study a Stack");

        Console.WriteLine("\nPress 5 to View Progress");

        Console.WriteLine("\nPress 0 to return to Main Menu\n");

        string manageOption = Console.ReadLine();

        switch(manageOption) {
            case "0":
                //main menu
            break;
            case "1":
                StackManager.Populate();
            break;
            case "2":
                StackManager.Delete();
            break;
            case "3":
                StackManager.Rename();
            break;  
            case "4":
                studySession.Study();
            break;
            case "5":
                studySession.Results();
            break;     
        }
    }
}
