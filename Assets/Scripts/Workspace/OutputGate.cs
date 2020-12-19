using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UntitledLogicGame.Workspace
{
	public class OutputGate : Gate
	{
		#region Unity Properties

		#endregion

		#region Public Properties

		public bool State {
			get
			{
			 return InputAnchor.Activated;
			}
		}

		#endregion

		#region Private Properties

		private Anchor InputAnchor {
			get
			{
			 if (_inputAnchor == null)
				_inputAnchor = Anchors.FirstOrDefault(g => g.Name == "A");
			 return _inputAnchor;
			}
		}
		private Anchor _inputAnchor;

		#endregion

		#region Unity Methods

		private void Start()
		{
			Utils.RandomName("Output", gameObject);
		}

		#endregion

		#region Public Methods

		#endregion

		#region Private Methods

		#endregion
	}

}
