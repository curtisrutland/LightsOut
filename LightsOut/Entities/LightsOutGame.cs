using System;
using System.Collections.Generic;
using System.Linq;

namespace LightsOut.Entities {
    public class LightsOutGame {
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        public List<LightsOutCell> Cells { get; private set; }

        private int TotalCells { get { return Rows * Columns; } }
        private readonly LightsOutCell[,] cellMatrix;
        private readonly Random random = new Random();

        public LightsOutGame() : this(Constants.DefaultRows, Constants.DefaultColumns) { }

        public LightsOutGame(int rows, int columns) {
            Rows = rows;
            Columns = columns;
            Cells = new List<LightsOutCell>(TotalCells);
            cellMatrix = new LightsOutCell[Rows, Columns];
            InitializeCells();
            CreateNewGame();
        }

        public void ToggleCells(LightsOutCell clickedCell) {
            int row = clickedCell.Row, column = clickedCell.Column;
            //self
            clickedCell.Toggle();
            //up
            if (row - 1 >= 0)
                cellMatrix[row - 1, column].Toggle();
            //down
            if (row + 1 < Rows)
                cellMatrix[row + 1, column].Toggle();
            //left
            if(column - 1 >= 0)
                cellMatrix[row, column - 1].Toggle();
            //right
            if (column + 1 < Columns)
                cellMatrix[row, column + 1].Toggle();
        }

        private void InitializeCells() {
            for (var r = 0; r < Rows; r++)
                for (var c = 0; c < Columns; c++) {
                    var cell = new LightsOutCell(r, c);
                    Cells.Add(cell);
                    cellMatrix[r, c] = cell;
                }
        }

        private void CreateNewGame() {
            var indexes = new List<int>();
            do {
                var index = random.Next(0, TotalCells);
                if (!indexes.Contains(index))
                    indexes.Add(index);
            } while (indexes.Count < Constants.TilesToRandomize);
            foreach (var index in indexes)
                ToggleCells(Cells[index]);
        }
    }
}
