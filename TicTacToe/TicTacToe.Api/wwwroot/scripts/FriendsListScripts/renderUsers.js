'use strict'

function renderUserList(users, friends) {
    friendList.innerHTML = '';
    userList.innerHTML = '';
    
    users.forEach(user => {
        const isFriend = friends.some(friend => friend.id === user.id);
        if (!isFriend && user.role === 2) {
            return;
        }
        
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
        wins.textContent = `Wins: ${user.statistic.wins}`;
        const losses = document.createElement('span');
        losses.textContent = `Losses: ${user.statistic.losses}`;
        userStats.appendChild(wins);
        userStats.appendChild(losses);

        userInfo.appendChild(userStats);

        const button = document.createElement('button');
        button.classList.add('btn', isFriend ? 'delete-button' : 'add-button');
        button.textContent = isFriend ? 'Delete' : 'Add';
        button.onclick = isFriend ? () => deleteFriend(user.id) : () => addFriend(user.id);

        li.appendChild(userInfo);
        li.appendChild(button);
        isFriend ? friendList.appendChild(li): userList.appendChild(li);
    });
}
