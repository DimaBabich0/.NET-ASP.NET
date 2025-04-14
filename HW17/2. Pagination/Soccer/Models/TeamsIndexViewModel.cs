namespace Soccer.Models
{
    public class TeamsIndexViewModel
    {
        public IEnumerable<Teams> Teams { get; }
        public TeamsPageViewModel PageViewModel { get; }

        public TeamsIndexViewModel(IEnumerable<Teams> teams, TeamsPageViewModel viewModel)
        {
            Teams = teams;
            PageViewModel = viewModel;
        }
    }
}
