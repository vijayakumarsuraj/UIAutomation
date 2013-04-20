using System;
using System.Collections;
using System.Threading;
using System.Windows.Automation;

namespace Automation.UI.Tree {

    /// <summary>
    ///     Base class for the query engines used. These are responsible for taking the conditions specified in a
    ///     query and finding an automation element using them.
    /// </summary>
    public abstract class SearchEngine {

        /// <summary>
        ///     The maximum time a thread will sleep before re-trying when waiting for an element to become available.
        /// </summary>
        protected const int MaxSleepTimeout = 1000;

        /// <summary>
        ///     The default amount of time to wait for an element to become available.
        /// </summary>
        protected readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(5);

        /// <summary>
        ///     Gets the first automation element found using the conditions specified in the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="timeout">The maximum amount of time to wait for the required element to become available.</param>
        /// <returns>The automation element (or null if no element was found).</returns>
        public abstract AutomationElement GetFirstResult(Query query, TimeSpan timeout);

        /// <summary>
        ///     Gets the first automation element found using the conditions specified in the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>The automation element (or null if no element was found).</returns>
        public AutomationElement GetFirstResult(Query query) {
            return GetFirstResult(query, DefaultTimeout);
        }

        /// <summary>
        ///     Gets all the automation elements found using the condition specified in the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="timeout">The maximum amount of time to wait for at least one element to become available.</param>
        /// <returns>The collection of automation elements.</returns>
        public abstract IEnumerable GetAllResults(Query query, TimeSpan timeout);

        /// <summary>
        ///     Gets all the automation elements found using the condition specified in the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>The collection of automation elements.</returns>
        public IEnumerable GetAllResults(Query query) {
            return GetAllResults(query, DefaultTimeout);
        }

        /// <summary>
        ///     Invokes the specified callback repeatedly until either the specified timeout expires or an
        ///     automation element is returned.
        /// </summary>
        /// <param name="callback">The get results callback method.</param>
        /// <param name="timeout">The maximum amount of time to wait for the callback to return an automation element.</param>
        /// <returns>The automation element, or enumarable, or null if no element was found.</returns>
        protected object ExecuteGetResult(GetResultCallback callback, TimeSpan timeout) {
            var start = DateTime.Now;
            var timeoutRemaining = (int) timeout.TotalMilliseconds;
            while (true) {
                var result = callback();
                if (result != null) return result;
                if (timeoutRemaining > 0) {
                    // The amount of time slept will never be more than the MaxSleepTimeout constant.
                    // The total time spent sleeping will also never be more than the total timeout specified.
                    var actualSleepTimeout = timeoutRemaining > MaxSleepTimeout ? MaxSleepTimeout : timeoutRemaining;
                    Thread.Sleep(actualSleepTimeout);
                    // The timeout is decremented so that the remaining time is the total time - elapsed time.
                    timeoutRemaining -= (int) (DateTime.Now - start).TotalMilliseconds;
                } else {
                    // Time is up and still no element. So return null.
                    return null;
                }
            }
        }

        /// <summary>
        ///     Callback to be executed that will search for an automation element.
        /// </summary>
        /// <returns>The automation element, or enumarable, or null if no element was available.</returns>
        protected delegate object GetResultCallback();

    }

}
