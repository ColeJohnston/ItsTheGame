using System;
using System.Diagnostics;
using System.Net.Security;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters;

namespace players
{
    struct players
    {
        public string name;
        public double score;
        public double time;

        public void setName(string name)
        {
            this.name = name;
        }
        public void setTime(double time)
        {
            this.time = time;
        }
        public void setScore(double score)
        {
            this.score = score;
        }

        public string getName()
        {
            return this.name;
        }
        public double getScore()
        {
            return this.score;
        }

        public double getTime()
        {
            return this.time;
        }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            double[] scores = new double[0];
            bool PA = true;
            int id = 0;
            players[] players = new players[0];
            while (PA)
            {
                int[] player = new int[2], treasure = new int[2], ghost1 = new int[2], ghost2 = new int[2];
                string[,] position = new string[3, 3];
                int level = 1, lives = 3;
                bool alive = true;
                Intro();
                Console.Clear();
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                player = playerStartingLocation();
                treasure = treasureStartingLocation(player);
                position = initializeMap(player, treasure);
                while (alive)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("LEVEL: " + level);
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("\t      LIVES: " + lives);
                    Console.ForegroundColor = ConsoleColor.White;
                    switch (level)
                    {
                        case 1:
                            map134(position);
                            position[player[0], player[1]] = "X";
                            position[treasure[0], treasure[1]] = "*";
                            player = playerUpdate134(player, keyPress(), position);
                            Console.Clear();
                            if (player[0] == treasure[0] && player[1] == treasure[1])
                            {
                                position = mapReset(position, player, treasure, ghost1, ghost2);
                                level++;
                                Console.WriteLine("You made it to level " + level);
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("\n\nPRESS ENTER TO CONTINUE");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.ReadLine();
                                Console.Clear();
                                player = playerStartingLocation();
                                treasure = treasureStartingLocation(player);
                            }

                            break;
                        case 2:
                            position[player[0], player[1]] = "X";
                            position[treasure[0], treasure[1]] = "*";
                            map2(position);
                            player = playerUpdate2(player, keyPress(), position, treasure);
                            Console.Clear();
                            if (player[0] == treasure[0] && player[1] == treasure[1])
                            {
                                position = mapReset(position, player, treasure, ghost1, ghost2);
                                level++;
                                Console.WriteLine("You made it to level " + level);
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("\n\nPRESS ENTER TO CONTINUE");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.ReadLine();
                                Console.Clear();
                                player = playerStartingLocation();
                                treasure = treasureStartingLocation(player);
                                ghost1 = ghost1StartingLocation(player, treasure);
                            }

                            break;
                        case 3:
                            position[player[0], player[1]] = "X";
                            position[treasure[0], treasure[1]] = "*";
                            position[ghost1[0], ghost1[1]] = "!";
                            map134(position);
                            player = playerUpdate134(player, keyPress(), position);
                            ghost1 = ghostUpdate(ghost1, position);
                            Console.Clear();
                            if (player[0] == treasure[0] && player[1] == treasure[1])
                            {
                                position = mapReset(position, player, treasure, ghost1, ghost2);
                                level++;
                                Console.WriteLine("You made it to level " + level);
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("\n\nPRESS ENTER TO CONTINUE");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.ReadLine();
                                Console.Clear();
                                player = playerStartingLocation();
                                treasure = treasureStartingLocation(player);
                                ghost1 = ghost1StartingLocation(player, treasure);
                                ghost2 = ghost2StartingLocation(player, treasure, ghost1);
                            }
                            else if (player[0] == ghost1[0] && player[1] == ghost1[1])
                            {
                                lives--;
                                position = mapReset(position, player, treasure, ghost1, ghost2);
                                player = playerStartingLocation();
                                treasure = treasureStartingLocation(player);
                                ghost1 = ghost1StartingLocation(player, treasure);
                                Console.Clear();
                                if (lives > 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.WriteLine("You lost a life");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("REMAINING: " + lives);
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.WriteLine("\n\nPRESS ENTER TO CONTINUE");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.ReadLine();
                                    Console.Clear();
                                }
                                else
                                {
                                    PA = Lose();
                                    alive = false;
                                }
                            }
                            break;
                        case 4:
                            position[player[0], player[1]] = "X";
                            position[treasure[0], treasure[1]] = "*";
                            position[ghost1[0], ghost1[1]] = "!";
                            position[ghost2[0], ghost2[1]] = "!";
                            map134(position);
                            player = playerUpdate134(player, keyPress(), position);
                            ghost1 = ghostUpdate(ghost1, position);
                            ghost2 = ghostUpdate(ghost2, position);
                            Console.Clear();

                            if (player[0] == treasure[0] && player[1] == treasure[1])
                            {
                                position = mapReset(position, player, treasure, ghost1, ghost2);
                                level++;
                                Console.WriteLine("You made it to level " + level);
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("\n\nPRESS ENTER TO CONTINUE");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.ReadLine();
                                Console.Clear();
                                player = playerStartingLocation();
                                treasure = treasureStartingLocation(player);
                                ghost1 = ghost1StartingLocation(player, treasure);
                                ghost2 = ghost2StartingLocation(player, treasure, ghost1);
                            }

                            else if (player[0] == ghost1[0] && player[1] == ghost1[1] || player[0] == ghost2[0] && player[1] == ghost2[1])
                            {
                                lives--;
                                position = mapReset(position, player, treasure, ghost1, ghost2);
                                player = playerStartingLocation();
                                treasure = treasureStartingLocation(player);
                                ghost1 = ghost1StartingLocation(player, treasure);
                                Console.Clear();
                                if (lives > 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.WriteLine("You lost a life");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("REMAINING: " + lives);
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.WriteLine("\n\nPRESS ENTER TO CONTINUE");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.ReadLine();
                                    Console.Clear();
                                }
                                else
                                {
                                    PA = Lose();
                                    alive = false;
                                }
                            }
                            break;
                        case 5:
                            position[player[0], player[1]] = "X";
                            position[treasure[0], treasure[1]] = "*";
                            position[ghost1[0], ghost1[1]] = "!";
                            position[ghost2[0], ghost2[1]] = "!";

                            map5(position);
                            player = playerUpdate5(player, keyPress(), position, treasure);
                            ghost1 = ghostUpdate(ghost1, position);
                            ghost2 = ghostUpdate(ghost2, position);
                            Console.Clear();
                            if (player[0] == treasure[0] && player[1] == treasure[1])
                            {
                                Array.Resize(ref players, id + 1);
                                Array.Resize(ref scores, id + 1);
                                stopwatch.Stop();
                                players[id].score = Math.Round(WinMessage(stopwatch, lives), 2);
                                scores[id] = players[id].getScore();
                                players[id].time = Math.Round((double)(stopwatch.Elapsed.TotalMilliseconds / 1000), 2);
                                Console.WriteLine("What is your name for the leaderboard?");
                                players[id].name = Console.ReadLine();
                                Console.Clear();
                                Leaderboard(scores, players, LeaderboardOrder(scores));
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("\n\n\nPress ENTER to continue");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.ReadLine();
                                Console.Clear();
                                PA = WinTA();
                                alive = false;
                                id++;
                            }
                            else if (player[0] == ghost1[0] && player[1] == ghost1[1] || player[0] == ghost2[0] && player[1] == ghost2[1])
                            {
                                lives--;
                                position = mapReset(position, player, treasure, ghost1, ghost2);
                                player = playerStartingLocation();
                                treasure = treasureStartingLocation(player);
                                ghost1 = ghost1StartingLocation(player, treasure);
                                ghost2 = ghost2StartingLocation(player, treasure, ghost1);
                                Console.Clear();
                                if (lives > 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.WriteLine("You lost a life");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("Remaining: " + lives);
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.WriteLine("\n\nPRESS ENTER TO CONTINUE");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.ReadLine();
                                    Console.Clear();
                                }
                                else
                                {
                                    PA = Lose();
                                    alive = false;
                                }
                            }
                            break;
                    }
                }
                Console.Clear();
            }
            Leaderboard(scores, players, LeaderboardOrder(scores));
            Console.WriteLine("\n\n\n\n\nThank you for playing!!!");

        }
        static int[] LeaderboardOrder(double[] scores)
        {
            double[] sortedScores = new double[scores.Length];
            int[] order = new int[scores.Length];
            scores.CopyTo(sortedScores, 0);
            Array.Sort(sortedScores);
            for (int i = sortedScores.Length - 1; i >= 0; i--)
            {
                order[(sortedScores.Length - (i + 1))] = Array.IndexOf(scores, sortedScores[i]);
            }
            return order;
        }
        static void Leaderboard(double[] scores, players[] player, int[] order)
        {
            Console.WriteLine("\tLEADERBOARD");
            Console.WriteLine("--------------------------");
            Console.WriteLine("\nPlayer \t Time \t Score");
            Console.WriteLine("       (Seconds)");
            Console.WriteLine("----------------------\n");
            for (
                int i = 0; i < scores.Length; i++)
            {
                if (i == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                }
                Console.WriteLine(player[order[i]].getName() + "     " + player[order[i]].getTime() + "    " + player[order[i]].getScore());
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        static double WinMessage(Stopwatch sw, int lives)
        {
            double score = 7000;
            Console.WriteLine("YOU WIN!!!");
            Console.WriteLine("Congratulations");
            score += Math.Round((1000 * lives) - (double)(sw.Elapsed.TotalMilliseconds / 10), 2);
            Console.WriteLine("Your score was " + score);
            return score;
        }
        static bool WinTA()
        {
            bool PA;
            Console.WriteLine("Would you like to try again?");
            Console.WriteLine("Y/N");
            PA = (Console.ReadLine().ToUpper() == "Y");
            return PA;
        }
        static bool Lose()
        {
            Console.WriteLine("You Lose");
            Console.WriteLine("Would you like to play again?");
            Console.WriteLine("Y/N");
            if (Console.ReadLine().ToUpper() == "Y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static void Intro()
        {
            Console.WriteLine("Instructions ");
            Console.WriteLine("--------------------------------------------");
            Console.Write("-Navigate to the ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("Treasure ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("while avoiding barriers and ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("deadly ghosts ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nwhich can pass through walls, the treasure, and even each other.");
            Console.WriteLine("\n-You are given 3 lives total");
            Console.WriteLine("\n-Each time you lose a life, you restart the current level");
            Console.WriteLine("\n-If you lose all 3 lives, you will be forced to start over");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("\n-Score ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("is calculated by speed and lives remaining");
            Console.WriteLine("\nPress Enter to see map key");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("KEY");
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("X - You");
            Console.WriteLine("/ - Barrier");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("* - Treasure");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("! - Deadly Ghosts");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nPress enter when you think you're ready");
            Console.ReadLine();
        }
        static string[,] mapReset(string[,] position, int[] player, int[] treasure, int[] ghost1, int[] ghost2)
        {
            position[player[0], player[1]] = "_";
            position[treasure[0], treasure[1]] = "_";
            position[ghost1[0], ghost1[1]] = "_";
            position[ghost2[0], ghost2[1]] = "_";
            return position;
        }
        static int[] playerStartingLocation()
        {
            return randomizer();
        }
        static int[] treasureStartingLocation(int[] player)
        {
            int[] start = new int[2];
            do
            {
                start = randomizer();
            } while (start[0] == player[0] && start[1] == player[1]);
            return start;
        }
        static int[] ghost1StartingLocation(int[] player, int[] treasure)
        {
            int[] start = new int[2];
            do
            {
                start = randomizer();
            } while ((start[0] == player[0] && start[1] == player[1]) || (start[0] == treasure[0] && start[1] == treasure[1]));
            return start;
        }
        static int[] ghost2StartingLocation(int[] player, int[] treasure, int[] ghost1)
        {
            int[] start = new int[2];
            do
            {
                start = randomizer();
            } while ((start[0] == player[0] && start[1] == player[1]) || (start[0] == treasure[0] && start[1] == treasure[1]) || (start[0] == ghost1[0] && start[1] == ghost1[1]));
            return start;
        }
        static string[,] initializeMap(int[] player, int[] tStart)
        {
            string[,] position = new string[3, 3];
            for (int i = 0; i < position.GetLength(0); i++)
            {
                for (int j = 0; j < position.GetLength(1); j++)
                {
                    position[i, j] = "_";
                }
            }
            position[player[0], player[1]] = "X";
            position[tStart[0], tStart[1]] = "*";


            return position;
        }
        static void map134(string[,] position)
        {
            Console.WriteLine(" --------- --------- --------- ");
            Console.WriteLine("|         |         |         |");
            Console.WriteLine("|    " + position[0, 0] + "    |    " + position[0, 1] + "    |    " + position[0, 2] + "    |");
            Console.WriteLine("|         |         |         |");
            Console.WriteLine("|--------- --------- ---------");
            Console.WriteLine("|         |         |         |");
            Console.WriteLine("|    " + position[1, 0] + "    |    " + position[1, 1] + "    |    " + position[1, 2] + "    |");
            Console.WriteLine("|         |         |         |");
            Console.WriteLine(" --------- --------- ---------");
            Console.WriteLine("|         |         |         |");
            Console.WriteLine("|    " + position[2, 0] + "    |    " + position[2, 1] + "    |    " + position[2, 2] + "    |");
            Console.WriteLine("|         |         |         |");
            Console.WriteLine(" --------- --------- ---------");
        }
        static void map2(string[,] position)
        {
            Console.WriteLine(" --------- --------- --------- ");
            Console.WriteLine("|         /         |         |");
            Console.WriteLine("|    " + position[0, 0] + "    /    " + position[0, 1] + "    |    " + position[0, 2] + "    |");
            Console.WriteLine("|         /         |         |");
            Console.WriteLine("|--------- ///////// ---------");
            Console.WriteLine("|         |         |         |");
            Console.WriteLine("|    " + position[1, 0] + "    |    " + position[1, 1] + "    |    " + position[1, 2] + "    |");
            Console.WriteLine("|         |         |         |");
            Console.WriteLine(" --------- --------- /////////");
            Console.WriteLine("|         /         |         |");
            Console.WriteLine("|    " + position[2, 0] + "    /    " + position[2, 1] + "    |    " + position[2, 2] + "    |");
            Console.WriteLine("|         /         |         |");
            Console.WriteLine(" --------- --------- ---------");
        }
        static void map5(string[,] position)
        {
            Console.WriteLine(" --------- --------- --------- ");
            Console.WriteLine("|         /         /         |");
            Console.WriteLine("|    " + position[0, 0] + "    /    " + position[0, 1] + "    /    " + position[0, 2] + "    |");
            Console.WriteLine("|         /         /         |");
            Console.WriteLine("|--------- --------- ---------");
            Console.WriteLine("|         |         |         |");
            Console.WriteLine("|    " + position[1, 0] + "    |    " + position[1, 1] + "    |    " + position[1, 2] + "    |");
            Console.WriteLine("|         |         |         |");
            Console.WriteLine(" --------- --------- /////////");
            Console.WriteLine("|         |         |         |");
            Console.WriteLine("|    " + position[2, 0] + "    |    " + position[2, 1] + "    |    " + position[2, 2] + "    |");
            Console.WriteLine("|         |         |         |");
            Console.WriteLine(" --------- --------- ---------");
        }

        static string keyPress()
        {
            ConsoleKeyInfo keyInfo;
            keyInfo = Console.ReadKey(true);
            string movement = "";
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    movement = "UP";
                    PerformEnter();
                    break;
                case ConsoleKey.DownArrow:
                    movement = "DOWN";
                    PerformEnter();
                    break;
                case ConsoleKey.LeftArrow:
                    movement = "LEFT";
                    PerformEnter();
                    break;
                case ConsoleKey.RightArrow:
                    movement = "RIGHT";
                    PerformEnter();
                    break;
            }
            static void PerformEnter()
            {
                Console.Write("\r\n");
            }
            return movement;
        }
        static int[] ghostUpdate(int[] ghost, string[,] position)
        {
            position[ghost[0], ghost[1]] = "_";
            int[] newCoordinates = new int[2];
            int x;
            Random random = new Random();
            x = random.Next(4);
            if (x == 0 && ghost[0] != 0)
            {
                ghost[0]--;
            }
            else if (x == 1 && ghost[0] != 2)
            {
                ghost[0]++;
            }
            else if (x == 2 && ghost[1] != 2)
            {
                ghost[1]++;
            }
            else if (x == 3 && ghost[1] != 0)
            {
                ghost[1]--;
            }
            Console.WriteLine("OUT");
            position[ghost[0], ghost[1]] = "!";
            return ghost;
        }
        static int[] randomizer()
        {
            Random res = new Random();
            int[] i = new int[2];
            i[0] = res.Next(3);
            i[1] = res.Next(3);
            return i;
        }

        static int[] playerUpdate134(int[] player, string keyPress, string[,] position)
        {
            position[player[0], player[1]] = "_";
            int[] newCoordinates = new int[2];
            if (keyPress == "UP" && player[0] != 0)
            {
                player[0]--;
            }
            else if (keyPress == "DOWN" && player[0] != 2)
            {
                player[0]++;
            }
            else if (keyPress == "RIGHT" && player[1] != 2)
            {
                player[1]++;
            }
            else if (keyPress == "LEFT" && player[1] != 0)
            {
                player[1]--;
            }
            Console.WriteLine("OUT");
            position[player[0], player[1]] = "X";
            return player;
        }
        static int[] playerUpdate2(int[] player, string keyPress, string[,] position, int[] treasure)
        {
            position[player[0], player[1]] = "_";
            int[] newCoordinates = new int[2];
            if (keyPress == "UP" && player[0] != 0 && !(player[0] == 1 && player[1] == 1) && !(player[0] == 2 && player[1] == 2))
            {
                player[0]--;
            }
            else if (keyPress == "DOWN" && player[0] != 2 && !(player[0] == 0 && player[1] == 1) && !(player[0] == 1 && player[1] == 2))
            {
                player[0]++;
            }
            else if (keyPress == "RIGHT" && player[1] != 2 && !(player[0] == 0 && player[1] == 0) && !(player[0] == 2 && player[1] == 0))
            {
                player[1]++;
            }
            else if (keyPress == "LEFT" && player[1] != 0 && !(player[0] == 0 && player[1] == 1) && !(player[0] == 2 && player[1] == 1))
            {
                player[1]--;
            }
            Console.WriteLine("OUT");
            position[player[0], player[1]] = "X";
            position[treasure[0], treasure[1]] = "*";
            return player;
        }
        static int[] playerUpdate5(int[] player, string keyPress, string[,] position, int[] treasure)
        {
            position[player[0], player[1]] = "_";
            int[] newCoordinates = new int[2];
            if (keyPress == "UP" && player[0] != 0 && !(player[0] == 2 && player[1] == 2))
            {
                player[0]--;
            }
            else if (keyPress == "DOWN" && player[0] != 2 && !(player[0] == 1 && player[1] == 2))
            {
                player[0]++;
            }
            else if (keyPress == "RIGHT" && player[1] != 2 && !(player[0] == 0 && player[1] == 0) && !(player[0] == 0 && player[1] == 1))
            {
                player[1]++;
            }
            else if (keyPress == "LEFT" && player[1] != 0 && !(player[0] == 0 && player[1] == 1) && !(player[0] == 0 && player[1] == 2))
            {
                player[1]--;
            }
            Console.WriteLine("OUT");
            position[player[0], player[1]] = "X";
            position[treasure[0], treasure[1]] = "*";
            return player;
        }
    }
}