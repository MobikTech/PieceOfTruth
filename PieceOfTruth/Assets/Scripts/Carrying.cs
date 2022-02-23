using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class Carrying : MonoBehaviour
{
    [SerializeField] private float _pickUpRange;
    
    #nullable enable
    private  Grabable? _objectToPickUp;
    #nullable disable

    
    private Transform _cameraTransform;

    private void Start()
    {
        _cameraTransform = gameObject.GetComponentInChildren<Camera>().transform;
    }

    private void OnGrab()
    {
        if (_objectToPickUp == null)
        {
            PickUpObject();
        }
        else
        {
            DropObject();
        }
    }

    private void PickUpObject()
    {
        PickUpGrabable();
        
        if (_objectToPickUp == null)
        {
            return;
        }
        
        _objectToPickUp.Pick(_cameraTransform);
    }

    private void DropObject()
    {
        Debug.Assert(_objectToPickUp != null, nameof(_objectToPickUp) + " != null");
        
        _objectToPickUp.Drop();

        _objectToPickUp = null;
    }

    private void PickUpGrabable()
    {
        if(!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward),
            out var hit, _pickUpRange))
        {
            return;
        }
        _objectToPickUp = hit.transform.gameObject.GetComponent<Grabable>();
    }
}
