using System;
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

        /// <summary>
        ///     Gets a pattern for the specified element.
        /// </summary>
        /// <typeparam name="T">The type of the returned pattern.</typeparam>
        /// <returns>The required pattern.</returns>
        public static T GetPattern<T>(AutomationElement element) where T : BasePattern {
            // First check if a field named 'Pattern' exists for the provided generic type.
            var patternType = typeof(T);
            var patternField = patternType.GetField("Pattern");
            if (patternField == null)
                throw new NotSupportedException("The pattern '" + patternType + "' is not supported");
            // Then get the value of this static field and use that to get the required pattern.
            var pattern = (AutomationPattern) patternField.GetValue(null);
            return (T) element.GetCurrentPattern(pattern);
        }

    }

}
