'use strict';

//TODO: renderUserList нужно тут как-то использовать

const userUl = document.getElementById('user-list');
const token = sessionStorage.getItem('token');

if (!token) {
    window.location.href = '../pages/auth.html';
}

document.addEventListener('DOMContentLoaded', async (e) => {
    try {
        const response = await fetch('/api/User/getAllUsers', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        });

        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(`Ошибка при получении пользователей: ${errorText}`);
        }
        
        const users = await response.json();
        console.log('users:');
        console.dir(users);

        renderAllUsers(userUl, users);
    }
    catch (error) {
        console.error('Ошибка:', error.message);
    }
});