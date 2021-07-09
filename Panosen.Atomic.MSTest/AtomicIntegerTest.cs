using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Panosen.Atomic.MSTest
{
    [TestClass]
    public class AtomicIntegerTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            int value = 0;
            AtomicInteger value2 = 0;

            int x = 40;
            int y = 1000;

            List<Task> tasks = new List<Task>();

            for (int i = 0; i < x; i++)
            {
                var task = Task.Run(() =>
                {
                    for (int i = 0; i < y; i++)
                    {
                        value++;
                        value2.GetAndIncrement();
                    }
                });

                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());

            Assert.AreNotEqual(x * y, value);
            Assert.AreEqual(x * y, value2.Value);
        }
    }
}
