using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace CompuLogic.Workspace
{
	public class Anchor : MonoBehaviour
	{
		#region Unity Properties

		public string Name;
		public bool IsInput;
		public float ScaleIncrease;
		public Vector2 Orientation;
		public float TextSpace;
		

		#endregion

		#region Public Properties

		public List<Cable> Cables { get; set; }
		public Gate Gate { get; set; }
		public bool Activated
		{
			get
			{
				if (IsInput)
					return Cables.Count > 0 && Cables.First().Activated;
				else
					return _activated;
			}
			set
			{
				if (!IsInput)
					_activated = value;
			}
		}
		public bool Hovering { get; internal set; }
		public bool ShowName
		{
			set
			{
				Text.gameObject.SetActive(value);
				if (value)
				{
					Text.text = Name;
					var rect = Text.GetComponent<RectTransform>();
					rect.localRotation = (Mathf.Abs(Orientation.y) > Mathf.Epsilon) ? Quaternion.AngleAxis(90f, Vector3.forward) : Quaternion.identity;
					rect.localPosition = new Vector3(Orientation.x, Orientation.y, 0f) * TextSpace;
					Text.alignment = (Orientation.x < -Mathf.Epsilon || Orientation.y < -Mathf.Epsilon) ? TextAlignmentOptions.MidlineRight : TextAlignmentOptions.MidlineLeft;
					
				}
			}
		}
		public TextMeshPro Text
		{
			get
			{
				if (_text == null)
					_text = GetComponentInChildren<TextMeshPro>(true);
				return _text;
			}
		}
		public SpriteRenderer Sprite
		{
			get
			{
				if (_sprite == null)
					_sprite = GetComponentInChildren<SpriteRenderer>();
				return _sprite;
			}
		}

		#endregion

		#region Private Properties

		private SpriteRenderer _sprite;
		private TextMeshPro _text;
		private Vector3 _scale;
		private bool _activated;
		private bool? _lastActivated;

		#endregion

		#region Unity Methods

		// Start is called before the first frame update
		private void Start()
		{
			Gate = GetComponentInParent<Gate>();
			Utils.RandomName($"{Gate.GateType}_{Name}", gameObject);
			_scale = Sprite.transform.localScale;
			Cables = new List<Cable>();
			Orientation = Orientation.normalized;
		}

		// Update is called once per frame
		private void Update()
		{
			UpdateState();
		}

		private void OnMouseEnter()
		{
			Sprite.transform.localScale = _scale * ScaleIncrease;
			GameManager.Instance.CurrentAnchor = this;
			Hovering = true;
		}

		private void OnMouseExit()
		{
			Sprite.transform.localScale = _scale;
			if (Equals(GameManager.Instance.CurrentAnchor))
				GameManager.Instance.CurrentAnchor = null;
			Hovering = false;
		}

		private void OnDestroy()
		{
			if(Cables != null)
			{
				foreach(var cable in Cables)
				{
					Destroy(cable.gameObject);
				}
			}
			
		}

		#endregion

		#region Public Methods

		public bool HasInputAnchor(Anchor target)
		{
			if (IsInput)
			 return Cables.Any(c => c.HasInputAnchor(target));
			else
			 return Gate.HasInputAnchor(target);
		}

		#endregion

		#region Private Methods

		private void UpdateState()
		{
			if (_lastActivated == null || _lastActivated != Activated)
			{
				Sprite.color = Activated ? GameManager.Instance.ActivatedColor : GameManager.Instance.DeadColor;
				_lastActivated = Activated;
			}
		}

		#endregion
	}
}