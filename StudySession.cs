using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleTableExt;

public class studySession {

    public static void Study() {
        Console.Clear();

        Console.WriteLine("Study Session\n");

        var returnStack = Controller.returnStack();

        Console.WriteLine("\nEnter the name of a stack below..\n");

        // CREATE LIST OF CARDS BASED ON STACK ID

        foreach (Controller.Records item in returnStack)
        {
            Console.WriteLine($"Stack Name: {item.Name}");
        }

        Console.WriteLine("\n");

        string stackChoice = Console.ReadLine();

        var findId = returnStack.Find(item => item.Name == (stackChoice));

        int foundID = findId.Id;

        var returnCards = Controller.returnCards();

        if(returnCards != null) {

            List<Controller.Cards> Cards = new();

                foreach (Controller.Cards item in returnCards)
                {
                    if(item.StackId == foundID) {
                        Cards.Add(item);
                    }
                }

            if(Cards.Count != 0) {

            // QUESTION AND ANSWER

                decimal Counter = 0;
                decimal Amount = Cards.Count;

                foreach (Controller.Cards item in Cards)
                {
                    Console.WriteLine($"Question: {item.Question}\n");

                    string Answer = Console.ReadLine();

                    if(Answer == item.Answer) {
                        Counter++;
                    }
                }

                decimal Score = Counter / Amount;

                var dateAndTime = DateTime.Now;
                var dateNow = dateAndTime.Date;

                Console.WriteLine($"Study Complete! On {dateNow.ToString("dd-MM-yyyy")} you scored {Counter} out of {Amount}.\n");

                Controller.saveSession(foundID, Score);

                Console.WriteLine($"Press any key to return to Main Menu\n");

                Console.ReadLine();

            } else {
            Console.WriteLine("\nThis stack holds no cards, Press ENTER to populate the stack.\n");

            Console.ReadLine();

            StackManager.Populate();
            }

        } else {
            Console.WriteLine("\nNo stacks hold any cards, Press ENTER to populate a stack.\n");

            Console.ReadLine();

            StackManager.Populate();
        }
    }

    public static void Results() {
        Console.Clear();

        Console.WriteLine("Study Results\n");

        

        Console.ReadLine();
    }
}