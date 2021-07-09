using System;
using System.Globalization;
using System.Threading;

namespace Panosen.Atomic
{
    /// <summary>
    /// 原子boolean
    /// </summary>
    [Serializable]
    public class AtomicBoolean : IFormattable
    {
        private volatile int booleanValue;

        /// <summary>
        /// Current Value
        /// </summary>
        public bool Value
        {
            get
            {
                return this.booleanValue != 0;
            }
            set
            {
                this.booleanValue = value ? 1 : 0;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public AtomicBoolean()
            : this(false)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public AtomicBoolean(bool initialValue)
        {
            Value = initialValue;
        }

        /// <summary>
        /// CompareAndSet
        /// </summary>
        /// <param name="expect"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public bool CompareAndSet(bool expect, bool update)
        {
            int expectedIntValue = expect ? 1 : 0;
            int newIntValue = update ? 1 : 0;
            return Interlocked.CompareExchange(ref this.booleanValue, newIntValue, expectedIntValue) == expectedIntValue;
        }

        /// <summary>
        /// GetAndSet
        /// </summary>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public bool GetAndSet(bool newValue)
        {
            return Interlocked.Exchange(ref this.booleanValue, newValue ? 1 : 0) != 0;
        }

        /// <summary>
        /// WeakCompareAndSet
        /// </summary>
        /// <param name="expect"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public bool WeakCompareAndSet(bool expect, bool update)
        {
            return CompareAndSet(expect, update);
        }

        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return obj as AtomicBoolean == this;
        }

        /// <summary>
        /// GetHashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>
        /// ToString
        /// </summary>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// ToString
        /// </summary>
        public string ToString(IFormatProvider formatProvider)
        {
            return Value.ToString(formatProvider);
        }

        /// <summary>
        /// ToString
        /// </summary>
        public string ToString(string format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// ToString
        /// </summary>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return Value.ToString(formatProvider);
        }

        /// <summary>
        /// ==
        /// </summary>
        public static bool operator ==(AtomicBoolean left, AtomicBoolean right)
        {
            if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
                return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
                return false;

            return left.Value == right.Value;
        }

        /// <summary>
        /// !=
        /// </summary>
        public static bool operator !=(AtomicBoolean left, AtomicBoolean right)
        {
            return !(left == right);
        }

        /// <summary>
        /// bool
        /// </summary>
        public static implicit operator bool(AtomicBoolean atomic)
        {
            if (ReferenceEquals(atomic, null))
            {
                return false;
            }
            else
            {
                return atomic.Value;
            }
        }

        /// <summary>
        /// AtomicBoolean
        /// </summary>
        public static implicit operator AtomicBoolean(bool value)
        {
            return new AtomicBoolean(value);
        }
    }
}