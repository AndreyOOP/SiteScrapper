using System;
using System.Collections.Generic;

namespace ParserCore
{
    /// <inheritdoc/>
    public class InOutKey<TIn, TOut> : IInOutKey
    {
        /// <inheritdoc/>
        public Type InType { get => typeof(TIn); }

        /// <inheritdoc/>
        public Type OutType { get => typeof(TOut); }

        public override bool Equals(object obj)
        {
            return obj is InOutKey<TIn, TOut> key &&
                   EqualityComparer<Type>.Default.Equals(InType, key.InType) &&
                   EqualityComparer<Type>.Default.Equals(OutType, key.OutType);
        }

        public override int GetHashCode()
        {
            var hashCode = 1947211957;
            hashCode = hashCode * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(InType);
            hashCode = hashCode * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(OutType);
            return hashCode;
        }

        public static bool operator ==(InOutKey<TIn, TOut> left, object right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(InOutKey<TIn, TOut> left, object right)
        {
            return !left.Equals(right);
        }
    }
}
