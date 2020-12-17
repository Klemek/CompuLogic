using System.Collections.Generic;

namespace UntitledLogicGame.Workspace.Gates
{
    #region 000 - Technical

    internal class NoneGate : GateDefinition
    {
        public override string[] Inputs { get; } = new string[] { };
        public override string[] Outputs { get; } = new string[] { };
        internal override Dictionary<InputState, OutputState> TruthTable => EmptyTruthTable;
    }

    #endregion

    #region 100 - Special

    #endregion

    #region 200 - Basic

    internal class BufferGate : GateDefinition
    {
        public override string[] Inputs { get; } = new string[] { "A" };
        public override string[] Outputs { get; } = new string[] { "Q" };
        internal override Dictionary<InputState, OutputState> TruthTable { get; } = new Dictionary<InputState, OutputState>
        {
            { new InputState(   false    ), new OutputState(  false    ) },
            { new InputState(   true     ), new OutputState(  true    ) },
        };
    }

    internal class ANDGate : GateDefinition
    {
        public override string[] Inputs { get; } = new string[] { "A", "B" };
        public override string[] Outputs { get; } = new string[] { "Q" };
        internal override Dictionary<InputState, OutputState> TruthTable { get; } = new Dictionary<InputState, OutputState>
        {
            { new InputState(   false,    false  ), new OutputState(  false  ) },
            { new InputState(   false,    true   ), new OutputState(  false  ) },
            { new InputState(   true,     false  ), new OutputState(  false  ) },
            { new InputState(   true,     true   ), new OutputState(  true   ) },
        };
    }

    internal class ORGate : GateDefinition
    {
        public override string[] Inputs { get; } = new string[] { "A", "B" };
        public override string[] Outputs { get; } = new string[] { "Q" };
        internal override Dictionary<InputState, OutputState> TruthTable { get; } = new Dictionary<InputState, OutputState>
        {
            { new InputState(   false,    false  ), new OutputState(  false  ) },
            { new InputState(   false,    true   ), new OutputState(  true  ) },
            { new InputState(   true,     false  ), new OutputState(  true  ) },
            { new InputState(   true,     true   ), new OutputState(  true   ) },
        };
    }

    internal class XORGate : GateDefinition
    {
        public override string[] Inputs { get; } = new string[] { "A", "B" };
        public override string[] Outputs { get; } = new string[] { "Q" };
        internal override Dictionary<InputState, OutputState> TruthTable { get; } = new Dictionary<InputState, OutputState>
        {
            { new InputState(   false,    false  ), new OutputState(  false  ) },
            { new InputState(   false,    true   ), new OutputState(  true  ) },
            { new InputState(   true,     false  ), new OutputState(  true  ) },
            { new InputState(   true,     true   ), new OutputState(  false   ) },
        };
    }

    #endregion

    #region 300 - Inverted Basic

    internal class NOTGate : GateDefinition
    {
        public override string[] Inputs { get; } = new string[] { "A" };
        public override string[] Outputs { get; } = new string[] { "Q" };
        internal override Dictionary<InputState, OutputState> TruthTable { get; } = new Dictionary<InputState, OutputState>
        {
            { new InputState(   false    ), new OutputState(  true    ) },
            { new InputState(   true     ), new OutputState(  false    ) },
        };
    }

    internal class NANDGate : GateDefinition
    {
        public override string[] Inputs { get; } = new string[] { "A", "B" };
        public override string[] Outputs { get; } = new string[] { "Q" };
        internal override Dictionary<InputState, OutputState> TruthTable { get; } = new Dictionary<InputState, OutputState>
        {
            { new InputState(   false,    false  ), new OutputState(  true    ) },
            { new InputState(   false,    true   ), new OutputState(  true    ) },
            { new InputState(   true,     false  ), new OutputState(  true    ) },
            { new InputState(   true,     true   ), new OutputState(  false   ) },
        };
    }

    internal class NORGate : GateDefinition
    {
        public override string[] Inputs { get; } = new string[] { "A", "B" };
        public override string[] Outputs { get; } = new string[] { "Q" };
        internal override Dictionary<InputState, OutputState> TruthTable { get; } = new Dictionary<InputState, OutputState>
        {
            { new InputState(   false,    false  ), new OutputState(  true  ) },
            { new InputState(   false,    true   ), new OutputState(  false  ) },
            { new InputState(   true,     false  ), new OutputState(  false  ) },
            { new InputState(   true,     true   ), new OutputState(  false   ) },
        };
    }

    internal class XNORGate : GateDefinition
    {
        public override string[] Inputs { get; } = new string[] { "A", "B" };
        public override string[] Outputs { get; } = new string[] { "Q" };
        internal override Dictionary<InputState, OutputState> TruthTable { get; } = new Dictionary<InputState, OutputState>
        {
            { new InputState(   false,    false  ), new OutputState(  true  ) },
            { new InputState(   false,    true   ), new OutputState(  false  ) },
            { new InputState(   true,     false  ), new OutputState(  false  ) },
            { new InputState(   true,     true   ), new OutputState(  true   ) },
        };
    }

