using System;
using System.Collections.Generic;

namespace UntitledLogicGame.Workspace.Gates
{
	#region 000 - Technical

	internal class NoneGate : StatelessGateDefinition
	{
		public override string[] Inputs { get; } = new string[] { };
		public override string[] Outputs { get; } = new string[] { };

		internal override Func<InputState, OutputState> Function => (input) => new OutputState(0);
	}

	#endregion

	#region 100 - Special

	internal class INGate : NoneGate { }

	internal class OUTGate : NoneGate { }

	#endregion

	#region 200 - Basic

	internal class BUFGate : StatelessGateDefinition
	{
		public override string[] Inputs { get; } = new string[] { "A" };
		public override string[] Outputs { get; } = new string[] { "Q" };

		internal override Func<InputState, OutputState> Function => (input) => new OutputState(input[0]);
	}

	internal class ANDGate : StatelessGateDefinition
	{
		public override string[] Inputs { get; } = new string[] { "A", "B" };
		public override string[] Outputs { get; } = new string[] { "Q" };

		internal override Func<InputState, OutputState> Function => (input) => new OutputState(input[0] && input[1]);
	}

	internal class ORGate : StatelessGateDefinition
	{
		public override string[] Inputs { get; } = new string[] { "A", "B" };
		public override string[] Outputs { get; } = new string[] { "Q" };

		internal override Func<InputState, OutputState> Function => (input) => new OutputState(input[0] || input[1]);
	}

	internal class XORGate : StatelessGateDefinition
	{
		public override string[] Inputs { get; } = new string[] { "A", "B" };
		public override string[] Outputs { get; } = new string[] { "Q" };

		internal override Func<InputState, OutputState> Function => (input) => new OutputState(input[0] ^ input[1]);
	}

	internal class NOTGate : StatelessGateDefinition
	{
		public override string[] Inputs { get; } = new string[] { "A" };
		public override string[] Outputs { get; } = new string[] { "Q" };

		internal override Func<InputState, OutputState> Function => (input) => new OutputState(!input[0]);
	}

	internal class NANDGate : StatelessGateDefinition
	{
		public override string[] Inputs { get; } = new string[] { "A", "B" };
		public override string[] Outputs { get; } = new string[] { "Q" };

		internal override Func<InputState, OutputState> Function => (input) => new OutputState(!(input[0] && input[1]));
	}

	internal class NORGate : StatelessGateDefinition
	{
		public override string[] Inputs { get; } = new string[] { "A", "B" };
		public override string[] Outputs { get; } = new string[] { "Q" };

		internal override Func<InputState, OutputState> Function => (input) => new OutputState(!(input[0] || input[1]));
	}

	internal class XNORGate : StatelessGateDefinition
	{
		public override string[] Inputs { get; } = new string[] { "A", "B" };
		public override string[] Outputs { get; } = new string[] { "Q" };

		internal override Func<InputState, OutputState> Function => (input) => new OutputState(!(input[0] ^ input[1]));
	}

	#endregion

	#region 300 - Latches

	internal class SRLatchGate : StatefulGateDefinition
	{
		public new string Name => "SR Latch";
		public override string[] Inputs { get; } = new string[] { "S", "R" };
		public override string[] Outputs { get; } = new string[] { "Q", "Q̅" };

		private bool _q;

		internal override Func<InputState, OutputState> Function => (input) => 
		{
			var s = input[0];
			var r = input[1];
			if (r) // reset
				_q = false;
			else if (s) // set
				_q = true;
			return new OutputState(_q, !_q);
		};
	}

	internal class JKLatchGate : StatefulGateDefinition
	{
		public new string Name => "JK Latch";
		public override string[] Inputs { get; } = new string[] { "J", "K" };
		public override string[] Outputs { get; } = new string[] { "Q", "Q̅" };

		private bool _q;

