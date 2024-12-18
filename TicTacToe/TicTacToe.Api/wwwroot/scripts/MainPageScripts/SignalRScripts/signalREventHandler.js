async function putOnEventHandlers() {
    await connection.on("ReceiveStatusUpdate", (userId, isOnline) => {
        const statusElement = document.getElementById(`online-status-${userId}`);
        if (statusElement) {
            if (isOnline) {
                statusElement.textContent = '';
                statusElement.classList.add('online');
                statusElement.classList.remove('offline');
            } else {
                statusElement.textContent = '';
                statusElement.classList.add('offline');
                statusElement.classList.remove('online');
            }
        }
    });

    await connection.on("ReceiveInvitation", function (senderUserName, senderUserId) {
        console.log("ReceiveInvitation");
        showInvitationModal(senderUserName, senderUserId);
    });

    await connection.on("InvitationDeclined", function (toUserName) {
        alert(`Your invitation is declined.`);
    });

    await connection.on("StartGame", function (gameId, symbol, boardArray) {
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

        cells.forEach(cell => {
            cell.textContent = '';
            cell.classList.add('active');
        });

        currentGameId = gameId;
        playerSymbol = symbol;
        gameActive = true;
        isMyTurn = symbol === 'X';
        drawBoard(boardArray);

        statusText.textContent = isMyTurn ? "Your turn" : "Enemy's turn";
    });

    connection.on("ReceiveMove", async function (boardArray, currentTurn) {
        await drawBoard(boardArray);
        if (gameActive) {
            statusText.textContent = playerSymbol === currentTurn ? "Your turn" : "Enemy's turn";
        }
            isMyTurn = playerSymbol === currentTurn;
    });

    connection.on("ReceiveGameResult", function (result) {
        endGame(result);
    });

    connection.on("RestartGame", async function (boardArray) {
        gameActive = true;
        await drawBoard(boardArray);
        statusText.textContent = isMyTurn ? "Your turn" : "Enemy's turn";
        
        
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

    connection.on("UserIsBusy", function () {
        showUserIsBusyModal();
    });

}
