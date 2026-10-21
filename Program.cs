static void Main(string[] args)
        {
            PlayGame();
            Console.ReadLine();
        }

        // Main method that controls the entire game
        static void PlayGame()
        {
            // Create the game board. 
            char[] board = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            int player = 1; // 1 - first player (X), 2 - second player (O)
            bool gameIsRunning = true;

            while (gameIsRunning)
            {
                Console.Clear(); // Clear the screen before each turn
                DrawBoard(board);
                Console.WriteLine($"\nPlayer {player}'s turn ({(player == 1 ? 'X' : 'O')})");

                // Get the cell number from the user (with validation)
                int choice = GetPlayerChoice(board);

                // Place the player's symbol in the chosen cell
                char symbol = (player == 1) ? 'X' : 'O';
                board[choice - 1] = symbol;

                // Check if anyone won after this turn
                char winner = CheckWinner(board);

                if (winner != ' ') // If there is a winner
                {
                    Console.Clear();
                    DrawBoard(board);
                    Console.WriteLine($"\nPlayer {player} wins!");
                    gameIsRunning = false;
                }
                else if (CheckDraw(board)) // If it's a draw
                {
                    Console.Clear();
                    DrawBoard(board);
                    Console.WriteLine("\nIt's a draw!");
                    gameIsRunning = false;
                }
                else
                {
                    // If the game is not over, switch players
                    player = (player == 1) ? 2 : 1;
                }
            }
        }

        // Method to draw the game board
        static void DrawBoard(char[] board)
        {
            Console.WriteLine("-------------");
            Console.WriteLine($"| {board[0]} | {board[1]} | {board[2]} |");
            Console.WriteLine("|---|---|---|");
            Console.WriteLine($"| {board[3]} | {board[4]} | {board[5]} |");
            Console.WriteLine("|---|---|---|");
            Console.WriteLine($"| {board[6]} | {board[7]} | {board[8]} |");
            Console.WriteLine("-------------");
        }

        // Method to get the player's move with validation
        static int GetPlayerChoice(char[] board)
        {
            while (true)
            {
                Console.Write("Choose a cell (1-9): ");
                string input = Console.ReadLine();
                int choice;

                // Check if a number was entered
                if (!int.TryParse(input, out choice))
                {
                    Console.WriteLine("Error! Please enter a number.");
                    continue;
                }

                // Check if the number is between 1 and 9
                if (choice < 1 || choice > 9)
                {
                    Console.WriteLine("Error! Enter a number from 1 to 9.");
                    continue;
                }

                // Check if the cell is free (if there is no X or O)
                if (board[choice - 1] == 'X' || board[choice - 1] == 'O')
                {
                    Console.WriteLine("Error! This cell is already taken.");
                    continue;
                }

                return choice; // If all checks pass, return the choice
            }
        }

        // Method to check for a winner
        static char CheckWinner(char[] board)
        {
            // Check all possible lines (3 horizontals, 3 verticals, 2 diagonals)

            // Horizontals
            if (board[0] == board[1] && board[1] == board[2]) return board[0];
            if (board[3] == board[4] && board[4] == board[5]) return board[3];
            if (board[6] == board[7] && board[7] == board[8]) return board[6];

            // Verticals
            if (board[0] == board[3] && board[3] == board[6]) return board[0];
            if (board[1] == board[4] && board[4] == board[7]) return board[1];
            if (board[2] == board[5] && board[5] == board[8]) return board[2];

            // Diagonals
            if (board[0] == board[4] && board[4] == board[8]) return board[0];
            if (board[2] == board[4] && board[4] == board[6]) return board[2];

            // If there is no winner, return a space
            return ' ';
        }

        // Method to check for a draw
        static bool CheckDraw(char[] board)
        {
            // A draw occurs when all cells are taken, 
            for (int i = 0; i < board.Length; i++)
            {
                // If at least one cell still has a digit (i.e., is free) — it's not a draw
                if (board[i] != 'X' && board[i] != 'O')
                {
                    return false;
                }
            }
            return true; // If the loop reached the end and found no free cells — it's a draw
        }