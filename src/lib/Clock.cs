using System;

namespace lib
{
    // source: https://www.limilabs.com/blog/testing-datetime-now
    public class Clock : IDisposable
    {
        private static DateTime? _nowForTest;

        public static DateTime Now
        {
            get { return _nowForTest ?? DateTime.Now; }
        }

        public static IDisposable NowIs(DateTime dateTime)
        {
            _nowForTest = dateTime;
            return new Clock();
        }

        public void Dispose()
        {
            _nowForTest = null;
        }
    };
}