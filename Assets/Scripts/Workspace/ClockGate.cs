using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CompuLogic.Workspace
{
	public class ClockGate : Gate
	{
		#region Unity Properties

		#endregion

		#region Public Properties

		public bool State { get; set; } = true;

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
			}

			if (State)
			{
				OutputAnchor.Activated = Mathf.Repeat(Time.time, CustomProperties[0]) >= CustomProperties[0] * 0.5f;
			}
		}

		#endregion
	}

}
