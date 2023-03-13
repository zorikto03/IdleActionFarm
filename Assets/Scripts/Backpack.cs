using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Backpack : MonoBehaviour
{
    [SerializeField] int MaxVolume;
    [SerializeField] TextMeshProUGUI CountText;
    [SerializeField] CoinsController CoinsController;
    [SerializeField][Range(0.2f, 1f)] float minDuration;
    [SerializeField][Range(1f, 2f)] float maxDuration;

    Queue<GameObject> queue;
    int currentCount = 0;
    float upperPointY => currentCount * 0.1f;

    void Start()
    {
        queue = new Queue<GameObject>();
        SetText();
    }

    void SetText()
    {
        CountText.text = queue.Count.ToString() + "/" + MaxVolume.ToString();
    }

    public bool Add(Collect obj)
    {
        if (!obj.isTriggered)
        {
            if (currentCount < MaxVolume)
            {
                obj.isTriggered = true;
                currentCount++;
                //animate
                AnimateCollect(obj.gameObject);

                return true;
            }
        }
        return false;
    }

    void AnimateCollect(GameObject obj)
    {
        obj.transform.SetParent(transform);
        var point = new Vector3();
        point.y = upperPointY;

        obj.transform.DOLocalMove(point, 1f).
            SetEase(Ease.InOutBack)
            .OnComplete(() =>
            {
                queue.Enqueue(obj);
                SetText();
            });
    }

    public void GiveGrassAll(Vector3 position)
    {
        var count = currentCount;
        var seq = DOTween.Sequence();

        for(int i = 0; i < count; i++)
        {
            if (queue.Count > 0)
            {
                var duration = Random.Range(minDuration, maxDuration);
                var obj = queue.Dequeue();
                currentCount--;

                seq.Insert(0, obj.transform.DOMove(position, duration).
                    OnComplete(() =>
                    {
                        SetText();
                        CoinsController.Animate();
                        Destroy(obj);
                    }));
                seq.PrependInterval(0.1f);
            }
        }
        seq.Play();
    }

}
