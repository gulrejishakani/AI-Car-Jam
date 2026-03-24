using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GridManager gridManager;

    public GameObject[] carPrefabs; // 0=small,1=medium,2=big

    public float cellSize = 1.1f; // same as GridGenerator

    void Start()
    {
        SpawnCars();
    }

    void SpawnCars()
    {
        for (int i = 0; i < 8; i++) // jitni cars chahiye
        {
            TrySpawnCar();
        }
    }

    void TrySpawnCar()
    {
        int x = Random.Range(0, gridManager.cols);
        int y = Random.Range(0, gridManager.rows);

        int type = Random.Range(0, carPrefabs.Length);

        int length = GetCarLength(type);

        // boundary check
        if (x + length > gridManager.cols) return;

        // check free cells
        for (int i = 0; i < length; i++)
        {
            if (!gridManager.IsCellFree(x + i, y))
                return;
        }

        // spawn position
        Vector3 pos = new Vector3(x * cellSize, 0, y * cellSize);

        GameObject car = Instantiate(carPrefabs[type], pos, Quaternion.identity);

        // mark occupied
        for (int i = 0; i < length; i++)
        {
            gridManager.SetCell(x + i, y, 1);
        }
    }

    int GetCarLength(int type)
    {
        if (type == 0) return 2; // small
        if (type == 1) return 3; // medium
        return 4; // big
    }
}