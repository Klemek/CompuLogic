using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CompuLogic.Workspace.Gates;

namespace CompuLogic.Workspace
{
	public class Gate : MonoBehaviour
	{
		#region Unity Properties

		public GateType GateType;

		#endregion

		#region Public Properties

		public IEnumerable<Anchor> Anchors 
		{
			get
			{
			 if(_anchors == null)
				_anchors = GetComponentsInChildren<Anchor>().ToList();
			 return _anchors;
			}
		}
		public IEnumerable<Anchor> InputAnchors => Anchors.Where(a => a.IsInput);
		public IEnumerable<Anchor> OutputAnchors => Anchors.Where(a => !a.IsInput);
		public BoxCollider2D Box {
			get
			{
			 if (_box == null)
				_box = GetComponentInChildren<BoxCollider2D>();
			 return _box;
			}
		}
		public GateSprite Sprite
		{
			get
			{
			 if(_sprite == null)
				_sprite = GetComponentInChildren<GateSprite>();
			 return _sprite;
			}
		}
		public GateDefinition Definition
		{
			get
			{
			 if(_definition == null)
				_definition = GateDefinition.Get(GateType, this);
			 return _definition;
			}
		}
		public string UIName { get; set; }

		#endregion

		#region Private Properties

		private IEnumerable<Anchor> _anchors;
		private GateDefinition _definition;
		private int _lastState = -1;
		private BoxCollider2D _box;
		private GateSprite _sprite;

		#endregion

		#region Unity Methods

		private void Start()
		{
			Utils.RandomName(GateType.ToString(), gameObject);
		}

		// Update is called once per frame
		private void Update()
		{
			UpdateState();
		}

		#endregion

		#region Public Methods

		public bool HasInputAnchor(Anchor target)
		{
			return !Definition.HasState && (
				InputAnchors.Contains(target) ||
				InputAnchors.Any(a => a.HasInputAnchor(target))
			 );
		}

		#endregion

		#region Private Methods

		private void UpdateState()
		{
			var state = Definition.GetState(this).ToInt();
			if (state != _lastState)
			{
			 Definition.Compute(this);
			 _lastState = state;
			}
		}

		#endregion

	}
}