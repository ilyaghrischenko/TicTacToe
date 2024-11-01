async function inviteToGame(toUserName) {
    await connection.invoke("SendInvitation", toUserName).catch(function (err) {
        return console.error(err.toString());
    });
}

function showInvitationModal(fromUserName) {
    const modal = document.createElement('div');
    modal.classList.add('invitation-modal');

    const modalContent = document.createElement('div');
    modalContent.classList.add('invitation-modal-content');

    const message = document.createElement('p');
    message.textContent = `${fromUserName} inviting you to a game.`;

    const acceptBtn = document.createElement('button');
    acceptBtn.textContent = 'Accept';
    acceptBtn.classList.add('btn');
    acceptBtn.onclick = function () {
        connection.invoke("AcceptInvitation", fromUserName).catch(function (err) {
            return console.error(err.toString());
        });
        modal.remove();
    };

    const declineBtn = document.createElement('button');
    declineBtn.textContent = 'Decline';
    declineBtn.classList.add('btn');
    declineBtn.onclick = function () {
        connection.invoke("DeclineInvitation", fromUserName).catch(function (err) {
            return console.error(err.toString());
        });
        modal.remove();
    };

    modalContent.appendChild(message);
    modalContent.appendChild(acceptBtn);
    modalContent.appendChild(declineBtn);
    modal.appendChild(modalContent);

    const centralPanel = document.querySelector('.central-panel');
    centralPanel.appendChild(modal);
}

