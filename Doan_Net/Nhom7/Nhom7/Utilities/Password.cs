using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
namespace Nhom7.Utilities
{
    class Password
    {

        public static bool verify(string password, string hashPassword, string algorithm = "brcypt")
        {
            if (algorithm == "md5")
                return Password.create_MD5(password) == hashPassword;
            if (algorithm == "sha1")
                return Password.Create_SHA1(password) == hashPassword;
            return false;
        }

        public static string create_MD5(string text)
        {
            return Password.ComputeHash(text, new MD5CryptoServiceProvider());
        }
        public static string Create_Bcrypt(string text)
        {
            return text;
        }
        public static string Create_SHA1(string text)
        {
            return Password.ComputeHash(text, new SHA1CryptoServiceProvider());
        }

        public static string ComputeHash(string input, HashAlgorithm algorithm)
        {
            ////string s1 = "&%~";
            ////string s2 = "+)A";
            //input = s1 + input + s2;
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}
