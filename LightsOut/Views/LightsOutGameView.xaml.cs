using System;
using System.Windows;
using System.Windows.Controls;
using LightsOut.Entities;

namespace LightsOut.Views {
    public partial class LightsOutGameView {

        private readonly LightsOutGame game;

        public LightsOutGameView() : this(Constants.DefaultRows, Constants.DefaultColumns) { }

        public LightsOutGameView(int rows, int columns) {
            InitializeComponent();
            for (var r = 0; r < rows; r++)
                LayoutRoot.RowDefinitions.Add(new RowDefinition());
            for (var c = 0; c < columns; c++)
                LayoutRoot.ColumnDefinitions.Add(new ColumnDefinition());
            game = new LightsOutGame(rows, columns);
            foreach (var cell in game.Cells) {
                var cellView = new LightsOutCellView { DataContext = cell };
                cellView.Toggled += CellViewToggled;
                LayoutRoot.Children.Add(cellView);
            }

        }

        private void CellViewToggled(object sender, EventArgs e) {
            var lightsOutCellView = sender as LightsOutCellView;
            if (lightsOutCellView == null) return;
            var cell = lightsOutCellView.DataContext as LightsOutCell;
            if (cell == null) return;
            game.ToggleCells(cell);
        }

        private static void EvaluateGame() {
            throw new NotImplementedException();
        }
    }
}
