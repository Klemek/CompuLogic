using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CompuLogic.Workspace;

namespace CompuLogic
{
	public class GameManager : MonoBehaviour
	{
		#region Static Properties

		public static GameManager Instance { 
			get
			{
			 if (_instance == null)
				_instance = FindObjectOfType<GameManager>();
			 return _instance;
			}
		}
		private static GameManager _instance;

		#endregion

		#region Unity Properties

		[Header("Prefabs")]
		public Cable CablePrefab;
		public Gate GatePrefab;
		public Anchor AnchorPrefab;
		public Anchor BigAnchorPrefab;

		[Header("Groups")]
		public Transform GatesGroup;
		public Transform CablesGroup;

		[Header("Colors")]
		public Color DeadColor;
		public Color ActivatedColor;

		[Header("Gates")]
		public TextAsset GateBook;
		public List<Sprite> GateSprites;

		#endregion

		#region Public Properties
		
		public List<Gate> GatePrefabs { get; set; }
		public Anchor CurrentAnchor { get; set; }
		public Gate CurrentGate { get; set; }
		public PointerManager PointerManager 
		{
			get
			{
			 if (_pointerManager == null)
				_pointerManager = GetComponent<PointerManager>();
			 return _pointerManager;
			}
		}

		#endregion

		#region Private Properties

		private PointerManager _pointerManager;

		#endregion

		#region Unity Methods

		private void Start()
		{
			var factory = gameObject.AddComponent<GatePrefabFactory>();
			factory.Init(GatePrefab, AnchorPrefab, BigAnchorPrefab, GateSprites);
			GatePrefabs = factory.GeneratePrefabs(GateBook);
		}

		#endregion

		#region Public Methods

		public void CreateGate(Gate gatePrefab)
		{
			var gate = Instantiate(gatePrefab, GatesGroup);
			gate.gameObject.SetActive(true);
			gate.transform.position = PointerManager.MousePos - gate.Box.transform.position;
			PointerManager.DragGate(gate, true);
		}

		#endregion

		#region Private Methods

		#endregion
	}

}