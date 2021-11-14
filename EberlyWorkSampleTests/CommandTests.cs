using EberlyWorkSample;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EberlyWorkSampleTests
{
    [TestClass]
    public class CommandTests
    {
        /// <summary>
        /// Test the ADD command
        /// </summary>
        [TestMethod]
        public void AddKeyValueTest()
        {
            var dictionary = new Dictionary<string, List<string>>();
            Commands.ReadCommand("ADD foo bar", dictionary, false);
            Assert.AreEqual("bar", dictionary["foo"][0]);
        }

        /// <summary>
        /// Test the REMOVE command
        /// </summary>
        [TestMethod]
        public void RemoveKeyValueTest()
        {
            var dictionary = new Dictionary<string, List<string>>();
            dictionary.Add("foo", new List<string>() { "bar", "baz" });
            Commands.ReadCommand("REMOVE foo bar", dictionary, false);
            Assert.IsFalse(dictionary["foo"].Contains("bar"));

            Commands.ReadCommand("REMOVE foo baz", dictionary, false);
            Assert.IsFalse(dictionary.ContainsKey("foo"));
        }

        /// <summary>
        /// Test the KEYS command
        /// </summary>
        [TestMethod]
        public void PrintKeysTest()
        {
            var dictionary = new Dictionary<string, List<string>>();
            dictionary.Add("foo", new List<string>() { "bar" });
            dictionary.Add("bang", new List<string>() { "baz" });
            
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            
            Commands.ReadCommand("KEYS", dictionary, false);
            var output = stringWriter.ToString();
            Assert.AreEqual(output, "1) foo\r\n2) bang\r\n");
        }

        /// <summary>
        /// Test the MEMBERS command
        /// </summary>
        [TestMethod]
        public void PrintMembersTest()
        {
            var dictionary = new Dictionary<string, List<string>>();
            dictionary.Add("foo", new List<string>() { "bar" });
            dictionary.Add("bang", new List<string>() { "baz" });

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            Commands.ReadCommand("MEMBERS foo", dictionary, false);
            var output = stringWriter.ToString();
            Assert.AreEqual(output, "1) bar\r\n");
        }

        /// <summary>
        /// Test the REMOVEALL command
        /// </summary>
        [TestMethod]
        public void RemoveAllMembersFromKeyTest()
        {
            var dictionary = new Dictionary<string, List<string>>();
            dictionary.Add("foo", new List<string>() { "bar", "bam" });
            dictionary.Add("bang", new List<string>() { "baz" });

            Commands.ReadCommand("REMOVEALL foo", dictionary, false);
            Assert.IsTrue(dictionary.ContainsKey("bang"));
            Assert.IsFalse(dictionary.ContainsKey("foo"));
        }

        /// <summary>
        /// Test the CLEAR command
        /// </summary>
        [TestMethod]
        public void ClearDictionaryTest()
        {
            var dictionary = new Dictionary<string, List<string>>();
            dictionary.Add("foo", new List<string>() { "bar", "bam" });
            dictionary.Add("bang", new List<string>() { "baz" });

            Commands.ReadCommand("CLEAR", dictionary, false);
            Assert.IsFalse(dictionary.Any());
        }

        /// <summary>
        /// Test the KEYEXISTS command
        /// </summary>
        [TestMethod]
        public void KeyExistsTest()
        {
            var dictionary = new Dictionary<string, List<string>>();
            dictionary.Add("foo", new List<string>() { "bar" });

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            Commands.ReadCommand("KEYEXISTS foo", dictionary, false);
            var output = stringWriter.ToString();
            Assert.AreEqual(output, "True\r\n");

            stringWriter.GetStringBuilder().Clear();
            Commands.ReadCommand("KEYEXISTS bang", dictionary, false);
            output = stringWriter.ToString();
            Assert.AreEqual(output, "False\r\n");
        }

        /// <summary>
        /// Test the MEMBEREXISTS command
        /// </summary>
        [TestMethod]
        public void MemberExistsTest()
        {
            var dictionary = new Dictionary<string, List<string>>();
            dictionary.Add("foo", new List<string>() { "bar" });
            
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            Commands.ReadCommand("MEMBEREXISTS foo bar", dictionary, false);
            var output = stringWriter.ToString();
            Assert.AreEqual(output, "True\r\n");
            
            stringWriter.GetStringBuilder().Clear();
            Commands.ReadCommand("MEMBEREXISTS foo baz", dictionary, false);
            output = stringWriter.ToString();
            Assert.AreEqual(output, "False\r\n");

            stringWriter.GetStringBuilder().Clear();
            Commands.ReadCommand("MEMBEREXISTS bang bar", dictionary, false);
            output = stringWriter.ToString();
            Assert.AreEqual(output, "False\r\n");
        }

        /// <summary>
        /// Test the ALLMEMBERS command
        /// </summary>
        [TestMethod]
        public void PrintAllMembersTest()
        {
            var dictionary = new Dictionary<string, List<string>>();
            dictionary.Add("foo", new List<string>() { "bar", "bam" });
            dictionary.Add("bang", new List<string>() { "baz" });

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            Commands.ReadCommand("ALLMEMBERS", dictionary, false);
            var output = stringWriter.ToString();
            Assert.AreEqual(output, "1) bar\r\n2) bam\r\n3) baz\r\n");
        }

        /// <summary>
        /// Test the ITEMS command
        /// </summary>
        [TestMethod]
        public void PrintAllItemsTest()
        {
            var dictionary = new Dictionary<string, List<string>>();
            dictionary.Add("foo", new List<string>() { "bar", "bam" });
            dictionary.Add("bang", new List<string>() { "baz" });

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            Commands.ReadCommand("ITEMS", dictionary, false);
            var output = stringWriter.ToString();
            Assert.AreEqual(output, "1) foo: bar\r\n2) foo: bam\r\n3) bang: baz\r\n");
        }
    }
}
