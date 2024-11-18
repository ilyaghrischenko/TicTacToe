namespace TicTacToe.Application.SignalRHub;

public class GameSession
{
    public string GameId { get; set; }
    public string PlayerX { get; set; }
    public string PlayerO { get; set; }
    public string CurrentTurn { get; set; } = "X";
    public string[] Board { get; set; } = new string[9];

    private static readonly int[][] WinningConditions =
    [
        [0, 1, 2],
        [3, 4, 5],
        [6, 7, 8],
        [0, 3, 6],
        [1, 4, 7],
        [2, 5, 8],
        [0, 4, 8],
        [2, 4, 6]
    ];

    public string CheckWinner(string[] board)
    {
        foreach (var condition in WinningConditions)
        {
            if (!string.IsNullOrEmpty(board[condition[0]]) &&
                board[condition[0]] == board[condition[1]] &&
                board[condition[0]] == board[condition[2]])
            {
                return board[condition[0]]; // Возвращаем победивший символ
            }
        }

        return board.All(cell => !string.IsNullOrEmpty(cell)) ? "Draw" : null;
    }

    public void WriteMove(int cell)
    {
        Board[cell] = CurrentTurn switch
        {
            "X" => "X",
            "O" => "O",
            _ => Board[cell]
        };

        CurrentTurn = CurrentTurn == "X" ? "O" : "X";
    }

    public void Reset()
    {
        Board = new string[9];
    }
}