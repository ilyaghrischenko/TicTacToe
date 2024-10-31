async function putOnEventHandlers() {
    await connection.on("ReceiveInvitation", function (fromUserName) {
        console.log("ReceiveInvitation");
        showInvitationModal(fromUserName);
    });

    await connection.on("InvitationDeclined", function (toUserName) {
        alert(`${toUserName} отклонил ваше приглашение.`);
    });

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

    connection.on("ReceiveMove", function (cellIndex, playerName) {
        const symbol = playerName === sessionStorage.getItem('userLogin') ? playerSymbol : (playerSymbol === 'X' ? 'O' : 'X');
        board[cellIndex] = symbol;

        const cell = document.querySelector(`.cell[data-index='${cellIndex}']`);
        cell.textContent = symbol;
        cell.classList.add('active');

        isMyTurn = (playerName !== sessionStorage.getItem('userLogin'));
        statusText.textContent = isMyTurn ? "Ваш ход" : "Ход соперника";

        checkWinner();
    });

    connection.on("RestartGame", function () {
        gameActive = true;
        board.fill('');
        statusText.textContent = isMyTurn ? "Ваш ход" : "Ход соперника";
        cells.forEach(cell => {
            cell.textContent = '';
            cell.classList.remove('active');
        });
        isMyTurn = true;

        const restartButton = document.getElementById('restartBtn');
        if (restartButton) restartButton.remove();
    });
    
    connection.on("EndGame", function () {
        currentGameId = null;
        gameActive = false;
        board.fill('');
        cells.forEach(cell => {
            cell.textContent = '';
            cell.classList.remove('active');
        });
        statusText.textContent = "Игра окончена";
    });
}
