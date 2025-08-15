using UnityEngine;

using System.Collections.Generic;
public class WorldLoader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject hexagonPrefab;
    public float hexagonSize = 10.0f;
    public float hexagonSpacing = 1.0f;
    private HoneyComb[,] honeyCombs;
    public int rows = 6;
    public int columns = 6;
    public float animationDuration = 1.0f;
    public float animationTimeInterval = 1.0f;

    void Start()
    {
        honeyCombs = new HoneyComb[columns, rows];
        GenerateGrid();
        //List<Vector3> positions = CalculateHoneyCombCenters(meshLength);
        //for (int i = 0; i < positions.Count; i++)
        //{
        //    GameObject honeyCombObject = new GameObject("HoneyComb" + i);
        //    HoneyComb honeyComb = honeyCombObject.AddComponent<HoneyComb>();
        //    honeyComb.hexagonPrefab = hexagonPrefab;
        //    honeyComb.InstantiateChildren(a, padding, positions[i]);
        //    honeyCombs.Add(honeyComb);
        //}
    }

    private void GenerateGrid()
    {
        float movementMagnitude = (hexagonSize + hexagonSpacing) * Mathf.Sqrt(3);
        float[] angles = {
            0, 60, 120, 180, 240, 300
        };
        Vector3[] directions = new Vector3[angles.Length];
        for (int i = 0; i < angles.Length; i++)
        {
            float angle = angles[i];
            directions[i] = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad));
        }

        Vector3 rowMovement = (2 * directions[0] + directions[5]) * movementMagnitude;
        Vector3 columnMovement = (2 * directions[2] + directions[1]) * movementMagnitude;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                GameObject honeyCombObject = new GameObject($"HoneyComb_{col}_{row}");
                HoneyComb honeyComb = honeyCombObject.AddComponent<HoneyComb>();
                Vector3 position = col * rowMovement + row * columnMovement;
                honeyComb.Construct(hexagonPrefab, hexagonSize, hexagonSpacing, position);
                honeyComb.Instantiate();
                honeyCombs[col, row] = honeyComb;
                //GameObject hexagon= Instantiate(hexagonPrefab, position, Quaternion.Euler(90, 0, 0));
                //honeyCombs[row, col] = hexagonObject.GetComponent<HoneyComb>();
            }
        }
    }

    //private List<Vector3> CalculateHoneyCombCenters(int length)
    //{
    //    int columns = length;
    //    List<Vector3> centers = new List<Vector3>();
    //    float _a = a + padding;
    //    float _m = _a * Mathf.Sqrt(3) / 2;
    //    Vector3 initialCenter = new Vector3(0, 0, 0);
    //    Vector3 rowStart = initialCenter;
    //    for (int row = 0; row < length; row++)
    //    {
    //        for (int col = 0; col < columns; col++)
    //        {
    //            if (Random.Range(0f, 1.0f) < 0.1f)
    //            {
    //                continue; // Skip some honeycombs randomly
    //            }
    //            Vector3 position = rowStart + new Vector3(col * (4.5f * _a), 0, col * _m);
    //            centers.Add(position);
    //        }
    //        columns -= 1;
    //        rowStart += new Vector3(1.5f * _a, 0, 5 * _m);
    //    }
    //    return centers;
    //}

    private float counter = 0f;
    int honeyCombIndex = 0;

    //// Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;

        if (counter > animationTimeInterval)
        {
            int honeyCombIndexX = Random.Range(0, columns);
            int honeyCombIndexY = Random.Range(0, rows);
            honeyCombs[honeyCombIndexX, honeyCombIndexY].Rotation(animationDuration);
            counter = 0f;
        }
    }
}
