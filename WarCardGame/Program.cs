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
    private static int index = 0;

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
        int n = int.Parse(Console.ReadLine()); // the number of cards for player 1
        Console.Error.Write(n + " ");
        for (int i = 0; i < n; i++)
        {
            string cardp1 = Console.ReadLine(); // the n cards of player 1
            Console.Error.Write(cardp1 + " ");
            p1q.Enqueue(cardp1);
        }
        Console.Error.WriteLine();
        int m = int.Parse(Console.ReadLine()); // the number of cards for player 2
        Console.Error.Write(m + " ");
        for (int i = 0; i < m; i++)
        {
            string cardp2 = Console.ReadLine(); // the m cards of player 2
            Console.Error.Write(cardp2 + " ");
            p2q.Enqueue(cardp2);
        }
        Console.Error.WriteLine();

        while (true)
        {
            // safety off
            //if (index == 53) break;
            //rounds
            index++;
            Console.Error.WriteLine("Round " + index);

            Console.Error.Write(p1q.Count + " ");
            PrintValues(p1q);

            Console.Error.Write(p2q.Count + " ");
            PrintValues(p2q);

            var roundResult = Round(p1q.Dequeue() as string, p2q.Dequeue() as string);
            if (roundResult == RoundResult.War)
            {
                // WAR
                roundResult = War();
            }
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
        }
    }

    private static RoundResult Round(string cardp1, string cardp2)
    {
        if (CardValue(cardp1) > CardValue(cardp2))
        {
            // player 1 wins
            Console.Error.WriteLine("Player 1 wins Round " + index);
            p1q.Enqueue(cardp1);
            p1q.Enqueue(cardp2);
            return RoundResult.Player1Wins;
        }
        else if (CardValue(cardp2) > CardValue(cardp1))
        {
            // player 2 wins
            Console.Error.WriteLine("Player 2 wins Round " + index);
            p2q.Enqueue(cardp1);
            p2q.Enqueue(cardp2);
            return RoundResult.Player2Wins;
        }
        else if (CardValue(cardp1) == CardValue(cardp2))
        {
            return RoundResult.War;
        }
        else
        {
            throw new Exception("Could not determine round winner!");
        }
    }

    private static RoundResult War()
    {
        Console.Error.WriteLine("WAR in Round " + index);
        if (p1q.Count < 4)
        {
            return RoundResult.GameOver;
        }
        var p1warQ = CreateWarQueue(p1q);
        var cardp1 = p1q.Dequeue().ToString();
        PrintValues(p1warQ);
        Console.Error.WriteLine(cardp1);

        if (p2q.Count < 4)
        {
            return RoundResult.GameOver;
        }
        var p2warQ = CreateWarQueue(p2q);
        var cardp2 = p2q.Dequeue().ToString();
        PrintValues(p2warQ);
        Console.Error.WriteLine(cardp2);
        // assume 4 cards, 3 face down then 1 turned over.
        if (CardValue(cardp1) > CardValue(cardp2))
        {
            // player 1 wins
            Console.Error.WriteLine("Player 1 wins WAR in Round " + index);
            p1q.Enqueue(p1warQ.Dequeue());
            p1q.Enqueue(p1warQ.Dequeue());
            p1q.Enqueue(p1warQ.Dequeue());
            p1q.Enqueue(cardp1);

            p1q.Enqueue(p2warQ.Dequeue());
            p1q.Enqueue(p2warQ.Dequeue());
            p1q.Enqueue(p2warQ.Dequeue());
            p1q.Enqueue(cardp2);
            return RoundResult.Player1Wins;
        }
        else if (CardValue(cardp2) > CardValue(cardp1))
        {
            // player 2 wins
            Console.Error.WriteLine("Player 2 wins WAR in Round " + index);
            p2q.Enqueue(p2warQ.Dequeue());
            p2q.Enqueue(p2warQ.Dequeue());
            p2q.Enqueue(p2warQ.Dequeue());
            p2q.Enqueue(cardp2);

            p2q.Enqueue(p1warQ.Dequeue());
            p2q.Enqueue(p1warQ.Dequeue());
            p2q.Enqueue(p1warQ.Dequeue());
            p2q.Enqueue(cardp1);
            return RoundResult.Player2Wins;
        }
        else if (CardValue(cardp1) == CardValue(cardp2))
        {
            // WAR
            return War();
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

    public static void PrintValues(IEnumerable myCollection)
    {
        foreach (Object obj in myCollection)
            Console.Error.Write("{0} ", obj);
        Console.Error.WriteLine();
    }
}