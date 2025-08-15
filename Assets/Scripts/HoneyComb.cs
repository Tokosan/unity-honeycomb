using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

public class HoneyComb : MonoBehaviour
{
    // Resources
    public GameObject hexagonPrefab;
    public GameObject[] childHexagons = new GameObject[4];

    // Geometry fields
    public float hexagonSize = 10.0f;
    public float hexagonSpacing = 1.0f;
    public Vector3 position = Vector3.zero;

    // Flags
    public bool isRotating = false;
    public bool isMoving = false;
    private bool isChildAnimating = false;
    private bool isClosing = false;
    public bool isClosed = false;
    private bool isOpening = false;
    public bool isOpen = true;

    // Mechanics
    public float moveSpeed = 40.0f;
    public float rotateSpeed = 20.0f;
    public Vector3 direction = Vector3.right;

    public void Instantiate()
    {

        float centersDistance = (hexagonSize + hexagonSpacing) * Mathf.Sqrt(3);

        Debug.Log($"Creating honeycomb at {position} with centers distance {centersDistance}");
        transform.position = position;
        List<Vector3> childPositions = CalculateChildPositions(centersDistance);
        int hexagonIndex = 0;
        Color[] colors = new Color[] { Color.red, Color.green, Color.blue, Color.yellow };
        foreach (Vector3 pos in childPositions)
        {
            if (hexagonIndex == 0 || UnityEngine.Random.Range(0f, 1f) > 0.2f)
            {
                GameObject hexagon = Instantiate(hexagonPrefab, pos, Quaternion.Euler(90, 0, 0), transform);
                childHexagons[hexagonIndex] = hexagon;
                ApplyColorToHexagon(hexagon, colors[hexagonIndex]); // Apply color from array
                hexagonIndex++;
            }
        }
    }

    public void Construct(GameObject hexagonPrefab, float hexagonSize, float hexagonSpacing, Vector3 position)
    {
        this.hexagonPrefab = hexagonPrefab;
        this.hexagonSize = hexagonSize;
        this.hexagonSpacing = hexagonSpacing;
        this.position = position;
    }

    private void ApplyColorToHexagon(GameObject hexagon, Color colorToApply)
    {
        Renderer hexRenderer = hexagon.GetComponent<Renderer>();
        // Create a new material instance for this specific hexagon
        // This prevents all hexagons from sharing the same material and changing color together.
        hexRenderer.material = new Material(hexRenderer.material); 
        hexRenderer.material.color = colorToApply;
    }

    public void Move(Vector3 direction)
    {
        // This moves the whole honeycomb in the specified direction
        // direction has a magnitude (it's not normalized)
        transform.position += direction;
    }

    public void Rotate(float angle)
    {
        // This rotates the whole honeycomb around its center
        transform.Rotate(0, angle, 0);
    }

    private List<Vector3> CalculateChildPositions(float centersDistance)
    {
        List<Vector3> positions = new List<Vector3>();

        int[] angles = {0, 120, 240};

        // Center child
        positions.Add(transform.position);

        foreach (int angle in angles)
        {
            float rad = (angle - transform.rotation.eulerAngles.y) * Mathf.Deg2Rad;
            Vector3 newPos = transform.position + (new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad)) * centersDistance);
            positions.Add(newPos);
        }

        return positions;
    }

    public void Close(float duration)
    {
        if (isChildAnimating || isClosed) return; // Prevent multiple simultaneous closes
        float initialCenterDistance = (hexagonSize + hexagonSpacing) * Mathf.Sqrt(3);
        float finalCenterDistance = hexagonSize * Mathf.Sqrt(3);
        StartCoroutine(ChildMovementAnimation(duration, initialCenterDistance, finalCenterDistance, () => {
            isClosed = true;
            isOpen = false;
        }));
    }

    public void Open(float duration)
    {
        // This open the honeycomb
        if (isChildAnimating || isOpen) return; // Prevent multiple simultaneous opens
        float initialCenterDistance = hexagonSize * Mathf.Sqrt(3);
        float finalCenterDistance = (hexagonSize + hexagonSpacing) * Mathf.Sqrt(3);
        StartCoroutine(ChildMovementAnimation(duration, initialCenterDistance, finalCenterDistance, () => {
            isOpen = true;
            isClosed = false;
        }));
    }

    private IEnumerator ChildMovementAnimation(float duration, float initialCenterDistance, float finalCenterDistance, Action onComplete)
    {
        isChildAnimating = true;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float t = Mathf.Clamp01(elapsedTime / duration); // Normalized time (0 to 1)
            // Interpolate the center distance between initial and final states
            // Use Mathf.Lerp for linear interpolation or Mathf.SmoothStep for smoother start/end
            float currentCenterDistance = Mathf.SmoothStep(initialCenterDistance, finalCenterDistance, t);

            // Get the target position for this interpolation
            List<Vector3> targetPositions = CalculateChildPositions(currentCenterDistance);

            int i = 0;
            foreach (Transform child in transform)
            {
                // Directly move children to their calculated target position for this frame
                child.position = targetPositions[i];
                i++;
            }
            elapsedTime += Time.deltaTime;

            yield return null; // Wait for the next frame
        }

        // Ensure final state is exactly the closed position after the animation ends
        List<Vector3> finalTargetPositions = CalculateChildPositions(finalCenterDistance);
        int j = 0;
        foreach (Transform child in transform)
        {
            child.position = finalTargetPositions[j];
            j++;
        }
        isChildAnimating = false;
        onComplete?.Invoke();
    }

    public void Rotation(float duration)
    {
        if (isRotating) return; // Prevent multiple simultaneous rotations
        // This has to close, rotate, and then open.
        isRotating = true;
        StartCoroutine(RotationAnimation(duration));
    }

    private IEnumerator RotationAnimation(float duration)
    {
        // 1. Close the honeycomb
        Close(duration);
        yield return new WaitForSeconds(duration);

        // 2. Rotate the honeycomb
        float elapsedTime = 0f;
        int direction = UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1;
        float initialAngle = transform.eulerAngles.y;
        float targetAngle = initialAngle + 120 * direction;
        while (elapsedTime < duration)
        {
            float t = Mathf.Clamp01(elapsedTime / duration); // Normalized time (0 to 1)

            // Rotate the honeycomb around its center
            // Must rotate exactly 120 degrees
            // Direction
            float currentAngle = Mathf.SmoothStep(initialAngle, targetAngle, t);
            transform.rotation = Quaternion.Euler(0, currentAngle, 0);
            elapsedTime += Time.deltaTime;

            yield return null; // Wait for the next frame
        }

        // Make sure it's in the final angle
        transform.rotation = Quaternion.Euler(0, targetAngle, 0);

        // 3. Open the honeycomb
        Open(duration);
        yield return new WaitForSeconds(duration);

        isRotating = false;
    }

    void Update()
    {
        // if (isMoving)
        // {
        //     this.Move(direction * moveSpeed * Time.deltaTime);
        // }
        // if (isRotating)
        // {
        //     this.Rotate(rotateSpeed * Time.deltaTime * 2);
        // }
    }
}
