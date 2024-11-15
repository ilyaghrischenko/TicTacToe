using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;
using TicTacToe.Contracts;
using TicTacToe.Contracts.DbModelsServices;
using TicTacToe.Contracts.Repositories;

namespace TicTacToe.Application.SignalRHub
{
    public class GameHub(
        IGameService gameService,
        IReportService reportService,
        IUserRepository userRepository) : Hub
    {
        private readonly IGameService _gameService = gameService;
        private readonly IReportService _reportService = reportService;
        private readonly IUserRepository _userRepository = userRepository;
        
        private static ConcurrentDictionary<int, bool> OnlineStatus = new();
        private static ConcurrentDictionary<string, int> ConnectedUsers = new();
        private static ConcurrentDictionary<string, GameSession> GameSessions = new();

        public override Task OnConnectedAsync()
        {
            var userId = Context.GetHttpContext().Request.Query["userId"];
            if (int.TryParse(userId, out var userIdValue))
            {
                ConnectedUsers[Context.ConnectionId] = userIdValue;
                OnlineStatus[userIdValue] = true;
                NotifyStatusChange(userIdValue, true);
            }
            else
            {
                throw new ArgumentException("UserId is not valid");
            }

            Console.WriteLine($"User: {userId}, ConnectionId: {Context.ConnectionId}");
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            if (ConnectedUsers.TryRemove(Context.ConnectionId, out var userId))
            {
                OnlineStatus[userId] = false;
            }
            
            await EndGameOnDisconnect(Context.ConnectionId);
            await NotifyStatusChange(userId, false);
            await base.OnDisconnectedAsync(exception);
        }

        public Task<Dictionary<int, bool>> GetFriendsOnlineStatus(List<int> friendIds)
        {
            var friendsStatus = friendIds.ToDictionary(
                friendId => friendId,
                friendId => OnlineStatus.ContainsKey(friendId) && OnlineStatus[friendId]
            );
            return Task.FromResult(friendsStatus);
        }
        
        public async Task NotifyStatusChange(int userId, bool isOnline)
        {
            await Clients.All.SendAsync("ReceiveStatusUpdate", userId, isOnline);
        }
        
        public async Task SendInvitation(int toUserId)
        {
            var sender = await _userRepository.GetAsync(ConnectedUsers[Context.ConnectionId]);
            var senderUserName = sender.Login;
            var toConnectionId = ConnectedUsers.FirstOrDefault(u => u.Value == toUserId).Key;
            if (toConnectionId != null)
            {
                await Clients.Client(toConnectionId).SendAsync("ReceiveInvitation", senderUserName, sender.Id);
            }
        }

        public async Task AcceptInvitation(int senderUserId)
        {
            var senderConnectionId = ConnectedUsers.FirstOrDefault(u => u.Value == senderUserId).Key;

            if (senderConnectionId != null)
            {
                var gameId = Guid.NewGuid().ToString();
                var gameSession = new GameSession
                {
                    GameId = gameId,
                    PlayerX = senderConnectionId,
                    PlayerO = Context.ConnectionId,
                    CurrentTurn = senderConnectionId
                };
                GameSessions[gameId] = gameSession;

                await Groups.AddToGroupAsync(senderConnectionId, gameId);
                await Groups.AddToGroupAsync(Context.ConnectionId, gameId);

                await Clients.Client(senderConnectionId).SendAsync("StartGame", gameId, "X");
                await Clients.Client(Context.ConnectionId).SendAsync("StartGame", gameId, "O");
            }
        }

        public async Task DeclineInvitation(string senderUserLogin)
        {
            var senderUserId = (await  _userRepository.GetAsync(senderUserLogin)).Id;
            var senderConnectionId = ConnectedUsers.FirstOrDefault(u => u.Value == senderUserId).Key;
            if (senderConnectionId != null)
            {
                await Clients.Client(senderConnectionId)
                    .SendAsync("InvitationDeclined");
            }
        }

        public async Task MakeMove(string gameId, int cellIndex)
        {
            if (GameSessions.TryGetValue(gameId, out var gameSession))
            {
                if (gameSession.CurrentTurn == Context.ConnectionId)
                {
                    gameSession.CurrentTurn = gameSession.PlayerX == Context.ConnectionId
                        ? gameSession.PlayerO
                        : gameSession.PlayerX;

                    await Clients.Client(gameSession.CurrentTurn)
                        .SendAsync("ReceiveMove", cellIndex, ConnectedUsers[Context.ConnectionId]);
                }
            }
        }

        public async Task SendReport(string gameId, string message)
        {
            if (GameSessions.TryGetValue(gameId, out var gameSession))
            {
                if (gameSession.PlayerO == Context.ConnectionId)
                {
                    await _reportService.SendReportAsync(ConnectedUsers[gameSession.PlayerX], message);
                }
                else
                {
                    await _reportService.SendReportAsync(ConnectedUsers[gameSession.PlayerO], message);
                }
            }
        }

        public async Task RestartGame(string gameId)
        {
            await Clients.Group(gameId).SendAsync("RestartGame");
        }

        public async Task EndGame(string gameId)
        {
            if (GameSessions.TryRemove(gameId, out var gameSession))
            {
                await Clients.Group(gameId)
                    .SendAsync("EndGame");
                await Groups.RemoveFromGroupAsync(gameSession.PlayerX, gameId);
                await Groups.RemoveFromGroupAsync(gameSession.PlayerO, gameId);
            }
            else
            {
                throw new ArgumentException("Game not found");
            }
        }

        public async Task WriteStatistic(string gameId, string winedSymbol = null)
        {
            if (GameSessions.TryGetValue(gameId, out var gameSession))
            {
                if (!string.IsNullOrEmpty(winedSymbol))
                {
                    var playerX = ConnectedUsers[gameSession.PlayerX];
                    var playerO = ConnectedUsers[gameSession.PlayerO];

                    if (winedSymbol == "X")
                    {
                        await _gameService.WinAsync(playerX);
                        await _gameService.LoseAsync(playerO);
                    }
                    else
                    {
                        await _gameService.WinAsync(playerO);
                        await _gameService.LoseAsync(playerX);
                    }
                }
            }
        }
        
        private async Task EndGameOnDisconnect(string connectionId)
        {
            var gameSession = GameSessions.Values.FirstOrDefault(session =>
                session.PlayerX == connectionId || session.PlayerO == connectionId);

            if (gameSession != null)
            {
                if (ConnectedUsers.TryGetValue(connectionId, out var losingUserId))
                {
                    await _gameService.LoseAsync(losingUserId);
                }

                string remainingPlayer =
                    gameSession.PlayerX == connectionId ? gameSession.PlayerO : gameSession.PlayerX;
        
                string gameId = gameSession.GameId;
                GameSessions.TryRemove(gameId, out _);

                await Clients.Group(gameId).SendAsync("EndGame");
                await Groups.RemoveFromGroupAsync(connectionId, gameId);
                await Groups.RemoveFromGroupAsync(remainingPlayer, gameId);
            }
        }
    }
}