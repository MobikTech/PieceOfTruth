using UnityEngine;
using UnityEngine.InputSystem;

namespace PieceOfTruth
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController _controller;
        [SerializeField] private float _speed = 120f;
        [SerializeField] private float _gravity = -9.81f;
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private float _groundDistance = 0.4f;
        [SerializeField] private LayerMask _groundLayerMask;
        [SerializeField] private float _jumpHeight = 3f;
    
        private Vector2 _moveInput;
        private Vector3 _velocity;
        private bool _isGrounded;
        private const float FreeFallModifier = -2f;

        private void OnMove(InputValue value)
        {
            _moveInput = value.Get<Vector2>();
        }

        private void OnJump()
        {
            if (_isGrounded)
            {
                _velocity.y = Mathf.Sqrt(_jumpHeight * FreeFallModifier * _gravity);
            }
        }

        private void Update()
        {
            IsGrounded();
            MovePlayer();
            PlayerGravity();
        }

        private void MovePlayer()
        {
            float x = _moveInput.x;
            float z = _moveInput.y;

            var transform1 = transform;
            var move = transform1.right * x + transform1.forward * z;
            _controller.Move(move * _speed * Time.deltaTime);
        }

        private void PlayerGravity()
        {
            _velocity.y += _gravity * Time.deltaTime;
            _controller.Move(_velocity * Time.deltaTime);
        }

        private void IsGrounded()
        {
            _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundLayerMask);

            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = FreeFallModifier;
            }
        }
    }
}
