using System;
using BCrypt.Net;

class HashGenerator
{
    static void Main()
    {
        Console.WriteLine("Admin Hash:");
        Console.WriteLine(BCrypt.Net.BCrypt.HashPassword("admin123"));

        Console.WriteLine("\nViewer Hash:");
        Console.WriteLine(BCrypt.Net.BCrypt.HashPassword("viewer123"));
    }
}