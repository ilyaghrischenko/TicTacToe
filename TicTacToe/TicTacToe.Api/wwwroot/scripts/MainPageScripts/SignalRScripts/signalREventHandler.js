async function putOnEventHandlers() {
// Обработчик для приглашения на игру
    await connection.on("ReceiveInvitation", function (fromUserName) {
        console.log("ReceiveInvitation");
        showInvitationModal(fromUserName);
    });

// Обработчик для отклонения приглашения
    await connection.on("InvitationDeclined", function (toUserName) {
        alert(`${toUserName} отклонил ваше приглашение.`);
    });

// Обработчик для начала игры
    await connection.on("StartGame", function (gameId, symbol) {
        currentGameId = gameId;
        playerSymbol = symbol;
        gameActive = true;
        isMyTurn = symbol === 'X';

        statusText.textContent = isMyTurn ? "Ваш ход" : "Ход соперника";
    });

// Обработчик для получения хода от соперника
    connection.on("ReceiveMove", function (cellIndex, playerName) {
        const symbol = playerName === sessionStorage.getItem('userLogin') ? playerSymbol : (playerSymbol === 'X' ? 'O' : 'X');
        board[cellIndex] = symbol;

        const cell = document.querySelector(`.cell[data-index='${cellIndex}']`);
        cell.textContent = symbol;
        cell.classList.add('active');

        // Переключаем очередность хода
        isMyTurn = (playerName !== sessionStorage.getItem('userLogin'));
        statusText.textContent = isMyTurn ? "Ваш ход" : "Ход соперника";

        checkWinner();
    });

    connection.on("RestartGame", function () {
        gameActive = true;
        board = ['', '', '', '', '', '', '', '', ''];
        statusText.textContent = `Ход игрока: ${playerSymbol}`;
        cells.forEach(cell => {
            cell.textContent = '';
            cell.classList.remove('active');
        });
        isMyTurn = true; // Возвращение права хода текущему игроку

        const restartButton = document.getElementById('restartBtn');
        if (restartButton) restartButton.remove(); // Удаление кнопки после перезапуска
    });

}
