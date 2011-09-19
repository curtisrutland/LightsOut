using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LightsOut.Entities;

namespace LightsOut.Views {
    public partial class LightsOutCellView {

        public event EventHandler<EventArgs> Toggled;
        protected void OnToggled() {
            if (Toggled != null) Toggled(this, new EventArgs());
        }

        public LightsOutCellView() {
            InitializeComponent();
        }

        private void RectangleMouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            var lightsOutCell = DataContext as LightsOutCell;
            if (lightsOutCell != null)
                OnToggled();
        }
    }
}
