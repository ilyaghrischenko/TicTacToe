async function inviteToGame(toUserName) {
    await connection.invoke("SendInvitation", toUserName).catch(function (err) {
        return console.error(err.toString());
    });
}

function showInvitationModal(fromUserName) {
    // Создаем модальное окно
    const modal = document.createElement('div');
    modal.classList.add('invitation-modal'); // Применяем класс для модального окна

    const modalContent = document.createElement('div');
    modalContent.classList.add('invitation-modal-content'); // Применяем класс для контента модального окна

    const message = document.createElement('p');
    message.textContent = `${fromUserName} приглашает вас на игру.`;

    // Кнопка "Принять"
    const acceptBtn = document.createElement('button');
    acceptBtn.textContent = 'Принять';
    acceptBtn.classList.add('btn'); // Применяем класс стиля для кнопок
    acceptBtn.onclick = function () {
        connection.invoke("AcceptInvitation", fromUserName).catch(function (err) {
            return console.error(err.toString());
        });
        modal.remove(); // Удаляем модальное окно
    };

    // Кнопка "Отклонить"
    const declineBtn = document.createElement('button');
    declineBtn.textContent = 'Отклонить';
    declineBtn.classList.add('btn'); // Применяем класс стиля для кнопок
    declineBtn.onclick = function () {
        connection.invoke("DeclineInvitation", fromUserName).catch(function (err) {
            return console.error(err.toString());
        });
        modal.remove(); // Удаляем модальное окно
    };

    // Добавляем элементы в модальное окно
    modalContent.appendChild(message);
    modalContent.appendChild(acceptBtn);
    modalContent.appendChild(declineBtn);
    modal.appendChild(modalContent);

    // Добавляем модальное окно в центральную панель
    const centralPanel = document.querySelector('.central-panel');
    centralPanel.appendChild(modal);
}

