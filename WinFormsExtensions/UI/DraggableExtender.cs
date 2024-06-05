using System.Drawing;
using System.Windows.Forms;

namespace WinFormsExtensions.UI
{

    /// <summary>
    /// Allows the form to be moved by dragging with the left mouse button.
    /// </summary>
    public class DraggableExtender {

        #region Fields

        /// <summary>
        /// The form to be made movable
        /// </summary>
        private Form targerForm;

        /// <summary>
        /// True if dragging with the left mouse button, false otherwise.
        /// </summary>
        private bool isLeftDrag;

        /// <summary>
        /// Mouse cursor position at the start of dragging
        /// </summary>
        private Point cursolOffset;

        #endregion Fields

        #region Properties

        /// <summary>
        /// True if you want to move by dragging, false if you do not.
        /// </summary>
        public bool Enabled {
            get {
                return enabled;
            }
            set {
                enabled = value;
            }
        }

        private bool enabled = true;

        #endregion Properties

        /// <summary>
        /// Initializes the instance.
        /// </summary>
        /// <param name="form">The form to be made movable</param>
        public DraggableExtender(Form form) {
            targerForm = form;

            // Start measuring the distance the mouse cursor moves.
            targerForm.MouseDown += (sender, e) => {
                if (!enabled) { return; }

                if (e.Button == MouseButtons.Left)
                {
                    if (targerForm.FormBorderStyle == FormBorderStyle.None)
                    {
                        cursolOffset = new Point(-e.X, -e.Y);
                    } else
                    {
                        // Add the window border and title bar height to the mouse cursor position.
                        cursolOffset = new Point(
                            -e.X - SystemInformation.FrameBorderSize.Width,
                            -e.Y - SystemInformation.CaptionHeight - SystemInformation.FrameBorderSize.Height);
                    }

                    isLeftDrag = true;
                }
            };

            // Move the form by the distance the mouse cursor moves.
            targerForm.MouseMove += (sender, e) => {
                if (!enabled) { return; }

                if (isLeftDrag)
                {
                    Point p = Control.MousePosition;
                    p.Offset(cursolOffset);
                    targerForm.Location = p;
                }
            };

            // Stop measuring the distance the mouse cursor moves.
            targerForm.MouseUp += (sender, e) => {
                if (!enabled) { return; }

                if (e.Button == MouseButtons.Left)
                {
                    isLeftDrag = false;
                }
            };
        }

    }
}
