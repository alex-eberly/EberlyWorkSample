using System;
using System.Collections.Generic;
using System.Linq;

namespace EberlyWorkSample
{
    /// <summary>
    /// Commands not in the exercise that I thought may be helpful
    /// </summary>
    public static class ExtraCreditCommands
    {
        /// <summary>
        /// Parse input and execute extra credit command
        /// </summary>
        /// <param name="input">User input</param>
        /// <param name="dictionary">Dictionary to update or read from</param>
        public static void ReadExtraCreditCommand(string input, Dictionary<string, List<string>> dictionary)
        {
            switch (input.Split(' ')[0])
            {
                case ("multiadd"):
                    MultiAdd(input, dictionary);
                    break;
                case ("multiremove"):
                    MultiRemove(input, dictionary);
                    break;
                case ("multiremoveall"):
                    MultiRemoveAll(input, dictionary);
                    break;
                case ("help"):
                    Help();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Add new key to dictionary if not already present; add one or more values to key if not already present
        /// </summary>
        /// <param name="input">User input (i.e. MULTIADD foo bar baz)</param>
        /// <param name="dictionary">Dictionary to add to</param>
        private static void MultiAdd(string input, Dictionary<string, List<string>> dictionary)
        {
            var strArr = input.Split(' ');
            if (strArr.Length < 3)
            {
                Program.ThrowError("Please enter the key and value(s) to add after the command, separated by spaces i.e. \"MULTIADD foo bar baz");
                return;
            }

            var key = strArr[1];
            var values = strArr.Skip(2).ToList();

            if (!dictionary.ContainsKey(key))
            {
                // If key does not exist, add all values
                dictionary.Add(key, values);
                Console.WriteLine($"Added key {key} with value{(values.Count > 1 ? "s" : null)} {string.Join(", ", values)}");
            }
            else
            {
                // If key exists, add missing values
                var prevVals = dictionary[key];
                var newVals = prevVals.ToList();
                foreach (var value in values)
                {
                    if (!newVals.Contains(value))
                        newVals.Add(value);
                }
                dictionary[key] = newVals;

                var errorVals = values.Where(v => prevVals.Contains(v));
                var addedVals = newVals.Where(v => !prevVals.Contains(v));
                if (errorVals.Any())
                {
                    var plural = errorVals.Count() > 1;
                    Program.ThrowError($"Value{(plural ? "s" : null)} {string.Join(", ", errorVals)} already exist{(plural ? null : "s")} for key {key}");
                }
                if (addedVals.Any())
                {
                    Console.WriteLine($"Added value{(addedVals.Count() > 1 ? "s" : null)} {string.Join(", ", addedVals)} to key {key}");
                }
            }

            return;
        }

        /// <summary>
        /// Removes one or more values from key; removes key if no values are left
        /// </summary>
        /// <param name="input">User input (i.e. MULTIREMOVE foo bar baz)</param>
        /// <param name="dictionary">Dictionary to remove from</param>
        private static void MultiRemove(string input, Dictionary<string, List<string>> dictionary)
        {
            var strArr = input.Split(' ');
            if (strArr.Length < 3)
            {
                Program.ThrowError("Please enter the key and value(s) to remove after the command, separated by spaces i.e. \"MULTIREMOVE foo bar baz");
                return;
            }

            var key = strArr[1];
            var values = strArr.Skip(2).ToList();

            if (!dictionary.ContainsKey(key))
            {
                Program.ThrowError($"Key {key} does not exist");
                return;
            }
            else
            {
                var prevVals = dictionary[key];
                var newVals = prevVals.ToList();
                foreach (var value in values)
                {
                    if (newVals.Contains(value))
                    {
                        newVals.Remove(value);
                    }
                }

                if (newVals.Any())
                {
                    dictionary[key] = newVals;
                }
                else
                {
                    dictionary.Remove(key);
                }

                var errorVals = values.Where(v => !prevVals.Contains(v));
                var removedVals = prevVals.Where(v => !newVals.Contains(v));
                if (errorVals.Any())
                {
                    var plural = errorVals.Count() > 1;
                    Program.ThrowError($"Value{(plural ? "s" : null)} {string.Join(", ", errorVals)} do{(plural ? null : "es")} not exist for key {key}");
                }
                if (removedVals.Any())
                {
                    Console.WriteLine($"Removed value{(removedVals.Count() > 1 ? "s" : null)} {string.Join(", ", removedVals)} from key {key}");
                }
            }

            return;
        }

        /// <summary>
        /// Removes all values from one or more keys and deletes the key(s)
        /// </summary>
        /// <param name="input">User input (i.e. MULTIREMOVEALL foo bang)</param>
        /// <param name="dictionary">Dictionary to remove from</param>
        private static void MultiRemoveAll(string input, Dictionary<string, List<string>> dictionary)
        {
            var strArr = input.Split(' ');
            if (strArr.Length < 2)
            {
                Program.ThrowError("Please enter the key(s) to remove after the command i.e. \"MULTIREMOVEALL foo bang\"");
                return;
            }

            var keys = strArr.Skip(1).ToList();
            var removedKeys = new List<string>();
            var errorKeys = new List<string>();

            foreach (var key in keys)
            {
                if (dictionary.ContainsKey(key))
                {
                    removedKeys.Add(key);
                    dictionary.Remove(key);
                }
                else
                {
                    errorKeys.Add(key);
                }
            }

            if (errorKeys.Any())
            {
                var plural = errorKeys.Count > 1;
                Program.ThrowError($"Key{(plural ? "s" : null)} {string.Join(", ", errorKeys)} do{(plural ? null : "es")} not exist");
            }
            if (removedKeys.Any())
            {
                Console.WriteLine($"Removed key{(removedKeys.Count > 1 ? "s" : null)} {string.Join(", ", removedKeys)} from dictionary");
            }

            return;
        }

        /// <summary>
        /// Print out all available commands and how to execute them
        /// </summary>
        private static void Help()
        {
            Console.WriteLine("This command line application manages a dictionary that stores multiple values for each key. You may enter any of the following commands:");
            Console.WriteLine();
            Console.WriteLine("KEYS: Prints out a list of all keys in the dictionary");
            Console.WriteLine("MEMBERS KeyName: Prints out a list of all members of the specified key");
            Console.WriteLine("ADD KeyName ValueName: Adds the key if it does not exist, and adds the member to the key");
            Console.WriteLine("REMOVE KeyName ValueName: Removes the member from the key, and removes the key if no members are left");
            Console.WriteLine("REMOVEALL KeyName: Removes all members from the specified key and deletes the key");
            Console.WriteLine("CLEAR: Removes all keys and members from the dictionary");
            Console.WriteLine("KEYEXISTS KeyName: Prints True if the key exists in the dictionary, False if it does not");
            Console.WriteLine("MEMBEREXISTS KeyName MemberName: Prints True if the specified member exists for the key, False if the key or member do not exist");
            Console.WriteLine("ALLMEMBERS: Prints out a list of all members in the dictionary without the names of the keys");
            Console.WriteLine("ITEMS: Prints out a list of all key-value pairs in the dictionary");
            Console.WriteLine("MULTIADD KeyName ValueName1 ValueName2: Adds the key if it does not exist, and adds the members to the key");
            Console.WriteLine("MULTIREMOVE KeyName ValueName1 ValueName2: Removes the members from the key, and removes the key if no values are left");
            Console.WriteLine("MULTIREMOVEALL KeyName1 KeyName2: Removes all members from the specified keys and deletes the keys");
            Console.WriteLine("QUIT: Quit the application (CTRL + C also works)");
            return;
        }
    }
}
