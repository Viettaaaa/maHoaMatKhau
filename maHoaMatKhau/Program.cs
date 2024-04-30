using System;
using System.Collections.Generic;
using System.Text;

public static class StringExtensions
{
    // Tạo 1 bản rõ gốc và 1 bản mã
    static string Goc = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    static string Key = "OIZjkdPS76WRCKFUebcgGMQVJoluTXAD15HLExshafm428iwqrytp9YNBvzn03";
    static Dictionary<char, char> Dict_Create = new Dictionary<char, char>();
    static Dictionary<char, char> Dict_Decrypt = new Dictionary<char, char>();

    // Tạo 2 từ điển Dict_Create và Dict_Decrypt đê ánh xạ các kí tự từ bản rõ sang bản mã và ngược lại
    public static void CreateDictionary()
    {
        for (int i = 0; i < Key.Length; i++)
        {
            Dict_Create[Goc[i]] = Key[i];
            Dict_Decrypt[Key[i]] = Goc[i];
        }
        Dict_Create[' '] = ' ';    // Nhận diện dấu cách
        Dict_Decrypt[' '] = ' ';
    }

    // Nhận chuỗi và mã hóa từ Dict_Create
    public static string EncodeToString(this string input)
    {
        StringBuilder res = new StringBuilder();
        foreach (char c in input)
        {
            res.Append(Dict_Create[c]);
        }
        return res.ToString();
    }

    // Nhận chuỗi đã được mã hóa và giả mã từ Dict_Decrypt
    public static string DecodeToString(this string input)
    {
        StringBuilder res = new StringBuilder();
        foreach (char c in input)
        {
            res.Append(Dict_Decrypt[c]);
        }
        return res.ToString();
    }

    // Chuyển đổi chuỗi thành mảng Bytes qua mã ASCII
    public static byte[] EncodeToBytes(this string input)
    {
        byte[] asciiBytes = Encoding.ASCII.GetBytes(input);
        for (int i = 0; i < asciiBytes.Length; i++)
        {
            if (asciiBytes[i] == 57)
                asciiBytes[i] = 48;
            else if(asciiBytes[i] == 90 || asciiBytes[i] == 122)
                asciiBytes[i] -= 25;
            else
                asciiBytes[i] += 1;
        }
        return asciiBytes;
    }

    // Chuyển đổi mảng Bytes thành chuỗi qua mã ASCII
    public static string DecodeToBytes(this byte[] input)
    {
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] == 48)
                input[i] = 57;
            else if (input[i] == 65 || input[i] == 97)
                input[i] += 25;
            else
                input[i] -= 1;
        }
        string decodedString = Encoding.ASCII.GetString(input);
        return decodedString;
    }
}

class Program
{
    static void Main()
    {
        // Tạo từ điển
        StringExtensions.CreateDictionary();

        Console.Write("Nhap mat khau(chi bao gom so va chu cai): ");
        string passWord = Console.ReadLine();
        Console.WriteLine("Mat khau ban vua nhap la: " + passWord);

        byte[] encodedBytes = passWord.EncodeToBytes();
        string encodedString = passWord.EncodeToString();

        Console.WriteLine($"Encoded Bytes: {string.Join(",", encodedBytes)}");
        Console.WriteLine($"Encoded String: {encodedString}");

        string decodedString = encodedString.DecodeToString();
        string decodedBytes = encodedBytes.DecodeToBytes();

        Console.WriteLine($"Decoded String: {decodedString}");
        Console.WriteLine($"Decoded Bytes: {decodedBytes}");
    }
}
