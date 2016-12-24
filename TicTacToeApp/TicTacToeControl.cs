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

            GetCells().
                Select(delegate(Tuple<Tictactoe.Move, Tictactoe.Cell> tuple)
                {
                    var move = tuple.Item1;
                    var cell = tuple.Item2;

                    return CreateCell(cellSize, move, cell, tuple);
                })
                .ToList()
                .ForEach(cell => Controls.Add(cell));
        }

        private static string GetCellText(Tictactoe.Cell cell)
        {
            return Tictactoe.cellToString(cell);
        }

        private IEnumerable<Tuple<Tictactoe.Move, Tictactoe.Cell>> GetCells()
        {
            return GetCellIndexes()
                    .Select(
                        delegate(Tictactoe.Move move)
                        {
                            var option = _game.Cells[move.Y][move.X];

                            return new Tuple<Tictactoe.Move, Tictactoe.Cell>(move, option);
                        })
                ;
        }

        public IEnumerable<Tictactoe.Move> GetCellIndexes()
        {
            for (var y = 0; y <= 2; y++)
            {
                for (var x = 0; x <= 2; x++)
                {
                    yield return new Tictactoe.Move(x, y);
                }
            }
        }

        private Control CreateCell(Size cellSize, Tictactoe.Move move, Tictactoe.Cell cell, object tag)
        {
            var button = new Button
            {
                FlatStyle = FlatStyle.Popup,
                Size = cellSize,
                Location = new Point(move.X*cellSize.Width, move.Y*cellSize.Height),
                Tag = tag
            };

            button.Font = new Font(button.Font.FontFamily, button.Font.Size*4);
            button.Click += Button_Click;

            ApplyText(button, cell, move);

            return button;
        }

        private void ApplyText(Button button, Tictactoe.Cell cell, Tictactoe.Move move)
        {
            if (cell.IsEmpty)
            {
                button.ForeColor = Color.White;
                button.Text = Tictactoe.moveToNumber(move).ToString();
            }
            else
            {
                button.Text = GetCellText(cell);
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (_game.State != Tictactoe.GameState.InProgress)
            {
                return;
            }

            var button = (Button) sender;
            var tuple = (Tuple<Tictactoe.Move, Tictactoe.Cell>) button.Tag;

            var move = tuple.Item1;

            if (tuple.Item2 == Tictactoe.Cell.Empty)
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