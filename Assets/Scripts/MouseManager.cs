using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UntitledLogicGame
{
    public class MouseManager : MonoBehaviour
    {
        #region Static Properties

        public static MouseManager Instance => GameManager.Instance.MouseManager;

        #endregion

        #region Unity Properties

        #endregion

        #region Public Properties

        public static Vector3 MousePos { get; set; }

        public bool Interacting => _currentCable != null || _currentGate != null;

        public static bool Clicking => Input.GetButton("Fire1");

        #endregion

        #region Private Properties

        private Cable _currentCable;
        private Gate _currentGate;
        private Vector3 _currentGateInitialPos;
        private Vector3 _currentGateDelta;

        #endregion

        #region Unity Methods

        private void Start()
        {
            
        }

        private void FixedUpdate()
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            MousePos = mousePos;

            if (Clicking)
            {
                if(_currentCable == null)
                {
                    if (_currentGate != null)
                    {
                        _currentGate.transform.position = MousePos - _currentGateDelta;
                    }
                    else if (GameManager.Instance.CurrentAnchor != null)
                    {
                        _currentCable = Instantiate(GameManager.Instance.CablePrefab, GameManager.Instance.CablesGroup, true);
                        _currentCable.StartAnchor = GameManager.Instance.CurrentAnchor;
                    }
                    else if (GameManager.Instance.CurrentGate != null)
                    {
                        DragGate(GameManager.Instance.CurrentGate, false);
                    }
                }
            }
            else if (_currentCable != null)
            {
                if (GameManager.Instance.CurrentAnchor == null || _currentCable.StartAnchor.IsInput == GameManager.Instance.CurrentAnchor.IsInput)
                {
                    Destroy(_currentCable.gameObject);
                }
                else
                {
                    _currentCable.EndAnchor = GameManager.Instance.CurrentAnchor;
                }
                _currentCable = null;
            }
            else if(_currentGate != null)
            {
                foreach (var renderer in _currentGate.GetComponentsInChildren<SpriteRenderer>())
                {
                    renderer.sortingLayerName = "default";
                }
                _currentGate.transform.position = _currentGate.transform.position.Round();
                var currentBox = _currentGate.Box;
                if (FindObjectsOfType<Gate>()
                    .Where(g => !g.Equals(_currentGate))
                    .Select(g => g.Box)
                    .Any(b => currentBox.IsTouching(b)))
                {
                    // Collision with another gate
                    if(_currentGateInitialPos == null)
                    {
                        Destroy(_currentGate.gameObject);
                    }
                    else
                    {
                        _currentGate.transform.position = _currentGateInitialPos; // Reset pos
                    }
                }
                _currentGate = null;
            }
        }

        #endregion

        #region Public Methods

        public void DragGate(Gate gate, bool created)
        {
            _currentGate = gate;
            _currentGateDelta = MousePos - _currentGate.transform.position;
            if(!created)
                _currentGateInitialPos = _currentGate.transform.position;
            foreach (var renderer in _currentGate.GetComponentsInChildren<SpriteRenderer>())
            {
                renderer.sortingLayerName = "moving";
            }
        }

        #endregion

        #region Private Methods

        #endregion
    }
}