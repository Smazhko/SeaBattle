void PrintBattleField (int[,] battleFeild)
{
    int rows = 10; //battleFeild.GetLength(0);
    int cols = 10; //battleFeild.GetLength(1);


    System.Console.WriteLine("     А   B   C   D   E   F   G   H   K   L");
    System.Console.WriteLine("   ┌───┬───┬───┬───┬───┬───┬───┬───┬───┬───┐");
    for (int i = 0; i < rows; i++)
    {
        if (i < rows - 1)
            Console.Write((i + 1) + "  │");
        else
            Console.Write((i + 1) + " │");
        
        for (int j = 0; j < cols; j++)
        {
            if (battleFeild[i, j] == 1) 
                Console.Write(" X ");
            else
                Console.Write(" · ");

            if (j < cols - 1)
                Console.Write(" ");
            else
                Console.Write("│\n");
        }
    }
    System.Console.WriteLine("   └───┴───┴───┴───┴───┴───┴───┴───┴───┴───┘\n");        
}

int [,] CloneArray (int[,] battleFeild)
{
    int rows = battleFeild.GetLength(0);
    int columns = battleFeild.GetLength(1);

    int [,] newArray = new int[rows + 1, columns + 1];

    for (int i = 0; i < rows; i++)
        for (int j = 0; j < columns; j++)
            newArray[i, j] = battleFeild[i, j];
    
    return newArray;
}

int [] CountShips (int[,] battleField)
{
    int[,] battleFieldCopy = CloneArray(battleField);

    int oneDeckShipsCount = 0;
    int twoDeckShipsCount = 0;
    int threeDeckShipsCount = 0;
    int fourDeckShipsCount = 0;
    int rows = battleFieldCopy.GetLength(0);
    int columns = battleFieldCopy.GetLength(1);
    
    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < columns; j++)
        {
            if (battleFieldCopy[i, j] == 1)
            {
                battleFieldCopy[i, j] = 0;
                if (battleFieldCopy[i, j + 1] == 1) 
                {
                    battleFieldCopy[i, j + 1] = 0;
                    if (battleFieldCopy[i, j + 2] == 1)
                    {
                        battleFieldCopy[i, j + 2] = 0;
                        if (battleFieldCopy[i, j + 3] == 1)
                        {
                            battleFieldCopy[i, j + 3] = 0;
                            fourDeckShipsCount++;
                            continue;
                        }
                        threeDeckShipsCount++;
                        continue; 
                    }
                    twoDeckShipsCount ++;
                    continue;
                }

                if (battleFieldCopy[i + 1, j] == 1)
                {
                    battleFieldCopy[i + 1, j] = 0;
                    if (battleFieldCopy[i + 2, j] == 1)
                    {
                        battleFieldCopy[i + 2, j] = 0;
                        if (battleFieldCopy[i + 3, j] == 1)
                        {
                            battleFieldCopy[i + 3, j] = 0;
                            fourDeckShipsCount++;
                            continue;
                        }
                        threeDeckShipsCount++;
                        continue;
                    }
                    twoDeckShipsCount++;
                    continue;
                }

                oneDeckShipsCount ++;
            }
        }
    }

    return new int[]
    {oneDeckShipsCount, twoDeckShipsCount, threeDeckShipsCount, fourDeckShipsCount};
}

//======================================================

int[,] battleField = new int[,]
{
    {0, 1, 0, 0, 0, 0, 1, 1, 0, 0},
    {0, 1, 0, 1, 0, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 1, 0, 1, 0, 1},
    {1, 0, 1, 0, 0, 0, 0, 1, 0, 1},
    {1, 0, 1, 0, 0, 0, 0, 1, 0, 1},
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
    {0, 1, 1, 1, 0, 1, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 1, 0, 1, 0, 1},
    {0, 1, 0, 0, 0, 0, 0, 0, 0, 0},
    {0, 0, 0, 1, 0, 1, 1, 0, 0, 1}
};

PrintBattleField(battleField);

int[] ShipsCount = CountShips(battleField);
System.Console.WriteLine($"На данном поле {ShipsCount[3]+ShipsCount[2]+ShipsCount[1]+ShipsCount[0]} кораблей:");
System.Console.WriteLine($"4-палубных - {ShipsCount[3]}, 3-палубных - {ShipsCount[2]}, 2-палубных - {ShipsCount[1]} и 1-палубных - {ShipsCount[0]}.");
