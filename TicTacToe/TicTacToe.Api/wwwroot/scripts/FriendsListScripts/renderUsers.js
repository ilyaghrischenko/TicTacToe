'use strict'

function renderUserList(users, friends) {
    // Очистка списков перед добавлением новых данных
    friendList.innerHTML = '';
    userList.innerHTML = '';

    // Рендеринг всех пользователей
    users.forEach(user => {
        const li = document.createElement('li');
        li.classList.add('user-item');

        // Проверяем, является ли пользователь другом
        const isFriend = friends.some(friend => friend.id === user.id);

        // Создаем блок с данными пользователя
        const userInfo = document.createElement('div');
        userInfo.classList.add('user-info');

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

        // Создаем блок для статистики
        const userStats = document.createElement('div');
        userStats.classList.add('user-stats');
        const wins = document.createElement('span');
        wins.textContent = `Победы: ${user.statistic.wins}`;
        const losses = document.createElement('span');
        losses.textContent = `Проигрыши: ${user.statistic.losses}`;
        userStats.appendChild(wins);
        userStats.appendChild(losses);

        // Добавляем статистику под логином
        userInfo.appendChild(userStats);

        // Создаем кнопку (Добавить/Удалить)
        const button = document.createElement('button');
        button.classList.add('btn', isFriend ? 'delete-button' : 'add-button');
        button.textContent = isFriend ? 'Удалить' : 'Добавить';
        button.onclick = isFriend ? () => deleteFriend(user.id) : () => addFriend(user.id);

        // Добавляем всё в li
        li.appendChild(userInfo);
        li.appendChild(button);
        isFriend ? friendList.appendChild(li): userList.appendChild(li);
    });
}
