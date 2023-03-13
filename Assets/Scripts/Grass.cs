using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    [SerializeField] int Count;
    [SerializeField] GameObject cuttedGrass;

    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sickle"))
        {
            for (int i = 0; i < Count; i++)
            {
                var x = Random.Range(0.1f, 0.3f);
                var z = Random.Range(0.1f, 0.3f);
                var pos = transform.position + new Vector3(x, 0, z);
                Instantiate(cuttedGrass, pos, transform.rotation);
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sickle"))
        {
            for (int i = 0; i < Count; i++)
            {
                var x = Random.Range(0.1f, 0.3f);
                var z = Random.Range(0.1f, 0.3f);
                var pos = transform.position + new Vector3(x, 0, z);
                Instantiate(cuttedGrass, pos, transform.rotation);
            }
            Destroy(gameObject);
        }
    }

}
