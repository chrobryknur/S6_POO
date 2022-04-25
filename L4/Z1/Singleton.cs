using System;
using System.Threading;
using System.Timers;

namespace Z1
{
    public class Singleton
    {
        private Singleton() {}
        public static Singleton Instance()
        {
            if (instance == null)
            {
                instance = new Singleton();
            }

            return instance;
        }

        private static Singleton instance;

        public static void Main(string[] args)
        {

        }
    };

    public class ThreadSingleton
    {
        private ThreadSingleton() {}
        public static ThreadSingleton Instance()
        {
            if(instance == null)
            {
                instance = new ThreadLocal<ThreadSingleton>();
            }

            return instance.Value;
        }

        private static ThreadLocal<ThreadSingleton> instance;
    }

    public class TimeLimitedSingleton
    {
        private TimeLimitedSingleton() {}

        public static TimeLimitedSingleton Instance()
        {
            if(instance == null)
            {
                instance = new TimeLimitedSingleton();
            }

            StartTimer();


            return instance;
        }

        private static void StartTimer()
        {
            timer = new System.Timers.Timer();
            timer.Interval = 5000;
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            instance = new TimeLimitedSingleton();
        }

        private static TimeLimitedSingleton instance;
        private static System.Timers.Timer timer;
    }
}