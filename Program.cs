using System;

namespace TicTacToe
{
    namespace Game
    {

        public enum Symbol
        {
            Empty,
            Cross,
            Circle
        }


        public class Board
        {
            private Symbol[,] cells;

            public Board()
            {
                cells = new Symbol[3, 3];
                Reset();
            }


            public void Reset()
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        cells[i, j] = Symbol.Empty;
                    }
                }
            }


            public void SetCell(int row, int col, Symbol symbol)
            {
                cells[row, col] = symbol;
            }


            public Symbol GetCell(int row, int col)
            {
                return cells[row, col];
            }

            public bool IsWin(Symbol symbol)
            {

                for (int i = 0; i < 3; i++)
                {
                    if (cells[i, 0] == symbol && cells[i, 1] == symbol && cells[i, 2] == symbol)
                    {
                        return true;
                    }
                }

                for (int j = 0; j < 3; j++)
                {
                    if (cells[0, j] == symbol && cells[1, j] == symbol && cells[2, j] == symbol)
                    {
                        return true;
                    }
                }


                if ((cells[0, 0] == symbol && cells[1, 1] == symbol && cells[2, 2] == symbol) ||
                    (cells[0, 2] == symbol && cells[1, 1] == symbol && cells[2, 0] == symbol))
                {
                    return true;
                }

                return false;
            }

            public bool IsDraw()
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (cells[i, j] == Symbol.Empty)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }

            public void Print()
            {
                Console.WriteLine("  0 1 2");
                for (int i = 0; i < 3; i++)
                {
                    Console.Write($"{i} ");
                    for (int j = 0; j < 3; j++)
                    {
                        switch (cells[i, j])
                        {
                            case Symbol.Empty:
                                Console.Write("  ");
                                break;
                            case Symbol.Cross:
                                Console.Write("X ");
                                break;
                            case Symbol.Circle:
                                Console.Write("O ");
                                break;
                        }
                    }
                    Console.WriteLine();
                }
            }
        }


        public class Player
        {
            public Symbol Symbol { get; private set; }
            public string Name { get; private set; }

            public Player(Symbol symbol, string name)
            {
                Symbol = symbol;
                Name = name;
            }
        }


        public class TicTacToeGame
        {
            private Board board;
            private Player player1;
            private Player player2;

            public TicTacToeGame()
            {
                board = new Board();
                player1 = new Player(Symbol.Cross, "Игрок 1");
                player2 = new Player(Symbol.Circle, "Игрок 2");
            }
            public void Start()
            {
                Random random = new Random();
                bool player1Turn = random.Next(2) == 0;

                Console.WriteLine("Игра 'Крестики-нолики'!");

                while (true)
                {
                    board.Print();

                    if (player1Turn)
                    {
                        Console.WriteLine($"{player1.Name} (X) ходит:");
                        MakePlayerMove(player1);
                    }
                    else
                    {
                        Console.WriteLine($"{player2.Name} (O) ходит:");
                        MakePlayerMove(player2);
                    }

                    if (board.IsWin(player1.Symbol))
                    {
                        Console.WriteLine($"{player1.Name} победил!");
                        break;
                    }
                    else if (board.IsWin(player2.Symbol))
                    {
                        Console.WriteLine($"{player2.Name} победил!");
                        break;
                    }
                    else if (board.IsDraw())
                    {
                        Console.WriteLine("Ничья!");
                        break;
                    }

                    player1Turn = !player1Turn;
                }
            }


            private void MakePlayerMove(Player player)
            {
                int row, col;
                while (true)
                {
                    Console.Write($"{player.Name}, введите номер строки (0-2): ");
                    if (int.TryParse(Console.ReadLine(), out row) && row >= 0 && row <= 2)
                    {
                        Console.Write("Введите номер столбца (0-2): ");
                        if (int.TryParse(Console.ReadLine(), out col) && col >= 0 && col <= 2)
                        {
                            if (board.GetCell(row, col) == Symbol.Empty)
                            {
                                board.SetCell(row, col, player.Symbol);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Клетка уже занята. Попробуйте другую.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Неверный номер столбца.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Неверный номер строки.");
                    }
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Game.TicTacToeGame game = new Game.TicTacToeGame();
            game.Start();
        }
    }
}