using System;


namespace SmartTime
{
    internal class Base64Helper
    {
        public static string Encode(string data)
        {
            byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(data);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}
