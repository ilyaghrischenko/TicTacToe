using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;
using TicTacToe.Domain.Interfaces;

namespace TicTacToe.Application.SignalRHub
{
    public class GameHub(IGameService gameService) : Hub
    {
        private static ConcurrentDictionary<string, string> ConnectedUsers = new();
        private static ConcurrentDictionary<string, GameSession> GameSessions = new();
        private readonly IGameService _gameService = gameService;

        public override Task OnConnectedAsync()
        {
            var userName = Context.GetHttpContext().Request.Query["userName"];

            if (!string.IsNullOrEmpty(userName))
            {
                ConnectedUsers[Context.ConnectionId] = userName;
            }

            Console.WriteLine($"User: {userName}, ConnectionId: {Context.ConnectionId}");
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await EndGameOnDisconnect(Context.ConnectionId); // Завершаем игру при отключении пользователя
            ConnectedUsers.TryRemove(Context.ConnectionId, out var userName);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendInvitation(string toUserName)
        {
            var fromUserName = ConnectedUsers[Context.ConnectionId];
            var toConnectionId = ConnectedUsers.FirstOrDefault(u => u.Value == toUserName).Key;
            if (toConnectionId != null)
            {
                await Clients.Client(toConnectionId).SendAsync("ReceiveInvitation", fromUserName);
            }
        }

        public async Task AcceptInvitation(string fromUserName)
        {
            var toUserName = ConnectedUsers[Context.ConnectionId];
            var fromConnectionId = ConnectedUsers.FirstOrDefault(u => u.Value == fromUserName).Key;

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

        public async Task DeclineInvitation(string fromUserName)
        {
            var fromConnectionId = ConnectedUsers.FirstOrDefault(u => u.Value == fromUserName).Key;
            if (fromConnectionId != null)
            {
                await Clients.Client(fromConnectionId)
                    .SendAsync("InvitationDeclined", ConnectedUsers[Context.ConnectionId]);
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

                    await Clients.Group(gameId)
                        .SendAsync("ReceiveMove", cellIndex, ConnectedUsers[Context.ConnectionId]);
                }
            }
        }

        public async Task RestartGame(string gameId)
        {
            await Clients.Group(gameId).SendAsync("RestartGame");
        }

        public async Task EndGame(string gameId)
        {
            GameSessions.TryRemove(gameId, out _);
            await Clients.Group(gameId)
                .SendAsync("EndGame");
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, gameId);
        }
        
        public async Task WriteStatistic(string gameId, string winedSymbol = null)
        {
            if (GameSessions.TryGetValue(gameId, out var gameSession))
            {
                // Проверка на победителя
                if (!string.IsNullOrEmpty(winedSymbol))
                {
                    // Получаем логины обоих игроков
                    var playerX = ConnectedUsers[gameSession.PlayerX];
                    var playerO = ConnectedUsers[gameSession.PlayerO];

                    // Обновляем статистику для победителя и проигравшего
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
                await _gameService.Lose(ConnectedUsers[connectionId]);
                
                string remainingPlayer =
                    gameSession.PlayerX == connectionId ? gameSession.PlayerO : gameSession.PlayerX;
                
                
                string gameId = gameSession.GameId;
                GameSessions.TryRemove(gameId, out _);

                
                await Clients.Client(remainingPlayer)
                    .SendAsync("EndGame");
                await Groups.RemoveFromGroupAsync(connectionId, gameId);
                await Groups.RemoveFromGroupAsync(remainingPlayer, gameId);
            }
        }

        // private async Task Punish(string connectionId)
        // {
        //     
        // }
    }
}