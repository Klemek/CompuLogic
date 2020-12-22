using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UntitledLogicGame.UI
{
	public class UIToolbarButton : MonoBehaviour, IPointerDownHandler
	{
		#region Unity Properties

		#endregion

		#region Public Properties

		public Image Image
		{
			get
			{
				if (_image == null)
					_image = GetComponentInChildren<Image>();
				return _image;
			}
		}
		public TextMeshProUGUI Text
		{
			get
			{
				if (_text == null)
					_text = GetComponentInChildren<TextMeshProUGUI>();
				return _text;
			}
		}
		public RectTransform Rect
		{
			get
			{
				if (_rect == null)
					_rect = GetComponent<RectTransform>();
				return _rect;
			}
		}
		public Action OnClick { get; set; }


		#endregion

		#region Private Properties


		private Image _image;
		private TextMeshProUGUI _text;
		private RectTransform _rect;

		#endregion

		#region Unity Methods

		public void OnPointerDown(PointerEventData eventData)
		{
			if (OnClick != null)
				OnClick();
		}

		#endregion

		#region Public Methods

		#endregion

		#region Private Methods

		#endregion
	}
}