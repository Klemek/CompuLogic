using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UntitledLogicGame.Workspace.Gates
{
    public enum GateType
    {
        None,
        Buffer,
        NOTGate,
        ANDGate,
        ORGate,
        XORGate,
        NANDGate,
        NORGate,
        XNORGate
    }

    public abstract class GateDefinition
    {
        private static Dictionary<GateType, GateDefinition> Definitions;
        public abstract string[] Inputs { get; }
        public abstract string[] Outputs { get; }
        internal abstract Dictionary<State, State> TruthTable { get; }

        private static void LoadAll()
        {
            Definitions = new Dictionary<GateType, GateDefinition>();
            foreach (var gateType in Enum.GetValues(typeof(GateType)).Cast<GateType>())
            {
                Type t = Type.GetType($"{typeof(GateDefinition).Namespace}.{gateType}", true);
                Definitions[gateType] = (GateDefinition)t.GetConstructor(new Type[0]).Invoke(new object[0]);
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
                foreach (var key in Utils.AllBoolArrayValues(Inputs.Length).Select(b => new State(b)))
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
            if(TruthTable.Count > 0)
            {
                State key = new State(GetState(gate));
                bool[] values = TruthTable[key].values;
                foreach (var output in Outputs.Select((value, i) => new { i, value }))
                {
                    gate.OutputAnchors.First(a => a.Name.Equals(output.value)).Activated = values[output.i];
                }
            }
        }
    }

    internal class State
    {
        internal int Length => values.Length;

        internal bool[] values;

        public State(IEnumerable<bool> args)
        {
            values = args.ToArray();
        }

        public State(params bool[] args)
        {
            values = args;
        }

        public override bool Equals(object obj)
        {
            return obj is State state && Enumerable.SequenceEqual(values, state.values);
        }

        public override int GetHashCode()
        {
            //https://stackoverflow.com/questions/6832139/gethashcode-from-booleans-only
            int hash = 17;
            for (int index = 0; index < values.Length; index++)
                hash = hash * 23 + values[index].GetHashCode();
            return hash;
        }

        public override string ToString()
        {
            return string.Join(",", values);
        }
    }

    #region Gates

    internal class None : GateDefinition
    {
        public override string[] Inputs { get; } = new string[] { };
        public override string[] Outputs { get; } = new string[] { };
        internal override Dictionary<State, State> TruthTable { get; } = new Dictionary<State, State>
        {
        };
    }

    internal class Buffer : GateDefinition
    {
        public override string[] Inputs { get; } = new string[] { "A" };
        public override string[] Outputs { get; } = new string[] { "Q" };
        internal override Dictionary<State, State> TruthTable { get; } = new Dictionary<State, State>
        {
            { new State(   false    ), new State(  false    ) },
            { new State(   true     ), new State(  true    ) },
        };
    }

    internal class NOTGate : GateDefinition
    {
        public override string[] Inputs { get; } = new string[] { "A" };
        public override string[] Outputs { get; } = new string[] { "Q" };
        internal override Dictionary<State, State> TruthTable { get; } = new Dictionary<State, State>
        {
            { new State(   false    ), new State(  true    ) },
            { new State(   true     ), new State(  false    ) },
        };
    }

    internal class ANDGate : GateDefinition
    {
        public override string[] Inputs { get; } = new string[] { "A", "B" };
        public override string[] Outputs { get; } = new string[] { "Q" };
        internal override Dictionary<State, State> TruthTable { get; } = new Dictionary<State, State>
        {
            { new State(   false,    false  ), new State(  false  ) },
            { new State(   false,    true   ), new State(  false  ) },
            { new State(   true,     false  ), new State(  false  ) },
            { new State(   true,     true   ), new State(  true   ) },
        };
    }

    internal class ORGate : GateDefinition
    {
        public override string[] Inputs { get; } = new string[] { "A", "B" };
        public override string[] Outputs { get; } = new string[] { "Q" };
        internal override Dictionary<State, State> TruthTable { get; } = new Dictionary<State, State>
        {
            { new State(   false,    false  ), new State(  false  ) },
            { new State(   false,    true   ), new State(  true  ) },
            { new State(   true,     false  ), new State(  true  ) },
            { new State(   true,     true   ), new State(  true   ) },
        };
    }

    internal class XORGate : GateDefinition
    {
        public override string[] Inputs { get; } = new string[] { "A", "B" };
        public override string[] Outputs { get; } = new string[] { "Q" };
        internal override Dictionary<State, State> TruthTable { get; } = new Dictionary<State, State>
        {
            { new State(   false,    false  ), new State(  false  ) },
            { new State(   false,    true   ), new State(  true  ) },
            { new State(   true,     false  ), new State(  true  ) },
            { new State(   true,     true   ), new State(  false   ) },
        };
    }

    internal class NANDGate : GateDefinition
    {
        public override string[] Inputs { get; } = new string[]{ "A", "B" };
        public override string[] Outputs { get; } = new string[] { "Q" };
        internal override Dictionary<State, State> TruthTable { get; } = new Dictionary<State, State>
        {
            { new State(   false,    false  ), new State(  true    ) },
            { new State(   false,    true   ), new State(  true    ) },
            { new State(   true,     false  ), new State(  true    ) },
            { new State(   true,     true   ), new State(  false   ) },
        };
    }

    internal class NORGate : GateDefinition
    {
        public override string[] Inputs { get; } = new string[] { "A", "B" };
        public override string[] Outputs { get; } = new string[] { "Q" };
        internal override Dictionary<State, State> TruthTable { get; } = new Dictionary<State, State>
        {
            { new State(   false,    false  ), new State(  true  ) },
            { new State(   false,    true   ), new State(  false  ) },
            { new State(   true,     false  ), new State(  false  ) },
            { new State(   true,     true   ), new State(  false   ) },
        };
    }

    internal class XNORGate : GateDefinition
    {
        public override string[] Inputs { get; } = new string[] { "A", "B" };
        public override string[] Outputs { get; } = new string[] { "Q" };
        internal override Dictionary<State, State> TruthTable { get; } = new Dictionary<State, State>
        {
            { new State(   false,    false  ), new State(  true  ) },
            { new State(   false,    true   ), new State(  false  ) },
            { new State(   true,     false  ), new State(  false  ) },
            { new State(   true,     true   ), new State(  true   ) },
        };
    }

    #endregion
}
