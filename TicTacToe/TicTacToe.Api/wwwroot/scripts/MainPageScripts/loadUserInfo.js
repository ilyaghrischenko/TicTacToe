'use strict'

document.addEventListener('DOMContentLoaded', async function () {
    const token = sessionStorage.getItem('token');
    const frindsList = document.getElementById('friends-list');
    if (!token) {
        window.location.href = '../pages/auth.html';
    }

    try {
        const response = await fetch('/api/User/getUser', {
            method: 'GET', headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        });

        const friendsResponse = await fetch('/api/User/getFriends', {
            method: 'GET', headers: {
                'Authorization': `Bearer ${token}`, 'Content-Type': 'application/json'
            }
        });

        if (response.ok && friendsResponse.ok) {
            const userData = await response.json();
            document.getElementById('login').textContent = userData.login;

            const statistic = userData.statistic;
            document.getElementById('wins').textContent = statistic.wins;
            document.getElementById('losses').textContent = statistic.losses;
            
            sessionStorage.setItem('userId', userData.id);
            await initiateConnection(userData.id);
            await putOnEventHandlers();
            
            renderFriendsList(await friendsResponse.json(), frindsList);
            
            if (userData.avatar !== null && userData.avatar.length > 0) {
                document.getElementById('avatar').src = `data:image/png;base64,${userData.avatar}`;
            } else {
                document.getElementById('avatar').src = '../images/avatar.png';
            }
        } else {
            window.location.href = '../pages/auth.html';
        }
    } catch (error) {
        alert('Network error:', error.toString());
        window.location.href = '../pages/auth.html';
    }
});

