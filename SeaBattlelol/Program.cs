char[,] board = new char[10, 10];
char[,] aiBoard = new char[10, 10];
char[,] aioBoardP = new char[10, 10];

string input;
int row;
int col;
int direction;

Ship ship = new Ship();

int fourIndex = 1;
int threeIndex = 2;
int twoIndex = 3;
int oneIndex = 4;

bool player = true;


while (CountOfShips(oneIndex, twoIndex, threeIndex, fourIndex))
{
    Console.WriteLine("Начнем расстановку. Напишите какой корабль хотите поставить");
    string type = Console.ReadLine();
    switch (type)
    {

        case "Однопалубный":
            if (oneIndex > 0)
            {
                Console.WriteLine("Напишите координату коробля:");
                input = Console.ReadLine().ToUpper();
                row = input[1] - '0';
                col = input[0] - 'A';
                Console.Clear();

                if (CanPlaceShip(row, col, 1, 1, board))
                {
                    ship.Create(board, row, col, 1, 1);
                    oneIndex--;
                }
                else { Console.WriteLine("Невозможно повставить корабль"); }
                Print(board);
            }
            else { Console.WriteLine("Однопалубные бэттэ"); }
            break;

        case "Двухпалубный":
            if (twoIndex > 0)
            {
                Console.WriteLine("Напишите первую координату и направление ( вправо(1),вниз(2))");
                input = Console.ReadLine().ToUpper();
                row = input[1] - 'U';
                col = input[0] - 'F';
                direction = Convert.ToInt32(Console.ReadLine());
                Console.Clear();

                if (CanPlaceShip(row, col, 2, direction, board))
                {
                    ship.Create(board, row, col, direction, 2);
                    twoIndex--;
                }
                else { Console.WriteLine("Невозможно повставить корабль"); }

                Print(board);
            }
            else { Console.WriteLine("Двухпалубные  бэттэ"); }
            break;

        case "Трёхпалубный":
            if (threeIndex > 0)
            {
                Console.WriteLine("Напишите первую координату и направление ( вправо(1),вниз(2))");
                input = Console.ReadLine().ToUpper();
                row = input[1] - '0';
                col = input[0] - 'A';
                direction = Convert.ToInt32(Console.ReadLine());
                Console.Clear();

                if (CanPlaceShip(row, col, 3, direction, board))
                {
                    ship.Create(board, row, col, direction, 3);
                    threeIndex--;
                }
                else { Console.WriteLine("Невозможно повставить корабль"); }

                Print(board);
            }
            else { Console.WriteLine("Трёхпалубные  бэттэ"); }
            break;

        case "Четырёхпалубный":
            if (fourIndex > 0)
            {
                Console.WriteLine("Напишите первую координату и направление ( вправо(1),вниз(2))");
                input = Console.ReadLine().ToUpper();
                row = input[1] - '0';
                col = input[0] - 'A';
                direction = Convert.ToInt32(Console.ReadLine());
                Console.Clear();

                if (CanPlaceShip(row, col, 4, direction, board))
                {
                    ship.Create(board, row, col, direction, 4);
                    fourIndex--;
                }
                else { Console.WriteLine("Невозможно повставить корабль"); }

                Print(board);

            }
            else { Console.WriteLine("Четырёхпалубные  бэттэ"); }
            break;
    }
}

fourIndex = 1;
threeIndex = 2;
twoIndex = 3;
oneIndex = 4;
Random rnd = new Random();


while (CountOfShips(oneIndex, twoIndex, threeIndex, fourIndex))
{
    while (oneIndex > 0)
    {
        row = rnd.Next(10);
        col = rnd.Next(10);
        if (CheckAdjacentShips(row, col, aiBoard) == false)
        {
            ship.Create(aiBoard, row, col, 1, 1);
            oneIndex--;
        }
    }
    while (twoIndex > 0)
    {
        row = rnd.Next(9);
        col = rnd.Next(9);
        direction = rnd.Next(1, 2);
        if (CheckAdjacentShips(row, col, aiBoard) == false)
        {
            ship.Create(aiBoard, row, col, direction, 2);
            twoIndex--;
        }
    }
    while (threeIndex > 0)
    {
        row = rnd.Next(8);
        col = rnd.Next(8);
        direction = rnd.Next(1, 2);
        if (CheckAdjacentShips(row, col, aiBoard) == false)
        {
            ship.Create(aiBoard, row, col, 1, 3);
            threeIndex--;
        }
    }
    while (fourIndex > 0)
    {
        row = rnd.Next(7);
        col = rnd.Next(7);
        direction = rnd.Next(1, 2);
        if (CheckAdjacentShips(row, col, aiBoard) == false)
        {
            ship.Create(aiBoard, row, col, direction, 4);
            fourIndex--;
        }
    }
}

