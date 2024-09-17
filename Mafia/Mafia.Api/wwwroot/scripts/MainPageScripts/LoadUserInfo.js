document.addEventListener('DOMContentLoaded', async function () {
    const token = sessionStorage.getItem('jwtToken'); // Получаем токен из sessionStorage

    if (!token) {
        // Если токен отсутствует, перенаправляем на страницу авторизации
        window.location.href = '../pages/auth.html';
        return;
    }

    try {
        const response = await fetch('/api/Main/userinfo', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`, // Добавляем токен в заголовок Authorization
                'Content-Type': 'application/json'
            }
        });

        if (response.ok) {
            const userData = await response.json();
            document.getElementById('login').textContent = userData.login;
            document.getElementById('wins').textContent = userData.wins;
            document.getElementById('losses').textContent = userData.losses;
        } else {
            // В случае ошибки (например, невалидный токен), перенаправляем на страницу входа
            window.location.href = '../pages/auth.html';
        }
    } catch (error) {
        console.error('Network error:', error);
        window.location.href = '../pages/auth.html'; // Перенаправляем на страницу входа в случае ошибки
    }
});
