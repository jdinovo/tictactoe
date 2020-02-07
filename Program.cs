using System;

namespace TicTacToe {
    class MainClass {
        public static void Main(string[] args) {

            Game game = new Game();

            game.Run();


        }
    }

    class Game {
        static readonly Player P1 = new Player(1, "X");
        static readonly Player P2 = new Player(2, "O");
        readonly Board board = new Board();
        Player curPlayer = P1;
        Boolean winner;
        byte moves;

        // Run game
        public void Run() {

            board.Display();

            while (!winner) {
                GetUserInput();
            }
        }

        void GetUserInput() {
            Console.Write($" Player {curPlayer.Number} ({curPlayer.Piece}) - Enter Move:");

            // Receive user input and attempt to parse it 
            Boolean parceable = int.TryParse(Console.ReadLine(), out int input);
            // Validate input
            if (parceable && input > 0 && input < 10) {
                if ((P1.Board & board.ShortGameBoard[input - 1]) != board.ShortGameBoard[input - 1] && (P2.Board & board.ShortGameBoard[input - 1]) != board.ShortGameBoard[input - 1]) {
                    // Update that player's board
                    curPlayer.Board = (short)(curPlayer.Board | board.ShortGameBoard[input - 1]);
                    board.Update(P1, P2);
                    board.Display();
                    winner = CheckWinner(curPlayer);
                    // Switch current player
                    curPlayer = curPlayer.Equals(P1) ? P2 : P1;
                    moves++;
                } else {
                    Console.WriteLine(" Position already taken.");
                }
            } else {
                Console.WriteLine(" Error: Enter a value from 1-9.");
            }
        }

        Boolean CheckWinner(Player p) {
            short[] winningMasks = { 448, 292, 273, 146, 84, 73, 56, 7 };

            // Compare players board to winning masks
            foreach (short mask in winningMasks) {
                if ((p.Board & mask) == mask) {
                    Console.WriteLine($"PLAYER {p.Number} ({p.Piece}) - Wins!");
                    return true;
                }
            }
            if(moves == 8) {
                Console.WriteLine("Its a draw.");
                return true;
            }
            return false;
        }

        
    }
    class Player {
        public int Number { get; }
        public string Piece { get; }
        public short Board { get; set; } = 0;

        public Player() { }

        public Player(int number, string piece) {
            this.Piece = piece;
            this.Number = number;
        }
    }
    class Board {
        readonly string[] GameBoard = { " ", " ", " ", " ", " ", " ", " ", " ", " " };
        public short[] ShortGameBoard { get; } = { 256, 128, 64, 32, 16, 8, 4, 2, 1 };

        // Update the display board
        public void Update(Player p1, Player p2) {
            for (int i = 0; i < GameBoard.Length; i++) {
                if (ShortGameBoard[i] == (ShortGameBoard[i] & p1.Board)) {
                    GameBoard[i] = p1.Piece;
                } else if (ShortGameBoard[i] == (ShortGameBoard[i] & p2.Board)) {
                    GameBoard[i] = p2.Piece;
                }
            }
        }

        // Display the board to the console
        public void Display() {
            Console.Clear();
            Console.WriteLine($@" {GameBoard[0]} | {GameBoard[1]} | {GameBoard[2]}
---+---+--- 
 {GameBoard[3]} | {GameBoard[4]} | {GameBoard[5]}
---+---+---
 {GameBoard[6]} | {GameBoard[7]} | {GameBoard[8]} ");
        }
    }
}
