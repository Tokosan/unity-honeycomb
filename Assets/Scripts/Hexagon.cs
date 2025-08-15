using UnityEngine;

public class Hexagon : MonoBehaviour
{
    public Vector3 center = Vector3.zero;
    public Quaternion rotation = Quaternion.Euler(90, 0, 0);
    public GameObject hexagonPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject Instantiate()
    {
        transform.position = center;
        GameObject hexagon = Instantiate(hexagonPrefab, center, rotation, transform);
        // hexagon.transform.localPosition = Vector3.zero; // Reset local position to center
        return hexagon;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
