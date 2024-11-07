'use strict'

function renderFriendsList(friends, friendList) {
    friendList.innerHTML = '';

    friends.forEach(user => {
        const li = document.createElement('li');
        li.classList.add('user-item');

        const userInfo = document.createElement('div');
        userInfo.classList.add('friend-info');

        const avatarImg = document.createElement('img');
        avatarImg.src = user.avatar && user.avatar.length > 0 ? `data:image/png;base64,${user.avatar}` : '../images/avatar.png';
        avatarImg.alt = `${user.login}'s avatar`;
        avatarImg.classList.add('avatar');
        userInfo.appendChild(avatarImg);

        const userName = document.createElement('span');
        userName.textContent = user.login;
        userInfo.appendChild(userName);

        const button = document.createElement('button');
        button.classList.add('btn', 'invite-button');
        button.textContent = 'Invite';
        button.onclick = () => inviteToGame(user.id, button);

        li.appendChild(userInfo);
        li.appendChild(button);
        friendList.appendChild(li);
    });
}
