// // using UnityEngine;

// // public class GridLoader : MonoBehaviour
// // {
// //     public GameObject hexagonPrefab;
// //     public int rows;
// //     public int columns;
// //     public float rowAngle = 0f;
// //     public float columnAngle = 120f;
// //     public float hexagonSize = 10f;
// //     public float hexagonSpacing = 1f;
// //     private MainHexagon[,] hexagons;
// //     // Start is called once before the first execution of Update after the MonoBehaviour is created
// //     void Start()
// //     {
// //         hexagons = new MainHexagon[rows, columns];
// //         MainHexagon mainHexagon = new MainHexagon(Vector3.zero, hexagonSize, hexagonSpacing, hexagonPrefab);
// //         mainHexagon.Instantiate();
// //         // GenerateGrid();
// //     }

// //     private void GenerateGrid()
// //     {
//         float movementMagnitude = (hexagonSize + hexagonSpacing) * Mathf.Sqrt(3);
//         float[] angles = {
//             0, 60, 120, 180, 240, 300
//         };
//         Vector3[] directions = new Vector3[angles.Length];
//         for (int i = 0; i < angles.Length; i++)
//         {
//             float angle = angles[i];
//             directions[i] = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad));
//         }
//         // This alows us to move in every direction of the hexagon, where the indexes are:
// //         // /**
// //         //     2   1
// //         // 3           0
// //         //     4   5
// //         // */
// //         // Vector3 startCenter = 2 * directions[4] * movementMagnitude;
// //         // Hexagon startHexagon = InstantiateHexagon(startCenter, "Hexagon_Start", false, true);

// //         // Vector3 rowMovement = directions[0] * 2 * movementMagnitude + directions[5] * movementMagnitude;
// //         // Vector3 columnMovement = directions[2] * 2 * movementMagnitude + directions[1] * movementMagnitude;


// //         // for (int row = 0; row < rows; row++)
// //         // {
// //         //     for (int col = 0; col < columns; col++)
// //         //     {
// //         //         // if (Random.Range(0f, 1.0f) < 0.2f)
// //         //         // {
// //         //         //     continue; // Skip some hexagons randomly
// //         //         // }
// //         //         // We create the main Hexagon object
// //         //         float height = CalculateHeight(row, col) *  20f;
// //         //         Vector3 center = (col * rowMovement) + (row * columnMovement) + new Vector3(0, height, 0);
// //         //         Hexagon hexagon = InstantiateHexagon(center, $"Hexagon_{row}_{col}", true);
// //         //         // Then we create the extras.
// //         //         for (int i = 0; i < 3; i++)
// //         //         {
// //         //             if (row > 0 && col > 0 && Random.Range(0f, 1.0f) < 0.2f)
// //         //             {
// //         //                 continue; // Skip some hexagons randomly
// //         //             }
// //         //             Vector3 position = center + directions[2 * i] * movementMagnitude;
// //         //             Hexagon extraHexagon = InstantiateHexagon(position, $"Hexagon_{row}_{col}_Extra_{i}");
// //         //         }
// //         //     }
// //         // }
// //     }



// //     private float CalculateHeight(int row, int column)
// //     {
// //         float xCoord = row / (float)rows * 5;
// //         float yCoord = column / (float)columns * 5;
// //         return Mathf.PerlinNoise(xCoord, yCoord);
// //     }

// //     // Update is called once per frame
// //     void Update()
// //     {
        
// //     }
// // }
