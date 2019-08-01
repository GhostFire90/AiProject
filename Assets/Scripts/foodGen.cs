using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foodGen : MonoBehaviour
{
    public Object food;
    // Start is called before the first frame update
    void Start()
    {
        Mesh planeMesh = gameObject.GetComponent<MeshFilter>().mesh;
        Bounds bounds = planeMesh.bounds;

        float minX = gameObject.transform.position.x - gameObject.transform.localScale.x * bounds.size.x * 0.5f;
        float minZ = gameObject.transform.position.z - gameObject.transform.localScale.z * bounds.size.z * 0.5f;

        
        for (int x = 100; x >= 0; --x)
        {
            Vector3 newVec = new Vector3(Random.Range(minX, -minX),
                                     gameObject.transform.position.y + 1,
                                     Random.Range(minZ, -minZ));
            Instantiate(food, newVec, Quaternion.identity);
            Debug.Log("creating");
        }
    }
}
