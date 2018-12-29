using DotnetUnitTest;
using System;
using System.Diagnostics;
using System.Threading;
using Xunit;

namespace Test
{
    public class AsyncServiceTest
    {

        [Fact]
        public void TestCallback()
        {
            var actual = string.Empty;

            var aService = new AsyncService((s) => { actual = s; });
            aService.DoSomethingThatCallsBack("A");

            Assert.Equal("AA", actual);
        }

        [Fact]
        public void TestEventualCallback()
        {
            AutoResetEvent autoResetEvent = new AutoResetEvent(false);
            var actual = string.Empty;

            var sw = new Stopwatch();
            sw.Start();

            var aService = new AsyncService();
            aService.DoSomethingThatCallsBackEventually("A", (s) => { actual = s; autoResetEvent.Set(); });
            sw.Stop();

            Assert.True(sw.ElapsedMilliseconds < 500);
            Assert.True(autoResetEvent.WaitOne());
            Assert.Equal("DelayedA", actual);
        }

        [Fact]
        public void TestEventualCallbackSuccessWithTimeout()
        {
            AutoResetEvent autoResetEvent = new AutoResetEvent(false);

            var actual = string.Empty;

            var aService = new AsyncService();

            void onSuccess(string s)
            {
                actual = s;
                autoResetEvent.Set();
            }

            void onFailure(string s)
            {
                actual = s;
                autoResetEvent.Set();
            }

            aService.DoSomethingThatCallsbackEventuallyOrTimesOut("A", onSuccess, onFailure, 2500);

            Assert.True(autoResetEvent.WaitOne());
            Assert.Equal("DelayedA", actual);
        }

        [Fact]
        public void TestEventualCallbackFailureWithTimeout()
        {
            AutoResetEvent autoResetEvent = new AutoResetEvent(false);

            var actual = string.Empty;

            var aService = new AsyncService();

            void onSuccess(string s)
            {
                actual = s;
                autoResetEvent.Set();
            }
            void onFailure(string s)
            {
                actual = s;
                autoResetEvent.Set();
            }

            aService.DoSomethingThatCallsbackEventuallyOrTimesOut("A", onSuccess, onFailure, 1500);

            Assert.True(autoResetEvent.WaitOne());
            Assert.Equal("Timed Out", actual);
        }



        [Fact]
        public void TestEvent()
        {
            var actual = string.Empty;

            var aService = new AsyncService();
            aService.SomethingHappened += (_, s) => { actual = s; };

            aService.DoSomethingThatFiresAnEvent("A");

            Assert.Equal("AA", actual);
        }

        [Fact]
        public void TestEventualEvent()
        {
            AutoResetEvent autoResetEvent = new AutoResetEvent(false);
            var actual = string.Empty;

            var sw = new Stopwatch();
            sw.Start();

            var aService = new AsyncService();
            aService.SomethingHappened += (_, s) => { actual = s; autoResetEvent.Set(); };

            aService.DoSomethingThatFiresAnEventEventually("A");

            sw.Stop();

            Assert.True(sw.ElapsedMilliseconds < 500);
            Assert.True(autoResetEvent.WaitOne());
            Assert.Equal("DelayedA", actual);
        }


        [Fact]
        public void TestEventualEventTimesOut()
        {
            AutoResetEvent autoResetEvent = new AutoResetEvent(false);
            var actual = string.Empty;

            var sw = new Stopwatch();
            sw.Start();

            var aService = new AsyncService();
            aService.SomethingHappened += (_, s) =>
            {
                actual = s;
                autoResetEvent.Set();
            };

            aService.DoSomethingThatFiresAnEventEventually("A");

            sw.Stop();

            Assert.True(sw.ElapsedMilliseconds < 500);
            Assert.False(autoResetEvent.WaitOne(1500));
            Assert.Equal("", actual);
        }
    }
}
