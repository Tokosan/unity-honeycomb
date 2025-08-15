// using UnityEngine;

// public class Hexagon : MonoBehaviour
// {
//     public class HexagonData
//     {
//         public Vector3 center;
//         public float size;
//         public float padding;
//         public GameObject hexagonPrefab;
//         private Hexagon[] childHexagons;

//         public HexagonData(Vector3 center, float size, float padding, GameObject hexagonPrefab)
//         {
//             this.center = center;
//             this.size = size;
//             this.padding = padding;
//             this.hexagonPrefab = hexagonPrefab;
//             this.childHexagons = new Hexagon[3];
//         }
//     }

//     Quaternion rotation = Quaternion.Euler(90, 0, 0);

//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     public GameObject Instantiate()
//     {
//         transform.position = center;
//         GameObject hexagon = Instantiate(hexagonPrefab, center, rotation, transform);
//         if (isStart)
//         {
//             ApplyColorToHexagon(hexagon, Color.green);
//         }
//         CreateChildren();
//         return hexagon;
//     }

//     private void CreateChildren()
//     {
        
//     }

//     private void ApplyColorToHexagon(GameObject hexagon, Color colorToApply)
//     {
//         Renderer hexRenderer = hexagon.GetComponent<Renderer>();
//         // Create a new material instance for this specific hexagon
//         // This prevents all hexagons from sharing the same material and changing color together.
//         hexRenderer.material = new Material(hexRenderer.material);
//         hexRenderer.material.color = colorToApply;
//     }


//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }
