using System;
using System.Collections.Generic;
using System.Linq;

namespace LightsOut.Entities {
    public class LightsOutGame {
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        public List<LightsOutCell> Cells { get; private set; }

        private int TotalCells { get { return Rows * Columns; } }
        private readonly Random random = new Random();

        public LightsOutGame() : this(Constants.DefaultRows, Constants.DefaultColumns) { }

        public LightsOutGame(int rows, int columns) {
            Rows = rows;
            Columns = columns;
            Cells = new List<LightsOutCell>(TotalCells);
            InitializeCells();
            CreateNewGame();
        }

        private void InitializeCells() {
            for (var r = 0; r < Rows; r++)
                for (var c = 0; c < Columns; c++) {
                    Cells.Add(new LightsOutCell(r, c));
                }
        }

        private void CreateNewGame() {
            var indexes = new List<int>();
            do {
                var index = random.Next(0, TotalCells);
                if (!indexes.Contains(index))
                    indexes.Add(index);
            } while (indexes.Count < Constants.MaxStepsRequiredToWin);
            foreach(var index in indexes)
                ToggleCells(Cells[index]);
        }

        public void ToggleCells(LightsOutCell clickedCell) {
            var row = clickedCell.Row;
            var column = clickedCell.Column;
            for(var r = row - 1; r <= row + 1; r ++) {
                if(r < 0 || r >= Rows)
                    continue;
                for(var c = column - 1; c<= column + 1; c ++) {
                    if(c < 0 || c >= Columns)
                        continue;
                    var cell = Cells.Where(x => x.Row == r && x.Column == c).SingleOrDefault();
                    if(cell != null)
                        cell.Toggle();
                }
            }
        }
    }
}
