using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace LyricsMergeLib.Parser.Attribute
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    internal class LyricParserAttribute : System.Attribute
    {
        private string _extension = null;
        private Type _parser = null;

        public LyricParserAttribute(string extension, Type parser)
        {
            Type iparser = parser.GetInterface("ILyricParser");
            if (iparser == null)
            {
                throw new ArgumentException("Parser Type Is Not Implement ILyricParser Interface.", "ILyricParser");
            }
            ConstructorInfo cinfo = parser.GetConstructor(Type.EmptyTypes);
            if (cinfo == null)
            {
                throw new ArgumentException("Parser Type Must Have An Empty Constructor.", "ILyricParser");
            }
            this._parser = parser;
            this._extension = extension;
        }

        public string Extension
        {
            get { return this._extension; }
            set { this._extension = value; }
        }

        public ILyricParser LyricParser
        {
            get
            {
                return (ILyricParser)Activator.CreateInstance(this._parser);
            }
        }
    }
}
