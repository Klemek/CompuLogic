﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CompuLogic.Workspace
{
	public class InputGate : Gate
	{
		#region Unity Properties

		#endregion

		#region Public Properties

		public bool State { get; set; }

		#endregion

		#region Private Properties

		private Anchor OutputAnchor {
			get
			{
			 if (_outputAnchor == null)
				_outputAnchor = Anchors.FirstOrDefault(g => g.Name == "Q");
			 return _outputAnchor;
			}
		}
		private Anchor _outputAnchor;

		#endregion

		#region Unity Methods

		private void Start()
		{
			Utils.RandomName("Input", gameObject);
		}

		private void Update()
		{
			UpdateState();
		}

		#endregion

		#region Public Methods

		#endregion

		#region Private Methods

		private void UpdateState()
		{
			if ((Sprite.Hovering || OutputAnchor.Hovering) && PointerManager.Instance.DoubleClick())
			{
				State = !State;
				OutputAnchor.Activated = State;
			}
		}

		#endregion
	}

}
