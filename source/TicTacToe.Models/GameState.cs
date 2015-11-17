namespace TicTacToe.Models
{
    public enum GameState
    {
        WaitingForSecondPlayer = 0,
        TurnX = 1,
        TurnO = 2,
        Draw = 3,
        FirstPlayerWon = 4,
        SecondPlayerWon = 5
    }
}
