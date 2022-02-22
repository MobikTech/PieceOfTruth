using UnityEngine;
using UnityEngine.InputSystem;

namespace PieceOfTruth
{
    public class MouseLook : MonoBehaviour
    {
        [SerializeField] private float _mouseSensitivity = 100f;
        [SerializeField] private Transform _playerBody;
        private float _xRotation = 0f;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            OnMouseMovement();
        }

        private void OnMouseMovement()
        {
            float mouseX = Mouse.current.delta.x.ReadValue() * _mouseSensitivity * Time.deltaTime;
            float mouseY = Mouse.current.delta.y.ReadValue() * _mouseSensitivity * Time.deltaTime;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            transform.localRotation= Quaternion.Euler(_xRotation, 0f, 0f);
            _playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
