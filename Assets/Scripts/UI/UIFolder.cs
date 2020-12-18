using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UntitledLogicGame.UI
{
	public class UIFolder : UIToolbarButton
	{
		#region Unity Properties

		public SVGImage folderImage;
		public SVGImage backImage;

		#endregion

		#region Public Properties

		public bool IsBack { get; set; }
		public string Name
		{
			set
			{
				Text.text = value;
			}
		}
		

		#endregion

		#region Private Properties

		#endregion

		#region Unity Methods

		private void OnEnable()
		{
			Image.sprite = IsBack ? backImage.sprite : folderImage.sprite;
		}

		#endregion

		#region Public Methods

		#endregion

		#region Private Methods

		#endregion
	}

}