bool t = false;

while (true)
{
    while (player)
    {
        Console.WriteLine("Напишите, куда хотите выстрелить");
        input = Console.ReadLine().ToUpper();
        row = input[1] - '0';
        col = input[0] - 'A';
        if (Shoot(row, col, aiBoard) == true)
        {
            aioBoardP[row, col] = 'X';
            Print(aioBoardP);
            if (isWin(board) == true)
            {
                Console.WriteLine("Победил пользователь");
                Thread.Sleep(3000);
                Environment.Exit(0);
            }
            continue;
        }
        else { player = false; }
    }

    while (player == false)
    {
        row = rnd.Next(10);
        col = rnd.Next(10);
        if (Shoot(row, col, board) == true)
        {
            board[row, col] = 'X';
            Print(board);
            if (isWin(board) == true)
            {
                Console.WriteLine("Победил терминатор");
                break;
            }
            t = true;
            while (t)
            {
                row = rnd.Next(row - 1, row + 1);
                col = rnd.Next(col - 1, col + 1);
                if ((row >= 0 && row <= 9 && col >= 0 && col <= 9))
                {
                    if (Shoot(row, col, board) == true)
                    {
                        board[row, col] = 'X';
                        Print(board);
                        if (isWin(board) == true)
                        {
                            Console.WriteLine("Победил Терминатор");
                            Thread.Sleep(3000);
                            Environment.Exit(0);
                        }
                        continue;
                    }
                    else { t = false; player = true; }
                }
                else { continue; }
            }
            continue;
        }
        else { player = true; }
    }
}



//POLE
void Print(char[,] c)
{
    Console.WriteLine("   A B C D E F G H I J");
    Console.WriteLine("   -------------------");
    for (int i = 0; i < 10; i++)
    {
        Console.Write((i) + "| ");
        for (int j = 0; j < 10; j++)
        {

            Console.Write(c[i, j] + "  ");
        }
        Console.WriteLine();
    }
}

static bool CountOfShips(int x, int y, int z, int v)
{
    if (x == 0 && y == 0 && z == 0 && v == 0)
    {
        return false;
    }
    return true;
}

static bool CheckAdjacentShips(int x, int y, char[,] grid)
{
    for (int i = Math.Max(0, x - 1); i <= Math.Min(10 - 1, x + 1); i++)
    {
        for (int j = Math.Max(0, y - 1); j <= Math.Min(10 - 1, y + 1); j++)
        {
            if (grid[i, j] == '*' && (i != x || j != y))
            {
                return true;
            }
        }
    }
    return false;
}

bool Shoot(int row, int col, char[,] b)
{
    if (b[row, col] == ' ' || b[row, col] == '~')
    {
        b[row, col] = 'O'; 
        return false;
    }
    else if (b[row, col] == '*')
    {
        b[row, col] = 'X'; 
        return true;
    }
    return false;
}

bool isWin(char[,] b)
{
    for (int i = 0; i < 10; i++)
    {
        for (int j = 0; j < 10; j++)
        {
            if (b[i, j] == '*')
            {
                return false;
            }
        }
    }
    return true;
}

static bool CanPlaceShip(int x, int y, int size, int direction, char[,] gameBoard)
{
    if (direction == 2)
    {
        
        if (x + size > 10)
            return false;

        
        for (int i = x; i < x + size; i++)
        {
            if (gameBoard[i, y] != '\0')
                return false;
        }
    }
    else if (direction == 1)
    {
        
        if (y + size > 10)
            return false;

       
        for (int j = y; j < y + size; j++)
        {
            if (gameBoard[x, j] != '\0')
                return false;
        }
    }

    return true;
}
public class Ship
{
    public void Create(char[,] gameBoard, int x, int y, int d, int size)
    {
        if (d == 2)
        {
            for (int i = x; i < x + size; i++)
            {
                gameBoard[i, y] = '*'; 
            }
        }
        else if (d == 1)
        {
            for (int j = y; j < y + size; j++)
            {
                gameBoard[x, j] = '*';
            }
        }
    }

}
