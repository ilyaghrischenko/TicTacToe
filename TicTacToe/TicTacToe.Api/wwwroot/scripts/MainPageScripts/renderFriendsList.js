'use strict'

async function renderFriendsList(friends, friendList) {
    friendList.innerHTML = '';

    const ids = [];
    friends.forEach(user => {
        const li = document.createElement('li');
        li.classList.add('user-item');

        const userInfo = document.createElement('div');
        userInfo.classList.add('friend-info');

        const avatarImg = document.createElement('img');
        avatarImg.src = user.avatar && user.avatar.length > 0 ? `data:image/png;base64,${user.avatar}` : '../images/avatar.png';
        avatarImg.alt = `${user.login}'s avatar`;
        avatarImg.classList.add('avatar');
        avatarImg.onclick = () => showProfileModal(avatarImg.src, user.login, user.statistic);
        userInfo.appendChild(avatarImg);

        const userName = document.createElement('span');
        userName.textContent = user.login;
        userInfo.appendChild(userName);
        
        const onlineStatus = document.createElement('span');
        onlineStatus.classList.add('online-status');
        onlineStatus.id = `online-status-${user.id}`;
        userInfo.appendChild(onlineStatus);

        const button = document.createElement('button');
        button.classList.add('btn', 'invite-button');
        button.textContent = 'Invite';
        button.onclick = () => inviteToGame(user.id, button);

        ids.push(user.id);
        li.appendChild(userInfo);
        li.appendChild(button);
        friendList.appendChild(li);
    });
    
    await renderFriendsOnlineStatus(ids);
}

async function renderFriendsOnlineStatus(ids) {
    try {
        const friendsStatuses = await connection.invoke("GetFriendsOnlineStatus", ids);

        for (const friendId in friendsStatuses) {
            if (friendsStatuses.hasOwnProperty(friendId)) {
                const status = friendsStatuses[friendId];
                const statusElement = document.getElementById(`online-status-${friendId}`);
                if (statusElement) {
                    if (status) {
                        statusElement.textContent = '';
                        statusElement.classList.add('online');
                        statusElement.classList.remove('offline');
                    } else {
                        statusElement.textContent = '';
                        statusElement.classList.add('offline');
                        statusElement.classList.remove('online');
                    }
                }
            }
        }
    }
    catch (error) {
        console.error('Error fetching friends online status:', error);
    }
}