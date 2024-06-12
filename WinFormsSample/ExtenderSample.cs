using WinFormsExtensions.UI;

namespace WinFormsSample {
    public partial class ExtenderSample : Form {
        public ExtenderSample() {
            InitializeComponent();

            new DraggableExtender(this);
        }
    }
}
