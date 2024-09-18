document.addEventListener('DOMContentLoaded', async function () {
    const token = sessionStorage.getItem('token'); // Получаем токен из sessionStorage
    
    if (!token) {
        // Если токен отсутствует, перенаправляем на страницу авторизации
        window.location.href = '../pages/auth.html';
    }

    try {
        const response = await fetch('/api/User/getUser', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`, // Добавляем токен в заголовок Authorization
                'Content-Type': 'application/json'
            }
        });

        if (response.ok) {
            const userData = await response.json();
            document.getElementById('login').textContent = userData.login;
            
            const statistic = userData.statistic;
            document.getElementById('wins').textContent = statistic.wins;
            document.getElementById('losses').textContent = statistic.losses;
            
            console.dir(userData);
        } else {
            // В случае ошибки (например, невалидный токен), перенаправляем на страницу входа
            window.location.href = '../pages/auth.html';
        }
    } catch (error) {
        console.error('Network error:', error);
        window.location.href = '../pages/auth.html'; // Перенаправляем на страницу входа в случае ошибки
    }
});
