using System;
using System.Diagnostics;
using System.Windows.Automation;

namespace Automation.UI.Examples {

    public class Example1 {

        public static void Main(string[] args) {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));

            var example = new Example1();
            example.QueryExample();
            example.QueryBuilderExample();
            example.PatternExample();
        }

        public void QueryExample() {
            var root = AutomationElement.RootElement;
            var window = UITree.Query(root).UsingTreeWalkerEngine()
                // Query 1.
                .FindChildren().Where()
                .NameContains("Microsoft Visual Studio").And().TypeIs(ControlType.Window)
                .Select().FirstResult()
                // Get the underlying automation element of query 1.
                .Element;

            var titleBar = UITree.Query(window).UsingDefaultEngine()
                // Query 2
                .FindChildren().Where()
                .TypeIs(ControlType.TitleBar)
                .Select().FirstResult()
                // Get the underlying automation element of query 2.
                .Element;

            Console.WriteLine(titleBar.Current.Name);
        }

        public void QueryBuilderExample() {
            var root = AutomationElement.RootElement;
            var titleBar = UITree
                .Query(root).UsingTreeWalkerEngine()
                // Query 1.
                .FindChildren().Where()
                .NameContains("Microsoft Visual Studio").And().TypeIs(ControlType.Window)
                .Select().FirstResult()
                // Continue with another query using the first result of query 1.
                .Query().UsingDefaultEngine()
                // Query 2
                .FindChildren().Where()
                .TypeIs(ControlType.TitleBar)
                .Select().FirstResult()
                // Get the underlying automation element of query 2.
                .Element;

            Console.WriteLine(titleBar.Current.Name);
        }

        public void PatternExample() {
            var root = AutomationElement.RootElement;
            var window = UITree.Query(root).UsingTreeWalkerEngine()
                // Query 1.
                .FindChildren().Where()
                .NameContains("Microsoft Visual Studio").And().TypeIs(ControlType.Window)
                .Select().FirstResult();

            // Execute using the 'Window' pattern.
            window.Execute<WindowPattern>(p => p.SetWindowVisualState(WindowVisualState.Minimized));
        }

    }

}
