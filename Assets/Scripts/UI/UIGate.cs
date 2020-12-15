using System.Collections;
using System.Collections.Generic;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UntitledLogicGame.UI
{
    public class UIGate : MonoBehaviour, IPointerDownHandler
    {
        #region Unity Properties

        #endregion

        #region Public Properties

        public Gate GatePrefab {
            set
            {
                _gatePrefab = value;
                var sprite = _gatePrefab.GetComponentInChildren<SpriteRenderer>().sprite;
                GetComponent<SVGImage>().sprite = sprite;
                RectTransform.sizeDelta = new Vector2(100 * sprite.rect.width / 700f, 100); // TODO get max width from UIManager
                gameObject.name = "UI_" + _gatePrefab.GateType.ToString();
            }
        }

        public RectTransform RectTransform
        {
            get
            {
                if (_rectTransform == null)
                    _rectTransform = GetComponent<RectTransform>();
                return _rectTransform;
            }
        }

        #endregion

        #region Private Properties

        private Gate _gatePrefab;
        private RectTransform _rectTransform;

        #endregion

        #region Unity Methods

        public void OnPointerDown(PointerEventData eventData)
        {
            var position = Camera.main.ScreenToWorldPoint(transform.position);
            position.z = 0f;
            GameManager.Instance.CreateGate(_gatePrefab, position);
        }

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        #endregion
    }
}