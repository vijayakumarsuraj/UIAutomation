using System.Globalization;
using System.Windows.Automation;

namespace Automation.UI.Util {

    /// <summary>
    ///     Provides convenience methods for working with automation properties.
    /// </summary>
    public static class AutomationPropertyHelper {

        /// <summary>
        ///     Gets the string representation of the specified automation property to a string.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>The string representation of the property.</returns>
        public static string ToString(object property) {
            if (property.GetType() == typeof (ControlType)) {
                var id = ((ControlType) property).Id;
                return id.ToString(CultureInfo.InvariantCulture);
            }

            return property.ToString();
        }

    }

}
