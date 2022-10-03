using FluentAssertions;
using MockingUnitTestsDemoApp.Impl.Models;
using MockingUnitTestsDemoApp.Impl.Repositories.Interfaces;
using MockingUnitTestsDemoApp.Impl.Services;
using MockingUnitTestsDemoApp.Impl.Services.Interfaces;
using NSubstitute;

namespace Test_XUnitMockTestDemo.Services
{
    public class PlayerService_Tests
    {
        private readonly IPlayerRepository _mockPlayerRepository;
        private readonly ITeamRepository _mockTeamRepository;
        private readonly ILeagueRepository _mockLeagueRepository;
        private readonly IPlayerService _subj;
        private List<Team> testTeamList = new List<Team>{
                new Team { ID = 2, Name = "T1", LeagueID=1, FoundingDate = DateTime.Parse("1990-12-31")},
            };
        private List<Player> testPlayerList = new List<Player>
            {
                new Player { ID = 1, FirstName = "P1", LastName = "P4",TeamID=2, DateOfBirth = DateTime.Parse("2000-12-4")},
                new Player { ID = 2, FirstName = "P2", LastName = "P5",TeamID =2,DateOfBirth  = DateTime.Parse("1999-1-31")},
                new Player { ID = 3, FirstName = "P3", LastName = "P6",TeamID=2, DateOfBirth = DateTime.Parse("2002-11-1")},
            };

        public PlayerService_Tests()
        {
            _mockPlayerRepository = Substitute.For<IPlayerRepository>();
            _mockTeamRepository = Substitute.For<ITeamRepository>();
            _mockLeagueRepository = Substitute.For<ILeagueRepository>();
            _subj = new PlayerService(_mockPlayerRepository, _mockTeamRepository, _mockLeagueRepository);
        }

        [Theory]
        [InlineData(1, false)]
        [InlineData(3, true)]
        public void GetForLeague_HappyDay_ReturnPlayersList(int count, bool expectedResult)
        {
            var league = 1;
            _mockLeagueRepository.IsValid(Arg.Any<int>()).Returns(true);
            _mockTeamRepository.GetForLeague(Arg.Any<int>()).Returns(GetTeamFake());
            _mockPlayerRepository.GetForTeam(Arg.Any<int>()).Returns(GetPlayerFake());

            var result = _subj.GetForLeague(league).Count.Equals(count);

            result.Should().Be(expectedResult);
        }


        private List<Team> GetTeamFake()
        {
            return new List<Team>
            {
                new Team { ID = 2, Name = "T1", LeagueID=1, FoundingDate = DateTime.Parse("1990-12-31")},
            };
        }
        private List<Player> GetPlayerFake()
        {
            return new List<Player>
            {
                new Player { ID = 1, FirstName = "P1", LastName = "P4",TeamID=2, DateOfBirth = DateTime.Parse("2000-12-4")},
                new Player { ID = 2, FirstName = "P2", LastName = "P5",TeamID =2,DateOfBirth  = DateTime.Parse("1999-1-31")},
                new Player { ID = 3, FirstName = "P3", LastName = "P6",TeamID=2, DateOfBirth = DateTime.Parse("2002-11-1")},
            };
        }
    }
}