using System;
using System.Security.Cryptography;
using System.Text;
using Random = UnityEngine.Random;

namespace Common
{
    public static class Utils
    {
        public static Guid ToGuid(this string str)
        {
            var cryptoServiceProvider = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.Default.GetBytes(str);
            byte[] hash = cryptoServiceProvider.ComputeHash(bytes);

            return new Guid(hash);
        }

        public static string GetRandomId()
        {
            var id = string.Empty;
            for (var i = 0; i < 5; i++)
            {
                int rand = Random.Range(0, 36);
                
                if (rand < 26)
                    id += (char) (rand + 65);
                else
                    id += (rand - 26).ToString();
            }

            return id;
        }
    }
}