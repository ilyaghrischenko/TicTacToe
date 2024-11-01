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
        .then(async () => {
            if (await checkWinner()) {
                isMyTurn = !isMyTurn;
                return;
            }
            endTurn();
        })
        .catch((err) => {
            console.error("Error in move:", err.toString());
            isMyTurn = true;
            statusText.textContent = "Error in transmitting your move, try again";
        });
}

function endTurn() {
    isMyTurn = false;
    statusText.textContent = "Enemy's turn";
}

async function checkWinner() {
    for (let condition of winningConditions) {
        const [a, b, c] = condition;
        if (board[a] && board[a] === board[b] && board[a] === board[c]) {
            endGame(`Player with symbol ${board[a]} won`);

            if (isMyTurn) {
                connection.invoke("WriteStatistic", currentGameId, board[a])
                    .catch(function (err) {
                        console.error("Error in write statistic:", err.toString());
                    });
            }

            const token = sessionStorage.getItem('token');
            if (!token) {
                window.location.href = '../pages/auth.html';
            }

            const response = await fetch('/api/User/getUserStatistics', {
                method: 'GET',
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': 'application/json'
                }
            });

            if (response.ok) {
                const data = await response.json();
                document.getElementById('wins').textContent = data.wins;
                document.getElementById('losses').textContent = data.losses;
            }

            return true;
        }
    }

    if (!board.includes('')) {
        endGame('Draw');
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
