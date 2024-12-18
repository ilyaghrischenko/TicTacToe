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

    public  string CheckWinner()
    {
        foreach (var condition in WinningConditions)
        {
            string currentSymbol = Board[condition[0]];
            if (!string.IsNullOrEmpty(currentSymbol) &&
                currentSymbol == Board[condition[1]] &&
                currentSymbol == Board[condition[2]])
            {
                return currentSymbol; // Return the winning symbol (e.g., "X" or "O")
            }
        }
        // Check for a draw
        bool isBoardFull = Board.All(cell => !string.IsNullOrEmpty(cell));
        return isBoardFull ? "Draw" : "NoWinner";
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