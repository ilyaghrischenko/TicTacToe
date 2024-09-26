'use strict'

async function addFriend(userId) {
    try {
        const response = await fetch(`/api/Friends/addFriend`, {
            method: 'POST',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(userId)
        });
    console.log(userId);
        if (response.ok) {
            await loadUsersNFriends();
        } else {
            alert(`Ошибка: ${response.status}`);
        }
    } catch (error) {
        console.error('Ошибка при добавлении друга:', error);
    }
}

// Функция для удаления друга
async function deleteFriend(userId) {
    try {
        const response = await fetch(`/api/Friends/deleteFriend`, {
            method: 'POST',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(userId)
        });
        console.log(userId);
        if (response.ok) {
            await loadUsersNFriends();
        } else {
            alert(`Ошибка: ${response.status}`);
        }
    } catch (error) {
        console.error('Ошибка при удалении друга:', error);
    }
}