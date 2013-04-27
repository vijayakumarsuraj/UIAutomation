using System.Globalization;
using System.Windows.Automation;

namespace Automation.UI.Util {

    /// <summary>
    ///     Provides convenience methods for working with automation properties.
    /// </summary>
    public static class AutomationPropertyHelper {

        /// <summary>
        ///     Compares the specified property values to see if they are equal.
        /// </summary>
        /// <param name="property1">The first property.</param>
        /// <param name="property2">The second property.</param>
        /// <returns>True if the values are equal, false otherwise.</returns>
        public new static bool Equals(object property1, object property2) {
            // If the type of the property is ControlType, compare the ids instead.
            var type1 = property1.GetType();
            if (type1 == typeof(ControlType)) property1 = ((ControlType) property1).Id;
            // If the type of the property is ControlType, compare the ids instead.
            var type2 = property2.GetType();
            if (type2 == typeof(ControlType)) property2 = ((ControlType) property2).Id;
            // Compare and return.
            return property1.Equals(property2);
        }

        /// <summary>
        ///     Gets the programmatic name for the specified automation property.
        /// </summary>
        /// <param name="property">The automation property.</param>
        /// <returns>The programmatic name.</returns>
        public static string ProgrammaticName(AutomationProperty property) {
            var parts = property.ProgrammaticName.Split('.');
            return parts[parts.Length - 1].Replace("Property", "");
        }

        /// <summary>
        ///     Gets the string representation of the specified automation property value.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>The string representation of the property.</returns>
        public static string ToString(object property) {
            if (property.GetType() == typeof(ControlType)) {
                var id = ((ControlType) property).Id;
                return id.ToString(CultureInfo.InvariantCulture);
            }

            return property.ToString();
        }

    }

}
