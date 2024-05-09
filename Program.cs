using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Pages { get; set; }
    public string Genre { get; set; }
    public string Publisher { get; set; }
    private string _isbn;
    public string ISBN
    {
        get { return _isbn; }
        set
        {
            if (Regex.IsMatch(value, @"^\d{3}-\d{10}$"))
            {
                _isbn = value;
            }
            else
            {
                throw new ArgumentException("Invalid ISBN format.");
            }
        }
    }

    public Book(string title, string author, int pages, string genre, string publisher, string isbn)
    {
        Title = title;
        Author = author;
        Pages = pages;
        Genre = genre;
        Publisher = publisher;
        ISBN = isbn;
    }

    public static void WriteToFile(List<Book> books, string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var book in books)
            {
                writer.WriteLine($"{book.Title},{book.Author},{book.Pages},{book.Genre},{book.Publisher},{book.ISBN}");
            }
        }
    }

    public static List<Book> ReadFromFile(string filePath)
    {
        List<Book> books = new List<Book>();
        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(',');
                books.Add(new Book(parts[0], parts[1], int.Parse(parts[2]), parts[3], parts[4], parts[5]));
            }
        }
        return books;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Program 1: Properties, Regex, Generics, File I/O

        // Create Book instances
        Book book1 = new Book("Title1", "Author1", 300, "Fiction", "Publisher1", "123-1234567890");
        Book book2 = new Book("Title2", "Author2", 250, "Non-Fiction", "Publisher2", "456-0987654321");

        // Write books to file
        List<Book> books = new List<Book> { book1, book2 };
        Book.WriteToFile(books, "books.txt");

        // Read books from file
        List<Book> readBooks = Book.ReadFromFile("books.txt");
        foreach (var book in readBooks)
        {
            Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, Pages: {book.Pages}, Genre: {book.Genre}, Publisher: {book.Publisher}, ISBN: {book.ISBN}");
        }

        // Extract email addresses from text
        string text = "Contact us at email@example.com or support@example.org for assistance.";
        MatchCollection matches = Regex.Matches(text, @"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}\b");
        foreach (Match match in matches)
        {
            Console.WriteLine(match.Value);
        }

        // Create generic dictionary
        Dictionary<string, Student> studentData = new Dictionary<string, Student>();
        studentData.Add("A001", new Student("Alice", 85));
        studentData.Add("B002", new Student("Bob", 92));
    }
}

public class Student
{
    public string Name { get; set; }
    public int Grade { get; set; }

    public Student(string name, int grade)
    {
        Name = name;
        Grade = grade;
    }
}
