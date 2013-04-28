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

        private void QueryExample() {
            var root = AutomationElement.RootElement;
            var window = UITree.Query(root).UsingTreeWalkerEngine()
                // Query 1.
                .FindChildren().Where()
                .Name().Contains("Microsoft Visual Studio")
                .And()
                .Type().Is(ControlType.Window)
                .Select().FirstResult()
                // Get the underlying automation element of query 1.
                .Element;

            var titleBar = UITree.Query(window).UsingDefaultEngine()
                // Query 2
                .FindChildren().Where()
                .Type().Is(ControlType.TitleBar)
                .Select().FirstResult()
                // Get the underlying automation element of query 2.
                .Element;

            Console.WriteLine(titleBar.Current.Name);
        }

        private void QueryBuilderExample() {
            var root = AutomationElement.RootElement;
            var titleBar = UITree
                .Query(root).UsingTreeWalkerEngine()
                // Query 1.
                .FindChildren().Where()
                .Name().Contains("Microsoft Visual Studio")
                .And()
                .Type().Is(ControlType.Window)
                .Select().FirstResult()
                // Continue with another query using the first result of query 1.
                .Query().UsingDefaultEngine()
                // Query 2
                .FindChildren().Where()
                .Type().Is(ControlType.TitleBar)
                .Select().FirstResult()
                // Get the underlying automation element of query 2.
                .Element;

            Console.WriteLine(titleBar.Current.Name);
        }

        private void PatternExample() {
            var root = AutomationElement.RootElement;
            var window = UITree.Query(root).UsingTreeWalkerEngine()
                // Query 1.
                .FindChildren().Where()
                .Name().Contains("Microsoft Visual Studio")
                .And()
                .Type().Is(ControlType.Window)
                .Select().FirstResult();
            // Execute using the 'Window' pattern.
            window.Execute<WindowPattern>(p => p.SetWindowVisualState(WindowVisualState.Minimized));

            var windows = UITree.Query(root).UsingTreeWalkerEngine()
                // Query 2.
                .FindChildren().Where()
                .Name().Contains("Microsoft Visual Studio")
                .And()
                .Type().Is(ControlType.Window)
                .Select().AllResults();
            // Execute for all matched components using the 'Window' pattern.
            windows.Execute<WindowPattern>(p => p.SetWindowVisualState(WindowVisualState.Minimized));
        }

    }

}
