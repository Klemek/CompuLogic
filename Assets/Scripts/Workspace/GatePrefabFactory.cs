using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using CompuLogic.Workspace;
using CompuLogic.Workspace.Gates;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace CompuLogic.Workspace
{
	public class GatePrefabFactory : MonoBehaviour
	{
		
		#region Public Properties

		#endregion

		#region Private Properties

		private Gate _gatePrefab;
		private Anchor _anchorPrefab;
		private Anchor _bigAnchorPrefab;
		private List<Sprite> _gateSprites;
		private Sprite _defaultSprite => _gateSprites.First(s => s.name == "default");

		#endregion

		#region Public Methods

		public void Init(Gate gatePrefab, Anchor anchorPrefab, Anchor bigAnchorPrefab, List<Sprite> gateSprites)
		{
			_gatePrefab = gatePrefab;
			_anchorPrefab = anchorPrefab;
			_bigAnchorPrefab = bigAnchorPrefab;
			_gateSprites = gateSprites;
		}

		public List<Gate> GeneratePrefabs(TextAsset gateBook)
		{
			Debug.Log("Loading gates");
			var deserializer = new DeserializerBuilder()
				.WithNamingConvention(UnderscoredNamingConvention.Instance)
				.Build();
			var book = deserializer.Deserialize<GateBook>(gateBook.ToString());
			return book.List.Select(kp => NewPrefab(kp.Key, kp.Value)).ToList();
		}

		private Gate NewPrefab(int key, GateBookItem item)
		{
			var gate = Instantiate(_gatePrefab);
			var prefab = gate.gameObject;
			prefab.SetActive(false);
			prefab.name = $"prefab_{key}";
			prefab.hideFlags = HideFlags.HideInHierarchy;

			if (!string.IsNullOrEmpty(item.Class))
			{
				var newClass = System.Type.GetType($"{typeof(GatePrefabFactory).Namespace}.{item.Class}", true);
				Destroy(gate);
				gate = (Gate)prefab.AddComponent(newClass);
			}

			gate.GateType = (GateType)key;
			gate.CustomProperties = item.Properties;

			gate.UIName = string.IsNullOrEmpty(item.Name) ? gate.GateType.ToString() : item.Name;

			var showAnchorNames = false;

			if (string.IsNullOrEmpty(item.Skin))
			{
				gate.Sprite.Renderer.sprite = _defaultSprite;
				gate.Sprite.Renderer.drawMode = SpriteDrawMode.Sliced;
				gate.Sprite.Renderer.size = new Vector2(item.Width, item.Height);
				showAnchorNames = true;
			}
			else
			{
				var sprite = _gateSprites.First(s => s.name == item.Skin);
				gate.Sprite.Renderer.sprite = sprite;
			}
			gate.Sprite.ResetCollider();

			if(item.Input != null && item.Input.Count > 0)
			{
				foreach(var inputAnchor in item.InputAnchors)
				{
					var anchor = Instantiate(inputAnchor.Big ? _bigAnchorPrefab : _anchorPrefab);
					anchor.transform.parent = prefab.transform;
					inputAnchor.ConfigAnchor(anchor);
					anchor.ShowName = showAnchorNames;
					anchor.IsInput = true;
				}
			}

			if(item.Output != null && item.Output.Count > 0)
			{
				foreach(var outputAnchor in item.OutputAnchors)
				{
					var anchor = Instantiate(outputAnchor.Big ? _bigAnchorPrefab : _anchorPrefab);
					anchor.transform.parent = prefab.transform;
					outputAnchor.ConfigAnchor(anchor);
					anchor.ShowName = showAnchorNames;
				}
			}

			gate.Box.transform.position = new Vector3(
				item.Width / 2f,
				-item.Height / 2f,
				gate.Box.transform.position.z
			);
			gate.Box.transform.localScale = new Vector3(
				item.Width - 0.5f,
				item.Height - 0.5f,
				1f
			);

			Debug.Log($"Loaded gate {key} {gate.GateType}");

			return gate;
		}

		#endregion

		#region Private Methods

		#endregion

		#region Classes

		public class GateBook
		{
			public Dictionary<int, GateBookItem> List { get; set; }
		}

		public class GateBookItem
		{
			public string Skin { get; set; }
			public string Name { get; set; }
			public int Width { get; set; }
			public int Height { get; set; }
			public string Class { get; set; }
			public List<string> Input { get; set; }
			public List<string> Output { get; set; }
			public List<GateBookItemAnchor> InputAnchors => Input.Select(i => i.Split(new char[0])).Select(GateBookItemAnchor.Get).ToList();
			public List<GateBookItemAnchor> OutputAnchors => Output.Select(i => i.Split(new char[0])).Select(GateBookItemAnchor.Get).ToList();
			public List<float> Properties;
		}

		public class GateBookItemAnchor
		{
			public string Name { get; set; }
			public float X { get; set; }
			public float Y { get; set; }
			public string Orientation { get; set; }
			public bool Big { get; set; }
			public Vector2 OrientationV => new Vector2(
				Orientation == "W" ? -1 : (Orientation == "E" ? 1 : 0),
				Orientation == "N" ? 1 : (Orientation == "S" ? -1 : 0)
				);
			
			public static GateBookItemAnchor Get(string[] i)
			{
				return new GateBookItemAnchor
				{
					Name = i[0],
					X = float.Parse(i[1], CultureInfo.InvariantCulture.NumberFormat),
					Y = float.Parse(i[2], CultureInfo.InvariantCulture.NumberFormat),
					Orientation = i[3],
					Big = i.Length > 4 && i[4].Equals("big"),
				};
			}

			public void ConfigAnchor(Anchor anchor)
			{
				anchor.Name = Name;
				anchor.transform.position = new Vector3(
					X,
					-Y,
					anchor.transform.position.z
				);
				anchor.Orientation = OrientationV;
			}
		}
		#endregion
	}

}
