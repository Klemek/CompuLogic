using System.Collections.Generic;
using System.Linq;

namespace UntitledLogicGame.Workspace.Gates
{
    public class InputState : State 
    {
        public InputState(IEnumerable<bool> args) : base(args) { }

        public InputState(params bool[] args) : base(args) { }

        public InputState(int len) : base(len) { }
    }

	public class OutputState : State 
    {
        public OutputState(IEnumerable<bool> args) : base(args) { }

        public OutputState(params bool[] args) : base(args) { }

        public OutputState(int len) : base(len) { }
    }

	public abstract class State
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

        public State(int len)
        {
            values = new bool[len];
        }

        public bool this[int index]
        {
            get
            {
                if (index < 0 || index >= values.Length)
                    return false;
                return values[index];
            }
            set
            {
                if (index >= 0 && index < values.Length)
                    values[index] = value;
            }
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
}
