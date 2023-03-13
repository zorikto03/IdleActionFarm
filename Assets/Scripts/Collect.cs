using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    Backpack backpack;
    public bool isTriggered;

    void Start()
    {
        backpack = FindObjectOfType<Backpack>();   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            backpack.Add(this);
        }
    }

}
