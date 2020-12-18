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

		public SVGImage closedImage;
        public SVGImage openImage;

        #endregion

        #region Public Properties

        #endregion

        #region Private Properties

		private SVGImage Image
		{
			get
			{
				if(_image == null)
					_image = GetComponentInChildren<SVGImage>();
				return _image;
			}
		}

        private SVGImage _image;

        #endregion

        #region Unity Methods

        public void OnPointerEnter(PointerEventData eventData)
        {
			Image.sprite = openImage.sprite;
            PointerManager.Instance.DeleteOnRelease = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
			Image.sprite = closedImage.sprite;
            PointerManager.Instance.DeleteOnRelease = false;
        }

		private void OnEnable()
		{
			Image.sprite = closedImage.sprite;
		}

		#endregion

		#region Public Methods

		#endregion

		#region Private Methods

		#endregion
	}
}