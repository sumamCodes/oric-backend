using System;
using System.Text;

public static class FileHelper
{
    /// <summary>
    /// Converts a Base64 string to a byte array.
    /// </summary>
    public static byte[] ConvertBase64ToByteArray(string base64String)
    {
        if (string.IsNullOrEmpty(base64String))
            return new byte[0];

        try
        {
            return Convert.FromBase64String(base64String);
        }
        catch (FormatException)
        {
            throw new ArgumentException("Invalid Base64 string format.");
        }
    }

    /// <summary>
    /// Converts a byte array to a Base64 string.
    /// </summary>
    public static string ConvertByteArrayToBase64(byte[] byteArray)
    {
        if (byteArray == null || byteArray.Length == 0)
            return string.Empty; // ✅ Return empty string instead of null

        return Convert.ToBase64String(byteArray);
    }



    /// <summary>
    /// Converts a string to a UTF-8 encoded byte array.
    /// </summary>
    public static byte[] ConvertStringToByteArray(string text)
    {
        if (string.IsNullOrEmpty(text))
            return new byte[0];

        return Encoding.UTF8.GetBytes(text);
    }
}
