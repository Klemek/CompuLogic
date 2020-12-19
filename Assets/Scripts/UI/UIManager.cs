using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UntitledLogicGame.Workspace.Gates;

namespace UntitledLogicGame.UI
{
	public class UIManager : MonoBehaviour
	{
		#region Unity Properties

		[Header("Components")]
		public GameObject GateBar;
		public GameObject MovingBar;

		[Header("Prefabs")]
		public UIGate UIGatePrefab;
		public UIFolder UIFolderPrefab;

		#endregion

		#region Public Properties

		public GateCategory GateBarState
		{
			get => _gateBarState;
			set
			{
				_gateBarState = value;
				UpdateGateBar();
			}
		}

		#endregion

		#region Private Properties

		private bool _lastMouseInteracting;
		private GateCategory _gateBarState;
		private Dictionary<GateCategory, GameObject> _gateBarSateList;

		#endregion

		#region Unity Methods

		private IEnumerator Start()
		{
			yield return new WaitUntil(() => GameManager.Instance != null && GameManager.Instance.GatePrefabs == null);
			CreateGateBar();
			UpdateUI();
		}

		private void FixedUpdate()
		{
			UpdateUI();
		}

		#endregion

		#region Public Methods

		#endregion

		#region Private Methods

		private void UpdateUI()
		{
			if (PointerManager.Instance.Interacting != _lastMouseInteracting)
			{
			 //TODO animate go down
			 GateBar.SetActive(!PointerManager.Instance.Interacting);
			 MovingBar.SetActive(PointerManager.Instance.MovingObject);
			 _lastMouseInteracting = PointerManager.Instance.Interacting;
			}
		}

		private void CreateGateBar()
		{
			_gateBarSateList = new Dictionary<GateCategory, GameObject>();

			var allCategories = Enum.GetValues(typeof(GateCategory)).Cast<GateCategory>();

			foreach (var category in allCategories)
			{
				var parent = new GameObject(category.ToString(), typeof(RectTransform));
				parent.transform.parent = GateBar.transform;
				var rect = parent.GetComponent<RectTransform>();
				rect.anchorMin = Vector2.zero;
				rect.anchorMax = new Vector2(1, 1);
				rect.position = Vector2.zero;
				rect.offsetMin = Vector2.zero;
				parent.SetActive(false);
				if(category == GateCategory.None)
				{
					var currentPos = 0f;
					foreach (var subCategory in allCategories)
					{
						if(subCategory != GateCategory.None)
						{
							var uiFolder = Instantiate(UIFolderPrefab, parent.transform);
							uiFolder.Rect.anchoredPosition = new Vector2(currentPos, 0);
							uiFolder.Name = subCategory.ToString();
							uiFolder.OnClick = () =>
							{
								GateBarState = subCategory;
							};
							currentPos += 100f;
						}
					}
				}
				else
				{
					var uiFolder = Instantiate(UIFolderPrefab, parent.transform);
					uiFolder.Name = "Back";
					uiFolder.IsBack = true;
					uiFolder.OnClick = () =>
					{
						GateBarState = GateCategory.None;
					};
					var currentPos = 100f;
					var possibleGateTypes = GateDefinition.TypeCategoryList[category];
					foreach (var gatePrefab in GameManager.Instance.GatePrefabs.Where(g => possibleGateTypes.Contains(g.GateType)))
					{
						var uiGate = Instantiate(UIGatePrefab, parent.transform);
						uiGate.GatePrefab = gatePrefab;
						uiGate.Rect.anchoredPosition = new Vector2(currentPos, 0);
						uiGate.OnClick = () =>
						{
							GameManager.Instance.CreateGate(gatePrefab);
						};
						currentPos += 100f;
					}
				}
				_gateBarSateList[category] = parent;
			}
			UpdateGateBar();
		}

		private void UpdateGateBar()
		{
			//TODO animate ?
			foreach (var category in Enum.GetValues(typeof(GateCategory)).Cast<GateCategory>())
				_gateBarSateList[category].SetActive(category == GateBarState);
		}

		#endregion
	}
}