		internal override Func<InputState, OutputState> Function => (input) => 
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
		};
	}

	internal class DLatchGate : StatefulGateDefinition
	{
		public new string Name => "D Latch";
		public override string[] Inputs { get; } = new string[] { "D", "E" };
		public override string[] Outputs { get; } = new string[] { "Q", "Q̅" };

		private bool _q;

		internal override Func<InputState, OutputState> Function => (input) => 
		{
			var d = input[0];
			var e = input[1];
			if (e) // set
				_q = d;
			return new OutputState(_q, !_q);
		};
	}

	#endregion

	#region 400 - Flip-Flops

	internal class SRFlipFlopGate : StatefulGateDefinition
	{
		public new string Name => "SR Flip-Flop";
		public override string[] Inputs { get; } = new string[] { "CLK", "S", "R" };
		public override string[] Outputs { get; } = new string[] { "Q", "Q̅" };
		
		private bool _q;
		private bool _lastClk;

		internal override Func<InputState, OutputState> Function => (input) =>
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
		};
	}

	internal class JKFlipFlopGate : StatefulGateDefinition
	{
		public new string Name => "JK Flip-Flop";
		public override string[] Inputs { get; } = new string[] { "CLK", "J", "K" };
		public override string[] Outputs { get; } = new string[] { "Q", "Q̅" };

		private bool _q;
		private bool _lastClk;

		internal override Func<InputState, OutputState> Function => (input) =>
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
		};
	}

	internal class DFlipFlopGate : StatefulGateDefinition
	{
		public new string Name => "D Flip-Flop";
		public override string[] Inputs { get; } = new string[] { "CLK", "D" };
		public override string[] Outputs { get; } = new string[] { "Q", "Q̅" };

		private bool _q;
		private bool _lastClk;

		internal override Func<InputState, OutputState> Function => (input) =>
		{
			var clk = input[0];
			var d = input[1];
			if (clk && !_lastClk) // rising edge
				_q = d;
			_lastClk = clk;
			return new OutputState(_q, !_q);
		};
	}

	internal class TFlipFlopGate : StatefulGateDefinition
	{
		public new string Name => "T Flip-Flop";
		public override string[] Inputs { get; } = new string[] { "CLK", "T" };
		public override string[] Outputs { get; } = new string[] { "Q", "Q̅" };

		private bool _q;
		private bool _lastClk;

		internal override Func<InputState, OutputState> Function => (input) =>
		{
			var clk = input[0];
			var t = input[1];
			if (clk && !_lastClk) // rising edge
				if (t) // flip
					_q = !_q;
			_lastClk = clk;
			return new OutputState(_q, !_q);
		};
	}

	#endregion

	#region 500 - Arithmetic

	internal class HalfAddGate : StatelessGateDefinition
	{
		public new string Name => "Half Add.";
		public override string[] Inputs { get; } = new string[] { "A", "B" };
		public override string[] Outputs { get; } = new string[] { "S", "C" };

		internal override Func<InputState, OutputState> Function => (input) => 
		{
			var a = input[0];
			var b = input[1];

			var s = a || b;
			var c = a && b;

			return new OutputState(s, c);
		};
	}

	internal class FullAddGate : StatelessGateDefinition
	{
		public new string Name => "Full Add.";
		public override string[] Inputs { get; } = new string[] { "A", "B", "Cɪ" };
		public override string[] Outputs { get; } = new string[] { "S", "Cᴏ" };
		
		internal override Func<InputState, OutputState> Function => (input) =>
		{
			var a = input[0];
			var b = input[1];
			var ci = input[2];

			var s = a || b || ci;
			var co = a && b || a && ci || b && ci;

			return new OutputState(s, co);
		};
	}

	internal class HalfSubGate : StatelessGateDefinition
	{
		public new string Name => "Half Sub.";
		public override string[] Inputs { get; } = new string[] { "A", "B" };
		public override string[] Outputs { get; } = new string[] { "S", "C" };

		internal override Func<InputState, OutputState> Function => (input) =>
		{
			var a = input[0];
			var b = input[1];

			var s = a || b;
			var c = !a && b;

			return new OutputState(s, c);
		};

	}

	internal class FullSubGate : StatelessGateDefinition
	{
		public new string Name => "Full Add.";
		public override string[] Inputs { get; } = new string[] { "A", "B", "Cɪ" };
		public override string[] Outputs { get; } = new string[] { "S", "Cᴏ" };

		internal override Func<InputState, OutputState> Function => (input) =>
		{
			var a = input[0];
			var b = input[1];
			var ci = input[2];

			var s = a || b || ci;
			var co = a && !b || a && !ci || b && ci;

			return new OutputState(s, co);
		};

	}

	#endregion

	#region 600 - Data

	internal class MuxGate : StatelessGateDefinition
	{
		public override string[] Inputs { get; } = new string[] { "E", "S", "D₀", "D₁" };
		public override string[] Outputs { get; } = new string[] { "Y" };

		internal override Func<InputState, OutputState> Function => (input) =>
		{
			var e = input[0];
			var s = input[1];
			var d0 = input[2];
			var d1 = input[3];

			var y = e && (!s && d0 || s && d1);

			return new OutputState(y);
		};
	}

	internal class DemuxGate : StatelessGateDefinition
	{
		public override string[] Inputs { get; } = new string[] { "E", "S", "D" };
		public override string[] Outputs { get; } = new string[] { "Y₀", "Y₁" };

		internal override Func<InputState, OutputState> Function => (input) =>
		{
			var e = input[0];
			var s = input[1];
			var d = input[2];

			var y0 = e && !s && d;
			var y1 = e && s && d;

			return new OutputState(y0, y1);
		};
	}

	internal class Mux2bGate : StatelessGateDefinition
	{
		public new string Name => "2bits Mux";
		public override string[] Inputs { get; } = new string[] { "E", "S₀", "S₁", "D₀", "D₁", "D₂", "D₃" };
		public override string[] Outputs { get; } = new string[] { "Y" };

		internal override Func<InputState, OutputState> Function => (input) =>
		{
			var e = input[0];
			var s0 = input[1];
			var s1 = input[2];
			var d0 = input[3];
			var d1 = input[4];
			var d2 = input[5];
			var d3 = input[6];

			var y = e && (
				!s0 && !s1 && d0 ||
				s0 && !s1 && d1 ||
				!s0 && s1 && d2 ||
				s0 && s1 && d3
			);

			return new OutputState(y);
		};
	}

	internal class Demux2bGate : StatelessGateDefinition
	{
		public new string Name => "2bits Demux";
		public override string[] Inputs { get; } = new string[] { "E", "S₀", "S₁", "D" };
		public override string[] Outputs { get; } = new string[] { "Y₀", "Y₁", "Y₂", "Y₃" };

		internal override Func<InputState, OutputState> Function => (input) =>
		{
			var e = input[0];
			var s0 = input[1];
			var s1 = input[2];
			var d = input[3];

			var y0 = e && !s0 && !s1 && d;
			var y1 = e && s0 && !s1 && d;
			var y2 = e && !s0 && s1 && d;
			var y3 = e && s0 && s1 && d;

			return new OutputState(y0, y1, y2, y3);
		};
	}

	internal class Enc2b4bGate : StatelessGateDefinition
	{
		public new string Name => "2b/4b Enc.";
		public override string[] Inputs { get; } = new string[] { "D₀", "D₁" };
		public override string[] Outputs { get; } = new string[] { "Y₀", "Y₁", "Y₂", "Y₃" };

		internal override Func<InputState, OutputState> Function => (input) =>
		{
			var d0 = input[0];
			var d1 = input[1];

			var y0 = !d0 && !d1;
			var y1 = d0 && !d1;
			var y2 = !d0 && d1;
			var y3 = d0 && d1;

			return new OutputState(y0, y1, y2, y3);
		};
	}

	internal class Dec4b2bGate : StatelessGateDefinition
	{
		public new string Name => "4b/2b Dec.";
		public override string[] Inputs { get; } = new string[] { "D₀", "D₁", "D₂", "D₃"};
		public override string[] Outputs { get; } = new string[] { "Y₀", "Y₁" };
		
		internal override Func<InputState, OutputState> Function => (input) =>
		{
			var d0 = input[0];
			var d1 = input[1];
			var d2 = input[2];
			var d3 = input[3];

			var y0 = d1 && !d2 || d3;
			var y1 = d2 || d3;

			return new OutputState(y0, y1);
		};
	}

	#endregion

	#region 700 - Registers

	internal class SISO4bGate : StatefulGateDefinition
	{
		public new string Name => "4bits SISO";
		public override string[] Inputs { get; } = new string[] { "CLK", "D" };
		public override string[] Outputs { get; } = new string[] { "Q" };

		private bool _q0;
		private bool _q1;
		private bool _q2;
		private bool _q3;
		private bool _lastClk;

		internal override Func<InputState, OutputState> Function => (input) =>
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
		};
	}

	internal class SIPO4bGate : StatefulGateDefinition
	{
		public new string Name => "4bits SIPO";
		public override string[] Inputs { get; } = new string[] { "CLK", "D" };
		public override string[] Outputs { get; } = new string[] { "Q₀", "Q₁", "Q₂", "Q₃" };

		private bool _q0;
		private bool _q1;
		private bool _q2;
		private bool _q3;
		private bool _lastClk;

		internal override Func<InputState, OutputState> Function => (input) =>
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
		};
	}

	internal class PIPO4bGate : StatefulGateDefinition
	{
		public new string Name => "4bits PIPO";
		public override string[] Inputs { get; } = new string[] { "CLK", "D₀", "D₁", "D₂", "D₃"	};
		public override string[] Outputs { get; } = new string[] { "Q₀", "Q₁", "Q₂", "Q₃" };

		private bool _q0;
		private bool _q1;
		private bool _q2;
		private bool _q3;
		private bool _lastClk;

		internal override Func<InputState, OutputState> Function => (input) =>
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
		};
	}

	#endregion

	#region 800 - Counters

	internal class Counter2bGate : StatefulGateDefinition
	{
		public new string Name => "2bits Count.";
		public override string[] Inputs { get; } = new string[] { "CLK", "RST" };
		public override string[] Outputs { get; } = new string[] { "Q₀", "Q₁" };

		private bool _q0;
		private bool _q1;
		private bool _lastClk;

		internal override Func<InputState, OutputState> Function => (input) =>
		{
			var clk = input[0];
			var rst = input[1];
			if (clk && !_lastClk) // rising edge
			{
				if (rst)
				{
					_q0 = false;
					_q1 = false;
				}
				else
				{
					_q1 = _q0 ^ _q1;
					_q0 = !_q0;
				}
			}
			_lastClk = clk;
			return new OutputState(_q0, _q1);
		};
	}

	internal class Counter4bGate : StatefulGateDefinition
	{
		public new string Name => "4bits Count.";
		public override string[] Inputs { get; } = new string[] { "CLK", "RST" };
		public override string[] Outputs { get; } = new string[] { "Q₀", "Q₁", "Q₂", "Q₃" };

		private bool _q0;
		private bool _q1;
		private bool _q2;
		private bool _q3;
		private bool _lastClk;

		internal override Func<InputState, OutputState> Function => (input) =>
		{
			var clk = input[0];
			var rst = input[1];
			if (clk && !_lastClk) // rising edge
			{
				if (rst)
				{
					_q0 = false;
					_q1 = false;
					_q2 = false;
					_q3 = false;
				}
				else
				{
					_q3 = (_q3 || _q2 && _q1 && _q0) && (!_q3 && !_q2 && !_q1 && !_q0);
					_q2 = (_q2 || _q1 && _q0) && (!_q2 && !_q1 && !_q0);
					_q1 = _q0 ^ _q1;
					_q0 = !_q0;
				}
			}
			_lastClk = clk;
			return new OutputState(_q0, _q1, _q2, _q3);
		};
	}

	#endregion
}
