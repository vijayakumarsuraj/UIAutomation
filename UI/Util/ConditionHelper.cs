using System;
using System.Linq;
using System.Windows.Automation;

using Automation.UI.ElementFinder;

namespace Automation.UI.Util {

    /// <summary>
    ///     Convenience methods for working with Condition objects.
    /// </summary>
    public static class ConditionHelper {

        /// <summary>
        ///     Check to see if the specified automation element meets the requirements of the specified condition.
        /// </summary>
        /// <param name="condition">The condition to check.</param>
        /// <param name="element">The automation element to check.</param>
        /// <returns>True if the automation element meets the condition's requirements. False otherwise.</returns>
        public static bool IsMeetsRequirements(Condition condition, AutomationElement element) {
            var type = condition.GetType();
            if (condition == Condition.TrueCondition)
                // Always return true.
                return true;
            if (condition == Condition.FalseCondition)
                // Always returns false.
                return false;
            if (type == typeof (NotCondition))
                // Return the negation of the inner condition.
                return !IsMeetsRequirements(((NotCondition) condition).Condition, element);
            if (type == typeof (OrCondition))
                // Return true if any of the inner conditions are true.
                return ((OrCondition) condition).GetConditions().Any(inner => IsMeetsRequirements(inner, element));
            if (type == typeof (AndCondition))
                return ((AndCondition) condition).GetConditions().All(inner => IsMeetsRequirements(inner, element));
            if (type == typeof (PropertyCondition)) {
                var propertyCondition = (PropertyCondition) condition;
                var actual = (string) element.GetCurrentPropertyValue(propertyCondition.Property);
                var expected = (string) propertyCondition.Value;
                return Matchers.Exact.IsMatch(actual, expected);
            }
            if (type == typeof (StringPropertyCondition))
                return ((StringPropertyCondition) condition).IsMatch(element);
            throw new NotSupportedException("Condition '" + type + "' is not supported");
        }

        /// <summary>
        ///     Gets the string representation of the specified condition.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <returns>The string representation.</returns>
        public static string ToString(Condition condition) {
            var type = condition.GetType();
            if (condition == Condition.TrueCondition)
                return "TRUE";
            if (condition == Condition.FalseCondition)
                return "FALSE";
            if (type == typeof (NotCondition))
                return "NOT " + ToString(((NotCondition) condition).Condition);
            if (type == typeof (OrCondition))
                return string.Join(" OR ", ((OrCondition) condition).GetConditions().Select(ToString).ToList());
            if (type == typeof (AndCondition))
                return string.Join(" AND ", ((AndCondition) condition).GetConditions().Select(ToString).ToList());
            if (type == typeof (PropertyCondition))
                return ToString((PropertyCondition) condition);
            if (type == typeof (StringPropertyCondition))
                return ToString((StringPropertyCondition) condition);
            throw new NotSupportedException("Condition '" + type + "' not supported");
        }

        /// <summary>
        ///     Gets the string representation of the specified property condition.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <returns>The string representation.</returns>
        private static string ToString(PropertyCondition condition) {
            var parts = condition.Property.ProgrammaticName.Split('.');
            var property = parts[parts.Length - 1];
            var value = condition.Value.ToString();

            return property + "='" + value + "'";
        }

        /// <summary>
        ///     Gets the string representation of the specified string property condition.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        private static string ToString(StringPropertyCondition condition) {
            var propertyParts = condition.Property.ProgrammaticName.Split('.');
            var property = propertyParts[propertyParts.Length - 1];
            var value = (string) condition.Value;
            var op = condition.Matcher.ToString();

            return property + op + "'" + value + "'";
        }

    }

}
