using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace EberlyWorkSample
{
    public static class Commands
    {
        /// <summary>
        /// Parse input and execute command
        /// </summary>
        /// <param name="input">User input</param>
        /// <param name="dictionary">Dictionary to update or read from</param>
        public static void ReadCommand(string input, Dictionary<string, List<string>> dictionary, bool shouldContinue = true)
        {
            input = Regex.Replace((input?.Trim().ToLower() ?? ""), @"\s+", " ");
            dictionary = dictionary ?? new Dictionary<string, List<string>>();

            try
            {
                switch (input.Split(' ')[0])
                {
                    case ("keys"):
                        Keys(dictionary);
                        break;
                    case ("members"):
                        Members(input, dictionary);
                        break;
                    case ("add"):
                        Add(input, dictionary);
                        break;
                    case ("remove"):
                        Remove(input, dictionary);
                        break;
                    case ("removeall"):
                        RemoveAll(input, dictionary);
                        break;
                    case ("clear"):
                        Clear(dictionary);
                        break;
                    case ("keyexists"):
                        KeyExists(input, dictionary);
                        break;
                    case ("memberexists"):
                        MemberExists(input, dictionary);
                        break;
                    case ("allmembers"):
                        AllMembers(dictionary);
                        break;
                    case ("items"):
                        Items(dictionary);
                        break;
                    case ("multiadd"):
                    case ("multiremove"):
                    case ("multiremoveall"):
                    case ("help"):
                        ExtraCreditCommands.ReadExtraCreditCommand(input, dictionary);
                        break;
                    case ("quit"):
                        shouldContinue = false;
                        break;
                    default:
                        Program.ThrowError("Command not recognized. Enter HELP to view available commands");
                        break;
                }
            } 
            catch (Exception err)
            {
                Program.ThrowError(err.Message);
            };

            // Continue to loop until user decides to quit
            if (shouldContinue)
            {
                Console.WriteLine("------------------------");
                ReadCommand(Console.ReadLine(), dictionary);
            }
        }

        /// <summary>
        /// Print out all keys in dictionary
        /// </summary>
        /// <param name="dictionary">Dictionary to read from</param>
        private static void Keys(Dictionary<string, List<string>> dictionary)
        {
            var ind = 1;
            foreach (var key in dictionary.Keys.ToList())
            {
                Console.WriteLine($"{ind++}) {key}");
            }
        }

        /// <summary>
        /// Print out all members for a given key
        /// </summary>
        /// <param name="input">User input (i.e. MEMBERS foo)</param>
        /// <param name="dictionary">Dictionary to read from</param>
        private static void Members(string input, Dictionary<string, List<string>> dictionary)
        {
            var strArr = input.Split(' ');
            if (strArr.Length != 2)
            {
                Program.ThrowError(strArr.Length < 2 ? "Please enter the key you would like to return members from i.e. \"MEMBERS foo\""
                    : "Please only enter one key at a time i.e. \"MEMBERS foo\"");
                return;
            }

            var key = strArr[1];
            if (dictionary.ContainsKey(key))
            {
                var ind = 1;
                foreach (var val in dictionary[key])
                {
                    Console.WriteLine($"{ind++}) {val}");
                }
            } 
            else
            {
                Program.ThrowError($"Key {key} does not exist");
            }
        }

        /// <summary>
        /// Add new key to dictionary if not already present; add value to key if not already present
        /// </summary>
        /// <param name="input">User input (i.e. ADD foo bar)</param>
        /// <param name="dictionary">Dictionary to add to</param>
        private static void Add(string input, Dictionary<string, List<string>> dictionary)
        {
            var strArr = input.Split(' ');
            if (strArr.Length != 3)
            {
                Program.ThrowError(strArr.Length < 3 ? "Please enter the key and member to add after the command, separated by spaces i.e. \"ADD foo bar\""
                    : "Please only enter one member to add at a time i.e. \"ADD foo bar\"");
                return;
            }

            var key = strArr[1];
            var value = strArr[2];

            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, new List<string>() { value });
                Console.WriteLine($"Added key {key} with member {value}");
            }
            else
            {
                if (!dictionary[key].Contains(value))
                {
                    dictionary[key].Add(value);
                    Console.WriteLine($"Added member {value} to key {key}");
                }
                else
                {
                    Program.ThrowError($"Member {value} already exists on key {key}");
                }
            }

            return;
        }

        /// <summary>
        /// Remove a value from a key; remove the key if no values are left
        /// </summary>
        /// <param name="input">User input (i.e. REMOVE foo bar)</param>
        /// <param name="dictionary">Dictionary to remove from</param>
        private static void Remove(string input, Dictionary<string, List<string>> dictionary)
        {
            var strArr = input.Split(' ');
            if (strArr.Length != 3)
            {
                Program.ThrowError(strArr.Length < 3 ? "Please enter the key and member to remove after the command i.e. \"REMOVE foo bar\""
                    : "Please only enter one member to remove at a time i.e. \"REMOVE foo bar\"");
                return;
            }

            var key = strArr[1];
            var value = strArr[2];

            if (!dictionary.ContainsKey(key))
            {
                Program.ThrowError($"Key {key} does not exist");
                return;
            }

            if (!dictionary[key].Contains(value))
            {
                Program.ThrowError($"Member {value} does not exist for key {key}");
                return;
            }

            dictionary[key].Remove(value);
            if (dictionary[key].Count == 0)
            {
                dictionary.Remove(key);
            }
            Console.WriteLine($"Removed member {value} from key {key}");
            return;
        }

        /// <summary>
        /// Removes all values from a key and deletes the key
        /// </summary>
        /// <param name="input">User input (i.e. REMOVEALL foo)</param>
        /// <param name="dictionary">Dictionary to remove from</param>
        private static void RemoveAll(string input, Dictionary<string, List<string>> dictionary)
        {
            var strArr = input.Split(' ');
            if (strArr.Length != 2)
            {
                Program.ThrowError(strArr.Length < 2 ? "Please enter the key to remove after the command i.e. \"REMOVEALL foo\""
                    : "Please only enter one key to remove at a time i.e. \"REMOVEALL foo\"");
                return;
            }

            var key = strArr[1];
            if (!dictionary.ContainsKey(key))
            {
                Program.ThrowError($"Key {key} does not exist");
                return;
            }

            dictionary.Remove(key);
            Console.WriteLine($"Removed all members from key {key}");
            return;
        }

        /// <summary>
        /// Clears entire dictionary
        /// </summary>
        /// <param name="dictionary">Dictionary to clear</param>
        private static void Clear(Dictionary<string, List<string>> dictionary)
        {
            dictionary.Clear();
            Console.WriteLine("Dictionary cleared");
            return;
        }

        /// <summary>
        /// Prints out whether a key exists or not
        /// </summary>
        /// <param name="input">User input (i.e. KEYEXISTS foo)</param>
        /// <param name="dictionary">Dictionary to read from</param>
        private static void KeyExists(string input, Dictionary<string, List<string>> dictionary)
        {
            var strArr = input.Split(' ');
            if (strArr.Length != 2)
            {
                Program.ThrowError(strArr.Length < 2 ? "Please enter the key to validate after the command i.e. \"KEYEXISTS foo\""
                    : "Please only enter one key at a time i.e. \"KEYEXISTS foo\"");
                return;
            }

            Console.WriteLine(dictionary.ContainsKey(strArr[1]));
        }

        /// <summary>
        /// Prints out whether a value exists on a key (returns false if key does not exist) 
        /// </summary>
        /// <param name="input">User input (i.e. MEMBEREXISTS foo bar)</param>
        /// <param name="dictionary">Dictionary to read from</param>
        private static void MemberExists(string input, Dictionary<string, List<string>> dictionary)
        {
            var strArr = input.Split(' ');
            if (strArr.Length != 3)
            {
                Program.ThrowError(strArr.Length < 3 ? "Please enter the key and member to validate after the command i.e. \"MEMBEREXISTS foo bar\""
                    : "Please only enter one key and member at a time i.e. \"MEMBEREXISTS foo bar\"");
                return;
            }

            Console.WriteLine(dictionary.ContainsKey(strArr[1]) && dictionary[strArr[1]].Contains(strArr[2]));
        }

        /// <summary>
        /// Returns all values in the dictionary
        /// </summary>
        /// <param name="dictionary">Dictionary to read from</param>
        private static void AllMembers(Dictionary<string, List<string>> dictionary)
        {
            var ind = 1;
            foreach (var value in dictionary.Values.SelectMany(v => v).ToList())
            {
                Console.WriteLine($"{ind++}) {value}");
            }
        }

        /// <summary>
        /// Returns all values for all keys in the dictionary with the name of the key
        /// </summary>
        /// <param name="dictionary">Dictionary to read from</param>
        private static void Items(Dictionary<string, List<string>> dictionary)
        {
            var ind = 1;
            foreach (var value in dictionary.SelectMany(kv => kv.Value.Select(v => $"{kv.Key}: {v}")))
            {
                Console.WriteLine($"{ind++}) {value}");
            }
        }
    }
}
