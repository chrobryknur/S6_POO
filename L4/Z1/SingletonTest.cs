using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using Z1;

namespace SingletonTest
{
    [TestClass]
    public class SingletonTest
    {
        [TestMethod]
        public void Two_instances_of_singletons_should_reference_the_same_object()
        {
            Singleton singleton1 = Singleton.Instance();
            Singleton singleton2 = Singleton.Instance();
            Assert.AreEqual(singleton1, singleton2);
        }

        [TestMethod]
        public void Two_instances_of_thread_singletons_in_the_same_thread_should_reference_the_same_object()
        {
            ThreadSingleton singleton1 = ThreadSingleton.Instance();
            ThreadSingleton singleton2 = ThreadSingleton.Instance();
            Assert.AreEqual(singleton1, singleton2);
        }

        [TestMethod]
        public void Two_instances_of_thread_singletons_in_two_different_threads_should_reference_two_different_objects()
        {
            ThreadSingleton singleton1 = null;
            ThreadSingleton singleton2 = null;

            Thread thread1 = new Thread(() =>
            {
                singleton1 = ThreadSingleton.Instance();
            });

            Thread thread2 = new Thread(() =>
            {
                singleton2 = ThreadSingleton.Instance();
            });

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

            Assert.AreEqual(singleton1, singleton2);
        }

        [TestMethod]
        public void Two_instances_time_limited_singletons_should_reference_the_same_object_in_timeframe_shorter_than_5s()
        {
            TimeLimitedSingleton singleton1 = TimeLimitedSingleton.Instance();
            Thread.Sleep(1000);
            TimeLimitedSingleton singleton2 = TimeLimitedSingleton.Instance();

            Assert.AreEqual(singleton1, singleton2);
        }

        [TestMethod]
        public void Two_instances_time_limited_singletons_should_reference_two_different_objects_in_timeframe_longer_than_5s()
        {
            TimeLimitedSingleton singleton1 = TimeLimitedSingleton.Instance();
            Thread.Sleep(6000);
            TimeLimitedSingleton singleton2 = TimeLimitedSingleton.Instance();

            Assert.AreNotEqual(singleton1, singleton2);
        }
    }
}