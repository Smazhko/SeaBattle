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

int [,] EnlargeArray (int[,] battleFeild)
{
    int [,] newArray = new int[battleFeild.GetLength(0) + 2, battleFeild.GetLength(1) + 2];

    for (int i = 0; i < battleFeild.GetLength(0); i++)
        for (int j = 0; j < battleFeild.GetLength(1); j++)
            newArray[i + 1, j + 1] = battleFeild[i, j];
    
    return newArray;
}

/*int [] CountShipsByTypeOLD (int[,] battleField)
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
*/

int [] CountShipsByType (int[,] battleField)
{
    int[,] newField = EnlargeArray(battleField);
    int [] results = new int [Math.Max(battleField.GetLength(0), battleField.GetLength(1)) + 1];
    
    for (int i = 1; i < newField.GetLength(0); i++)
    {
        for (int j = 1; j < newField.GetLength(1); j++)
        {
            int shipLength = 1;
            
            if (newField[i, j] == 1 && 
                newField[i - 1, j] == 0 && 
                newField[i, j - 1] == 0)
            {
                if (newField[i, j + 1] == 0) // если ДА, то считаем длину ВЕРТИКАЛЬНО
                {
                    while (true)
                    {
                        if (newField[i + shipLength, j] == 0)
                        {
                            results[shipLength] ++; 
                            break;
                        }
                        else
                            shipLength++;
                    }
                }
                else   // ИНАЧЕ считаем ГОРИЗОНТАЛЬНО
                {
                    while (true)
                    {
                        if (newField[i, j + shipLength] == 0)
                        {
                            results[shipLength] ++; 
                            break;
                        }
                        else
                            shipLength++;
                    }
                }
            }

        }
    }
    return results;
}

int CountShipsTotal (int[,] battleField) 
// считаем правые нижние концы всех кораблей
{
    int summ = 0;

    for (int i = 0; i < battleField.GetLength(0); i++)
    {
        for (int j = 0; j < battleField.GetLength(1); j++)
        {
            if (battleField[i, j] == 1 && 
                (i == 0 || battleField[i - 1, j] == 0) && 
                (j == 0 || battleField[i, j - 1] == 0)) summ++;
        }
    }
    return summ;
}

void PrintResult (int[] arr)
{
    for (int i = 1; i < arr.Length; i++)
        if (arr[i] != 0) 
            Console.WriteLine($"{i}-палубных: {arr[i]} шт.");
}

//======================================================

int[,] battleField = new int[,]
{
    {0, 1, 0, 0, 0, 0, 1, 1, 0, 1},
    {0, 1, 0, 1, 0, 0, 0, 0, 0, 1},
    {0, 0, 0, 0, 0, 1, 0, 1, 0, 1},
    {1, 0, 1, 0, 0, 0, 0, 1, 0, 1},
    {1, 0, 1, 0, 1, 1, 0, 1, 0, 1},
    {0, 0, 0, 0, 0, 0, 0, 1, 0, 1},
    {1, 1, 1, 1, 0, 1, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 1, 0, 1, 0, 1},
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
};

PrintBattleField(battleField);

//int[] ShipsCount = CountShipsByTypeOLD(battleField);
//System.Console.WriteLine($"На данном поле {CountShipsTotal(battleField)} кораблей:");
//System.Console.WriteLine($"4-палубных - {ShipsCount[3]}, 3-палубных - {ShipsCount[2]}, 2-палубных - {ShipsCount[1]} и 1-палубных - {ShipsCount[0]}.");

System.Console.WriteLine($"На данном поле {CountShipsTotal(battleField)} кораблей:");
PrintResult(CountShipsByType(battleField));

