using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestDOTween : MonoBehaviour
{
    [SerializeField] GameObject character;
    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();

        DOTween.Sequence().
            Append(transform.DOMove(character.transform.position, 1f)).
            Append(transform.DOScale(0.3f, 3f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
