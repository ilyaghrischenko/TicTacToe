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
        const allButtons = document.querySelectorAll('.btn');
        allButtons.forEach(button => {
            if (button.innerText !== 'Restart') {
                button.disabled = true;
                button.innerHTML = 'Unavailable'
                button.style.backgroundColor = 'black';
            }
        });
        
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

        checkWinner();

        // Переключаем очередность хода
        isMyTurn = (playerName !== sessionStorage.getItem('userLogin'));
        statusText.textContent = isMyTurn ? "Ваш ход" : "Ход соперника";
    });

}
