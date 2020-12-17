using System;
using System.Collections.Generic;
using System.Linq;

namespace UntitledLogicGame.Workspace.Gates
{
    public abstract class GateDefinition
    {
        private static Dictionary<GateType, GateDefinition> Definitions;
        internal static Dictionary<InputState, OutputState> EmptyTruthTable = new Dictionary<InputState, OutputState>();
        public abstract string[] Inputs { get; }
        public abstract string[] Outputs { get; }
        internal abstract Dictionary<InputState, OutputState> TruthTable { get; }
        public string Name { get; internal set; }
        public bool HasState { get; } = false;
        public GateDefinition New => (GateDefinition)GetType().GetConstructor(new Type[0]).Invoke(new object[0]);

        private static void LoadAll()
        {
            Definitions = new Dictionary<GateType, GateDefinition>();
            foreach (var gateType in Enum.GetValues(typeof(GateType)).Cast<GateType>())
            {
                try
                {
                    Type t = Type.GetType($"{typeof(GateDefinition).Namespace}.{gateType}Gate", true);
                    Definitions[gateType] = (GateDefinition)t.GetConstructor(new Type[0]).Invoke(new object[0]);
                    Definitions[gateType].Name = gateType.ToString();
                }
                catch
                {
                    Definitions[gateType] = (GateDefinition)typeof(NoneGate).GetConstructor(new Type[0]).Invoke(new object[0]);
                    Definitions[gateType].Name = gateType.ToString();
                }
            }
        }

        public static GateDefinition Get(GateType gateType, Gate gate)
        {
            if (Definitions == null)
                LoadAll();

            GateDefinition definition = Definitions[gateType];

            foreach (var inputName in definition.Inputs)
            {
                if(!gate.InputAnchors.Any(a => a.Name.Equals(inputName)))
                    throw new InvalidOperationException($"Gate has no {inputName} input anchor");
            }

            foreach (var outputName in definition.Outputs)
            {
                if (!gate.OutputAnchors.Any(a => a.Name.Equals(outputName)))
                    throw new InvalidOperationException($"Gate has no {outputName} output anchor");
            }

            if (definition.HasState)
                return definition.New;
            else
                return definition;
        }

        internal GateDefinition()
        {
            foreach(var key in TruthTable.Keys)
            {
                if (key.Length != Inputs.Length)
                    throw new InvalidOperationException($"{GetType()} invalid inputs ({key})");
            }
            if(Inputs.Length != 0)
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

        public bool[] GetState(Gate gate)
        {
            return Inputs.Select(i => gate.InputAnchors.First(a => a.Name.Equals(i)).Activated).ToArray();
        }

        public void Compute(Gate gate)
        {
            InputState input = new InputState(GetState(gate));
            State output = Compute(input);
            foreach (var outputAnchor in Outputs.Select((name, i) => new { i, name }))
                gate.OutputAnchors.First(a => a.Name.Equals(outputAnchor.name)).Activated = output[outputAnchor.i];
        }

        internal OutputState Compute(InputState input)
        {
            if (TruthTable.Count > 0)
            {
                return TruthTable[input];
            }
            else
            {
                return new OutputState(0);
            }
        }
    }
}
