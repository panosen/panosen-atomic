using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Panosen.Atomic
{
    /// <summary>
    /// 原子int数组
    /// </summary>
    [Serializable]
    public class AtomicIntegerArray : AtomicReferenceArray<int>
    {
        /// <summary>
        /// AtomicIntegerArray
        /// </summary>
        public AtomicIntegerArray(int length)
            : base(length)
        {
        }

        /// <summary>
        /// AtomicIntegerArray
        /// </summary>
        public AtomicIntegerArray(int[] array)
            : base(array)
        {
        }

        /// <summary>
        /// AtomicIntegerArray
        /// </summary>
        public AtomicIntegerArray(IEnumerable<int> items)
            : base(items)
        {
        }
    }
}