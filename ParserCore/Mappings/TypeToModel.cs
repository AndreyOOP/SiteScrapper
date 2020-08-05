using System;

namespace ParserCore
{
    public class TypeToModel
    {
        public Type Type { get; set; }

        /// <summary>
        /// Model of type <see cref="Type"/>
        /// </summary>
        public object Model { get; set; }
    }
}
