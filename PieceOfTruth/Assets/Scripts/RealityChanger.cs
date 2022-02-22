using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

namespace PieceOfTruth
{
    public enum Realities
    {
        First,
        Second
    }
    
    [RequireComponent(typeof(Volume), typeof(PlayerInput))]
    public class RealityChanger : MonoBehaviour, IDisposable
    {
        [SerializeField] private VolumeProfile _firstRealityVolume;
        [SerializeField] private VolumeProfile _secondRealityVolume;

        private Volume _currentVolume;
        private PlayerInput _playerInput;
        private Realities _currentReality = Realities.First;

        private void Awake()
        {
            _currentVolume = GetComponent<Volume>();
            _playerInput = GetComponent<PlayerInput>();

            _playerInput.actions["Change"].performed += ChangeReality;
        }

        private void OnDestroy() => Dispose();

        private void ChangeReality(InputAction.CallbackContext context)
        {
            switch (_currentReality)
            {
                case Realities.First:
                    _currentReality = Realities.Second;
                    _currentVolume.profile = _secondRealityVolume;
                    break;
                case Realities.Second:
                    _currentReality = Realities.First;
                    _currentVolume.profile = _firstRealityVolume;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        public void Dispose()
        {
            _playerInput.actions["Change"].performed -= ChangeReality;
        }
    }
}