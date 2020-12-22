using System;
using System.Collections.Generic;
using System.Linq;

namespace CompuLogic.Workspace.Gates
{
	public abstract class GateDefinition
	{
		// Static properties
		private static Dictionary<GateType, GateDefinition> Definitions;
		public static Dictionary<GateCategory, List<GateType>> TypeCategoryList => 
			Enum.GetValues(typeof(GateCategory)).Cast<GateCategory>()
			.ToDictionary(
				c => c, 
				c => Enum.GetValues(typeof(GateType)).Cast<GateType>()
				.Where(t => (GateCategory)((int)t / 100) == c).ToList()
			);

		// Public properties
		public GateType Type { get; private set; }
		public GateCategory Category => (GateCategory)((int)Type / 100);
		public abstract bool HasState { get; }
		public Dictionary<InputState, OutputState> TruthTable { get; private set; } = new Dictionary<InputState, OutputState>();

		// Herited properties
		public abstract string[] Inputs { get; }
		public abstract string[] Outputs { get; }
		internal abstract Func<InputState, OutputState> Function { get; }

		// Private properties
		private GateDefinition New => (GateDefinition)GetType().GetConstructor(new Type[0]).Invoke(new object[0]);

		private static void LoadAll()
		{
			Definitions = new Dictionary<GateType, GateDefinition>();
			foreach (var gateType in Enum.GetValues(typeof(GateType)).Cast<GateType>())
			{
				var t = System.Type.GetType($"{typeof(GateDefinition).Namespace}.{gateType}Gate", true);
				Definitions[gateType] = (GateDefinition)t.GetConstructor(new Type[0]).Invoke(new object[0]);
				Definitions[gateType].Type = gateType;
			}
		}

		public static GateDefinition Get(GateType gateType, Gate gate)
		{
			if (Definitions == null)
				LoadAll();

			var definition = Definitions[gateType];

			foreach (var inputName in definition.Inputs)
			{
				if (!gate.InputAnchors.Any(a => a.Name.Equals(inputName)))
					throw new InvalidOperationException($"Gate {gateType} has no {inputName} input anchor");
			}

			foreach (var outputName in definition.Outputs)
			{
				if (!gate.OutputAnchors.Any(a => a.Name.Equals(outputName)))
					throw new InvalidOperationException($"Gate {gateType} has no {outputName} output anchor");
			}

			if (definition.HasState)
				return definition.New;
			else
				return definition;
		}

		internal GateDefinition()
		{
			if (!HasState)
			{
				CreateTruthTable(Function);
				if (TruthTable.Count > 0)
				{
					foreach (var key in TruthTable.Keys)
					{
						if (key.Length != Inputs.Length)
							throw new InvalidOperationException($"{GetType()} invalid inputs ({key})");
					}
					if (Inputs.Length != 0)
					{
						foreach (var key in Utils.AllBoolArrayValues(Inputs.Length).Select(b => new InputState(b)))
						{
							if (!TruthTable.Keys.Contains(key))
								throw new InvalidOperationException($"{GetType()} no outputs for ({key})");
							var values = TruthTable[key];
							if (values.Length != Outputs.Length)
								throw new InvalidOperationException($"{GetType()} invalid outputs for ({key})");
						}
					}
				}
			}
			else
			{
				// Only test basic value
				var sample = new InputState(Inputs.Length);
				var output = Function(sample);
				if (output.Length != Outputs.Length)
					throw new InvalidOperationException($"{GetType()} invalid outputs for sample ({sample})");
			}
		}

		public bool[] GetState(Gate gate)
		{
			return Inputs.Select(i => gate.InputAnchors.First(a => a.Name.Equals(i)).Activated).ToArray();
		}

		public void Compute(Gate gate)
		{
			var input = new InputState(GetState(gate));
			var output = HasState ? Function(input) : (TruthTable.Count > 0 ? TruthTable[input] : new OutputState(0));
			foreach (var outputAnchor in Outputs.Select((name, i) => new { i, name }))
				gate.OutputAnchors.First(a => a.Name.Equals(outputAnchor.name)).Activated = output[outputAnchor.i];
		}

		private void CreateTruthTable(Func<InputState, OutputState> function)
		{
			TruthTable = Utils.AllBoolArrayValues(Inputs.Length).ToDictionary(key => new InputState(key), key => function(new InputState(key)));
		}
	}

	internal abstract class StatelessGateDefinition : GateDefinition
	{
		public override bool HasState { get; } = false;
	}

	internal abstract class StatefulGateDefinition : GateDefinition
	{
		public override bool HasState { get; } = true;
	}
}
