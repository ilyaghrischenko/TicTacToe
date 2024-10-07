'use strict'

function renderFriendsList(friends, friendList) {
    // Очистка списков перед добавлением новых данных
    friendList.innerHTML = '';

    // Рендеринг всех пользователей
    friends.forEach(user => {
        const li = document.createElement('li');
        li.classList.add('user-item');

        // Создаем блок с данными пользователя
        const userInfo = document.createElement('div');
        userInfo.classList.add('friend-info');

        // Создаем аватар
        const avatarImg = document.createElement('img');
        avatarImg.src = user.avatar && user.avatar.length > 0 ? `data:image/png;base64,${user.avatar}` : '../images/avatar.png';
        avatarImg.alt = `${user.login}'s avatar`;
        avatarImg.classList.add('avatar');
        userInfo.appendChild(avatarImg);

        // Добавляем логин пользователя
        const userName = document.createElement('span');
        userName.textContent = user.login;
        userInfo.appendChild(userName);

        // Создаем кнопку (Добавить/Удалить)
        const button = document.createElement('button');
        button.classList.add('btn', 'invite-button');
        button.textContent = 'Invite';
        button.onclick = () => inviteToGame(user.login);

        // Добавляем всё в li
        li.appendChild(userInfo);
        li.appendChild(button);
        friendList.appendChild(li);
    });
}
