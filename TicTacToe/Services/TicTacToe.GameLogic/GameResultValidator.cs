namespace TicTacToe.GameLogic
{
    public class GameResultValidator : IGameResultValidator
    {
        // O-X
        // O-X
        // --X
        public GameResult GetResult(string board)
        {
            board = board.ToLowerInvariant();

            GameResult result = GameResult.NotFinished;

            for (int i = 0; i < board.Length; i += 3)
            {
                // check row for a winner
                if (board[i] == board[i + 1] && board[i + 1] == board[i + 2])
                {
                    result = DecideWinner(board[i]);
                }
            }

            for (int i = 0; i < 3; i++)
            {
                // check column for a winner
                if (board[i] == board[i+3] && board[i+3] == board[i+6])
                {
                    result = DecideWinner(board[i]);
                }
            }

            if (board[0] == board[4] && board[4] == board[8])
            {
                // diagonal check
                result = DecideWinner(board[0]);
            }

            if (board[2] == board[4] && board[4] == board[6])
            {
                // diagonal check
                result= DecideWinner(board[2]);
            }

            return result;
        }
 
        private GameResult DecideWinner(char symbol)
        {
            if (symbol == 'o')
            {
                return GameResult.WonByO;
            }
            else if (symbol == 'x')
            {
                return GameResult.WonByX;
            }

            return GameResult.NotFinished;
        }
    }
}