using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Panosen.Atomic
{
    /// <summary>
    /// 原子引用数组
    /// </summary>
    [Serializable]
    public class AtomicReferenceArray<T>
    {
        private readonly T[] array;

        /// <summary>
        /// Length
        /// </summary>
        public int Length
        {
            get
            {
                return this.array.Length;
            }
        }

        /// <summary>
        /// AtomicReferenceArray
        /// </summary>
        public AtomicReferenceArray(int length)
        {
            this.array = new T[length];
        }

        /// <summary>
        /// AtomicReferenceArray
        /// </summary>
        public AtomicReferenceArray(T[] array)
        {
            if (array == null)
                throw new ArgumentNullException("array");

            this.array = new T[array.Length];
            Array.Copy(array, 0, this.array, 0, array.Length);
        }

        /// <summary>
        /// AtomicReferenceArray
        /// </summary>
        public AtomicReferenceArray(IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException("items");

            this.array = items.ToArray();
        }

        /// <summary>
        /// this
        /// </summary>
        public T this[int index]
        {
            get
            {
                lock (this.array)
                {
                    return this.array[index];
                }
            }
            set
            {
                lock (this.array)
                {
                    this.array[index] = value;
                }
            }
        }

        /// <summary>
        /// CompareAndSet
        /// </summary>
        public bool CompareAndSet(int index, T expect, T update)
        {
            lock (this.array)
            {
                if (this.array[index].Equals(expect))
                {
                    this.array[index] = update;
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// GetAndSet
        /// </summary>
        public T GetAndSet(int index, T newValue)
        {
            lock (this.array)
            {
                T tmp = this.array[index];
                this.array[index] = newValue;
                return tmp;
            }
        }

        /// <summary>
        /// WeakCompareAndSet
        /// </summary>
        public bool WeakCompareAndSet(int index, T expect, T update)
        {
            return CompareAndSet(index, expect, update);
        }

        /// <summary>
        /// ToArray
        /// </summary>
        public T[] ToArray()
        {
            return (T[])this.array.Clone();
        }

        /// <summary>
        /// T[]
        /// </summary>
        public static implicit operator T[](AtomicReferenceArray<T> atomic)
        {
            if (atomic == null)
                return null;

            return atomic.ToArray();
        }

        /// <summary>
        /// AtomicReferenceArray&lt;T&gt;
        /// </summary>
        public static implicit operator AtomicReferenceArray<T>(T[] array)
        {
            if (array == null)
                return null;
            return new AtomicReferenceArray<T>(array);
        }
    }
}
