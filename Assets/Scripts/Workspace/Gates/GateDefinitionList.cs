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
			{ new InputState(	false	), new OutputState(	false	) },
			{ new InputState(	true	), new OutputState(	true	) },
		};
	}

	internal class ANDGate : GateDefinition
	{
		public override string[] Inputs { get; } = new string[] { "A", "B" };
		public override string[] Outputs { get; } = new string[] { "Q" };
		internal override Dictionary<InputState, OutputState> TruthTable { get; } = new Dictionary<InputState, OutputState>
		{
			{ new InputState(	false,	false	), new OutputState(	false	) },
			{ new InputState(	false,	true	), new OutputState(	false	) },
			{ new InputState(	true,	false	), new OutputState(	false	) },
			{ new InputState(	true,	true	), new OutputState(	true	) },
		};
	}

	internal class ORGate : GateDefinition
	{
		public override string[] Inputs { get; } = new string[] { "A", "B" };
		public override string[] Outputs { get; } = new string[] { "Q" };
		internal override Dictionary<InputState, OutputState> TruthTable { get; } = new Dictionary<InputState, OutputState>
		{
			{ new InputState(	false,	false	), new OutputState(	false	) },
			{ new InputState(	false,	true	), new OutputState(	true	) },
			{ new InputState(	true,	false	), new OutputState(	true	) },
			{ new InputState(	true,	true	), new OutputState(	true	) },
		};
	}

	internal class XORGate : GateDefinition
	{
		public override string[] Inputs { get; } = new string[] { "A", "B" };
		public override string[] Outputs { get; } = new string[] { "Q" };
		internal override Dictionary<InputState, OutputState> TruthTable { get; } = new Dictionary<InputState, OutputState>
		{
			{ new InputState(	false,	false	), new OutputState(	false	) },
			{ new InputState(	false,	true	), new OutputState(	true	) },
			{ new InputState(	true,	false	), new OutputState(	true	) },
			{ new InputState(	true,	true	), new OutputState(	false	) },
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
			{ new InputState(	false	), new OutputState(	true	) },
			{ new InputState(	true	), new OutputState(	false	) },
		};
	}

	internal class NANDGate : GateDefinition
	{
		public override string[] Inputs { get; } = new string[] { "A", "B" };
		public override string[] Outputs { get; } = new string[] { "Q" };
		internal override Dictionary<InputState, OutputState> TruthTable { get; } = new Dictionary<InputState, OutputState>
		{
			{ new InputState(	false,	false	), new OutputState(	true	) },
			{ new InputState(	false,	true	), new OutputState(	true	) },
			{ new InputState(	true,	false	), new OutputState(	true	) },
			{ new InputState(	true,	true	), new OutputState(	false	) },
		};
	}

	internal class NORGate : GateDefinition
	{
		public override string[] Inputs { get; } = new string[] { "A", "B" };
		public override string[] Outputs { get; } = new string[] { "Q" };
		internal override Dictionary<InputState, OutputState> TruthTable { get; } = new Dictionary<InputState, OutputState>
		{
			{ new InputState(	false,	false	), new OutputState(	true	) },
			{ new InputState(	false,	true	), new OutputState(	false	) },
			{ new InputState(	true,	false	), new OutputState(	false	) },
			{ new InputState(	true,	true	), new OutputState(	false	) },
		};
	}

	internal class XNORGate : GateDefinition
	{
		public override string[] Inputs { get; } = new string[] { "A", "B" };
		public override string[] Outputs { get; } = new string[] { "Q" };
		internal override Dictionary<InputState, OutputState> TruthTable { get; } = new Dictionary<InputState, OutputState>
		{
			{ new InputState(	false,	false	), new OutputState(	true	) },
			{ new InputState(	false,	true	), new OutputState(	false	) },
			{ new InputState(	true,	false	), new OutputState(	false	) },
			{ new InputState(	true,	true	), new OutputState(	true	) },
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
			if (r) // reset
				_q = false;
			else if (s) // set
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
			if (k && j) // flip
				_q = !_q;
			else if (k) // reset
				_q = false;
			else if (j) // set
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
			if (e) // set
				_q = d;
			return new OutputState(_q, !_q);
		}
	}

	#endregion

	#region 500 - Flip-Flops

	internal class SRFlipFlopGate : GateDefinition
	{
		public override string[] Inputs { get; } = new string[] { "CLK", "S", "R" };
		public override string[] Outputs { get; } = new string[] { "Q", "Q̅" };
		internal override Dictionary<InputState, OutputState> TruthTable => EmptyTruthTable;
		public new string Name => "SR Flip-Flop";
		public new bool HasState => true;

		private bool _q;
		private bool _lastClk;

		internal new OutputState Compute(InputState input)
		{
			var clk = input[0];
			var s = input[1];
			var r = input[2];
			if (clk && !_lastClk) // rising edge
				if (r) // reset
					_q = false;
				else if (s) // set
					_q = true;
			_lastClk = clk;
			return new OutputState(_q, !_q);
		}
	}

	internal class JKFlipFlopGate : GateDefinition
	{
		public override string[] Inputs { get; } = new string[] { "CLK", "J", "K" };
		public override string[] Outputs { get; } = new string[] { "Q", "Q̅" };
		internal override Dictionary<InputState, OutputState> TruthTable => EmptyTruthTable;
		public new string Name => "JK Flip-Flop";
		public new bool HasState => true;

		private bool _q;
		private bool _lastClk;

		internal new OutputState Compute(InputState input)
		{
			var clk = input[0];
			var j = input[1];
			var k = input[2];
			if (clk && !_lastClk) // rising edge
				if (k && j) // flip
					_q = !_q;
				else if (k) // reset
					_q = false;
				else if (j) // set
					_q = true;
			_lastClk = clk;
			return new OutputState(_q, !_q);
		}
	}

	internal class DFlipFlopGate : GateDefinition
	{
		public override string[] Inputs { get; } = new string[] { "CLK", "D" };
		public override string[] Outputs { get; } = new string[] { "Q", "Q̅" };
		internal override Dictionary<InputState, OutputState> TruthTable => EmptyTruthTable;
		public new string Name => "D Flip-Flop";
		public new bool HasState => true;

		private bool _q;
		private bool _lastClk;

		internal new OutputState Compute(InputState input)
		{
			var clk = input[0];
			var d = input[1];
			if (clk && !_lastClk) // rising edge
				_q = d;
			_lastClk = clk;
			return new OutputState(_q, !_q);
		}
	}

	internal class TFlipFlopGate : GateDefinition
	{
		public override string[] Inputs { get; } = new string[] { "CLK", "T" };
		public override string[] Outputs { get; } = new string[] { "Q", "Q̅" };
		internal override Dictionary<InputState, OutputState> TruthTable => EmptyTruthTable;
		public new string Name => "T Flip-Flop";
		public new bool HasState => true;

		private bool _q;
		private bool _lastClk;

		internal new OutputState Compute(InputState input)
		{
			var clk = input[0];
			var t = input[1];
			if (clk && !_lastClk) // rising edge
				if (t) // flip
					_q = !_q;
			_lastClk = clk;
			return new OutputState(_q, !_q);
		}
	}

	#endregion

	#region 600 - Combinational

	internal class HalfAddGate : GateDefinition
	{
		public override string[] Inputs { get; } = new string[] { "A", "B" };
		public override string[] Outputs { get; } = new string[] { "S", "C" };
		internal override Dictionary<InputState, OutputState> TruthTable { get; } = new Dictionary<InputState, OutputState>
		{
			{ new InputState(	false,	false	), new OutputState(	false,	false	) },
			{ new InputState(	false,	true	), new OutputState(	true,	false	) },
			{ new InputState(	true,	false	), new OutputState(	true,	false	) },
			{ new InputState(	true,	true	), new OutputState(	false,	true	) },
		};
		public new string Name => "Half Add.";
	}

	internal class FullAddGate : GateDefinition
	{
		public override string[] Inputs { get; } = new string[] { "A", "B", "Cɪ" };
		public override string[] Outputs { get; } = new string[] { "S", "Cᴏ" };
		internal override Dictionary<InputState, OutputState> TruthTable { get; } = new Dictionary<InputState, OutputState>
		{
			{ new InputState(	false,	false,	false	), new OutputState(	false,	false	) },
			{ new InputState(	false,	false,	true	), new OutputState(	true,	false	) },
			{ new InputState(	false,	true,	false	), new OutputState(	true,	false	) },
			{ new InputState(	false,	true,	true	), new OutputState(	false,	true	) },
			{ new InputState(	true,	false,	false	), new OutputState(	true,	false	) },
			{ new InputState(	true,	false,	true	), new OutputState(	false,	true	) },
			{ new InputState(	true,	true,	false	), new OutputState(	false,	true	) },
			{ new InputState(	true,	true,	true	), new OutputState(	true,	true	) },
		};
		public new string Name => "Full Add.";
	}

	internal class HalfSubGate : GateDefinition
	{
		public override string[] Inputs { get; } = new string[] { "A", "B" };
		public override string[] Outputs { get; } = new string[] { "S", "C" };
		internal override Dictionary<InputState, OutputState> TruthTable { get; } = new Dictionary<InputState, OutputState>
		{
			{ new InputState(	false,	false	), new OutputState(	false,	false	) },
			{ new InputState(	false,	true	), new OutputState(	true,	true	) },
			{ new InputState(	true,	false	), new OutputState(	true,	false	) },
			{ new InputState(	true,	true	), new OutputState(	false,	false	) },
		};
		public new string Name => "Half Sub.";
	}

	internal class FullSubGate : GateDefinition
	{
		public override string[] Inputs { get; } = new string[] { "A", "B", "Cɪ" };
		public override string[] Outputs { get; } = new string[] { "S", "Cᴏ" };
		internal override Dictionary<InputState, OutputState> TruthTable { get; } = new Dictionary<InputState, OutputState>
		{
			{ new InputState(	false,	false,	false	), new OutputState(	false,	false	) },
			{ new InputState(	false,	false,	true	), new OutputState(	true,	true	) },
			{ new InputState(	false,	true,	false	), new OutputState(	true,	true	) },
			{ new InputState(	false,	true,	true	), new OutputState(	false,	true	) },
			{ new InputState(	true,	false,	false	), new OutputState(	true,	false	) },
			{ new InputState(	true,	false,	true	), new OutputState(	false,	false	) },
			{ new InputState(	true,	true,	false	), new OutputState(	false,	false	) },
			{ new InputState(	true,	true,	true	), new OutputState(	true,	true	) },
		};
		public new string Name => "Full Add.";
	}

	internal class MuxGate : GateDefinition
	{
		public override string[] Inputs { get; } = new string[] { "E", "S", "D₀", "D₁" };
		public override string[] Outputs { get; } = new string[] { "Y" };
		internal override Dictionary<InputState, OutputState> TruthTable { get; } = new Dictionary<InputState, OutputState>
		{
			{ new InputState(	false,	false,	false,	false	), new OutputState(	false	) }, // => E=0
			{ new InputState(	false,	false,	false,	true	), new OutputState(	false	) }, // => E=0
			{ new InputState(	false,	false,	true,	false	), new OutputState(	false	) }, // => E=0
			{ new InputState(	false,	false,	true,	true	), new OutputState(	false	) }, // => E=0
			{ new InputState(	false,	true,	false,	false	), new OutputState(	false	) }, // => E=0
			{ new InputState(	false,	true,	false,	true	), new OutputState(	false	) }, // => E=0
			{ new InputState(	false,	true,	true,	false	), new OutputState(	false	) }, // => E=0
			{ new InputState(	false,	true,	true,	true	), new OutputState( false	) }, // => E=0
			{ new InputState(	true,	false,	false,	false	), new OutputState(	false	) }, // => S=0 => Y=D₀=0
			{ new InputState(	true,	false,	false,	true	), new OutputState(	false	) }, // => S=0 => Y=D₀=0
			{ new InputState(	true,	false,	true,	false	), new OutputState( true	) }, // => S=0 => Y=D₀=1
			{ new InputState(	true,	false,	true,	true	), new OutputState(	true	) }, // => S=0 => Y=D₀=1
			{ new InputState(	true,	true,	false,	false	), new OutputState(	false	) }, // => S=1 => Y=D₁=0
			{ new InputState(	true,	true,	false,	true	), new OutputState(	true	) }, // => S=1 => Y=D₁=1
			{ new InputState(	true,	true,	true,	false	), new OutputState( false	) }, // => S=1 => Y=D₁=0
			{ new InputState(	true,	true,	true,	true	), new OutputState( true	) }, // => S=1 => Y=D₁=1
		};
	}

	internal class DemuxGate : GateDefinition
	{
		public override string[] Inputs { get; } = new string[] { "E", "S", "D" };
		public override string[] Outputs { get; } = new string[] { "Y₀", "Y₁" };
		internal override Dictionary<InputState, OutputState> TruthTable { get; } = new Dictionary<InputState, OutputState>
		{
			{ new InputState(	false,	false,	false	), new OutputState(	false,	false	) }, // => E=0
			{ new InputState(	false,	false,	true	), new OutputState(	false,	false	) }, // => E=0
			{ new InputState(	false,	true,	false	), new OutputState(	false,	false	) }, // => E=0
			{ new InputState(	false,	true,	true	), new OutputState(	false,	false	) }, // => E=0
			{ new InputState(	true,	false,	false	), new OutputState(	false,	false	) }, // => S=0 => Y₀=D=0
			{ new InputState(	true,	false,	true	), new OutputState(	true,	false	) }, // => S=0 => Y₀=D=1
			{ new InputState(	true,	true,	false	), new OutputState( false,	false	) }, // => S=1 => Y₁=D=0
			{ new InputState(	true,	true,	true	), new OutputState(	false,	true	) }, // => S=1 => Y₁=D=1
		};
	}

	internal class EncGate : GateDefinition
	{
		public override string[] Inputs { get; } = new string[] { "D₀", "D₁" };
		public override string[] Outputs { get; } = new string[] { "Y₀", "Y₁", "Y₂", "Y₃" };
		internal override Dictionary<InputState, OutputState> TruthTable { get; } = new Dictionary<InputState, OutputState>
		{
			{ new InputState(	false,	false	), new OutputState( true,	false,	false,	false	) }, // D̅₀D̅₁
			{ new InputState(	false,	true	), new OutputState( false,	true,	false,	false	) }, // D₀D̅₁
			{ new InputState(	true,	false	), new OutputState( false,	false,	true,	false	) }, // D̅₀D₁
			{ new InputState(	true,	true	), new OutputState( false,	false,	false,	true	) }, // D₀D₁
		};
	}

	internal class DecGate : GateDefinition
	{
		public override string[] Inputs { get; } = new string[] { "D₃", "D₂", "D₁", "D₀" };
		public override string[] Outputs { get; } = new string[] { "Y₀", "Y₁" };
		internal override Dictionary<InputState, OutputState> TruthTable { get; } = new Dictionary<InputState, OutputState>
		{
			{ new InputState(	false,	false,	false,	false	), new OutputState( false,	false	) }, // XX
			{ new InputState(	false,	false,	false,	true	), new OutputState( false,	false	) }, // D̅₀D̅₁
			{ new InputState(	false,	false,	true,	false	), new OutputState( true,	true	) }, // D₀D̅₁
			{ new InputState(	false,	false,	true,	true	), new OutputState( true,	false	) }, // D₀D̅₁
			{ new InputState(	false,	true,	false,	false	), new OutputState( false,	true	) }, // D̅₀D₁
			{ new InputState(	false,	true,	false,	true	), new OutputState( false,	true	) }, // D̅₀D₁
			{ new InputState(	false,	true,	true,	false	), new OutputState( false,	true	) }, // D̅₀D₁
			{ new InputState(	false,	true,	true,	true	), new OutputState( false,	true	) }, // D̅₀D₁
			{ new InputState(	true,	false,	false,	false	), new OutputState( true,	true	) }, // D₀D₁
			{ new InputState(	true,	false,	false,	true	), new OutputState( true,	true	) }, // D₀D₁
			{ new InputState(	true,	false,	true,	false	), new OutputState( true,	true	) }, // D₀D₁
			{ new InputState(	true,	false,	true,	true	), new OutputState( true,	true	) }, // D₀D₁
			{ new InputState(	true,	true,	false,	false	), new OutputState( true,	true	) }, // D₀D₁
			{ new InputState(	true,	true,	false,	true	), new OutputState( true,	true	) }, // D₀D₁
			{ new InputState(	true,	true,	true,	false	), new OutputState( true,	true	) }, // D₀D₁
			{ new InputState(	true,	true,	true,	true	), new OutputState( true,	true	) }, // D₀D₁
		};
	}

	#endregion

	#region 700 - Registers

	internal class SISO4bGate : GateDefinition
	{
		public override string[] Inputs { get; } = new string[] { "CLK", "D" };
		public override string[] Outputs { get; } = new string[] { "Q" };
		internal override Dictionary<InputState, OutputState> TruthTable => EmptyTruthTable;
		public new string Name => "4 bits SISO";
		public new bool HasState => true;

		private bool _q0;
		private bool _q1;
		private bool _q2;
		private bool _q3;
		private bool _lastClk;

		internal new OutputState Compute(InputState input)
		{
			var clk = input[0];
			var d = input[1];
			if (clk && !_lastClk) // rising edge
			{
				_q3 = _q2;
				_q2 = _q1;
				_q1 = _q0;
				_q0 = d;
			}
			_lastClk = clk;
			return new OutputState(_q3);
		}
	}

	internal class SIPO4bGate : GateDefinition
	{
		public override string[] Inputs { get; } = new string[] { "CLK", "D" };
		public override string[] Outputs { get; } = new string[] { "Q₀", "Q₁", "Q₂", "Q₃" };
		internal override Dictionary<InputState, OutputState> TruthTable => EmptyTruthTable;
		public new string Name => "4 bits SIPO";
		public new bool HasState => true;

		private bool _q0;
		private bool _q1;
		private bool _q2;
		private bool _q3;
		private bool _lastClk;

		internal new OutputState Compute(InputState input)
		{
			var clk = input[0];
			var d = input[1];
			if (clk && !_lastClk) // rising edge
			{
				_q3 = _q2;
				_q2 = _q1;
				_q1 = _q0;
				_q0 = d;
			}
			_lastClk = clk;
			return new OutputState(_q0, _q1, _q2, _q3);
		}
	}

	internal class PIPO4bGate : GateDefinition
	{
		public override string[] Inputs { get; } = new string[] { "CLK", "D₀", "D₁", "D₂", "D₃"  };
		public override string[] Outputs { get; } = new string[] { "Q₀", "Q₁", "Q₂", "Q₃" };
		internal override Dictionary<InputState, OutputState> TruthTable => EmptyTruthTable;
		public new string Name => "4 bits PIPO";
		public new bool HasState => true;

		private bool _q0;
		private bool _q1;
		private bool _q2;
		private bool _q3;
		private bool _lastClk;

		internal new OutputState Compute(InputState input)
		{
			var clk = input[0];
			var d0 = input[1];
			var d1 = input[2];
			var d2 = input[3];
			var d3 = input[4];
			if (clk && !_lastClk) // rising edge
			{
				_q3 = d3;
				_q2 = d2;
				_q1 = d1;
				_q0 = d0;
			}
			_lastClk = clk;
			return new OutputState(_q0, _q1, _q2, _q3);
		}
	}

	#endregion
}
