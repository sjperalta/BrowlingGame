namespace BrowlingGame.BLL.ScoreCalculator.Dto
{
    public class FrameInputDto
    {
        public int Attemp1 { get; set; }
        public int Attemp2 { get; set; }
        public int Attemp3 { get; set; }
        public int First { get; set; }
        public int Second { get; set; }
        public IStrategyBase? Strategy { get; set; }
    }
}
