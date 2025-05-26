// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;

class CybersecurityChatbot
{
    static Dictionary<string, List<string>> keywordResponses = new Dictionary<string, List<string>>()
    {
        { "password", new List<string> {
            "Use strong, unique passwords for each account.",
            "Avoid using personal details in your passwords.",
            "Consider using a password manager to keep track of your passwords."
        }},
        { "scam", new List<string> {
            "Watch out for emails asking for personal information.",
            "Scammers often impersonate trusted organizations. Always verify.",
            "Never click on suspicious links or download unknown attachments."
        }},
        { "privacy", new List<string> {
            "Review your privacy settings on social media regularly.",
            "Avoid sharing too much personal information online.",
            "Use encrypted messaging apps for private communication."
        }},
        { "phishing", new List<string> {
            "Be cautious of urgent requests in emails.",
            "Check the sender's email address closely — it may be fake.",
            "Don’t click on links in unsolicited emails. Type the URL directly instead."
        }}
    };

    static List<string> sentimentKeywords = new List<string> { "worried", "curious", "frustrated" };
    static string userName = "";
    static string userInterest = "";

    static void Main(string[] args)
    {
        Console.WriteLine("Hello! I'm your Cybersecurity Awareness Bot. What's your name?");
        userName = Console.ReadLine();
        Console.WriteLine($"Nice to meet you, {userName}! What cybersecurity topic interests you most?");
        userInterest = Console.ReadLine();

        Console.WriteLine($"Great! I'll remember that you're interested in {userInterest}.\n");

        while (true)
        {
            Console.Write("You: ");
            string userInput = Console.ReadLine().ToLower();

            if (userInput == "exit" || userInput == "quit")
            {
                Console.WriteLine("Chatbot: Stay safe online! Goodbye!");
                break;
            }

            // Sentiment Detection
            if (DetectSentiment(userInput, out string sentiment))
            {
                Console.WriteLine(GetSentimentResponse(sentiment));
                continue;
            }

            // Keyword Recognition
            bool matched = false;
            foreach (var keyword in keywordResponses.Keys)
            {
                if (userInput.Contains(keyword))
                {
                    matched = true;
                    var responses = keywordResponses[keyword];
                    var random = new Random();
                    Console.WriteLine("Chatbot: " + responses[random.Next(responses.Count)]);

                    // Personalised follow-up
                    if (!string.IsNullOrWhiteSpace(userInterest) && userInput.Contains(userInterest))
                    {
                        Console.WriteLine($"Chatbot: As someone interested in {userInterest}, this is especially important.");
                    }

                    break;
                }
            }

            // Unknown input
            if (!matched)
            {
                Console.WriteLine("Chatbot: I'm not sure I understand. Can you try rephrasing?");
            }
        }
    }

    static bool DetectSentiment(string input, out string sentiment)
    {
        foreach (var word in sentimentKeywords)
        {
            if (input.Contains(word))
            {
                sentiment = word;
                return true;
            }
        }
        sentiment = "";
        return false;
    }

    static string GetSentimentResponse(string sentiment)
    {
        switch (sentiment)
        {
            case "worried":
                return "Chatbot: It's completely understandable to feel that way. Let me help you stay safe.";
            case "frustrated":
                return "Chatbot: Cybersecurity can be tricky, but you're doing great. Ask me anything!";
            case "curious":
                return "Chatbot: Curiosity is great! Learning about cybersecurity is a smart move.";
            default:
                return "Chatbot: Thanks for sharing how you feel.";
        }
    }
}
