﻿
using IniParser.Parser;

namespace IniParser.Model.Configuration
{
    /// <summary>
    ///     Defines data for a Parser configuration object.
    /// </summary>
    ///     With a configuration object you can redefine how the parser
    ///     will detect special items in the ini file by defining new regex
    ///     (e.g. you can redefine the comment regex so it just treat text as
    ///     a comment iff the comment caracter is the first in the line)
    ///     or changing the set of characters used to define elements in
    ///     the ini file (e.g. change the 'comment' caracter from ';' to '#')
    ///     You can also define how the parser should treat errors, or how liberal
    ///     or conservative should it be when parsing files with "strange" formats.
    public class IniParserConfiguration : IDeepCloneable<IniParserConfiguration>,
                                          IOverwritable<IniParserConfiguration>
    {
        /// <summary>
        ///     Default values used if an instance of <see cref="IniDataParser"/>
        ///     is created without specifying a configuration.
        /// </summary>
        public IniParserConfiguration()
        {
        }

        /// <summary>
        ///     Copy ctor.
        /// </summary>
        /// <param name="ori">
        ///     Original instance to be copied.
        /// </param>
        public IniParserConfiguration(IniParserConfiguration ori)
        {
            this.OverwriteWith(ori);
        }

        #region IOverwritable<T> Members
        /// <summary>
        /// Replaces contents of this instance with the parameter
        /// </summary>
        public void OverwriteWith(IniParserConfiguration ori)
        {
            if (ori == null) return;

            AllowKeysWithoutSection = ori.AllowKeysWithoutSection;
            DuplicatePropertiesBehaviour = ori.DuplicatePropertiesBehaviour;
            ConcatenateDuplicatePropertiesString = ori.ConcatenateDuplicatePropertiesString;
            AllowDuplicateSections = ori.AllowDuplicateSections;
            ThrowExceptionsOnError = ori.ThrowExceptionsOnError;
            SkipInvalidLines = ori.SkipInvalidLines;
            TrimSections = ori.TrimSections;
            TrimProperties = ori.TrimProperties;
        }
        #endregion

        /// <summary>
        ///     Retrieving section / keys by name is done with a case-insensitive
        ///     search.
        /// </summary>
        /// <remarks>
        ///     Defaults to false (case sensitive search)
        /// </remarks>
        public bool CaseInsensitive { get; set; } = false;

        /// <summary>
        ///     Allows having keys in the file that don't belong to any section.
        ///     i.e. allows defining keys before defining a section.
        ///     If set to false and keys without a section are defined,
        ///     the <see cref="IniDataParser"/> will stop with an error.
        /// </summary>
        /// <remarks>
        ///     Defaults to true.
        /// </remarks>
        public bool AllowKeysWithoutSection { get; set; } = true;

        public enum EDuplicatePropertiesBehaviour
        {
            /// <summary>
            /// Duplicated keys are not allowed. WHen a duplicated key is found, the parser will 
            /// stop with an error.
            /// </summary>
            DisallowAndStopWithError,

            /// <summary>
            /// Duplicated keys are allowed. The value of the duplicate key will be the first
            /// value found in the set of duplicated key names.
            /// </summary>
            AllowAndKeepFirstValue,

            /// <summary>
            /// Duplicated keys are allowed. The value of the duplicate key will be the last
            /// value found in the set of duplicated key names.
            /// </summary>
            AllowAndKeepLastValue,

            /// <summary>
            /// Duplicated keys are allowed. The value of the duplicate keys will be a string that
            /// results in the concatenation of all the duplicated values found, separated by
            /// the character
            /// </summary>
            AllowAndConcatenateValues
        }

        /// <summary>
        ///    Sets the policy to use when two or more properties with the same key name are found
        ///    on the same section
        /// </summary>
        /// <remarks>
        ///     Defaults to <see cref="EDuplicatePropertiesBehaviour.DisallowAndStopWithError"/>
        /// </remarks>
        public EDuplicatePropertiesBehaviour DuplicatePropertiesBehaviour { get; set; } = EDuplicatePropertiesBehaviour.DisallowAndStopWithError;

        /// <summary>
        ///     Gets or sets the string used to concatenate duplicated keys.
        /// </summary>
        /// <remarks>
        ///     Defaults to ";"
        /// </remarks>
        public string ConcatenateDuplicatePropertiesString { get; set; } = ";";

        /// <summary>
        ///     If true the <see cref="IniDataParser"/> instance will thrown an exception
        ///     if an error is found.
        ///     If false the parser will just stop execution and return a null value.
        /// </summary>
        /// <remarks>
        ///     Defaults to true.
        /// </remarks>
        public bool ThrowExceptionsOnError { get; set; } = true;

        /// <summary>
        ///     If set to false and the <see cref="IniDataParser"/> finds a duplicate section
        ///     the parser will stop with an error.
        ///     If set to true, duplicated sections are allowed in the file, but only a
        ///     <see cref="Section"/> element will be created in the <see cref="IniData.Sections"/>
        ///     collection.
        /// </summary>
        /// <remarks>
        ///     Defaults to false.
        /// </remarks>
        public bool AllowDuplicateSections { get; set; } = false;

        /// <summary>
        ///     If set to true, it continues parsing the file even if a bad formed line is found
        ///     If set to false, it will throw an exception the moment the parser finds a 
        ///     bad formed line.
        /// </summary>
        /// <remarks>
        ///     Defaults to false.
        /// </remarks>
        public bool SkipInvalidLines { get; set; } = false;

        /// <summary>
        ///     If set to true, it will trim the whitespace out of the property when parsing.
        ///     If set to false, it will consider all the whitespace in the line as part of the
        ///     property when extracting the key and values.
        /// </summary>
        /// <remarks>
        ///     Defaults to true.
        /// </remarks>
        public bool TrimProperties { get; set; } = true;

        /// <summary>
        ///     If set to true, it will trim the whitespace out of the section name when parsing.
        ///     If set to false, it will consider all the whitespace in the line as part of the
        ///     section name.
        /// </summary>
        /// <remarks>
        ///     Defaults to true.
        /// </remarks>
        public bool TrimSections { get; set; } = true;

        #region IDeepCloneable<T> Members
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        public IniParserConfiguration DeepClone()
        {
            return new IniParserConfiguration(this);
        }
        #endregion

    }
}
