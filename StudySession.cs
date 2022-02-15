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
        var dateNow = dateAndTime.Date.ToString("yyyy-MM-dd");

        Console.WriteLine($"Study Complete! On {dateNow} you scored {Counter} out of {Amount}.\n");

        Controller.saveSession(foundID, Score, dateNow);

        Console.WriteLine($"Press any key to return to Main Menu\n");

        Console.ReadLine();
    }

    public static void Results() {
        Console.Clear();

        Console.WriteLine("Study Results\n");

        var studyProgress = Controller.retrieveProgress();

        var values = new[] {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"};

        var monthCount = values.Select(v => new {Month = v,
            Count = studyProgress.Count(item => item.Month == v)
            });

        foreach (var x in monthCount)
        {
            decimal Score = 0.0M;
            decimal Counter = 0.0M;
            foreach (Controller.StudyData item in studyProgress)
            {
                if(x.Month == item.Month) {
                    Score = Score + item.Score;
                    Counter = Counter + 1;
                }
            }

            float ScoreFloat = Convert.ToSingle(Score);
            float CounterFloat = Convert.ToSingle(Counter);

            float totalScore = ScoreFloat / CounterFloat * 100;

            if(x.Count > 0) {
                Console.WriteLine($"You studied {x.Count} time(s) in {x.Month}, with an average score of {totalScore}%");
            }
        }

        Console.WriteLine("\nPress any key to return to main menu");

        Console.ReadLine();
    }
}