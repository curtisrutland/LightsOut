using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;

namespace LightsOut {
    public partial class MainPage {

        private DateTime gameStartedAt;
        private readonly DispatcherTimer timer;

        public MainPage() {
            InitializeComponent();
            rowsNumericUpDown.Value = Constants.DefaultRows;
            columnsNumericUpDown.Value = Constants.DefaultColumns;
            timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += TimerTick;
        }

        void TimerTick(object sender, EventArgs e) {
            var timeElapsed = DateTime.Now - gameStartedAt;
            timeElapsedTextBlock.Text = timeElapsed.ToString(@"mm\:ss");
        }

        private void ThisLoaded(object sender, RoutedEventArgs e) {
            var canUndoBinding = new Binding("CanUndo") { Source = game };
            var canRedoBinding = new Binding("CanRedo") { Source = game };
            undoButton.SetBinding(IsEnabledProperty, canUndoBinding);
            redoButton.SetBinding(IsEnabledProperty, canRedoBinding);
            NewGame();
        }

        private void NewGame() {
            timeElapsedTextBlock.Text = "00:00";
            gameStartedAt = DateTime.Now;
            timer.Start();
            game.NewGame((int)rowsNumericUpDown.Value, (int)columnsNumericUpDown.Value);
        }

        private void NewGameClick(object sender, RoutedEventArgs e) {
            NewGame();
        }

        private void UndoButtonClick(object sender, RoutedEventArgs e) {
            game.Undo();
        }

        private void RedoButtonClick(object sender, RoutedEventArgs e) {
            game.Redo();
        }

        private void GameWon(object sender, EventArgs e) {
            messageTextBlock.Text = "You Won!";
            timer.Stop();
        }
    }
}
