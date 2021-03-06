﻿using System;
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

        private void ticTacToeControl1_CellClick(int x, int y)
        {
            _currentGameState = Tictactoe.playMove(_currentGameState, x, y);

            if (_currentGameState.State == Tictactoe.GameState.InProgress)
            {
                if (cbPlayAi.Checked)
                {
                    var computerMove = TictactoeComputerPlayer.getMove(_currentGameState);
                    _currentGameState = Tictactoe.playMove(_currentGameState, computerMove.Item1, computerMove.Item2);
                }
            }

            RefreshUi();
        }

        private void bNewGame_Click(object sender, EventArgs e)
        {
            NewGame();
        }
    }
}