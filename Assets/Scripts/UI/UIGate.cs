using System;
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
	public class UIGate : UIToolbarButton
	{
		#region Unity Properties

		#endregion

		#region Public Properties

		public Gate GatePrefab
		{
			set
			{
				var sprite = value.GetComponentInChildren<SpriteRenderer>().sprite;
				Image.sprite = sprite;
				Image.GetComponent<RectTransform>().sizeDelta = new Vector2(100f, 100 * sprite.rect.width / 700f); // TODO get max width from UIManager
				gameObject.name = "UI_" + value.Definition.Name;
				Text.text = value.Definition.Name;
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