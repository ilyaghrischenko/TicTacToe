function renderAllUsers(userUl, users) {
    userUl.innerHTML = '';
    
    users.forEach(user => {
        const li = document.createElement('li');
        li.classList.add('user-item');

        const userInfo = document.createElement('div');
        userInfo.classList.add('user-info');

        const avatarImg = document.createElement('img');
        avatarImg.src = user.avatar && user.avatar.length > 0 ? `data:image/png;base64,${user.avatar}` : '../images/avatar.png';
        avatarImg.alt = `${user.login}'s avatar`;
        avatarImg.classList.add('avatar');
        userInfo.appendChild(avatarImg);

        const userName = document.createElement('span');
        userName.textContent = user.login;
        userInfo.appendChild(userName);

        const userStats = document.createElement('div');
        userStats.classList.add('user-stats');
        const wins = document.createElement('span');
        wins.textContent = `Победы: ${user.statistic.wins}`;
        const losses = document.createElement('span');
        losses.textContent = `Проигрыши: ${user.statistic.losses}`;
        userStats.appendChild(wins);
        userStats.appendChild(losses);

        userInfo.appendChild(userStats);
        
        //TODO: Прописать логику показа жалоб пользователя (модальное окно)
        const showReportsButton = document.createElement('button');
        showReportsButton.classList.add('btn', 'reports-button');
        showReportsButton.textContent = 'Show reports';
        showReportsButton.onclick = () => showUserReports(user.id);
        
        const blockButton = document.createElement('button');
        blockButton.classList.add('btn', 'block-button');
        blockButton.textContent = 'Block';
        blockButton.onclick = () => blockUser(user.id);
        
        li.appendChild(userInfo);
        li.appendChild(showReportsButton);
        li.appendChild(blockButton);
        userUl.appendChild(li);
    });
}