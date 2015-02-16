using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;


namespace VigenereApp
{
    class Program
    {
       
         //Bu fonksiyon belirtilen harfin hangi sayıya denk geldiğini bulur.
        public static int FindAlphabethOfNumber(string key) 
        {
            int output;
            Dictionary<string, int> alphNumb = new Dictionary<string, int>();
            String alphabeth = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ";
            for (int i = 0; i < 29; i++)
            {
                alphNumb.Add(alphabeth[i].ToString(), i);
            }
             output = alphNumb.Where(d => d.Key == key.ToString()).Select(m => m.Value).Single();
            return output;
 
        }
        /*
         * Bu foksiyon girilen sayının hangi sayıya karşılık geldiğini bulur.
         */
        public static string FindNumberOfAlphabeth(int number)
        {
            string output;
            Dictionary<string, int> alphNumb = new Dictionary<string, int>();
            String alphabeth = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ";
            for (int i = 0; i < 29; i++)
            {
                alphNumb.Add(alphabeth[i].ToString(), i);
            }
            output = alphNumb.Where(d => d.Value == number).Select(m => m.Key).Single();
            return output;
        }

        /*
         *Bu foksiyonda girilen metin ve anahtar çözülür. 
         */
        public static int[] Descript(string value,string key)
        {
            int[] result = new int[value.Length];
            int index;
            for (int i = 0; i < value.Length; i++)
            {
                    index = i % 4;
                    result[i] = (FindAlphabethOfNumber(value[i].ToString()) - FindAlphabethOfNumber(key[index].ToString().ToUpper()));
                if (result[i] < 0)
                {
                    result[i] = (result[i] + 29)%29;
                }
                else
                {
                    result[i] = result[i] % 29;
                }

                

            }
            return result;

        }
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            String vigenere = "ŞAİGÖNHYÖSCIUAİGÖİOZDNCI";
            String alphabeth = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ";
            int[] descriptVigenereNum = new int[vigenere.Length];
            int[] tempArray = new int[vigenere.Length];
            string[] tempStringArray = new string[vigenere.Length];            
            List<string> favoritesWords = new List<string>();
            System.IO.StreamReader file = new System.IO.StreamReader("k_list.txt");
            string line;
            
            //k_list dosyasında baş harfi k ile başlayan 4 harfli kelimeler inceleniyor.
            while ((line = file.ReadLine()) != null)
            {
                if (line.Length == 4)
                    favoritesWords.Add(line);
            }

            file.Close();
            Console.WriteLine(favoritesWords.Count);

           
            //K ile başlayan 4 harfli kelimeler listesinden seçilen anahtar ile şifre çözülmeye çalışılıyor.
            foreach (string key in favoritesWords)
            {
                 tempArray = Descript(vigenere,key);
                 for (int i = 0; i < tempArray.Length; i++)
                 {
                     tempStringArray[i] = FindNumberOfAlphabeth(tempArray[i]);
                 }
                 Console.Write(key+" ");
                 Console.WriteLine(string.Join("", tempStringArray));

                 Console.WriteLine("\n");


            }

            sw.Stop();
            Console.WriteLine("Tüm sonuçlar {0} saniyede bulundu", sw.Elapsed);
            Console.ReadKey();
        }
    }
}
