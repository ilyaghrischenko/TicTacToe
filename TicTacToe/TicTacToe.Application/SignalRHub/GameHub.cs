using System.Collections.Concurrent;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace TicTacToe.Application.SignalRHub
{
    public class GameHub : Hub
    {
        // Хранение подключенных пользователей
        private static ConcurrentDictionary<string, string> ConnectedUsers = new();

        // Отслеживание активных игр
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

        public override Task OnDisconnectedAsync(Exception exception)
        {
            ConnectedUsers.TryRemove(Context.ConnectionId, out _);
            return base.OnDisconnectedAsync(exception);
        }

        // Отправка приглашения
        public async Task SendInvitation(string toUserName)
        {
            var fromUserName = ConnectedUsers[Context.ConnectionId];
            var toConnectionId = ConnectedUsers.FirstOrDefault(u => u.Value == toUserName).Key;
            if (toConnectionId != null)
            {
                await Clients.Client(toConnectionId).SendAsync("ReceiveInvitation", fromUserName);
            }
        }

        // Принятие приглашения
        public async Task AcceptInvitation(string fromUserName)
        {
            var toUserName = ConnectedUsers[Context.ConnectionId];
            var fromConnectionId = ConnectedUsers.FirstOrDefault(u => u.Value == fromUserName).Key;

            if (fromConnectionId != null)
            {
                // Создаем новую игровую сессию
                var gameId = Guid.NewGuid().ToString();
                var gameSession = new GameSession
                {
                    GameId = gameId,
                    PlayerX = fromConnectionId,
                    PlayerO = Context.ConnectionId,
                    CurrentTurn = fromConnectionId
                };
                GameSessions[gameId] = gameSession;

                // Добавляем игроков в группу игры
                await Groups.AddToGroupAsync(fromConnectionId, gameId);
                await Groups.AddToGroupAsync(Context.ConnectionId, gameId);

                // Начинаем игру
                await Clients.Client(fromConnectionId).SendAsync("StartGame", gameId, "X");
                await Clients.Client(Context.ConnectionId).SendAsync("StartGame", gameId, "O");
            }
        }

        // Отклонение приглашения
        public async Task DeclineInvitation(string fromUserName)
        {
            var fromConnectionId = ConnectedUsers.FirstOrDefault(u => u.Value == fromUserName).Key;

            if (fromConnectionId != null)
            {
                await Clients.Client(fromConnectionId)
                    .SendAsync("InvitationDeclined", ConnectedUsers[Context.ConnectionId]);
            }
        }

        // Обработка хода игрока
        public async Task MakeMove(string gameId, int cellIndex)
        {
            if (GameSessions.TryGetValue(gameId, out var gameSession))
            {
                if (gameSession.CurrentTurn == Context.ConnectionId)
                {
                    // Передаем ход другому игроку
                    gameSession.CurrentTurn = gameSession.PlayerX == Context.ConnectionId
                        ? gameSession.PlayerO
                        : gameSession.PlayerX;

                    // Отправляем ход обоим игрокам
                    await Clients.Group(gameId)
                        .SendAsync("ReceiveMove", cellIndex, ConnectedUsers[Context.ConnectionId]);
                }
            }
        }


        public async Task SendMessageToAll(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
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