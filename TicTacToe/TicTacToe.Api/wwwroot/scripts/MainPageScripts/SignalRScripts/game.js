'use strict';

const cells = document.querySelectorAll('.cell');
const statusText = document.getElementById('status');
const restartBtn = document.getElementById('restartBtn');
let gameActive = false;
let board = Array(9).fill('');
let currentGameId = null;
let playerSymbol = null;
let isMyTurn = false;

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

    if (board[cellIndex] !== '') return;

    board[cellIndex] = playerSymbol;
    cell.textContent = playerSymbol;
    cell.classList.add('active');

    connection.invoke("MakeMove", currentGameId, parseInt(cellIndex))
        .then(() => {
            if (checkWinner()) return;
            endTurn();
        })
        .catch((err) => {
            console.error("Error in move:", err.toString());
            isMyTurn = true;
            statusText.textContent = "Ошибка при передаче хода, попробуйте еще раз";
        });
}

function endTurn() {
    isMyTurn = false;
    statusText.textContent = "Ход соперника";
}

function checkWinner() {
    for (let condition of winningConditions) {
        const [a, b, c] = condition;
        if (board[a] && board[a] === board[b] && board[a] === board[c]) {
            endGame(`Победил игрок с символом ${board[a]}`);

            if (isMyTurn) {
                connection.invoke("WriteStatistic", currentGameId, board[a])
                    .catch(function (err) {
                        console.error("Error in write statistic:", err.toString());
                    });
            }
            
            return true;
        }
    }

    if (!board.includes('')) {
        endGame('Ничья');
        return true;
    }

    return false;
}

function endGame(message) {
    renderRestartButton();
    renderEndGameButton();
    gameActive = false;
    statusText.textContent = message;
}

cells.forEach(cell => cell.addEventListener('click', handleCellClick));
