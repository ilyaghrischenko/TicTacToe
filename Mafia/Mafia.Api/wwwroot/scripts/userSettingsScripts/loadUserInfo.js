document.addEventListener('DOMContentLoaded', async function () {
    const token = sessionStorage.getItem('token'); // Получаем токен из sessionStorage

    if (!token) {
        // Если токен отсутствует, перенаправляем на страницу авторизации
        window.location.href = '../pages/auth.html';
        return; // Прерываем выполнение кода
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
            document.getElementById('email').textContent = userData.email;
            if (userData.avatar !== null) {
                const imageBlob = await userData.avatar.blob(); // Получаем Blob
                const imageUrl = URL.createObjectURL(imageBlob); // Создаем URL из Blob
                document.getElementById('avatar').src = imageUrl;
            }
            else {
                document.getElementById('avatar').src = '../images/avatar.png';
            }

            console.dir(userData);
        } else {
            alert('Ошибка при получении данных пользователя');
            // В случае ошибки (например, невалидный токен), перенаправляем на страницу входа
            window.location.href = '../pages/auth.html';
        }
    } catch (error) {
        alert('Network exception occurred');
        console.error('Network error:', error);
        window.location.href = '../pages/auth.html'; // Перенаправляем на страницу входа в случае ошибки
    }
});
