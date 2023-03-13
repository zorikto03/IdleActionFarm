using UnityEngine;
using EzySlice;
using DG.Tweening;


public class Slicer : MonoBehaviour
{
    [SerializeField][Range(0.2f, 0.9f)] float minScatter;
    [SerializeField][Range(0.9f, 1.5f)] float maxScatter;
    [SerializeField] Ease ease;
    public Material materialAfterSlice;
    public LayerMask sliceMask;
    public bool isTouched;

    private void Update()
    {
        if (isTouched == true)
        {
            isTouched = false;

            Collider[] objectsToBeSliced = Physics.OverlapBox(transform.position, new Vector3(1, 0.1f, 0.1f), transform.rotation, sliceMask);
            
            foreach (Collider objectToBeSliced in objectsToBeSliced)
            {
                SlicedHull slicedObject = SliceObject(objectToBeSliced.gameObject, materialAfterSlice);

                GameObject upperHullGameobject = slicedObject.CreateUpperHull(objectToBeSliced.gameObject, materialAfterSlice);
                GameObject lowerHullGameobject = slicedObject.CreateLowerHull(objectToBeSliced.gameObject, materialAfterSlice);

                upperHullGameobject.transform.position = objectToBeSliced.transform.position;
                lowerHullGameobject.transform.position = objectToBeSliced.transform.position;

                MakeItPhysical(upperHullGameobject);
                MakeItPhysical(lowerHullGameobject);

                //add sliceable mask and animate upper hull
                AddCollect(upperHullGameobject);
                AnimateUpperHull(upperHullGameobject);

                //start growing timer at lower hull
                StartGrowingTimerToLowerHull(objectToBeSliced.gameObject, lowerHullGameobject);

                objectToBeSliced.gameObject.SetActive(false);
            }
        }
    }

    private void MakeItPhysical(GameObject obj)
    {
        var collider = obj.AddComponent<MeshCollider>();
        collider.convex = true;
        collider.isTrigger = true;
    }

    void AddCollect(GameObject obj)
    {
        obj.AddComponent<Collect>();
    }

    private SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)
    {
        return obj.Slice(transform.position, transform.up, crossSectionMaterial);
    }

    void AnimateUpperHull(GameObject obj)
    {
        var delta = Random.Range(minScatter, maxScatter);
        var position = obj.transform.position;
        position.x += delta;
        position.z += delta;

        var seq = DOTween.Sequence();
        seq.Append(obj.transform.DOMove(position, 0.5f));
        seq.Insert(0, obj.transform.DOScale(new Vector3(0.5f, 0.1f, 0.5f), 0.5f));
        seq.SetEase(ease);
    }

    void StartGrowingTimerToLowerHull(GameObject original, GameObject lowerHull)
    {
        var grow = lowerHull.AddComponent<GrowGrass>();
        grow.StartTimer(original);
    }
}
