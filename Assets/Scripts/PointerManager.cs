using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UntitledLogicGame.Workspace;

namespace UntitledLogicGame
{
	public class PointerManager : MonoBehaviour
	{
		#region Static Properties

		public static PointerManager Instance => GameManager.Instance.PointerManager;

		#endregion

		#region Unity Properties

		[Header("Interaction")]
		public float MinDistanceInteracting;

		[Header("Click")]
		public float DoubleClickThreshold;
		public float DoubleClickDelay;

		[Header("Cursor")]
		public Texture2D DefaultCursor;
		public Texture2D PointerCursor;
		public Texture2D MoveCursor;

		#endregion

		#region Public Properties

		public static Vector3 MousePos { get; set; }
		public bool Interacting => DraggingCable || MovingObject;
		public bool DraggingCable => _currentCable != null && (_currentCableInitialMousePos - MousePos).magnitude > MinDistanceInteracting;
		public bool MovingObject => _currentGate != null && (_currentGateInitialPos == null || (_currentGateInitialPos.Value - _currentGate.transform.position).magnitude > MinDistanceInteracting);
		public bool Clicking => Input.GetButton("Fire1");
		public bool DeleteOnRelease { get; set; }

		#endregion

		#region Private Properties

		private Cable _currentCable;
		private Vector3 _currentCableInitialMousePos;
		private Gate _currentGate;
		private Vector3? _currentGateInitialPos;
		private Vector3 _currentGateDelta;
		private Texture2D _currentCursor;
		private float _clicked = 0f;
		private float _clicktime = 0f;

		#endregion

		#region Unity Methods

		private void FixedUpdate()
		{
			UpdateMousePos();

			if (Clicking)
			{
				UpdateDrag();
			}
			else
			{
				UpdateDrop();
			}

			UpdateCursor();
		}

		#endregion

		#region Public Methods

		public void DragGate(Gate gate, bool created)
		{
			_currentGate = gate;
			_currentGateDelta = MousePos - _currentGate.transform.position;
			_currentGateInitialPos = created ? (Vector3?)null : _currentGate.transform.position;
			foreach (var renderer in _currentGate.GetComponentsInChildren<SpriteRenderer>())
			{
				renderer.sortingLayerName = "moving";
			}
		}

		public void RequestDelete()
		{
			if (_currentGate != null)
			{
				Destroy(_currentGate.gameObject);
				_currentGate = null;
			}
		}

		public bool DoubleClick()
		{
			
			if (Clicking)
			{
				_clicked += Time.deltaTime;
			}
			else
			{
				if(_clicked >= DoubleClickThreshold)
				{
					if(Time.time - _clicktime < DoubleClickDelay)
					{
						_clicked = 0f;
						_clicktime = 0f;
						return true;
					}
					else
					{
						_clicktime = Time.time;
					}
				}
				_clicked = 0f;
			}
			return false;
		}

		#endregion

		#region Private Methods

		private static void UpdateMousePos()
		{
			var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mousePos.z = 0f;
			MousePos = mousePos;
		}

		private void UpdateCursor()
		{
			var cursor = DefaultCursor;
			var position = Vector2.zero;
			var interacting = _currentCable != null || _currentGate != null;

			if (!interacting && GameManager.Instance.CurrentAnchor != null || _currentCable != null)
			{
				cursor = PointerCursor;
				position = new Vector2(cursor.width / 2f, 0f);
			}
			else if (!interacting && GameManager.Instance.CurrentGate != null || _currentGate != null)
			{
				cursor = MoveCursor;
				position = new Vector2(cursor.width / 2f, cursor.height / 2f);
			}

			if(_currentCursor != cursor)
			{
				Cursor.SetCursor(cursor, position, CursorMode.Auto);
				_currentCursor = cursor;
			}
		}

		private void UpdateDrag()
		{
			if (_currentCable != null) // Dragging cable
			{
				_currentCable.FallbackEndPos = MousePos;
			}
			else if (_currentGate != null) // Dragging gate
			{
				_currentGate.transform.position = MousePos - _currentGateDelta;
			}
			else if (GameManager.Instance.CurrentAnchor != null) // Dragging new cable
			{
				_currentCable = Instantiate(GameManager.Instance.CablePrefab, GameManager.Instance.CablesGroup, true);
				_currentCable.StartAnchor = GameManager.Instance.CurrentAnchor;
				_currentCable.FallbackEndPos = MousePos;
				_currentCableInitialMousePos = MousePos;
			}
			else if (GameManager.Instance.CurrentGate != null) // Dragging new gate
			{
				DragGate(GameManager.Instance.CurrentGate, false);
			}
		}

		private void UpdateDrop()
		{
			if (_currentCable != null) // Dropping cable
			{
				if (GameManager.Instance.CurrentAnchor == null || _currentCable.StartAnchor.IsInput == GameManager.Instance.CurrentAnchor.IsInput)
				{
					Destroy(_currentCable.gameObject);
				}
				else
				{
					_currentCable.EndAnchor = GameManager.Instance.CurrentAnchor;
				}
				_currentCable = null;
			}
			else if (_currentGate != null) // Dropping gate
			{
				if (DeleteOnRelease)
				{
					Destroy(_currentGate.gameObject);
				}
				else
				{
					foreach (var renderer in _currentGate.GetComponentsInChildren<SpriteRenderer>())
					{
						renderer.sortingLayerName = "default";
					}
					_currentGate.transform.position = _currentGate.transform.position.Round();
					var currentBox = _currentGate.Box;
					if (FindObjectsOfType<Gate>()
						.Where(g => !g.Equals(_currentGate))
						.Select(g => g.Box)
						.Any(b => currentBox.IsTouching(b)))
					{
						// Collision with another gate
						if (_currentGateInitialPos == null)
						{
							Destroy(_currentGate.gameObject);
						}
						else
						{
							_currentGate.transform.position = _currentGateInitialPos.Value; // Reset pos
						}
					}
				}
				_currentGate = null;
				DeleteOnRelease = false;
			}
		}

		

		#endregion
	}
}