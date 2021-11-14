using EberlyWorkSample;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EberlyWorkSampleTests
{
    [TestClass]
    public class ExtraCreditCommandTests
    {
        /// <summary>
        /// Test the MULTIADD command
        /// </summary>
        [TestMethod]
        public void AddKeysWithMultipleValuesTest()
        {
            var dictionary = new Dictionary<string, List<string>>();
            Commands.ReadCommand("MULTIADD foo bar baz", dictionary, false);
            Assert.IsTrue(dictionary["foo"].Contains("bar") && dictionary["foo"].Contains("baz"));

            Commands.ReadCommand("MULTIADD foo baz bam", dictionary, false);
            Assert.IsTrue(dictionary["foo"].Contains("bam") && dictionary["foo"].Count(v => v == "baz") == 1);
        }

        /// <summary>
        /// Test the MULTIREMOVE command
        /// </summary>
        [TestMethod]
        public void RemoveMultipleValuesFromKeyTest()
        {
            var dictionary = new Dictionary<string, List<string>>();
            dictionary.Add("foo", new List<string>() { "bar", "baz", "bam" });
            Commands.ReadCommand("MULTIREMOVE foo bar baz", dictionary, false);
            Assert.IsTrue(dictionary["foo"].Contains("bam") && !dictionary["foo"].Contains("bar") && !dictionary["foo"].Contains("baz"));

            Commands.ReadCommand("MULTIREMOVE foo bam", dictionary, false);
            Assert.IsFalse(dictionary.ContainsKey("foo"));
        }

        /// <summary>
        /// Test the MULTIREMOVEALL command
        /// </summary>
        [TestMethod]
        public void RemoveMultipleKeysTest()
        {
            var dictionary = new Dictionary<string, List<string>>();
            dictionary.Add("foo", new List<string>() { "bar", "baz" });
            dictionary.Add("bang", new List<string>() { "bar, baz" });
            dictionary.Add("bam", new List<string>() { "bar, baz" });

            Commands.ReadCommand("MULTIREMOVEALL foo bang", dictionary, false);
            Assert.IsFalse(dictionary.ContainsKey("foo") || dictionary.ContainsKey("bang"));
            Assert.IsTrue(dictionary.ContainsKey("bam"));
        }
    }
}
