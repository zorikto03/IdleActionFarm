using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Ambar : MonoBehaviour
{
    void Awake()
    {
        var collider = GetComponent<BoxCollider>();
        collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var backpack = other.GetComponentInChildren<Backpack>();
            backpack.GiveGrassAll(transform.position);
        }
    }
}
