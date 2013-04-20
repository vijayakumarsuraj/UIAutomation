using System.Windows.Automation;

namespace Automation.UI {

    /// <summary>
    ///     The base class for all UI elements.
    ///     This class provides a convenient wrapper for interacting with the UI automation elements.
    /// </summary>
    public abstract class UIComponent {

        /// <summary>
        ///     New UI component.
        /// </summary>
        /// <param name="parent">The parent component.</param>
        /// <param name="element">The underlying element wrapped by this component.</param>
        protected UIComponent(UIComponent parent, AutomationElement element) {
            Parent = parent;
            Element = element;
        }

        /// <summary>
        ///     The underlying element wrapped by this component.
        /// </summary>
        public AutomationElement Element { get; protected set; }

        /// <summary>
        ///     The UI component that is the parent of this component in the UI tree.
        ///     Will be nil for the root element (i.e. and application's main window).
        /// </summary>
        protected UIComponent Parent { get; set; }

    }

}
