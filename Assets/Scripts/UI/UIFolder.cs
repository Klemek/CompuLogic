using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UntitledLogicGame.UI
{
	public class UIFolder : UIToolbarButton
	{
		#region Unity Properties

		public Sprite folderImage;
		public Sprite backImage;

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
			Image.sprite = IsBack ? backImage : folderImage;
		}

		#endregion

		#region Public Methods

		#endregion

		#region Private Methods

		#endregion
	}

}
