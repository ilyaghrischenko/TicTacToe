async function putOnEventHandlers() {
    await connection.on("ReceiveInvitation", function (fromUserName) {
        console.log("ReceiveInvitation");
        showInvitationModal(fromUserName);
    });

    await connection.on("InvitationDeclined", function (toUserName) {
        alert(`Your invitation is declined.`);
    });

    await connection.on("StartGame", function (gameId, symbol) {
        const allButtons = document.querySelectorAll('.btn');
        allButtons.forEach(button => {
            if (button.innerText !== 'Restart') {
                button.disabled = true;
                button.style.backgroundColor = 'black';
            }
        });
        document.getElementById('reportBtn').classList.remove('hidden-report-button');
        document.getElementById('reportBtn').classList.add('visible-report-button');
        document.getElementById('reportBtn').addEventListener('click', function () {
            renderReportModal();
        });

        currentGameId = gameId;
        playerSymbol = symbol;
        gameActive = true;
        isMyTurn = symbol === 'X';

        statusText.textContent = isMyTurn ? "Your turn" : "Enemy's turn";
    });

    connection.on("ReceiveMove", async function (cellIndex, playerId) {
        const symbol = playerId === sessionStorage.getItem('userId') ? playerSymbol : (playerSymbol === 'X' ? 'O' : 'X');
        board[cellIndex] = symbol;

        const cell = document.querySelector(`.cell[data-index='${cellIndex}']`);
        cell.textContent = symbol;
        cell.classList.add('active');
        isMyTurn = (playerId !== sessionStorage.getItem('userLogin'));
        statusText.textContent = isMyTurn ? "Your turn" : "Enemy's turn";

        await checkWinner();
    });

    connection.on("RestartGame", function () {
        gameActive = true;
        board.fill('');
        statusText.textContent = isMyTurn ? "Your turn" : "Enemy's turn";
        cells.forEach(cell => {
            cell.textContent = '';
            cell.classList.remove('active');
        });
        
        document.getElementById('endGameBtn').remove();
        document.getElementById('restartBtn').remove();
    });

    connection.on("EndGame", function () {
        const allButtons = document.querySelectorAll('.btn');
        allButtons.forEach(button => {
            if (button.innerText !== 'Restart') {
                button.disabled = false;
                button.style.backgroundColor = 'var(--button-color)';
            }
        });
        document.getElementById('reportBtn').classList.remove('visible-report-button');
        document.getElementById('reportBtn').classList.add('hidden-report-button');

        const endGameBtn = document.getElementById('endGameBtn');
        const restartBtn = document.getElementById('restartBtn');

        if (restartBtn) restartBtn.remove();
        if (endGameBtn) endGameBtn.remove();

        currentGameId = null;
        gameActive = false;
        board.fill('');
        cells.forEach(cell => {
            cell.textContent = '';
            cell.classList.remove('active');
        });
        statusText.textContent = "Game finished";
    });
}
