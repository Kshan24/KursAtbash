using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Шифр_Атбаш
{
    public class Atbash
    {

        private const string alphabet = "abcdefghijklmnopqrstuvwxyz";


        private string Reverse(string inputText)
        {

            var reversedText = string.Empty;
            foreach (var s in inputText)
            {

                reversedText = s + reversedText;
            }

            return reversedText;
        }


        private string EncryptDecrypt(string text, string symbols, string cipher)
        {

            text = text.ToLower();

            var outputText = string.Empty;
            for (var i = 0; i < text.Length; i++)
            {

                var index = symbols.IndexOf(text[i]);
                if (index >= 0)
                {

                    outputText += cipher[index].ToString();
                }
            }

            return outputText;
        }


        public string EncryptText(string plainText)
        {
            return EncryptDecrypt(plainText, alphabet, Reverse(alphabet));
        }


        public string DecryptText(string encryptedText)
        {
            return EncryptDecrypt(encryptedText, Reverse(alphabet), alphabet);
        }
    }
    
    public class Programm
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Атбаш шифрование");
            var atbash = new Atbash();
            Console.Write("Введите текст сообщения: ");
            var message = Console.ReadLine();
            var encryptedMessage = atbash.EncryptText(message);
            Console.WriteLine("Зашифрованное сообщение: {0}", encryptedMessage);
            var decryptedMessage = atbash.DecryptText(encryptedMessage);
            Console.WriteLine("Расшифрованное сообщение: {0}", decryptedMessage);
            Console.ReadLine();
        }
    }
}