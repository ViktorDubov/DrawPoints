namespace Scripts.Core.EventBus
{
    public class GeneratePointsMessage : WeakMessage
    {
        public int XCount { get; }
        public int YCount { get; }
        public int ZCount { get; }
        public GeneratePointsMessage(int xCount, int yCount, int zCount)
        {
            XCount = xCount;
            YCount = yCount;
            ZCount = zCount;
        }
    }
    public class ClearPointsMessage : WeakMessage
    {
        public ClearPointsMessage() { }
    }
    public class TipMessage : WeakMessage
    {
        public string Tip { get; }
        public float ShowDelaySec { get; }
        public TipMessage(string tip, float showDelaySec)
        {
            Tip = tip;
            ShowDelaySec = showDelaySec;
        }
    }
}