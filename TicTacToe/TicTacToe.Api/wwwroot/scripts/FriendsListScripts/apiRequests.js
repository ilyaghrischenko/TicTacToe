'use strict'

async function addFriend(userId, button) {

    button.disabled = true;
    button.textContent = 'Adding...';
    button.style.backgroundColor = 'black';
    
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
            if (response.status === 403) {
                window.history.back();
                return;
            }
            
            alert(`Ошибка: ${response.status}`);
        }
    } catch (error) {
        console.error('Ошибка при добавлении друга:', error);
    }
}

async function deleteFriend(userId, button) {

    button.disabled = true;
    button.textContent = 'Deleting...';
    button.style.backgroundColor = 'black';
    
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
            if (response.status === 403) {
                window.history.back();
                return;
            }
            
            alert(`Ошибка: ${response.status}`);
        }
    } catch (error) {
        console.error('Ошибка при удалении друга:', error);
    }
}