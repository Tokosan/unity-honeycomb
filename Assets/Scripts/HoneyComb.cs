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
        this.transform.position = center;
        float centersDistance = (a + padding) * Mathf.Sqrt(3);
        List<Vector3> childPositions = CalculateChildPositions(centersDistance);
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

    private List<Vector3> CalculateChildPositions(float centersDistance)
    {
        float centerX = this.transform.position.x;
        float centerY = this.transform.position.y;
        float centerZ = this.transform.position.z;

        List<Vector3> positions = new List<Vector3>();

        int[] angles = new int[] {0, 120, 240};

        // Center child
        positions.Add(new Vector3(centerX, centerY, centerZ));

        foreach (int angle in angles)
        {
            float rad = angle * Mathf.Deg2Rad;
            Vector3 newPos = this.transform.position + new Vector3(
                centersDistance * Mathf.Cos(rad),
                0,
                centersDistance * Mathf.Sin(rad)
            );
            positions.Add(newPos);
        }

        return positions;
    }

    private void Close()
    {
        
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
