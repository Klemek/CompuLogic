using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace CompuLogic
{
	public class OverlineCorrector : MonoBehaviour
	{
		public Vector3 SubTextOffset;

		#region Unity Properties

		#endregion

		#region Public Properties

		#endregion

		#region Private Properties

		private bool _activate;

		#endregion

		#region Unity Methods

		private void Start()
		{
			_activate = GetComponent<TextMeshPro>().text.Contains("\x305");
		}

		private void FixedUpdate()
		{
			if (_activate)
			{
				var subText = GetComponentInChildren<TMP_SubMesh>();
				if(subText != null && Mathf.Abs(subText.transform.localPosition.magnitude) < Mathf.Epsilon)
					subText.transform.localPosition = SubTextOffset;
			}
		}

		#endregion

		#region Public Methods

		#endregion

		#region Private Methods

		#endregion
	}

}
