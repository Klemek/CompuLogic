using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UntitledLogicGame
{
    public class CameraManager : MonoBehaviour
    {
        #region Unity Properties

        [Header("Zooming")]
        public int MinSize;
        public int MaxSize;
        public float ScrollSensitivity;

        [Header("Moving")]
        public Camera Camera101;

        #endregion

        #region Public Properties

        #endregion

        #region Private Properties

        private Vector3 _startDragMousePos;
        private Vector3? _startDragPos;

        #endregion

        #region Unity Methods

        private void FixedUpdate()
        {
            UpdateZoom();
            UpdateDrag();
        }

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        private void UpdateZoom()
        {
            var size = Camera.main.orthographicSize;
            size -= Input.GetAxis("Mouse ScrollWheel") * ScrollSensitivity;
            size = Mathf.Clamp(size, MinSize, MaxSize);
            Camera.main.orthographicSize = size;
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -size);
        }

        private void UpdateDrag()
        {
            if (Input.GetMouseButton(2))
            {
                var mousePos = Camera101.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0f;

                if (_startDragPos == null)
                {
                    _startDragMousePos = mousePos;
                    _startDragPos = Camera.main.transform.position;
                }

                Camera.main.transform.position = _startDragPos.Value - (mousePos - _startDragMousePos) * Camera.main.orthographicSize;
            }
            else if (_startDragPos != null)
            {
                _startDragPos = null;
            }
        }

        #endregion
    }
}