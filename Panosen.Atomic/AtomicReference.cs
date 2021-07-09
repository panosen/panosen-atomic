using System;
using System.Globalization;
using System.Threading;

namespace Panosen.Atomic
{
    /// <summary>
    /// 原子引用
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class AtomicReference<T> where T : class
    {
        private volatile T reference;

        /// <summary>
        /// Value
        /// </summary>
        public T Value
        {
            get { return reference; }
            set { this.reference = value; }
        }

        /// <summary>
        /// AtomicReference
        /// </summary>
        public AtomicReference()
            : this(default(T))
        {
        }

        /// <summary>
        /// AtomicReference
        /// </summary>
        public AtomicReference(T initialValue)
        {
            reference = initialValue;
        }

        /// <summary>
        /// CompareAndSet
        /// </summary>
        public bool CompareAndSet(T expect, T update)
        {
            return ReferenceEquals(expect, Interlocked.CompareExchange(ref this.reference, update, expect));
        }

        /// <summary>
        /// GetAndSet
        /// </summary>
        public T GetAndSet(T newValue)
        {
            return Interlocked.Exchange(ref this.reference, newValue);
        }

        /// <summary>
        /// WeakCompareAndSet
        /// </summary>
        public bool WeakCompareAndSet(T expect, T update)
        {
            return CompareAndSet(expect, update);
        }

        /// <summary>
        /// Equals
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj as AtomicReference<T> == this;
        }

        /// <summary>
        /// GetHashCode
        /// </summary>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>
        /// ToString
        /// </summary>
        public override string ToString()
        {
            return Value.ToString();
        }

        /// <summary>
        /// ==
        /// </summary>
        public static bool operator ==(AtomicReference<T> left, AtomicReference<T> right)
        {
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
                return false;

            return ReferenceEquals(left.Value, right.Value);
        }

        /// <summary>
        /// !=
        /// </summary>
        public static bool operator !=(AtomicReference<T> left, AtomicReference<T> right)
        {
            return !(left == right);
        }

        /// <summary>
        /// T
        /// </summary>
        public static implicit operator T(AtomicReference<T> atomic)
        {
            if (atomic == null)
            {
                return default(T);
            }
            else
            {
                return atomic.Value;
            }
        }

        /// <summary>
        /// AtomicReference&lt;T&gt;
        /// </summary>
        public static implicit operator AtomicReference<T>(T value)
        {
            return new AtomicReference<T>(value);
        }
    }
}