function renderRestartButton() {
    // Проверка, существует ли уже кнопка
    let button = document.getElementById('restartBtn');
    if (!button) {
        // Создание кнопки, если она ещё не существует
        button = document.createElement('button');
        button.id = 'restartBtn';
        button.classList.add('btn');
        button.textContent = 'Restart';


        // Добавление события клика на кнопку
        button.addEventListener('click', handleRestartButtonClick);

        // Получаем central-panel и вставляем кнопку перед элементом <p id="status">
        const centralPanel = document.querySelector('.central-panel');
        const statusElement = document.getElementById('status');
        centralPanel.insertBefore(button, statusElement);
    }
}


// Функция для перезапуска игры
function restartGame() {
    gameActive = true;
    board = ['', '', '', '', '', '', '', '', ''];
    statusText.textContent = `Ход игрока: ${playerSymbol}`;
    cells.forEach(cell => {
        cell.textContent = '';
        cell.classList.remove('active');
    });
    isMyTurn = true; // Возвращение права хода текущему игроку
}

function handleRestartButtonClick() {
    // restartGame();
    connection.invoke("RestartGame", currentGameId).catch(function (err) {
        return console.error(err.toString());
    });
    
    document.getElementById('restartBtn').remove();
}
