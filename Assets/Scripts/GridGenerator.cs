/*using UnityEngine;

public class GridGenerator : MonoBehaviour
{
 public int rows = 6;
    public int cols = 6;
    public float cellSize = 1.1f;
    public GameObject cellPrefab;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int x = 0; x < cols; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                Vector3 pos = new Vector3(x * cellSize, 0, y * cellSize);
                GameObject cell = Instantiate(cellPrefab, pos, Quaternion.identity, transform);
                cell.name = $"Cell_{x}_{y}";
            }
        }
    }
}
*/


using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public int rows = 6;
    public int cols = 6;
    public float cellSize = 1.1f;
    public GameObject cellPrefab;

    // 🔥 Add this (tumhari given position)
    public Vector3 startPos = new Vector3(1.83f, 0f, 8.758f);

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int x = 0; x < cols; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                // 🔥 Yahan offset add kiya
                Vector3 pos = startPos + new Vector3(x * cellSize, 0, y * cellSize);

                GameObject cell = Instantiate(cellPrefab, pos, Quaternion.identity, transform);
                cell.name = $"Cell_{x}_{y}";
            }
        }
    }
}


