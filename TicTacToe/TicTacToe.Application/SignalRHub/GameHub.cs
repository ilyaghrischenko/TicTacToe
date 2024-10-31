using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;

namespace TicTacToe.Application.SignalRHub
{
    public class GameHub : Hub
    {
        private static ConcurrentDictionary<string, string> ConnectedUsers = new();
        private static ConcurrentDictionary<string, GameSession> GameSessions = new();

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
            ConnectedUsers.TryRemove(Context.ConnectionId, out var userName);
            await EndGameOnDisconnect(Context.ConnectionId); // Завершаем игру при отключении пользователя
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
                await Clients.Client(fromConnectionId).SendAsync("InvitationDeclined", ConnectedUsers[Context.ConnectionId]);
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

                    await Clients.Group(gameId).SendAsync("ReceiveMove", cellIndex, ConnectedUsers[Context.ConnectionId]);
                }
            }
        }
        
        public async Task RestartGame(string gameId)
        {
            if (GameSessions.TryGetValue(gameId, out var gameSession))
            {
                gameSession.CurrentTurn = gameSession.PlayerX;

                await Clients.Group(gameId).SendAsync("RestartGame");
            }
        }

        private async Task EndGameOnDisconnect(string connectionId)
        {
            var gameSession = GameSessions.Values.FirstOrDefault(session => session.PlayerX == connectionId || session.PlayerO == connectionId);
            if (gameSession != null)
            {
                string remainingPlayer = gameSession.PlayerX == connectionId ? gameSession.PlayerO : gameSession.PlayerX;
                string gameId = gameSession.GameId;
                GameSessions.TryRemove(gameId, out _);

                await Clients.Client(remainingPlayer).SendAsync("EndGame", gameId);
                
                await Groups.RemoveFromGroupAsync(connectionId, gameId);
                await Groups.RemoveFromGroupAsync(remainingPlayer, gameId);
            }
        }
    }

    public class GameSession
    {
        public string GameId { get; set; }
        public string PlayerX { get; set; }
        public string PlayerO { get; set; }
        public string CurrentTurn { get; set; }
    }
}
