function renderEndGameButton() {
    let button = document.getElementById('endGameBtn');
    if (!button) {
        button = document.createElement('button');
        button.id = 'endGameBtn';
        button.classList.add('btn');
        button.textContent = 'End game';

        button.addEventListener('click', handleEndGameButtonClick);

        const centralPanel = document.querySelector('.central-panel');
        const statusElement = document.getElementById('status');
        centralPanel.insertBefore(button, statusElement);
    }
}

function handleEndGameButtonClick() {
    connection.invoke("EndGame", currentGameId).catch(function (err) {
        console.error("Error in ending game:", err.toString());
    });
    document.getElementById('endGameBtn').remove();
}
