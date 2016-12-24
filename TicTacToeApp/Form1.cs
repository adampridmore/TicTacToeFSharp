using System;
using System.Windows.Forms;

namespace TicTacToeApp
{
    public partial class Form1 : Form
    {
        Tictactoe.Game _currentGameState;

        public Form1()
        {
            InitializeComponent();

            NewGame();

            RefreshUi();
        }

        private void NewGame()
        {
            _currentGameState = Tictactoe.newGame;
            RefreshUi();
        }

        private void RefreshUi()
        {
            ticTacToeControl1.Game = _currentGameState;

            tbGameState.Text = Tictactoe.gameStateToString(_currentGameState.State);
            tbToPlay.Text = Tictactoe.tokenToString(_currentGameState.NextMove);
        }

        private void ticTacToeControl1_CellClick(Tictactoe.Move move)
        {
            _currentGameState = Tictactoe.playMove(_currentGameState, move);

            if (_currentGameState.State == Tictactoe.GameState.InProgress)
            {
                if (cbPlayAi.Checked)
                {
                    _currentGameState = PlayComputerAiMove(_currentGameState);
                }
            }

            RefreshUi();
        }

        private static Tictactoe.Game PlayComputerAiMove(Tictactoe.Game currentGameState)
        {
            var computerMove = TictactoeComputerPlayer.getMove(currentGameState);
            return Tictactoe.playMove(currentGameState, computerMove);
        }

        private void bNewGame_Click(object sender, EventArgs e)
        {
            NewGame();
        }
    }
}