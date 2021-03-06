using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Panosen.Atomic.MSTest
{
    [TestClass]
    public class AtomicLongTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            AtomicLong value = 0;

            int x = 50;
            int y = 1000;

            List<Task> tasks = new List<Task>();

            for (int i = 0; i < x; i++)
            {
                var task = Task.Run(() =>
                {
                    for (int i = 0; i < y; i++)
                    {
                        value.GetAndIncrement();
                    }
                });

                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());

            Assert.AreEqual(x * y, value.Value);
        }
    }
}
