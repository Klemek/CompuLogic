namespace CompuLogic.Workspace.Gates
{
	public enum GateType
	{
		// 000 - Technical
		None = 000,
		// 100 - I/O
		IN = 100,
		OUT = 110,
		CLK1 = 120,
		CLK2 = 130,
		CLK3 = 140,
		CLK4 = 150,
		// 200 - Basic
		BUF = 200,
		AND = 210,
		OR = 220,
		XOR = 230,
		NOT = 240,
		NAND = 250,
		NOR = 260,
		XNOR = 270,
		// 300 - Latches
		SRLatch = 300,
		JKLatch = 310,
		DLatch = 320,
		// 500 - Flip-Flops
		SRFlipFlop = 400,
		JKFlipFlop = 410,
		DFlipFlop = 420,
		TFlipFlop = 430,
		// 500 - Arithmetic
		HalfAdd = 500,
		FullAdd = 510,
		HalfSub = 520,
		FullSub = 530,
		// 600 - Data
		Mux = 600,
		Demux = 610,
		Mux2b = 620,
		Demux2b = 630,
		Enc2b4b = 640,
		Dec4b2b = 650,
		// 700 - Registers
		SISO4b = 700,
		SIPO4b = 710,
		PIPO4b = 720,
		// 800 - Counters
		Counter2b = 800,
		Counter4b = 810
	}

	public enum GateCategory
	{
		None = 00,
		IO = 01,
		Basic = 02,
		Latches = 03,
		FlipFlops = 04,
		Arithmetic = 05,
		Data = 06,
		Registers = 07,
		Counters = 08
	}
}
