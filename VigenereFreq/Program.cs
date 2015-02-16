using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace VigenereApp
{
    class Program
    {



        public static Dictionary<string, int> FindDoubleLetterFrequency(string input)
        {
            Dictionary<string, int> FrequencyOfLetter = new Dictionary<string, int>();
            Dictionary<string, int> sortedFrequencyOfLetter = new Dictionary<string, int>();
            // Dictionary<string, int> SameLetter = new Dictionary<string, int>();
            int count = 1;
            for (int i = 0; i < input.Length - 1; i++)
            {

                if (FrequencyOfLetter.ContainsKey(input[i].ToString() + input[i + 1].ToString()))
                {
                    FrequencyOfLetter[input[i].ToString() + input[i + 1].ToString()] = FrequencyOfLetter[input[i].ToString() + input[i + 1].ToString()] + 1;
                }
                else
                {
                    FrequencyOfLetter.Add(input[i].ToString() + input[i + 1].ToString(), count);
                }



            }


            foreach (KeyValuePair<string, int> kv in FrequencyOfLetter.OrderByDescending(key => key.Value))
            {
                sortedFrequencyOfLetter.Add(kv.Key, kv.Value);
                //Console.WriteLine("{0}={1}", kv.Key, kv.Value);

            }
            return sortedFrequencyOfLetter;


        }
        public static Dictionary<string, int> FindLetterFrequency(string input)
        {
            Dictionary<string, int> FrequencyOfLetter = new Dictionary<string, int>();
            Dictionary<string, int> sortedFrequencyOfLetter = new Dictionary<string, int>();
            int count = 1;
            for (int i = 1; i <= input.Length - 1; i++)
            {

                if (FrequencyOfLetter.ContainsKey(input[i].ToString()))
                {
                    FrequencyOfLetter[input[i].ToString()] = FrequencyOfLetter[input[i].ToString()] + 1;
                }

                else
                {
                    FrequencyOfLetter.Add(input[i].ToString(), count);
                }
            }

            foreach (KeyValuePair<string, int> kv in FrequencyOfLetter.OrderByDescending(key => key.Value))
            {
                sortedFrequencyOfLetter.Add(kv.Key, kv.Value);
                //Console.WriteLine("{0}={1}", kv.Key, kv.Value);

            }

            return sortedFrequencyOfLetter;
        }


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
         *Bu foksiyonda girilen metin çözülür. 
         */
        public static int[] Descript(string value, string key)
        {
            int[] result = new int[value.Length];
            int index;
            for (int i = 0; i < value.Length; i++)
            {
                index = i % 4;
                result[i] = (FindAlphabethOfNumber(value[i].ToString()) - FindAlphabethOfNumber(key[index].ToString().ToUpper()));
                if (result[i] < 0)
                {
                    result[i] = (result[i] + 29) % 29;
                }
                else
                {
                    result[i] = result[i] % 29;
                }

            }
            return result;

        }
        public static int[] FindKey(string value, string solvedVigenere)
        {
            int[] result = new int[value.Length];
            int index;
            for (int i = 0; i < value.Length; i++)
            {
                index = i % 4;
                result[i] = (FindAlphabethOfNumber(value[i].ToString()) - FindAlphabethOfNumber(solvedVigenere[index].ToString().ToUpper()));
                if (result[i] < 0)
                {
                    result[i] = (result[i] + 29) % 29;
                }
                else
                {
                    result[i] = result[i] % 29;
                }

            }
            return result;

        }

        public static string ConvertStringArrayToStringJoin(string[] array)
        {

            string result = string.Join("", array);
            return result;
        }

        public static string VigenereSolvingProcess(string vigenere, string key, string letterFrequency, string mostUsingDoubleLetter)
        {
            int[] tempArray = new int[vigenere.Length];
            int[] tempArray2 = new int[vigenere.Length];
            String[] tempStringArray = new String[vigenere.Length];
            String[] tempStringArray2 = new String[key.Length];
            tempArray = Descript(vigenere, key);
            for (int i = 0; i < tempArray.Length; i++)
            {
                tempStringArray[i] = FindNumberOfAlphabeth(tempArray[i]);
            }
            String resultString = ConvertStringArrayToStringJoin(tempStringArray).Replace(mostUsingDoubleLetter, letterFrequency);

            tempArray2 = FindKey(vigenere, resultString);

            //İlk anahtar bulma fonksiyonundan gelen bilginin ilk dördünü çeviriyor
            for (int i = 0; i < key.Length; i++)
            {
                tempStringArray2[i] = FindNumberOfAlphabeth(tempArray2[i]);
            }

            key = null;
            key = ConvertStringArrayToStringJoin(tempStringArray2);
            //Console.Write("Anahtar = " + key + " ,");
            tempArray = Descript(vigenere, key);
            for (int i = 0; i < tempArray.Length; i++)
            {
                tempStringArray[i] = FindNumberOfAlphabeth(tempArray[i]);
            }

            return ConvertStringArrayToStringJoin(tempStringArray);
        }

        public static String CleanUnvowelSentences(String input)
        {
            string pattern = "[BCÇDFGĞHJKLMNPRSŞTVYZ]{3,}";
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection matches = rgx.Matches(input);
            if (matches.Count > 0)
            {
                return "";
            }
            else
                return input;

        }
        public static String CleanVowelSentences(String input)
        {
            string pattern = "[AEIİOÖUÜ]{3,}";
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection matches = rgx.Matches(input);
            if (matches.Count > 0)
            {
                return "";
            }
            else
                return input;

        }

        public static void OneLetterFrequency(string vigenere,string[] a,string key)
        {
            
            String letterFrequency = "AEİNRLIKDMYUTSBOÜŞZGÇHĞVCÖPFJ";
            String[] vigSentence = new string[vigenere.Length];
            int index = 0;
            int[] tempArray3 = new int[vigenere.Length];
            string[] tempStringArray3 = new string[vigenere.Length];
            string[] tempStringArray4 = new string[vigenere.Length];
            int[] tempArray4 = new int[vigenere.Length];

            for (int i = 0; i < letterFrequency.Length; i++)
            {
                for (int k = 0; k < vigenere.Length; k++)
                {
                    index = k % 4;
                    if (index == 3)
                    {
                        a[k] = letterFrequency[i].ToString();


                    }

                }
                tempArray3 = FindKey(vigenere, ConvertStringArrayToStringJoin(a));

                for (int p = 0; p < key.Length; p++)
                {
                    tempStringArray3[p] = FindNumberOfAlphabeth(tempArray3[p]);
                }
                tempArray4 = Descript(vigenere, ConvertStringArrayToStringJoin(tempStringArray3));
                for (int t = 0; t < vigenere.Length; t++)
                {
                    tempStringArray4[t] = FindNumberOfAlphabeth(tempArray4[t]);
                }
                //Console.WriteLine("Anahtar "+ ConvertStringArrayToStringJoin(tempStringArray3)+" Cümle : "+ConvertStringArrayToStringJoin(a));
                if (CleanUnvowelSentences(ConvertStringArrayToStringJoin(tempStringArray4)) != "" && CleanVowelSentences(ConvertStringArrayToStringJoin(tempStringArray4)) != "")
                {
                    Console.WriteLine("Anahtar " + ConvertStringArrayToStringJoin(tempStringArray3) + " Cümle : " + ConvertStringArrayToStringJoin(tempStringArray4));
                    Console.WriteLine();
                }
                
            }
        }

        static void Main(string[] args)
        {
            Dictionary<string, int> sortedFrequencyOfLetter = new Dictionary<string, int>();
            Dictionary<string, int> sortedFrequencyOfLetterByOne = new Dictionary<string, int>();
            //String vigenere = "VRYVBGMZVCNÜÖRYBKDİRYRYKTÖZKORYFUUNAUEŞPECY";
            String vigenere = "ŞAİGÖNHYÖSCIUAİGÖİOZDNCI";

            String[] doubleLetterFrequency = { "AR", "LA", "AN", "ER", "İN", "LE", "DE", "EN", "IN", "DA" };
            String key = "KAAA";
            sortedFrequencyOfLetter = FindDoubleLetterFrequency(vigenere);
            string mostUsingDoubleLetter = sortedFrequencyOfLetter.Select(k => k.Key).FirstOrDefault().ToString();
            string[] solvedLetters = new string[doubleLetterFrequency.Length];
            for (int i = 0; i < doubleLetterFrequency.Length; i++)
            {
                solvedLetters[i] = VigenereSolvingProcess(vigenere, key, doubleLetterFrequency[i].ToString(), mostUsingDoubleLetter);
                //Console.WriteLine(i + 1 + "'inci çözüm = " + solvedLetters[i]);
                //Console.WriteLine("");
            }



            string[] unfound = new string[(int)(vigenere.Length / 4)];
            int index;
            int count = 0;
            for (int k = 0; k < vigenere.Length; k++)
            {
                index = k % 4;
                if (index == 3)
                {
                    unfound[count] = vigenere[k].ToString();
                    count++;
                }

            }
            sortedFrequencyOfLetterByOne = FindLetterFrequency(ConvertStringArrayToStringJoin(unfound));

            Console.WriteLine("Tahmin edilen anahtar kelimler ve çözülmüş cümleler: ");
            Console.WriteLine("");
            for (int i = 0; i < solvedLetters.Length; i++) 
            {
                string[] a = solvedLetters[i].Select(c => c.ToString()).ToArray();
                OneLetterFrequency(vigenere, a, key);
            }

            Console.ReadKey();
        }
    }
}
