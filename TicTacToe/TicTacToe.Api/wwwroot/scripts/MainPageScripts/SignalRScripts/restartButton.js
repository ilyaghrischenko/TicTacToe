function renderRestartButton() {
    let button = document.getElementById('restartBtn');
    if (!button) {
        button = document.createElement('button');
        button.id = 'restartBtn';
        button.classList.add('btn');
        button.textContent = 'Restart';

        button.addEventListener('click', handleRestartButtonClick);

        const gameButtonsContainer = document.querySelector('.game-buttons-container');
        gameButtonsContainer.appendChild(button);
    }
}

function handleRestartButtonClick() {
    connection.invoke("RestartGame", currentGameId).catch(function (err) {
        console.error("Error in restart:", err.toString());
    });
    document.getElementById('restartBtn').remove();
}
