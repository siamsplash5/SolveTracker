namespace SolveTracker.ViewModels.Dashboard
{
    public class ProgrammerViewModel
    {
        public string DisplayName { get; set; }
        public int DailySolveCount { get; set; }
        public int TotalSolveCount { get; set; }
        public int WeeklySolveCount { get; set; }
        public List<DailySolveCountSummary> DailySolveCountSummary { get; set; }
        public List<TotalSolveCountSummary> TotalSolveCountSummary { get; set; }

        public ProgrammerViewModel()
        {
            DailySolveCountSummary = [];
            TotalSolveCountSummary = [];
        }
    }
}
