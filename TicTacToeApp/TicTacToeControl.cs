using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TicTacToeApp
{
    public partial class TicTacToeControl : UserControl
    {
        public event CellClicked CellClick;

        private Tictactoe.Game _game;

        public delegate void CellClicked(Tictactoe.Move move);

        public TicTacToeControl()
        {
            InitializeComponent();
        }

        public Tictactoe.Game Game
        {
            get { return _game; }
            set
            {
                _game = value;
                RefreshForm();
            }
        }

        private void RefreshForm()
        {
            Controls.Clear();

            if (_game == null)
            {
                return;
            }

            var cellSize = new Size(Size.Width/3, Size.Height/3);

            foreach (var tuple in GetCells())
            {
                var x = tuple.Item1;
                var y = tuple.Item2;
                var cell = tuple.Item3;
                var cellText = GetCellText(cell);

                Controls.Add(CreateCell(cellSize, x, y, cellText, tuple));
            }
        }

        private static string GetCellText(Tictactoe.Cell cell)
        {
            return Tictactoe.cellToString(cell);
        }

        private IEnumerable<Tuple<int, int, Tictactoe.Cell>> GetCells()
        {
            return GetCellIndexes()
                    .Select(
                        delegate(Tuple<int, int> xy)
                        {
                            var x = xy.Item1;
                            var y = xy.Item2;
                            var option = _game.Cells[y][x];

                            return new Tuple<int, int, Tictactoe.Cell>(x, y, option);
                        })
                ;
        }

        public IEnumerable<Tuple<int, int>> GetCellIndexes()
        {
            for (var y = 0; y <= 2; y++)
            {
                for (var x = 0; x <= 2; x++)
                {
                    yield return new Tuple<int, int>(x, y);
                }
            }
        }

        private Control CreateCell(Size cellSize, int x, int y, string text, object tag)
        {
            var button = new Button
            {
                FlatStyle = FlatStyle.Popup,
                Text = text,
                Size = cellSize,
                Location = new Point(x*cellSize.Width, y*cellSize.Height),
                Tag = tag,
            };

            button.Font = new Font(button.Font.FontFamily, button.Font.Size*4);

            button.Click += Button_Click;

            return button;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (_game.State != Tictactoe.GameState.InProgress)
            {
                return;
            }

            var button = (Button) sender;
            var tuple = (Tuple<int, int, Tictactoe.Cell>) button.Tag;

            var move = new Tictactoe.Move(tuple.Item1, tuple.Item2);

            if (tuple.Item3 == Tictactoe.Cell.Empty)
            {
                OnCellClick(move);
            }
        }

        protected virtual void OnCellClick(Tictactoe.Move move)
        {
            CellClick?.Invoke(move);
        }
    }
}