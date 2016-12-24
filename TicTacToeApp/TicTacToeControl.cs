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

        public delegate void CellClicked(Tictactoe.Position position);

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
                Select(delegate(Tuple<Tictactoe.Position, Tictactoe.Cell> tuple)
                {
                    var position = tuple.Item1;
                    var cell = tuple.Item2;

                    return CreateCell(cellSize, position, cell, tuple);
                })
                .ToList()
                .ForEach(cell => Controls.Add(cell));
        }

        private static string GetCellText(Tictactoe.Cell cell)
        {
            return Tictactoe.cellToString(cell);
        }

        private IEnumerable<Tuple<Tictactoe.Position, Tictactoe.Cell>> GetCells()
        {
            return GetCellIndexes()
                    .Select(
                        delegate(Tictactoe.Position position)
                        {
                            var option = _game.Cells[position.Y][position.X];

                            return new Tuple<Tictactoe.Position, Tictactoe.Cell>(position, option);
                        })
                ;
        }

        public IEnumerable<Tictactoe.Position> GetCellIndexes()
        {
            for (var y = 0; y <= 2; y++)
            {
                for (var x = 0; x <= 2; x++)
                {
                    yield return new Tictactoe.Position(x, y);
                }
            }
        }

        private Control CreateCell(Size cellSize, Tictactoe.Position position, Tictactoe.Cell cell, object tag)
        {
            var button = new Button
            {
                FlatStyle = FlatStyle.Popup,
                Size = cellSize,
                Location = new Point(position.X*cellSize.Width, position.Y*cellSize.Height),
                Tag = tag
            };

            button.Font = new Font(button.Font.FontFamily, (cellSize.Height * 0.6f));
            button.Click += Button_Click;

            ApplyText(button, cell, position);

            return button;
        }

        private void ApplyText(Button button, Tictactoe.Cell cell, Tictactoe.Position position)
        {
            if (cell.IsEmpty)
            {
                button.ForeColor = Color.White;
                button.Text = Tictactoe.positionToNumber(position).ToString();
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
            var tuple = (Tuple<Tictactoe.Position, Tictactoe.Cell>) button.Tag;

            var position = tuple.Item1;

            if (tuple.Item2 == Tictactoe.Cell.Empty)
            {
                OnCellClick(position);
            }
        }

        protected virtual void OnCellClick(Tictactoe.Position position)
        {
            CellClick?.Invoke(position);
        }

        private void TicTacToeControl_Resize(object sender, EventArgs e)
        {
            RefreshForm();
        }
    }
}