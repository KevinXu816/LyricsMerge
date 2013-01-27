using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using LyricsMergeLib.Parser.Attribute;

namespace LyricsMergeLib.Parser.Utility
{
    internal static class LyricParserTypeEnumUtility
    {

        public static ILyricParser GetLyricParserInstance(LyricParserTypeEnum value)
        {
            FieldInfo fi = typeof(LyricParserTypeEnum).GetField(value.ToString());
            LyricParserAttribute[] attributes =
              (LyricParserAttribute[])fi.GetCustomAttributes(
              typeof(LyricParserAttribute), false);
            return (attributes.Length > 0) ? attributes[0].LyricParser : null;
        }


        public static LyricParserTypeEnum FindLyricParserByExtension(string extension)
        {
            extension = (extension ?? string.Empty).Trim().ToLower();
            if (string.IsNullOrEmpty(extension))
                return LyricParserTypeEnum.NULL;
            LyricParserTypeEnum[] types = (LyricParserTypeEnum[])Enum.GetValues(typeof(LyricParserTypeEnum));
            foreach(LyricParserTypeEnum type in types)
            {
                string ext = GetLyricParserExtension(type);
                ext = (ext ?? string.Empty).Trim().ToLower();
                if (string.IsNullOrEmpty(ext))
                    continue;
                ext = string.Format(".{0}", ext);
                if (extension.Equals(ext))
                    return type;
            }
            return LyricParserTypeEnum.NULL;
        }


        public static string GetLyricParserExtension(LyricParserTypeEnum value)
        {
            FieldInfo fi = typeof(LyricParserTypeEnum).GetField(value.ToString());
            LyricParserAttribute[] attributes =
              (LyricParserAttribute[])fi.GetCustomAttributes(
              typeof(LyricParserAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Extension : string.Empty;
        }

    }
}
