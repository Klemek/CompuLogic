using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UntitledLogicGame.UI
{
	public class UIDelete : UIToolbarButton, IPointerEnterHandler, IPointerExitHandler
	{
		#region Unity Properties

		public Sprite closedImage;
		public Sprite openImage;

		#endregion

		#region Public Properties

		#endregion

		#region Private Properties

		#endregion

		#region Unity Methods

		public void OnPointerEnter(PointerEventData eventData)
		{
			Image.sprite = openImage;
			PointerManager.Instance.DeleteOnRelease = true;
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			Image.sprite = closedImage;
			PointerManager.Instance.DeleteOnRelease = false;
		}

		private void OnEnable()
		{
			Image.sprite = closedImage;
		}

		#endregion

		#region Public Methods

		#endregion

		#region Private Methods

		#endregion
	}
}