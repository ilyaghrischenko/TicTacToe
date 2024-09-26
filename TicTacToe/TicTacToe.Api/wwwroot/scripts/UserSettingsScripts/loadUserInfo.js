document.addEventListener('DOMContentLoaded', async function () {
    const token = sessionStorage.getItem('token');

    if (!token) {
        window.location.href = '../pages/auth.html';
        return;
    }

    try {
        const response = await fetch('/api/User/getUser', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        });

        if (response.ok) {
            const userData = await response.json();
            document.getElementById('login').textContent = userData.login;
            document.getElementById('email').textContent = userData.email;

            if (userData.avatar !== null && userData.avatar.length > 0) {
                document.getElementById('avatar').src = `data:image/png;base64,${userData.avatar}`;
            } else {
                document.getElementById('avatar').src = '../images/avatar.png';
            }
        } else {
            window.location.href = '../pages/auth.html';
        }
    } catch (error) {
        alert('Network exception occurred');
        console.error('Network error:', error);
        window.location.href = '../pages/auth.html';
    }
});