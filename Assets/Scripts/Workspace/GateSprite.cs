using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UntitledLogicGame.Workspace.Gates;

namespace UntitledLogicGame.Workspace
{
	public class GateSprite : MonoBehaviour
	{
		#region Unity Properties

		#endregion

		#region Public Properties

		public bool Hovering { get; internal set; }
		public SpriteRenderer Renderer {
			get
			{
			 if (_renderer == null)
				_renderer = GetComponentInChildren<SpriteRenderer>();
			 return _renderer;
			}
		}

		#endregion

		#region Private Properties

		private Gate _gate;
		private SpriteRenderer _renderer;

		#endregion

		#region Unity Methods

		private void Start()
		{
			_gate = GetComponentInParent<Gate>();
		}

		private void OnMouseEnter()
		{
			GameManager.Instance.CurrentGate = _gate;
			Hovering = true;
		}

		private void OnMouseExit()
		{
			if (_gate.Equals(GameManager.Instance.CurrentGate))
			 GameManager.Instance.CurrentGate = null;
			Hovering = false;
		}

		#endregion

		#region Public Methods

		public void ResetCollider()
		{
			Destroy(GetComponent<PolygonCollider2D>());
			gameObject.AddComponent<PolygonCollider2D>();
		}

		#endregion

		#region Private Methods

		#endregion

	}
}