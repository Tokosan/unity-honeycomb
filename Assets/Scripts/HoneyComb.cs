using UnityEngine;

using System.Collections.Generic;

public class HoneyComb : MonoBehaviour
{
    public float a = 10.0f; // arista
    public float padding = 1.0f;
    public Vector3 center = Vector3.zero;
    public GameObject hexagonPrefab;
    public bool isRotating = false;
    public bool isMoving = false;
    public float moveSpeed = 40.0f;
    public float rotateSpeed = 20.0f;
    public Vector3 direction = Vector3.right;
    // Update is called once per frame
    public void InstantiateChildren(float a, float padding, Vector3 center)
    {
        // Calculate the number of children based on the size of the honeycomb
        List<Vector3> childPositions = CalculateChildPositions(a, padding, center);
        this.transform.position = center;
        foreach (Vector3 pos in childPositions)
        {
            Instantiate(hexagonPrefab, pos, Quaternion.Euler(90, 0, 0), this.transform);
        }
    }

    public void Move(Vector3 direction)
    {
        transform.position += direction;
    }

    public void Rotate(float angle)
    {
        transform.Rotate(0, angle, 0);
    }

    private List<Vector3> CalculateChildPositions(float a, float padding, Vector3 center)
    {
        float centerX = center.x;
        float centerY = center.y;
        float centerZ = center.z;

        List<Vector3> positions = new List<Vector3>();

        float _a = a + padding;

        // Center child
        positions.Add(new Vector3(centerX, centerY, centerZ));
        // Top Left
        positions.Add(new Vector3(
            centerX - _a * Mathf.Sqrt(3) / 2,
            centerY,
            centerZ + _a * 1.5f
        ));
        // Bottom Left
        positions.Add(new Vector3(
            centerX - _a * Mathf.Sqrt(3) / 2,
            centerY,
            centerZ - _a * 1.5f
        ));
        // Center Right
        positions.Add(new Vector3(
            centerX + _a * Mathf.Sqrt(3),
            centerY,
            centerZ
        ));

        return positions;
    }

    void Update()
    {
        if (isMoving)
        {
            this.Move(direction * moveSpeed * Time.deltaTime);
        }
        if (isRotating)
        {
            this.Rotate(rotateSpeed * Time.deltaTime * 2);
        }
    }
}
