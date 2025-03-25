namespace ActivityPlannerAPI.Models
{
    public class DailyScore
    {
        public required string Date { get; set; }
        public int Score { get; set; }
        public required string Conditions { get; set; }
    }
}
