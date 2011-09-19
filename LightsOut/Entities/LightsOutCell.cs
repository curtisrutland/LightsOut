using System.ComponentModel;

namespace LightsOut.Entities {
    public class LightsOutCell : INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName) {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool toggled;
        public bool Toggled {
            get { return toggled; }
            private set {
                if (toggled == value) return;
                toggled = value;
                OnPropertyChanged("Toggled");
            }
        }

        public int Row { get; private set; }
        public int Column { get; private set; }

        public LightsOutCell(int row, int column) {
            Row = row;
            Column = column;
            Toggled = false;
        }

        public void Toggle() {
            Toggled = !Toggled;
        }
    }
}