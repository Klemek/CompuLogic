using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UntitledLogicGame.Workspace;

namespace UntitledLogicGame.UI
{
    public class UIGate : MonoBehaviour, IPointerDownHandler
    {
        #region Unity Properties

        #endregion

        #region Public Properties

        public Gate GatePrefab 
        {
            set
            {
                _gatePrefab = value;
                var sprite = _gatePrefab.GetComponentInChildren<SpriteRenderer>().sprite;
                var subimage = GetComponentInChildren<SVGImage>();
                subimage.sprite = sprite;
                subimage.GetComponent<RectTransform>().sizeDelta = new Vector2(100f, 100 * sprite.rect.width / 700f); // TODO get max width from UIManager
                gameObject.name = "UI_" + _gatePrefab.GateType.ToString();
                GetComponentInChildren<TextMeshProUGUI>().text = _gatePrefab.GateType.ToString();
            }
        }

        #endregion

        #region Private Properties

        private Gate _gatePrefab;
        private RectTransform _rectTransform;

        #endregion

        #region Unity Methods

        private void Start()
        {
            
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            GameManager.Instance.CreateGate(_gatePrefab);
        }

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        #endregion
    }
}