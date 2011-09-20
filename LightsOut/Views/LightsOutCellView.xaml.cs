using System;
using System.Windows;
using LightsOut.Entities;

namespace LightsOut.Views {
    public partial class LightsOutCellView {

        public event EventHandler<EventArgs> Click;
        protected void OnClick() {
            if (Click != null) Click(this, new EventArgs());
        }

        public LightsOutCellView() {
            InitializeComponent();
        }

        private void ButtonClick(object sender, RoutedEventArgs e) {
            var lightsOutCell = DataContext as LightsOutCell;
            if (lightsOutCell != null) //sanity check
                OnClick();
        }
    }
}
