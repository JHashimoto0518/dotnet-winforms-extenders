using System.Drawing;
using System.Windows.Forms;

namespace WinFormsExtensions.UI {

    /// <summary>
    /// Allows the form to be moved by dragging with the left mouse button.
    /// </summary>
    public class DraggableExtender {

        /// <summary>
        /// The form to be made movable
        /// </summary>
        private readonly Form targetForm;

        /// <summary>
        /// True if dragging with the left mouse button, false otherwise.
        /// </summary>
        private bool isLeftDrag;

        /// <summary>
        /// Mouse cursor position at the start of dragging
        /// </summary>
        private Point cursorOffset;

        /// <summary>
        /// True if you want to move by dragging, false if you do not.
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Initializes the instance.
        /// </summary>
        /// <param name="form">The form to be made movable</param>
        public DraggableExtender(Form form) {
            targetForm = form;

            // Start measuring the distance the mouse cursor moves.
            targetForm.MouseDown += (sender, e) => {
                if (!Enabled) { return; }
                if (e.Button != MouseButtons.Left) { return; }

                if (targetForm.FormBorderStyle == FormBorderStyle.None) {
                    cursorOffset = new Point(-e.X, -e.Y);
                } else {
                    // Add the window border and title bar height to the mouse cursor position.
                    cursorOffset = new Point(
                        -e.X - SystemInformation.FrameBorderSize.Width,
                        -e.Y - SystemInformation.CaptionHeight - SystemInformation.FrameBorderSize.Height);
                }

                isLeftDrag = true;
            };

            // Move the form by the distance the mouse cursor moves.
            targetForm.MouseMove += (sender, e) => {
                if (!Enabled) { return; }
                if (!isLeftDrag) { return; }

                Point p = Control.MousePosition;
                p.Offset(cursorOffset);
                targetForm.Location = p;
            };

            // Stop measuring the distance the mouse cursor moves.
            targetForm.MouseUp += (sender, e) => {
                if (!Enabled) { return; }
                if (e.Button != MouseButtons.Left) { return; }

                isLeftDrag = false;
            };
        }
    }
}
