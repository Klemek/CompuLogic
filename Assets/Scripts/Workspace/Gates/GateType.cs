namespace UntitledLogicGame.Workspace.Gates
{
    public enum GateType
    {
        // 000 - Technical
        NONE = 000,
        // 100 - Special
        IN = 100,
        OUT = 110,
        // 200 - Basic
        BUF = 200,
        AND = 210,
        OR = 220,
        XOR = 230,
        // 300 - Inverted Basic
        NOT = 300,
        NAND = 310,
        NOR = 320,
        XNOR = 330,
        // 400 - Latches
        SRLatch = 400,
        JKLatch = 410,
        DLatch = 420,
        // 500 - Flip-Flops
        SRFlipFlop = 500,
        JKFlipFlop = 510,
        DFlipFlop = 520,
        TFlipFlop = 530,
        // 600 - Combinational
        HalfAdd = 600,
        FullAdd = 610,
        HalSub = 620,
        FullSub = 630,
        Mux = 740,
        Demux = 750,
        Enc = 760,
        Dec = 770
    }
}
