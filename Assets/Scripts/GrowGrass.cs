using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GrowGrass : MonoBehaviour
{
    float timeToGrowing = 10;


    public void StartTimer(GameObject original)
    {
        var seq = DOTween.Sequence();
        seq.AppendInterval(timeToGrowing);
        seq.OnComplete(() =>
        {
            original.SetActive(true);
            Destroy(gameObject);
        });
    }
}
