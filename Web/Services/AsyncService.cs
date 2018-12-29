using System;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetUnitTest
{
    /// <summary>
    /// Event、Action、Async单元测试示例
    /// </summary>
    public class AsyncService
    {

        public event EventHandler<string> SomethingHappened;

        public AsyncService()
        {
        }

        private Action<string> callback;

        public AsyncService(Action<string> callback)
        {
            this.callback = callback;
        }

        /// <summary>
        /// 立即回调
        /// </summary>
        /// <param name="str"></param>
        public void DoSomethingThatCallsBack(string str)
        {
            callback(str + str);
        }

        /// <summary>
        /// 等待2秒再回调
        /// </summary>
        /// <param name="str"></param>
        /// <param name="callback"></param>
        public async void DoSomethingThatCallsBackEventually(string str, Action<string> callback)
        {
            var s = await LongRunningOperation(str);
            callback(s);
        }

        /// <summary>
        /// timeout < 2s 回调success，否则回调 failure
        /// </summary>
        /// <param name="str"></param>
        /// <param name="success"></param>
        /// <param name="failure"></param>
        /// <param name="timeout"></param>
        public async void DoSomethingThatCallsbackEventuallyOrTimesOut(string str, Action<string> success, Action<string> failure, int timeout)
        {
            var task = LongRunningOperation(str);
            if (await Task.WhenAny(task, Task.Delay(timeout)) == task)
            {
                success(await task);
            }
            else
            {
                failure("Timed Out");
            }
        }

        /// <summary>
        /// 立即触发SomethingHappened事件
        /// </summary>
        /// <param name="str"></param>
        public void DoSomethingThatFiresAnEvent(string str)
        {
            SomethingHappened?.Invoke(this, str + str);
        }

        /// <summary>
        /// 等待2秒触发SomethingHappened事件
        /// </summary>
        /// <param name="str"></param>
        public async void DoSomethingThatFiresAnEventEventually(string str)
        {
            var s = await LongRunningOperation(str);

            SomethingHappened?.Invoke(this, s);
        }


        private Task<string> LongRunningOperation(string s)
        {
            return Task.Run(() =>
            {
                Thread.Sleep(2000);
                return "Delayed" + s;
            });
        }

    }
}
