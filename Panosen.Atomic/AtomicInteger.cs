using System;
using System.Globalization;
using System.Threading;

namespace Panosen.Atomic
{
    /// <summary>
    /// 原子int
    /// </summary>
    [Serializable]
    public class AtomicInteger : IFormattable
    {
        private volatile int integerValue;

        /// <summary>
        /// Value
        /// </summary>
        public int Value
        {
            get { return this.integerValue; }
            set { this.integerValue = value; }
        }

        /// <summary>
        /// Atomic
        /// </summary>
        public AtomicInteger()
            : this(0)
        {
        }

        /// <summary>
        /// AtomicInteger
        /// </summary>
        public AtomicInteger(int initialValue)
        {
            this.integerValue = initialValue;
        }

        /// <summary>
        /// ++i
        /// </summary>
        /// <param name="delta"></param>
        /// <returns></returns>
        public int AddAndGet(int delta)
        {
            return Interlocked.Add(ref this.integerValue, delta);
        }

        /// <summary>
        /// CompareAndSet
        /// </summary>
        public bool CompareAndSet(int expect, int update)
        {
            return Interlocked.CompareExchange(ref this.integerValue, update, expect) == expect;
        }

        /// <summary>
        /// --i
        /// </summary>
        /// <returns></returns>
        public int DecrementAndGet()
        {
            return Interlocked.Decrement(ref this.integerValue);
        }

        /// <summary>
        /// i--
        /// </summary>
        /// <returns></returns>
        public int GetAndDecrement()
        {
            return Interlocked.Decrement(ref this.integerValue) + 1;
        }

        /// <summary>
        /// i++
        /// </summary>
        /// <returns></returns>
        public int GetAndIncrement()
        {
            return Interlocked.Increment(ref this.integerValue) - 1;
        }

        /// <summary>
        /// GetAndSet
        /// </summary>
        public int GetAndSet(int newValue)
        {
            return Interlocked.Exchange(ref this.integerValue, newValue);
        }

        /// <summary>
        /// IncrementAndGet
        /// </summary>
        public int IncrementAndGet()
        {
            return Interlocked.Increment(ref this.integerValue);
        }

        /// <summary>
        /// WeakCompareAndSet
        /// </summary>
        public bool WeakCompareAndSet(int expect, int update)
        {
            return CompareAndSet(expect, update);
        }

        /// <summary>
        /// Equals
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj as AtomicInteger == this;
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
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(AtomicInteger left, AtomicInteger right)
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
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(AtomicInteger left, AtomicInteger right)
        {
            return !(left == right);
        }

        /// <summary>
        /// 转换为int
        /// </summary>
        /// <param name="atomic"></param>
        public static implicit operator int(AtomicInteger atomic)
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
        /// 转换为 AtomicInteger
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator AtomicInteger(int value)
        {
            return new AtomicInteger(value);
        }
    }
}