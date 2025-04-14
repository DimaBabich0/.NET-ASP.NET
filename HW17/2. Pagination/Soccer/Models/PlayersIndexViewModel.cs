namespace Soccer.Models
{
    public class PlayersIndexViewModel
    {
        public IEnumerable<Players> Players { get; }
        public PlayersPageViewModel PageViewModel { get; }

        public PlayersIndexViewModel(IEnumerable<Players> players, PlayersPageViewModel viewModel)
        {
            Players = players;
            PageViewModel = viewModel;
        }
    }
}
