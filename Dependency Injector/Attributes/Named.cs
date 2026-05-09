using System;
using System.Collections.Generic;
using System.Text;

namespace DI.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field)]
    public class Named : Attribute
    {
        public string Name { get; }
        public Named(string name) => Name = name;
    }
}
