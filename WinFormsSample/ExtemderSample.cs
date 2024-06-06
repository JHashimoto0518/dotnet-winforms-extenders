using WinFormsExtensions.UI;

namespace WinFormsSample {
    public partial class ExtemderSample : Form {
        public ExtemderSample() {
            InitializeComponent();

            new DraggableExtender(this);
        }
    }
}
