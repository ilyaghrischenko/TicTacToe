function renderRestartButton() {
    let button = document.getElementById('restartBtn');
    if (!button) {
        button = document.createElement('button');
        button.id = 'restartBtn';
        button.classList.add('btn');
        button.textContent = 'Restart';

        button.addEventListener('click', handleRestartButtonClick);

        const centralPanel = document.querySelector('.central-panel');
        const statusElement = document.getElementById('status');
        centralPanel.insertBefore(button, statusElement);
    }
}

function handleRestartButtonClick() {
    connection.invoke("RestartGame", currentGameId).catch(function (err) {
        console.error("Error in restart:", err.toString());
    });
    document.getElementById('restartBtn').remove();
}
