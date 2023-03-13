using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassBed : MonoBehaviour
{
    [SerializeField] GameObject grass;
    [SerializeField] int CountToX;
    [SerializeField] int CountToZ;

    float scaleX = 10; // half of Scale X
    float scaleZ = 20;  // half of Scale Z

    List<GameObject> grasses;

    // Start is called before the first frame update
    void Awake()
    {
        grasses = new List<GameObject>();

        var deltaX = scaleX / CountToX;
        var deltaZ = scaleZ / CountToZ;

        for (int i = 0; i < CountToX; i++)
        {
            for (int j = 0; j < CountToZ; j++)
            {
                var instanceGrass = Instantiate(grass, transform);
                instanceGrass.transform.localPosition = new Vector3(i * deltaX + 0.5f, 0.5f, j * deltaZ + 0.5f);
                grasses.Add(instanceGrass);
            }
        }
    }
}
