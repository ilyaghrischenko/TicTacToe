'use strict';

const cells = document.querySelectorAll('.cell');
const statusText = document.getElementById('status');
const restartBtn = document.getElementById('restartBtn');
let gameActive = false;
let board = Array(9).fill('');
let currentGameId = null;
let playerSymbol = null;
let isMyTurn = false;


function handleCellClick(event) {
    const cell = event.target;
    const cellIndex = cell.getAttribute('data-index');
    if (cell.textContent !== '') return;

    connection.invoke("MakeMove", currentGameId, parseInt(cellIndex), playerSymbol)
        .then(async () => {
            isMyTurn = !isMyTurn;
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
async function endGame(message) {
    renderRestartButton();
    renderEndGameButton();
    gameActive = false;
    statusText.textContent = message;

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
}

async function drawBoard(boardArray) {
    // Проверяем, что массив корректный
    if (!Array.isArray(boardArray) || boardArray.length !== 9) {
        console.error("Invalid board array:", boardArray);
        return;
    }

    // Обновляем каждую ячейку игрового поля
    boardArray.forEach((symbol, index) => {
        const cell = document.querySelector(`[data-index="${index}"]`); // Находим ячейку по индексу
        if (cell) {
            cell.textContent = symbol || ''; // Устанавливаем символ (X, O или пустая строка)
            if (symbol) {
                cell.classList.add('active'); // Добавляем класс, если ячейка заполнена
            } else {
                cell.classList.remove('active'); // Убираем класс, если ячейка пуста
            }
        }
    });
}



cells.forEach(cell => cell.addEventListener('click', handleCellClick));
