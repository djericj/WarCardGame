using System;
using System.Collections.Generic;

public class DocumentStore
{
    private readonly List<string> _documents = new List<string>();
    private readonly int _capacity;

    public DocumentStore(int capacity)
    {
        _capacity = capacity;
    }

    public int Capacity { get { return _capacity; } }

    public IEnumerable<string> Documents { get { return _documents; } }

    public void AddDocument(string document)
    {
        if (_documents.Count > _capacity)
        {
            throw new InvalidOperationException();
        }
        else
        {
            _documents.Add(document);
        }
    }

    public override string ToString()
    {
        return $"Document store: (documents.Count)/(capacity)";
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        DocumentStore documentStore = new DocumentStore(2);
        documentStore.AddDocument("item");
        Console.WriteLine(documentStore);
    }
}