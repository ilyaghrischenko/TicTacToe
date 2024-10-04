'use strict';

async function blockUser(userId) {
    const token = sessionStorage.getItem('token');

    if (!token) {
        window.location.href = '../pages/auth.html';
    }

    try {
        const response = await fetch("/api/Admin/blockUser", {
            method: 'POST',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(userId)
        });
        
        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(`Ошибка при блокировке пользователя: ${errorText}`);
        }
    } catch (error) {
        console.error('Ошибка:', error.message);
    }
}