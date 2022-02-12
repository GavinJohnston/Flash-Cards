using System;
using System.Collections.Generic;

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

        List<Controller.Cards> Cards = new();

        foreach (Controller.Cards item in returnCards)
        {
            if(item.StackId == foundID) {
                Cards.Add(item);
            }
        }

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
        var dateNow = dateAndTime.Date.ToString("dd/MM/yyyy");

        Console.WriteLine($"Study Complete! On {dateNow} you scored {Counter} out of {Amount}.\n");

        Controller.saveSession(foundID, Score, dateNow);

        Console.WriteLine($"Press any key to return to Main Menu\n");

        Console.ReadLine();
    }
}