using System;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// The accessor used by the field
    /// </summary>
    public enum EKeyword
    {
        /// <summary>
        /// public
        /// </summary>
        PUBLIC = 0,

        /// <summary>
        /// private
        /// </summary>
        PRIVATE = 1,

        /// <summary>
        /// protected
        /// </summary>
        PROTECTED = 2,

        /// <summary>
        /// static
        /// </summary>
        STATIC = 3,

        /// <summary>
        /// class
        /// </summary>
        CLASS = 4,

        /// <summary>
        /// interface
        /// </summary>
        INTERFACE = 5,

        /// <summary>
        /// enum
        /// </summary>
        ENUM = 6,

        /// <summary>
        /// readonly
        /// </summary>
        READONLY = 7,

        /// <summary>
        /// const
        /// </summary>
        CONST = 8,

        /// <summary>
        /// get
        /// </summary>
        GET = 9,

        /// <summary>
        /// set
        /// </summary>
        SET = 10,

        /// <summary>
        /// out
        /// </summary>
        OUT = 11,

        /// <summary>
        /// ref
        /// </summary>
        REF = 12,

        /// <summary>
        /// internal
        /// </summary>
        INTERNAL = 13,

        /// <summary>
        /// abstract
        /// </summary>
        ABSTRACT = 14,

        /// <summary>
        /// virtual
        /// </summary>
        VIRTUAL = 15,

        /// <summary>
        /// override
        /// </summary>
        OVERRIDE = 16
    }

    /// <summary>
    /// Utilities for ECSharpAccesseor enum
    /// </summary>
    public static class UTEKeyword
    {
        /// <summary>
        /// Get UTECSharpAccesseor as accessor string
        /// </summary>
        /// <param name="keyword">accesor</param>
        /// <returns>csharp accessor string</returns>
        public static string ToCSharpString(this EKeyword keyword)
        {
            return Enum.GetName( typeof(EKeyword), keyword ).ToLower();
        }

        /// <summary>
        /// Is this a property keyword
        /// </summary>
        /// <param name="keyword">accessor</param>
        /// <returns></returns>
        public static bool IsPropertyFunction(this EKeyword keyword)
        {
            return keyword == EKeyword.GET || keyword == EKeyword.SET;
        }

        /// <summary>
        /// Is this an accessor
        /// </summary>
        /// <param name="keyword">accessor</param>
        /// <returns></returns>
        public static bool IsAccessor(this EKeyword keyword)
        {
            return keyword == EKeyword.PUBLIC || keyword == EKeyword.PROTECTED || keyword == EKeyword.PRIVATE;
        }
    }
}