    #endregion

    #region 400 - Latches

    internal class SRLatchGate : GateDefinition
    {
        public override string[] Inputs { get; } = new string[] { "S", "R" };
        public override string[] Outputs { get; } = new string[] { "Q", "Q̅" };
        internal override Dictionary<InputState, OutputState> TruthTable => EmptyTruthTable;
        public new string Name => "SR Latch";
        public new bool HasState => true;

        private bool _q;

        internal new OutputState Compute(InputState input)
        {
            var s = input[0];
            var r = input[1];
            if (r)
                _q = false;
            else if (s)
                _q = true;
            return new OutputState(_q, !_q);
        }
    }

    internal class JKLatchGate : GateDefinition
    {
        public override string[] Inputs { get; } = new string[] { "J", "K" };
        public override string[] Outputs { get; } = new string[] { "Q", "Q̅" };
        internal override Dictionary<InputState, OutputState> TruthTable => EmptyTruthTable;
        public new string Name => "JK Latch";
        public new bool HasState => true;

        private bool _q;

        internal new OutputState Compute(InputState input)
        {
            var j = input[0];
            var k = input[1];
            if (k && j)
                _q = !_q;
            else if (k)
                _q = false;
            else if (j)
                _q = true;
            return new OutputState(_q, !_q);
        }
    }

    internal class DLatchGate : GateDefinition
    {
        public override string[] Inputs { get; } = new string[] { "D", "E" };
        public override string[] Outputs { get; } = new string[] { "Q", "Q̅" };
        internal override Dictionary<InputState, OutputState> TruthTable => EmptyTruthTable;
        public new string Name => "D Latch";
        public new bool HasState => true;

        private bool _q;

        internal new OutputState Compute(InputState input)
        {
            var d = input[0];
            var e = input[1];
            if (e)
                _q = d;
            return new OutputState(_q, !_q);
        }
    }

    #endregion

    #region 500 - Flip-Flops

    internal class SRFlipFlopGate : GateDefinition
    {
        public override string[] Inputs { get; } = new string[] { "S", "R", "CLK" };
        public override string[] Outputs { get; } = new string[] { "Q", "Q̅" };
        internal override Dictionary<InputState, OutputState> TruthTable => EmptyTruthTable;
        public new string Name => "SR Flip-Flop";
        public new bool HasState => true;

        private bool _q;
        private bool _lastClk;

        internal new OutputState Compute(InputState input)
        {
            var s = input[0];
            var r = input[1];
            var clk = input[2];
            if (clk && !_lastClk) // rising edge
                if (r)
                    _q = false;
                else if (s)
                    _q = true;
            _lastClk = clk;
            return new OutputState(_q, !_q);
        }
    }

    internal class JKFlipFlopGate : GateDefinition
    {
        public override string[] Inputs { get; } = new string[] { "J", "K", "CLK" };
        public override string[] Outputs { get; } = new string[] { "Q", "Q̅" };
        internal override Dictionary<InputState, OutputState> TruthTable => EmptyTruthTable;
        public new string Name => "JK Flip-Flop";
        public new bool HasState => true;

        private bool _q;
        private bool _lastClk;

        internal new OutputState Compute(InputState input)
        {
            var j = input[0];
            var k = input[1];
            var clk = input[2];
            if (clk && !_lastClk) // rising edge
                if (k && j)
                    _q = !_q;
                else if (k)
                    _q = false;
                else if (j)
                    _q = true;
            _lastClk = clk;
            return new OutputState(_q, !_q);
        }
    }

    internal class DFlipFlopGate : GateDefinition
    {
        public override string[] Inputs { get; } = new string[] { "D", "CLK" };
        public override string[] Outputs { get; } = new string[] { "Q", "Q̅" };
        internal override Dictionary<InputState, OutputState> TruthTable => EmptyTruthTable;
        public new string Name => "D Flip-Flop";
        public new bool HasState => true;

        private bool _q;
        private bool _lastClk;

        internal new OutputState Compute(InputState input)
        {
            var d = input[0];
            var clk = input[1];
            if (clk && !_lastClk) // rising edge
                _q = d;
            _lastClk = clk;
            return new OutputState(_q, !_q);
        }
    }

    internal class TFlipFlopGate : GateDefinition
    {
        public override string[] Inputs { get; } = new string[] { "T", "CLK" };
        public override string[] Outputs { get; } = new string[] { "Q", "Q̅" };
        internal override Dictionary<InputState, OutputState> TruthTable => EmptyTruthTable;
        public new string Name => "T Flip-Flop";
        public new bool HasState => true;

        private bool _q;
        private bool _lastClk;

        internal new OutputState Compute(InputState input)
        {
            var t = input[0];
            var clk = input[1];
            if (clk && !_lastClk) // rising edge
                if (t)
                    _q = !_q;
            _lastClk = clk;
            return new OutputState(_q, !_q);
        }
    }

    #endregion
}
