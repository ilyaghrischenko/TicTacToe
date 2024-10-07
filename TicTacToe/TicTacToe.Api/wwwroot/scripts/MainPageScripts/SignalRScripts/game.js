'use strict';

const cells = document.querySelectorAll('.cell');
const statusText = document.getElementById('status');
const restartBtn = document.getElementById('restartBtn');
let gameActive = false;
let board = ['', '', '', '', '', '', '', '', ''];
let currentGameId = null;
let playerSymbol = null;
let isMyTurn = false;

// Комбинации для победы
const winningConditions = [
    [0, 1, 2],
    [3, 4, 5],
    [6, 7, 8],
    [0, 3, 6],
    [1, 4, 7],
    [2, 5, 8],
    [0, 4, 8],
    [2, 4, 6]
];

function handleCellClick(event) {
    if (!gameActive || !isMyTurn) return;

    const cell = event.target;
    const cellIndex = cell.getAttribute('data-index');

    if (board[cellIndex] !== '') {
        return;
    }

    board[cellIndex] = playerSymbol;
    cell.textContent = playerSymbol;
    cell.classList.add('active');

    // Отправляем ход на сервер
    connection.invoke("MakeMove", currentGameId, parseInt(cellIndex)).catch(function (err) {
        return console.error(err.toString());
    });

    if(checkWinner() === true){
        return;
    }

    // Переключаем ход
    isMyTurn = false;
    statusText.textContent = "Ход соперника";
}


function checkWinner() {
    // Перебираем все возможные комбинации для победы
    for (let condition of winningConditions) {
        const [a, b, c] = condition; // Деструктурируем индексы комбинации
        const symbolA = board[a];
        const symbolB = board[b];
        const symbolC = board[c];

        // Пропускаем комбинации, если хотя бы одна клетка пустая
        if (symbolA === '' || symbolB === '' || symbolC === '') {
            continue;
        }

        // Проверяем, совпадают ли символы во всех трёх клетках
        if (symbolA === symbolB && symbolB === symbolC) {
            gameActive = false;

            // Отображаем сообщение о победителе
            statusText.textContent = `Победил игрок с символом ${symbolA}`;
            return true; // Возвращаем символ победителя для дальнейших действий, если нужно
        }
    }

    // Если все клетки заполнены и никто не победил, объявляем ничью
    if (!board.includes('')) {
        gameActive = false;
        statusText.textContent = 'Ничья';
        return true;
    }

    // Игра продолжается, если победителя пока нет
    return false;
}


function restartGame() {
    gameActive = true;
    board = ['', '', '', '', '', '', '', '', ''];
    statusText.textContent = `Ход игрока: ${playerSymbol}`;
    cells.forEach(cell => {
        cell.textContent = '';
        cell.classList.remove('active');
    });
}

cells.forEach(cell => cell.addEventListener('click', handleCellClick));
restartBtn.addEventListener('click', restartGame);
