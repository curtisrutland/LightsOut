using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using LightsOut.Entities;

namespace LightsOut.Views {
    public partial class LightsOutGameView : INotifyPropertyChanged {

        public event EventHandler<EventArgs> GameWon;
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnGameWon() {
            if (GameWon != null) GameWon(this, new EventArgs());
        }

        protected void OnPropertyChanged(string propertyName) {
            if(PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private LightsOutGame game;
        private Stack<LightsOutCell> undoStack, redoStack;

        public LightsOutGameView() : this(Constants.DefaultRows, Constants.DefaultColumns) { }

        public bool CanUndo { get { return undoStack.Count > 0; } }
        public bool CanRedo { get { return redoStack.Count > 0; } }

        public LightsOutGameView(int rows, int columns) {
            InitializeComponent();
            Initialize(rows, columns);
        }

        private void Initialize(int rows, int columns) {
            undoStack = new Stack<LightsOutCell>();
            redoStack = new Stack<LightsOutCell>();
            SetRowsAndColumns(rows, columns);
            game = new LightsOutGame(rows, columns);
            game.NewGame();
            foreach (var cell in game.Cells) {
                var cellView = new LightsOutCellView {DataContext = cell};
                cellView.Click += CellViewClick;
                LayoutRoot.Children.Add(cellView);
            }
        }

        public void NewGame(int rows, int columns) {
            Initialize(rows, columns);
        }

        public void Undo() {
            if (!CanUndo) return;
            var cell = PopUndo();
            game.ToggleCells(cell);
            PushRedoCell(cell);
        }

        public void Redo() {
            if (!CanRedo) return;
            var cell = PopRedo();
            game.ToggleCells(cell);
            PushUndoCell(cell);
        }

        private void SetRowsAndColumns(int rows, int columns) {
            LayoutRoot.RowDefinitions.Clear();
            LayoutRoot.ColumnDefinitions.Clear();
            for (var r = 0; r < rows; r++)
                LayoutRoot.RowDefinitions.Add(new RowDefinition());
            for (var c = 0; c < columns; c++)
                LayoutRoot.ColumnDefinitions.Add(new ColumnDefinition());
        }

        private void CellViewClick(object sender, EventArgs e) {
            var lightsOutCellView = sender as LightsOutCellView;
            if (lightsOutCellView == null) return;
            var cell = lightsOutCellView.DataContext as LightsOutCell;
            if (cell == null) return;
            game.ToggleCells(cell);
            ClearRedoStack();
            PushUndoCell(cell);
            EvaluateGame();
        }

        private void EvaluateGame() {
            if (game.Won) {
                game.Finish();
                ClearUndoStack();
                ClearRedoStack();
                OnGameWon();
            }
        }

        #region Stack Operations
        //extracted as methods so I can send the property changed event

        private void ClearRedoStack() {
            redoStack.Clear();
            OnPropertyChanged("CanRedo");
        }

        private void ClearUndoStack() {
            undoStack.Clear();
            OnPropertyChanged("CanUndo");
        }

        private void PushRedoCell(LightsOutCell cell) {
            redoStack.Push(cell);
            OnPropertyChanged("CanRedo");
        }

        private LightsOutCell PopUndo() {
            var cell = undoStack.Pop();
            OnPropertyChanged("CanUndo");
            return cell;
        }

        private void PushUndoCell(LightsOutCell cell) {
            undoStack.Push(cell);
            OnPropertyChanged("CanUndo");
        }

        private LightsOutCell PopRedo() {
            var cell = redoStack.Pop();
            OnPropertyChanged("CanRedo");
            return cell;
        }
        #endregion
    }
}
