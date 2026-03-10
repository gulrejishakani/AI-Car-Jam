using UnityEngine;

public class GridManager : MonoBehaviour
{
   public int rows = 6;
    public int cols = 6;

    // 0 = empty, 1 = occupied
    public int[,] grid;

    void Awake()
    {
        grid = new int[cols, rows];
    }

    public bool IsCellFree(int x, int y)
    {
        return grid[x, y] == 0;
    }

    public void SetCell(int x, int y, int value)
    {
        grid[x, y] = value; // 1 = occupied, 0 = empty
    }
}

