using System.Windows.Automation;

using Automation.UI;
using Automation.UI.Tree.QueryParts;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UI.Tests {

    /// <summary>
    ///     Tests the various methods of the UI component interface.
    /// </summary>
    [TestClass]
    public class TestUIComponent {

        /// <summary>
        ///     Gets a query set to search children using a tree walker engine.
        /// </summary>
        /// <returns></returns>
        private QueryWherePart Query() {
            return UITree.Query(AutomationElement.RootElement).UsingTreeWalkerEngine().FindChildren();
        }

        /// <summary>
        ///     Tests the 'Execute' method.
        /// </summary>
        [TestMethod]
        public void TestExecuteAll() {
            // Fire the query.
            var windows = Query().Where()
                .Name().StartsWith("UIAutomation")
                .And()
                .Name().EndsWith("Visual Studio")
                .Select().AllResults();
            // Validate the result.
            Assert.IsNotNull(windows);

            // Execute arbitrary steps using the 'Window' pattern.
            windows.Execute<WindowPattern>(p => p.SetWindowVisualState(WindowVisualState.Minimized));
            // Validate results
            foreach (var result in windows) {
                var windowPattern = (WindowPattern) result.Element.GetCurrentPattern(WindowPattern.Pattern);
                var state = windowPattern.Current.WindowVisualState;
                Assert.IsTrue(state == WindowVisualState.Minimized);
                // Restore.
                windowPattern.SetWindowVisualState(WindowVisualState.Maximized);
            }
        }

        /// <summary>
        ///     Tests the 'Execute' method.
        /// </summary>
        [TestMethod]
        public void TestExecute() {
            // Fire the query.
            var window = Query().Where()
                .Name().StartsWith("UIAutomation")
                .And()
                .Name().EndsWith("Visual Studio")
                .Select().FirstResult();
            // Validate the result.
            Assert.IsNotNull(window);

            // Execute arbitrary steps using the 'Window' pattern.
            window.Execute<WindowPattern>(p => p.SetWindowVisualState(WindowVisualState.Minimized));
            // Validate the result.
            var windowPattern = (WindowPattern) window.Element.GetCurrentPattern(WindowPattern.Pattern);
            var state = windowPattern.Current.WindowVisualState;
            Assert.IsTrue(state == WindowVisualState.Minimized);
            // Restore.
            windowPattern.SetWindowVisualState(WindowVisualState.Maximized);
        }

    }

}
