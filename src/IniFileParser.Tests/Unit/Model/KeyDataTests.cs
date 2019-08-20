﻿using System;
using System.Collections.Generic;
using IniParser.Model;
using NUnit.Framework;

namespace IniFileParser.Tests.Unit.Model
{
    [TestFixture, Category("Test of data structures used to hold information retrieved for an INI file")]
    public class KeyDataTests
    {
        [Test]
        public void check_default_values()
        {
            var kd = new Property("key_name");

            Assert.That(kd, Is.Not.Null);
            Assert.That(kd.KeyName, Is.EqualTo("key_name"));
            Assert.That(kd.Comments, Is.Empty);
            Assert.That(kd.Value, Is.Empty);
        }

        [Test]
        public void create_key_with_invalid_name()
        {
            Assert.Throws(typeof(ArgumentException), () => new Property(""));
        }

        [Test]
        public void creating_keydata_programatically()
        {
            var strValueTest = "Test String";
            var strKeyTest = "Mykey";
            var commentListTest = new List<string>(new string[] { "testComment 1", "testComment 2" });

            //Create a key data
            Property kd = new Property(strKeyTest);
            kd.Value = strValueTest;
            kd.Comments = commentListTest;
            
            //Assert not null and empty
            Assert.That(kd, Is.Not.Null);
            Assert.That(kd.KeyName, Is.EqualTo(strKeyTest));
            Assert.That(kd.Value, Is.EqualTo(strValueTest));
            Assert.That(kd.Comments, Has.Count.EqualTo(2));
            Assert.That(kd.Comments[0], Is.EqualTo("testComment 1"));
            Assert.That(kd.Comments[1], Is.EqualTo("testComment 2"));

        }

        [Test]
        public void check_deep_clone()
        {
            var strValueTest = "Test String";
            var strKeyTest = "Mykey";
            var commentListTest = new List<string>(new string[] { "testComment 1", "testComment 2" });

            //Create a key data
            Property kd2 = new Property(strKeyTest);
            kd2.Value = strValueTest;
            kd2.Comments = commentListTest;

            Property kd = kd2.DeepClone();

            //Assert not null and empty
            Assert.That(kd, Is.Not.Null);
            Assert.That(kd.KeyName, Is.EqualTo(strKeyTest));
            Assert.That(kd.Value, Is.EqualTo(strValueTest));
            Assert.That(kd.Comments, Has.Count.EqualTo(2));
            Assert.That(kd.Comments[0], Is.EqualTo("testComment 1"));
            Assert.That(kd.Comments[1], Is.EqualTo("testComment 2"));
        }

        [Test]
        public void check_merge_keys()
        {
            var keys1 = new PropertyCollection();
            keys1.AddKeyAndValue( "key1", "value1");
            keys1.AddKeyAndValue( "key2", "value2");
            keys1.AddKeyAndValue( "key3", "value3");

            var keys2 = new PropertyCollection();
            keys2.AddKeyAndValue("key1", "value11");
            keys2.AddKeyAndValue("key4", "value4");

            keys1.Merge(keys2);

            Assert.That(keys1["key1"], Is.EqualTo("value11"));
            Assert.That(keys1["key2"], Is.EqualTo("value2"));
            Assert.That(keys1["key3"], Is.EqualTo("value3"));
            Assert.That(keys1["key4"], Is.EqualTo("value4"));
        }
         
        /// <summary>
        ///     Thanks to h.eriksson@artamir.org for the issue.
        /// </summary>
        [Test, Description("Test for Issue 5: http://code.google.com/p/ini-parser/issues/detail?id=5")]
        public void correct_comment_assigment_to_keydata()
        {
            IniData inidata = new IniData();
            inidata.Sections.AddSection("TestSection");

            Property key = new Property("TestKey");
            key.Value = "TestValue";
            key.Comments.Add("This is a comment");
            inidata["TestSection"].SetKeyData(key);

            Assert.That(inidata["TestSection"].GetKeyData("TestKey").Comments[0], Is.EqualTo("This is a comment"));
        }
    }
}
