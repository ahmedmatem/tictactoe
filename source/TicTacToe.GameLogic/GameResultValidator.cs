namespace TicTacToe.GameLogic
{
    using System;

    public class GameResultValidator : IGameResultValidator
    {
        public GameResult GetResult(string board)
        {
            char[,] boardAsArray = new char[3, 3];

            int row, col;
            for (int i = 0; i < board.Length; i++)
            {
                row = i / 3;
                col = i % 3;
                boardAsArray[row, col] = board[i];
            }

            // check diagonals
            if((boardAsArray[0, 0] == 'X' && boardAsArray[1, 1] == 'X' && boardAsArray[2, 2] == 'X') ||
                (boardAsArray[0, 2] == 'X' && boardAsArray[1, 1] == 'X' && boardAsArray[2, 0] == 'X'))
            {
                return GameResult.WonByX;
            }

            if ((boardAsArray[0, 0] == 'O' && boardAsArray[1, 1] == 'O' && boardAsArray[2, 2] == 'O') ||
                (boardAsArray[0, 2] == 'O' && boardAsArray[1, 1] == 'O' && boardAsArray[2, 0] == 'O'))
            {
                return GameResult.WonByO;
            }

            // check horizontals
            if ((boardAsArray[0, 0] == 'X' && boardAsArray[0, 1] == 'X' && boardAsArray[0, 2] == 'X') ||
                (boardAsArray[1, 0] == 'X' && boardAsArray[1, 1] == 'X' && boardAsArray[1, 2] == 'X') ||
                (boardAsArray[2, 0] == 'X' && boardAsArray[2, 1] == 'X' && boardAsArray[2, 2] == 'X'))
            {
                return GameResult.WonByX;
            }

            if ((boardAsArray[0, 0] == 'O' && boardAsArray[0, 1] == 'O' && boardAsArray[0, 2] == 'O') ||
                (boardAsArray[1, 0] == 'O' && boardAsArray[1, 1] == 'O' && boardAsArray[1, 2] == 'O') ||
                (boardAsArray[2, 0] == 'O' && boardAsArray[2, 1] == 'O' && boardAsArray[2, 2] == 'O'))
            {
                return GameResult.WonByO;
            }

            // check verticals
            if ((boardAsArray[0, 0] == 'X' && boardAsArray[1, 0] == 'X' && boardAsArray[2, 0] == 'X') ||
                (boardAsArray[0, 1] == 'X' && boardAsArray[1, 1] == 'X' && boardAsArray[2, 1] == 'X') ||
                (boardAsArray[0, 2] == 'X' && boardAsArray[1, 2] == 'X' && boardAsArray[2, 2] == 'X'))
            {
                return GameResult.WonByX;
            }

            if ((boardAsArray[0, 0] == 'O' && boardAsArray[1, 0] == 'O' && boardAsArray[2, 0] == 'O') ||
                (boardAsArray[0, 1] == 'O' && boardAsArray[1, 1] == 'O' && boardAsArray[2, 1] == 'O') ||
                (boardAsArray[0, 2] == 'O' && boardAsArray[1, 2] == 'O' && boardAsArray[2, 2] == 'O'))
            {
                return GameResult.WonByO;
            }

            // game not finished
            for (int index = 0; index < board.Length; index++)
            {
                if(board[index] == '-')
                {
                    return GameResult.NotFinished;
                }
            }

            // Game finished and nobody won
            return GameResult.Draw; ;
        }
    }
}
