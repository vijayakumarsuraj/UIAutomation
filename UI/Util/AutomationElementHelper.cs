using System.Windows.Automation;

namespace Automation.UI.Util {

    /// <summary>
    ///     Provides convenience methods for working with UI Automation's automation elements.
    /// </summary>
    public static class AutomationElementHelper {

        /// <summary>
        ///     Gets a string represenation of the specified automation element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The string representation.</returns>
        public static string ToString(AutomationElement element) {
            var name = element.Current.Name;
            var type = element.Current.ControlType.ProgrammaticName;
            var typeParts = type.Split('.');

            return typeParts[typeParts.Length - 1] + "('" + name + "')";
        }

    }

}
