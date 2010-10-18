using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions; 

namespace MyZilla.BusinessEntities
{
    public struct Dependencies
    {

        public const string NoComponentDependency = "NoComponentDependency";

    }

    public struct LoggingCategory
    {
        public const string General = "General";
        public const string Exception = "Exception";
        public const string Warning = "Warning";
        public const string Debug = "Debug";
    }

}
