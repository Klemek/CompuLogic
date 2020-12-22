using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using CompuLogic.Workspace;

namespace CompuLogic.UI
{
	public class UIGate : UIToolbarButton
	{
		#region Static Properties

		public float MaxSize { 
			get 
			{
				if(_maxSize == null)
					_maxSize = GameManager.Instance.GateSprites.Select(s => Math.Max(s.rect.width, s.rect.height)).Max();
				return _maxSize.Value;
			} 
		}
		private float? _maxSize;

		#endregion

		#region Unity Properties

		#endregion

		#region Public Properties

		public Gate GatePrefab
		{
			set
			{
				var sprite = value.GetComponentInChildren<SpriteRenderer>().sprite;
				Image.sprite = sprite;
				Image.GetComponent<RectTransform>().sizeDelta = new Vector2(100f, 100 * Math.Max(sprite.rect.width, sprite.rect.height) / MaxSize);
				gameObject.name = "UI_" + value.GateType.ToString();
				Text.text = value.UIName;
			}
		}

		#endregion

		#region Private Properties

		#endregion

		#region Unity Methods

		#endregion

		#region Public Methods

		#endregion

		#region Private Methods

		#endregion
	}
}