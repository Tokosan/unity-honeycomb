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
    public float closeAnimationDuration = 1.0f;
    void Start()
    {
        List<Vector3> positions = CalculateHoneyCombCenters(meshLength);
        for (int i = 0; i < positions.Count; i++)
        {
            // if (Random.Range(0f, 1.0f) < 0.1f)
            // {
            //     continue; // Skip some honeycombs randomly
            // }
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

    private float counter = 0f;
    int honeyCombIndex = 0;

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;

        if (counter > 0.2f)
        {
            honeyCombs[honeyCombIndex].Close(closeAnimationDuration);
            honeyCombIndex = (honeyCombIndex + 1) % honeyCombs.Count;
            honeyCombs[honeyCombIndex].Open(closeAnimationDuration);
            counter = 0f;
        }
    }
}
