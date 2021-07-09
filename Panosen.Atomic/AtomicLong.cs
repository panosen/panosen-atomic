using System;
using System.Globalization;
using System.Threading;

namespace Panosen.Atomic
{
    /// <summary>
    /// 原子long
    /// </summary>
    [Serializable]
    public class AtomicLong : IFormattable
    {
        private long longValue;

        /// <summary>
        /// Value
        /// </summary>
        public long Value
        {
            get
            {
                return Interlocked.Read(ref this.longValue);
            }
            set
            {
                Interlocked.Exchange(ref this.longValue, value);
            }
        }

        /// <summary>
        /// AtomicLong
        /// </summary>
        public AtomicLong()
            : this(0)
        {
        }

        /// <summary>
        /// AtomicLong
        /// </summary>
        public AtomicLong(long initialValue)
        {
            this.longValue = initialValue;
        }

        /// <summary>
        /// AddAndGet
        /// </summary>
        public long AddAndGet(long delta)
        {
            return Interlocked.Add(ref this.longValue, delta);
        }

        /// <summary>
        /// CompareAndSet
        /// </summary>
        public bool CompareAndSet(long expect, long update)
        {
            return Interlocked.CompareExchange(ref this.longValue, update, expect) == expect;
        }

        /// <summary>
        /// DecrementAndGet
        /// </summary>
        public long DecrementAndGet()
        {
            return Interlocked.Decrement(ref this.longValue);
        }

        /// <summary>
        /// GetAndDecrement
        /// </summary>
        public long GetAndDecrement()
        {
            return Interlocked.Decrement(ref this.longValue) + 1;
        }

        /// <summary>
        /// GetAndIncrement
        /// </summary>
        public long GetAndIncrement()
        {
            return Interlocked.Increment(ref this.longValue) - 1;
        }

        /// <summary>
        /// GetAndSet
        /// </summary>
        public long GetAndSet(long newValue)
        {
            return Interlocked.Exchange(ref this.longValue, newValue);
        }

        /// <summary>
        /// IncrementAndGet
        /// </summary>
        public long IncrementAndGet()
        {
            return Interlocked.Increment(ref this.longValue);
        }

        /// <summary>
        /// WeakCompareAndSet
        /// </summary>
        public bool WeakCompareAndSet(long expect, long update)
        {
            return CompareAndSet(expect, update);
        }

        /// <summary>
        /// Equals
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj as AtomicLong == this;
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
        public static bool operator ==(AtomicLong left, AtomicLong right)
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
        public static bool operator !=(AtomicLong left, AtomicLong right)
        {
            return !(left == right);
        }

        /// <summary>
        /// long
        /// </summary>
        public static implicit operator long(AtomicLong atomic)
        {
            if (ReferenceEquals(atomic, null))
            {
                return 0;
            }
            else
            {
                return atomic.Value;
            }
        }

        /// <summary>
        /// AtomicLong
        /// </summary>
        public static implicit operator AtomicLong(long value)
        {
            return new AtomicLong(value);
        }
    }
}
