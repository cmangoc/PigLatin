using System.Runtime.Serialization;

namespace PigLatin
{
    public class Program
    {
        public static void Main()
        {
            bool contLoop = false;
            string input = "";
            string inputText = "";

            Console.WriteLine("Welcome to the Pig Latin Translator!");
            do
            {
                Console.WriteLine();

                do
                {
                    inputText = GetUserInput("Enter a line to be translated: ");
                }while(inputText == "");
                
                Console.WriteLine(PLTranslator(inputText));
                do
                {
                    Console.WriteLine();
                    Console.WriteLine("Do you want to continue translating? (y/n)");
                    input = Console.ReadLine().Trim().ToLower();
                    if (input == "y")
                    {
                        contLoop = true;
                    }
                    else if (input == "n")
                    {
                        contLoop = false;
                        Console.WriteLine("Oodbyegay!");
                    }
                    else
                    {
                        Console.WriteLine("I didn't understand that, please try again");
                        contLoop = true;
                    }
                } while (contLoop && input != "y");
            } while (contLoop);
        }
        public static string GetUserInput(string prompt)
        {
            Console.WriteLine(prompt);
            String input = Console.ReadLine();
            return input;
        }
        public static string PLTranslator(string pretext)
        {
            string[] vowels = { "a", "e", "i", "o", "u", "A", "E", "I", "O", "U" };
            if (pretext == " " || pretext == "")
            {
                do
                {
                    Console.WriteLine("Please enter valid input: ");
                    pretext = Console.ReadLine();
                    pretext = pretext.Trim();
                } while (pretext == " " || pretext == "");
            }
            string[] capWords = pretext.Split(' ');
            pretext = pretext.Trim().ToLower();
            string finalSentence = "";
            string[] words = pretext.Split(" ");
            int wordCount = words.Count();
            string[] translatedWords = new string[wordCount];
            char punctCheck = pretext[pretext.Length - 1];
            bool c = char.IsPunctuation(punctCheck);

                //Console.WriteLine($"Number of words: {wordCount}");

                int trueFirst = 0;

            for (int i = 0; i < wordCount; i++)
            {
                //Console.WriteLine(words[i]);
                trueFirst = words[i].Length + 1;
                //Finds the first time every vowel is used
                for (int j = 0; j < vowels.Length; j++)
                {
                    int firstVowel = words[i].IndexOf(vowels[j]);
                    //int[] firstVowels = new int[vowels.Length];
                    //firstVowels[j] = firstVowel;
                    //Console.WriteLine(firstVowels[j] + " ");
                    
                    
                    if (firstVowel > -1 && trueFirst >= firstVowel)
                    {
                        trueFirst = firstVowel;
                        //Console.WriteLine("updated first");
                    }
                }
                //combines words into sentence
                string finalWord = "";
                
                //need loop to check for symbols and @
                if (c == true && words[i] == words[wordCount - 1])
                {
                    finalWord = words[i];
                    finalWord = finalWord.Remove(finalWord.Length - 1);
                    words[i] = finalWord;
                }
                //starting with a vowel just adds way
                if (trueFirst == 0)
                {
                    finalWord = words[i] + "way";
                }
                else
                if (c && trueFirst == words[i].Length + 2)
                {
                    finalWord = words[i];
                }
                //doesnt't start with a vowel but has vowels
                else
                if (trueFirst > 0 && trueFirst != words[i].Length + 1)
                {
                finalWord = words[i].Substring(trueFirst) + words[i].Substring(0,trueFirst) + "ay";
                } 
                //doesn't have any vowels = word is left alone
                else
                if (trueFirst == words[i].Length + 1)
                {
                    finalWord = words[i];
                }
                
                //keeps capital spots from original input
                char[] chars = capWords[i].ToCharArray();
                char[] capMaker = finalWord.ToCharArray();
                bool[] capTest = new bool[capWords[i].Length];
                for (int k = 0; k < capWords[i].Length; k++)
                {
                    capTest[k] = Char.IsUpper(chars[k]);
                    if (c && k == capWords[i].Length-1)
                    {
                        continue;
                    }
                    else
                    if (capTest[k] == true)
                    {
                        capMaker[k] = char.ToUpper(capMaker[k]);
                    }
                    else
                    if (capTest[k] == false)
                    {
                        capMaker[k] = char.ToLower(capMaker[k]);
                    }

                    finalWord = new string(capMaker);
                }

                //Constructs final translated sentence
                
                finalSentence = finalSentence + finalWord + " ";

            }
            finalSentence = finalSentence.Trim();
            if (c)
            {
                finalSentence = finalSentence + punctCheck;
            }
            return finalSentence;
        }
    }   
}