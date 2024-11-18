async function inviteToGame(toUserName, button) {
    button.disabled = true;
    button.textContent = 'Invited';
    button.style.backgroundColor = 'black';
    
    await connection.invoke("SendInvitation", toUserName)
        .then(() => {
            setTimeout(() => {
                if (!gameActive) {
                    button.disabled = false;
                    button.style.backgroundColor = 'var(--button-color)';
                }
                button.textContent = 'Invite';
            }, 6000);
        })
        .catch(function (err) {
            return console.error(err.toString());
        });
}

function showInvitationModal(senderUserName, senderUserId) {
    const modal = document.createElement('div');
    modal.classList.add('invitation-modal');

    const modalContent = document.createElement('div');
    modalContent.classList.add('invitation-modal-content');

    const message = document.createElement('p');
    message.textContent = `${senderUserName} inviting you to a game.`;

    const acceptBtn = document.createElement('button');
    acceptBtn.textContent = 'Accept';
    acceptBtn.classList.add('btn');
    acceptBtn.onclick = function () {
        connection.invoke("AcceptInvitation", senderUserId).catch(function (err) {
            return console.error(err.toString());
        });
        modal.remove();
    };

    const declineBtn = document.createElement('button');
    declineBtn.textContent = 'Decline';
    declineBtn.classList.add('btn');
    declineBtn.onclick = function () {
        connection.invoke("DeclineInvitation", senderUserName).catch(function (err) {
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

    setTimeout(() => {
        modal.remove();
    }, 5000);
}

function showUserIsBusyModal() {
    const modal = document.createElement('div');
    modal.classList.add('invitation-modal');

    const modalContent = document.createElement('div');
    modalContent.classList.add('invitation-modal-content');

    const message = document.createElement('p');
    message.textContent = `$User is playing right now.`;

    modalContent.appendChild(message);
    modal.appendChild(modalContent);

    const centralPanel = document.querySelector('.central-panel');
    centralPanel.appendChild(modal);

    setTimeout(() => {
        modal.remove();
    }, 3000);
}


