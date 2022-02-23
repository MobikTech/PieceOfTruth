using UnityEngine;
using UnityEngine.Serialization;

public class Grabable : MonoBehaviour
{
    [SerializeField] private float _grabDistance = 2f;
    private Transform _parent;
    private Rigidbody _rigidbody;
    private void Start()
    {
        _parent = gameObject.transform.parent;
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    public void Pick(Transform carryingTransform)
    {
        _rigidbody.isKinematic = true;
        
        transform.parent = carryingTransform;
        SetPosition();
        
    }

    public void Drop()
    {
        _rigidbody.isKinematic = false;

        transform.parent = _parent;
    }

    private void SetPosition()
    {
        var transformVar = transform;
        transformVar.localPosition = new Vector3(0, 0,  _grabDistance);
        transformVar.localRotation = Quaternion.identity;
    }
}
