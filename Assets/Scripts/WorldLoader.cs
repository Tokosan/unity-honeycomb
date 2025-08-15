using UnityEngine;

using System.Collections.Generic;
public class WorldLoader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject hexagonPrefab;
    public float a = 10.0f;
    public float padding = 1.0f;
    private List<HoneyComb> honeyCombs = new List<HoneyComb>();
    public int meshLength = 6;
    void Start()
    {
        List<Vector3> positions = CalculateHoneyCombCenters(meshLength);
        for (int i = 0; i < positions.Count; i++)
        {
            if (Random.Range(0f, 1.0f) < 0.1f)
            {
                continue; // Skip some honeycombs randomly
            }
            GameObject honeyCombObject = new GameObject("HoneyComb" + i);
            HoneyComb honeyComb = honeyCombObject.AddComponent<HoneyComb>();
            honeyComb.hexagonPrefab = hexagonPrefab;
            honeyComb.InstantiateChildren(a, padding, positions[i]);
            honeyComb.isRotating = true;
            honeyCombs.Add(honeyComb);
        }
    }

    private List<Vector3> CalculateHoneyCombCenters(int length)
    {
        int columns = length;
        List<Vector3> centers = new List<Vector3>();
        float _a = a + padding;
        float _m = _a * Mathf.Sqrt(3) / 2;
        Vector3 initialCenter = new Vector3(0, 0, 0);
        Vector3 rowStart = initialCenter;
        for (int row = 0; row < length; row++)
        {
            // if (row > 0 && row % 3 == 0)
            // {
            //     rowStart.x = initialCenter.x;
            //     rowStart.z -= _m;
            // }
            for (int col = 0; col < columns; col++)
            {
                Vector3 position = rowStart + new Vector3(col * (4.5f * _a), 0, col * _m);
                centers.Add(position);
            }
            columns -= 1;
            rowStart += new Vector3(1.5f * _a, 0, 5 * _m);
        }
        return centers;
    }

    // private float counter = 0f;
    // private float rotationSpeed = 0f;
    // private float rotationAccel = 1;

    // Update is called once per frame
    void Update()
    {
        // counter += Time.deltaTime;
        // rotationSpeed += rotationAccel;
        // if (Mathf.Abs(rotationSpeed) > 1000f)
        // {
        //     rotationAccel = -rotationAccel;
        // }
        // if (counter > 0.5f)
        // {
        //     float random = Random.Range(0f, 1.0f);
        //     int randomHoneyCombIndex = Random.Range(0, honeyCombs.Count);
        //     for (int i = 0; i < honeyCombs.Count; i++)
        //     {
        //         if (i == randomHoneyCombIndex)
        //         {
        //             // honeyCombs[i].isRotating = false;
        //             honeyCombs[i].isMoving = false;
        //         }
        //         else
        //         {
        //             honeyCombs[i].isMoving = true;
        //             honeyCombs[i].direction = new Vector3(
        //                 Random.Range(-1f, 1f),
        //                 0,
        //                 Random.Range(-1f, 1f)
        //             ).normalized;
        //             honeyCombs[i].rotateSpeed = rotationSpeed;
        //         }
        //     }
        //     counter = 0f;
        // }
    }
}
