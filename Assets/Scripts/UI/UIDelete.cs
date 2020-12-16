using System.Collections;
using System.Collections.Generic;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UntitledLogicGame.UI
{
    public class UIDelete : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        #region Unity Properties

        public SVGImage openImage;

        #endregion

        #region Public Properties

        #endregion

        #region Private Properties

        private SVGImage _image;
        private Sprite _closedSprite;

        #endregion

        #region Unity Methods

        private void Start()
        {
            _image = GetComponent<SVGImage>();
            _closedSprite = _image.sprite;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _image.sprite = openImage.sprite;
            PointerManager.Instance.DeleteOnRelease = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _image.sprite = _closedSprite;
            PointerManager.Instance.DeleteOnRelease = false;
        }

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        #endregion
    }
}