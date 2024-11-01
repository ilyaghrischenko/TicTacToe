using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;
using TicTacToe.Domain.Interfaces;
using TicTacToe.Domain.Interfaces.DbModelsServices;
using TicTacToe.Domain.Interfaces.Repositories;

namespace TicTacToe.Application.SignalRHub
{
    public class GameHub(
        IGameService gameService,
        IReportService reportService,
        IUserRepository userRepository) : Hub
    {
        private static ConcurrentDictionary<string, int> ConnectedUsers = new();
        private static ConcurrentDictionary<string, GameSession> GameSessions = new();
        
        private readonly IGameService _gameService = gameService;
        private readonly IReportService _reportService = reportService;
        private readonly IUserRepository _userRepository = userRepository;

        public override Task OnConnectedAsync()
        {
            var userId = Context.GetHttpContext().Request.Query["userId"];
            if (int.TryParse(userId, out var userIdValue))
                ConnectedUsers[Context.ConnectionId] = userIdValue;
            else
                throw new Exception("UserId is not valid");
    
            Console.WriteLine($"User: {userId}, ConnectionId: {Context.ConnectionId}");
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await EndGameOnDisconnect(Context.ConnectionId);
            ConnectedUsers.TryRemove(Context.ConnectionId, out var userName);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendInvitation(int toUserId)
        {
            var sender = await _userRepository.Get(ConnectedUsers[Context.ConnectionId]);
            var senderUserName = sender.Login;
            var toConnectionId = ConnectedUsers.FirstOrDefault(u => u.Value == toUserId).Key;
            if (toConnectionId != null)
            {
                await Clients.Client(toConnectionId).SendAsync("ReceiveInvitation", senderUserName);
            }
        }

        public async Task AcceptInvitation(int fromUserId)
        {
            var fromConnectionId = ConnectedUsers.FirstOrDefault(u => u.Value == fromUserId).Key;

            if (fromConnectionId != null)
            {
                var gameId = Guid.NewGuid().ToString();
                var gameSession = new GameSession
                {
                    GameId = gameId,
                    PlayerX = fromConnectionId,
                    PlayerO = Context.ConnectionId,
                    CurrentTurn = fromConnectionId
                };
                GameSessions[gameId] = gameSession;

                await Groups.AddToGroupAsync(fromConnectionId, gameId);
                await Groups.AddToGroupAsync(Context.ConnectionId, gameId);

                await Clients.Client(fromConnectionId).SendAsync("StartGame", gameId, "X");
                await Clients.Client(Context.ConnectionId).SendAsync("StartGame", gameId, "O");
            }
        }

        public async Task DeclineInvitation(string senderUserLogin)
        {
            var senderUserId = (await  _userRepository.Get(senderUserLogin)).Id;
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
                    await _reportService.SendReport(ConnectedUsers[gameSession.PlayerX], message);
                }
                else
                {
                    await _reportService.SendReport(ConnectedUsers[gameSession.PlayerO], message);
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
                throw new Exception("Game not found");
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
                        await _gameService.Win(playerX);
                        await _gameService.Lose(playerO);
                    }
                    else
                    {
                        await _gameService.Win(playerO);
                        await _gameService.Lose(playerX);
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
                    await _gameService.Lose(losingUserId);
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