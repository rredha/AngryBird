namespace Model.Bubble
{
    public class BubbleData
    {
        public bool Popped { get; set; }
        public BubbleData() => Popped = false;
        public BubbleData(bool popped) => Popped = popped;
        public void SetPopped() => Popped = true;
    }
}