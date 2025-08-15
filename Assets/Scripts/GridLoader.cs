using UnityEngine;

public class GridLoader : MonoBehaviour
{
    public GameObject hexagonPrefab;
    public int rows;
    public int columns;
    public float rowAngle = 0f;
    public float columnAngle = 120f;
    public float hexagonSize = 10f;
    public float hexagonSpacing = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        float movementMagnitude = (hexagonSize + hexagonSpacing) * Mathf.Sqrt(3);
        Vector3 rowDirection = new Vector3(Mathf.Cos(rowAngle * Mathf.Deg2Rad), 0, Mathf.Sin(rowAngle * Mathf.Deg2Rad)); // Direction for rows
        Vector3 columnDirection = new Vector3(Mathf.Cos(columnAngle * Mathf.Deg2Rad), 0, Mathf.Sin(columnAngle * Mathf.Deg2Rad)); // Direction for columns
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                // if (Random.Range(0f, 1.0f) < 0.2f)
                // {
                //     continue; // Skip some hexagons randomly
                // }
                GameObject hexagonObject = new GameObject("Hexagon_" + row + "_" + col);
                Hexagon hexagon = hexagonObject.AddComponent<Hexagon>();
                hexagon.hexagonPrefab = hexagonPrefab;
                float height = CalculateHeight(row, col);
                Vector3 position = (col * movementMagnitude * rowDirection) + (row * movementMagnitude * columnDirection) + (height * Vector3.up);
                hexagon.center = position;
                hexagon.Instantiate();
            }
        }
    }

    private float CalculateHeight(int row, int column)
    {
        float xCoord = row / (float)rows * 20;
        float yCoord = column / (float)columns * 20;
        return Mathf.PerlinNoise(xCoord, yCoord);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
