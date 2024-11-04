'use strict';

async function blockUser(userId) {
    const token = sessionStorage.getItem('token');

    if (!token) {
        window.location.href = '../pages/auth.html';
        return;
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
            if (response.status === 403) {
                window.history.back();
                return;
            }
            else if (response.status === 401) {
                window.location.href = '../pages/auth.html';
                return;
            }
            
            const errorText = await response.text();
            throw new Error(`Ошибка при блокировке пользователя: ${errorText}`);
        }
        
        window.location.reload();
    } catch (error) {
        console.error('Ошибка:', error.message);
    }
}