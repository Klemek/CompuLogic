﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CompuLogic.Workspace
{
	public class Cable : MonoBehaviour
	{
		#region Unity Properties

		#endregion

		#region Public Properties

		public Anchor StartAnchor
		{
			get => _startAnchor;
			set
			{
			 _startAnchor = value;
			 if (value.IsInput)
			 {
				if (value.Cables.Count > 0)
					Destroy(value.Cables.First().gameObject);
			 }
			}
		}
		public Anchor EndAnchor
		{
			get => _endAnchor;
			set
			{
			 if (!value.IsInput)
			 {
				_endAnchor = StartAnchor;
				StartAnchor = value;
			 }
			 else
			 {
				_endAnchor = value;
			 }

			 if (StartAnchor.HasInputAnchor(EndAnchor))
			 {
				// Loop detected
				Destroy(gameObject);
			 }
			 else
			 {
				StartAnchor.Cables.Add(this);
				if (EndAnchor.Cables.Count > 0)
					Destroy(EndAnchor.Cables.First().gameObject);
				EndAnchor.Cables = new List<Cable> { this };
			 }
			}
		}
		public bool Activated => StartAnchor != null && !StartAnchor.IsInput && StartAnchor.Activated;
		public Vector3 FallbackEndPos { get; set; }

		#endregion

		#region Private Properties

		private Vector3 _lastStartPos;
		private Vector3 _lastEndPos;
		private Anchor _startAnchor;
		private Anchor _endAnchor;
		private LineRenderer _line;
		private bool? _lastActivated;

		#endregion

		#region Unity Methods

		// Start is called before the first frame update
		private void Start()
		{
			_line = GetComponent<LineRenderer>();
			Utils.RandomName("Cable", gameObject);

		}

		// Update is called once per frame
		private void Update()
		{
			UpdateColor();
			UpdateLine();
		}

		private void OnDestroy()
		{
			if(StartAnchor != null)
			 StartAnchor.Cables.Remove(this);
			if (EndAnchor != null)
			 EndAnchor.Cables.Remove(this);
		}

		#endregion

		#region Public Methods

		public bool HasInputAnchor(Anchor target)
		{
			return StartAnchor.HasInputAnchor(target);
		}

		#endregion

		#region Private Methods

		private void UpdateColor()
		{
			if (_lastActivated == null || _lastActivated != Activated)
			{
			 _line.startColor = Activated ? GameManager.Instance.ActivatedColor : GameManager.Instance.DeadColor;
			 _line.endColor = Activated ? GameManager.Instance.ActivatedColor : GameManager.Instance.DeadColor;
			 _lastActivated = Activated;
			}
		}

		private void UpdateLine()
		{
			if (StartAnchor != null)
			{
			 var startPos = StartAnchor.transform.position;
			 var endPos = EndAnchor == null ? FallbackEndPos : EndAnchor.transform.position;

			 if (startPos != _lastStartPos || endPos != _lastEndPos)
			 {
				if (EndAnchor == null)
				{
					_line.positionCount = 2;
					_line.SetPosition(0, startPos);
					_line.SetPosition(1, endPos);
				}
				else
				{
					var startOr = StartAnchor.Orientation;
					var endOr = EndAnchor.Orientation;

					_line.positionCount = 4;
					_line.SetPosition(0, startPos);

					if (Mathf.Abs(startOr.x) > Mathf.Epsilon)
					{
						if (Mathf.Abs(endOr.x) > Mathf.Epsilon)
						{
							var middle = (startPos.x + endPos.x) / 2;
							_line.SetPosition(1, new Vector3(middle, startPos.y, startPos.z));
							_line.SetPosition(2, new Vector3(middle, endPos.y, startPos.z));
						}
						else
						{
							_line.SetPosition(1, new Vector3(endPos.x, startPos.y, startPos.z));
							_line.SetPosition(2, new Vector3(endPos.x, startPos.y, startPos.z));
						}
					}
					else
					{
						if (Mathf.Abs(endOr.x) > Mathf.Epsilon)
						{
							var middle = (startPos.y + endPos.y) / 2;
							_line.SetPosition(1, new Vector3(startPos.x, middle, startPos.z));
							_line.SetPosition(2, new Vector3(endPos.x, middle, startPos.z));
						}
						else
						{
							_line.SetPosition(1, new Vector3(startPos.x, endPos.y, startPos.z));
							_line.SetPosition(2, new Vector3(startPos.x, endPos.y, startPos.z));
						}
					}

					_line.SetPosition(3, endPos);
				}
			 }
			}
			else
			{
			 _line.positionCount = 0;
			}
		}

		#endregion

	}
}