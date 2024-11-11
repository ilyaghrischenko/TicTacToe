function renderEndGameButton() {
    let button = document.getElementById('endGameBtn');
    if (!button) {
        button = document.createElement('button');
        button.id = 'endGameBtn';
        button.classList.add('btn');
        button.textContent = 'End game';

        button.addEventListener('click', handleEndGameButtonClick);

        const gameButtonsContainer = document.querySelector('.game-buttons-container');
        gameButtonsContainer.appendChild(button);
    }
}

function handleEndGameButtonClick() {
    connection.invoke("EndGame", currentGameId).catch(function (err) {
        console.error("Error in ending game:", err.toString());
    });
    document.getElementById('endGameBtn').remove();
}
