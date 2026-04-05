using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelData[] levels;

    public int currentLevel = 0;

    public GameObject[] carPrefabs;

    public float cellSize = 1.1f;
    public Vector3 startPos;

    public GridManager gridManager;

    void Start()
    {
        LoadLevel(currentLevel);
    }

    void LoadLevel(int levelIndex)
    {
        LevelData level = levels[levelIndex];

        foreach (CarData car in level.cars)
        {
            SpawnCar(car);
        }
    }

    void SpawnCar(CarData data)
    {
        Vector3 pos = startPos + new Vector3(data.x * cellSize, 0, data.y * cellSize);

        Quaternion rot = data.isHorizontal 
            ? Quaternion.identity 
            : Quaternion.Euler(0, 90, 0);

        Instantiate(carPrefabs[data.prefabIndex], pos, rot);

        // grid block karo
        for (int i = 0; i < data.length; i++)
        {
            int x = data.isHorizontal ? data.x + i : data.x;
            int y = data.isHorizontal ? data.y : data.y + i;

            gridManager.SetCell(x, y, 1);
        }
    }
}