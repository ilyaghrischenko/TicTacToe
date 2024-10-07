'use strict';

const userUl = document.getElementById('user-list');
const token = sessionStorage.getItem('token');

if (!token) {
    window.location.href = '../pages/auth.html';
}

document.addEventListener('DOMContentLoaded', async (e) => {
    try {
        const response = await fetch('/api/Admin/getAppealedUsers', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        });

        if (!response.ok) {
            if (response.status === 401) {
                window.location.href = '../pages/main.html';
            }
            
            const errorText = await response.text();
            throw new Error(`Ошибка при получении пользователей: ${errorText}`);
        }
        
        const users = await response.json();
        renderAllUsers(userUl, users);
    }
    catch (error) {
        console.error('Ошибка:', error.message);
    }
});