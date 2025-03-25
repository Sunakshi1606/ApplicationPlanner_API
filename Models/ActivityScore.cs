namespace ActivityPlannerAPI.Models
{
    public class ActivityScore
    {
        public required string Activity { get; set; }
        public int Score { get; set; }
        public string? Reason { get; set; }
        public required string Image { get; set; }
        public required List<DailyScore> DailyScores { get; set; }
    }
}
