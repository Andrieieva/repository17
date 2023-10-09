using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Dictionary<string, int> votingOptions = new Dictionary<string, int>();
        bool votingOpen = true;

        Console.WriteLine("Welcome to the Voting System!");

        while (votingOpen)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Create a New Voting Topic");
            Console.WriteLine("2. Vote");
            Console.WriteLine("3. View Results");
            Console.WriteLine("4. Exit");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateVotingTopic(votingOptions);
                    break;
                case "2":
                    Vote(votingOptions);
                    break;
                case "3":
                    ViewResults(votingOptions);
                    break;
                case "4":
                    votingOpen = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }
        }

        Console.WriteLine("Thank you for using the Voting System!");
    }

    static void CreateVotingTopic(Dictionary<string, int> options)
    {
        Console.Write("Enter the voting topic: ");
        string topic = Console.ReadLine();

        if (options.ContainsKey(topic))
        {
            Console.WriteLine("This topic already exists.");
        }
        else
        {
            Console.Write("Enter the number of options: ");
            if (int.TryParse(Console.ReadLine(), out int optionCount) && optionCount > 0)
            {
                List<string> optionList = new List<string>();

                for (int i = 1; i <= optionCount; i++)
                {
                    Console.Write($"Enter option {i}: ");
                    string option = Console.ReadLine();
                    optionList.Add(option);
                    options.Add(option, 0);
                }

                Console.WriteLine($"Voting topic '{topic}' with {optionCount} options created successfully!");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number of options.");
            }
        }
    }

    static void Vote(Dictionary<string, int> options)
    {
        Console.WriteLine("Available Voting Topics:");
        int topicCount = 1;

        foreach (string topic in options.Keys.Distinct())
        {
            Console.WriteLine($"{topicCount}. {topic}");
            topicCount++;
        }

        Console.Write("Select a topic to vote: ");
        if (int.TryParse(Console.ReadLine(), out int topicIndex) && topicIndex > 0 && topicIndex <= options.Keys.Distinct().Count())
        {
            string selectedTopic = options.Keys.Distinct().ElementAt(topicIndex - 1);

            Console.WriteLine($"Options for '{selectedTopic}':");
            int optionCount = 1;

            foreach (var option in options.Where(kvp => kvp.Key == selectedTopic))
            {
                Console.WriteLine($"{optionCount}. {option.Key}");
                optionCount++;
            }

            Console.Write("Select an option to vote: ");
            if (int.TryParse(Console.ReadLine(), out int optionIndex) && optionIndex > 0 && optionIndex <= optionCount - 1)
            {
                string selectedOption = options.Where(kvp => kvp.Key == selectedTopic).ElementAt(optionIndex - 1).Key;
                options[selectedOption]++;
                Console.WriteLine($"You voted for '{selectedOption}'.");
            }
            else
            {
                Console.WriteLine("Invalid option selection. Please enter a valid option.");
            }
        }
        else
        {
            Console.WriteLine("Invalid topic selection. Please enter a valid topic.");
        }
    }

    static void ViewResults(Dictionary<string, int> options)
    {
        Console.WriteLine("\nVoting Results:");

        if (options.Count == 0)
        {
            Console.WriteLine("No voting topics available.");
        }
        else
        {
            foreach (var topic in options.Keys.Distinct())
            {
                Console.WriteLine($"Topic: {topic}");
                foreach (var option in options.Where(kvp => kvp.Key == topic))
                {
                    Console.WriteLine($"{option.Key}: {option.Value} votes");
                }
                Console.WriteLine();
            }
        }
    }
}
