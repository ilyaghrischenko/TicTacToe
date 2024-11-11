'use strict'

const friendList = document.getElementById('friend-list');
const userList = document.getElementById('user-list');
const token = sessionStorage.getItem('token');

if (!token) {
    window.location.href = '../pages/auth.html';
}

async function loadUsersNFriends() {
    try {
        const [usersResponse, friendsResponse] = await Promise.all([
            fetch('/api/User/getAllUsers', {
                method: 'GET',
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': 'application/json'
                }
            }),
            fetch('/api/User/getFriends', {
                method: 'GET',
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': 'application/json'
                }
            })
        ]);

        if (!usersResponse.ok) {
            if (usersResponse.status === 403) {
                window.history.back();
                return;
            }

            const errorText = await usersResponse.text();
            throw new Error(`Ошибка при получении пользователей: ${errorText}`);
        }

        if (!friendsResponse.ok) {
            if (friendsResponse.status === 403) {
                window.history.back();
                return;
            }

            const errorText = await friendsResponse.text();
            throw new Error(`Ошибка при получении друзей: ${errorText}`);
        }

        const users = await usersResponse.json();
        const friends = await friendsResponse.json();

        renderUserList(users, friends);
    } catch (error) {
        console.error('Ошибка:', error.message);
    }
}

document.addEventListener('DOMContentLoaded', async (e) => {
    await loadUsersNFriends();
});