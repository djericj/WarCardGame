using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/

internal class Solution
{
    private static readonly Queue p1q = new Queue();
    private static readonly Queue p2q = new Queue();
    private static int index = 1;

    private enum RoundResult
    {
        Unknown,
        Player1Wins,
        Player2Wins,
        War,
        GameOver
    }

    private static void Main(string[] args)
    {
        Log("************* START *************");

        int n = int.Parse(Console.ReadLine()); // the number of cards for player 1
        Log(n.ToString("00") + " ", false);
        for (int i = 0; i < n; i++)
        {
            string cardp1 = Console.ReadLine(); // the n cards of player 1
            Log(cardp1 + " ", false);
            p1q.Enqueue(cardp1);
        }
        Log("");
        int m = int.Parse(Console.ReadLine()); // the number of cards for player 2
        Log(m.ToString("00") + " ", false);
        for (int i = 0; i < m; i++)
        {
            string cardp2 = Console.ReadLine(); // the m cards of player 2
            Log(cardp2 + " ", false);
            p2q.Enqueue(cardp2);
        }
        Console.Error.WriteLine();

        while (true)
        {
            // safety off
            //if (index == 57) break;
            //rounds

            Log("************* Round " + index + " *************");

            Log(p1q.Count.ToString("00") + " ", false);
            PrintValues(p1q);

            Log(p2q.Count.ToString("00") + " ", false);
            PrintValues(p2q);

            var roundResult = Round();
            if (roundResult == RoundResult.GameOver)
            {
                Console.WriteLine("PAT");
                break;
            }

            if (p1q.Count == 0)
            {
                Console.WriteLine("2 " + index);
                break;
            }
            if (p2q.Count == 0)
            {
                Console.WriteLine("1 " + index);
                break;
            }

            index++;
        }
    }

    private static RoundResult Round()
    {
        var cardp1 = p1q.Dequeue() as string;
        var cardp2 = p2q.Dequeue() as string;

        if (CardValue(cardp1) > CardValue(cardp2))
        {
            // player 1 wins
            Log("Player 1 wins Round " + index);
            p1q.Enqueue(cardp1);
            p1q.Enqueue(cardp2);
            return RoundResult.Player1Wins;
        }
        else if (CardValue(cardp2) > CardValue(cardp1))
        {
            // player 2 wins
            Log("Player 2 wins Round " + index);
            p2q.Enqueue(cardp1);
            p2q.Enqueue(cardp2);
            return RoundResult.Player2Wins;
        }
        else if (CardValue(cardp1) == CardValue(cardp2))
        {
            return War(cardp1, cardp2);
        }
        else
        {
            throw new Exception("Could not determine round winner!");
        }
    }

    private static RoundResult War(string playCard1, string playCard2)
    {
        Log("WAR in Round " + index);
        if (p1q.Count < 4)
            return RoundResult.GameOver;

        var p1warQ = CreateWarQueue(p1q);
        var cardp1 = p1q.Dequeue().ToString();
        PrintValues(p1warQ);
        Log(cardp1);

        if (p2q.Count < 4)
            return RoundResult.GameOver;

        var p2warQ = CreateWarQueue(p2q);
        var cardp2 = p2q.Dequeue().ToString();
        PrintValues(p2warQ);
        Log(cardp2);

        // assume 4 cards, 3 face down then 1 turned over.
        if (CardValue(cardp1) > CardValue(cardp2))
        {
            // player 1 wins
            Log("Player 1 wins WAR in Round " + index);
            p1q.Enqueue(playCard1);
            p1q.Enqueue(p1warQ.Dequeue());
            p1q.Enqueue(p1warQ.Dequeue());
            p1q.Enqueue(p1warQ.Dequeue());
            p1q.Enqueue(cardp1);

            p1q.Enqueue(playCard2);
            p1q.Enqueue(p2warQ.Dequeue());
            p1q.Enqueue(p2warQ.Dequeue());
            p1q.Enqueue(p2warQ.Dequeue());
            p1q.Enqueue(cardp2);

            return RoundResult.Player1Wins;
        }
        else if (CardValue(cardp2) > CardValue(cardp1))
        {
            // player 2 wins
            Log("Player 2 wins WAR in Round " + index);

            p2q.Enqueue(playCard1);
            p2q.Enqueue(p1warQ.Dequeue());
            p2q.Enqueue(p1warQ.Dequeue());
            p2q.Enqueue(p1warQ.Dequeue());
            p2q.Enqueue(cardp1);

            p2q.Enqueue(playCard2);
            p2q.Enqueue(p2warQ.Dequeue());
            p2q.Enqueue(p2warQ.Dequeue());
            p2q.Enqueue(p2warQ.Dequeue());
            p2q.Enqueue(cardp2);

            return RoundResult.Player2Wins;
        }
        else if (CardValue(cardp1) == CardValue(cardp2))
        {
            // WAR
            return War(cardp1, cardp2);
        }
        else
        {
            throw new Exception("Could not determine WAR winner!");
        }
    }

    private static Queue CreateWarQueue(Queue inputQ)
    {
        var q = new Queue();
        if (inputQ.Count > 0) q.Enqueue(inputQ.Dequeue());
        if (inputQ.Count > 0) q.Enqueue(inputQ.Dequeue());
        if (inputQ.Count > 0) q.Enqueue(inputQ.Dequeue());
        return q;
    }

    private static int CardValue(string card)
    {
        if (card.StartsWith("A")) return 100;
        else if (card.StartsWith("K")) return 50;
        else if (card.StartsWith("Q")) return 40;
        else if (card.StartsWith("J")) return 20;
        else
        {
            string numericValue = card.Replace("D", "").Replace("H", "").Replace("C", "").Replace("S", "");
            int.TryParse(numericValue, out int val);
            return val;
        }
    }

    private static void PrintValues(IEnumerable myCollection)
    {
        foreach (Object obj in myCollection)
            Console.Error.Write("{0} ", obj);
        Console.Error.WriteLine();
    }

    private static void Log(string message, bool newLine = true)
    {
        if (newLine)
        {
            Console.Error.WriteLine(message);
        }
        else
        {
            Console.Error.Write(message);
        }
    }
}