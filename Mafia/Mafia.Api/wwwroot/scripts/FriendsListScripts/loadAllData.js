'use strict';

const token = sessionStorage.getItem('token'); // Получаем токен из sessionStorage
if (!token) {
    window.location.href = '../pages/auth.html'; // Перенаправляем на страницу авторизации
}

const userList = document.getElementById('user-list');
const friendList = document.getElementById('friend-list'); // Не забудьте добавить это в HTML

async function fetchData(url) {
    try {
        const response = await fetch(url, {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        });

        if (!response.ok) {
            throw new Error('Network response was not ok');
        }

        return response.json();
    } catch (error) {
        console.error('Network error:', error);
        window.location.href = '../pages/auth.html'; // Перенаправляем на страницу входа в случае ошибки
        throw error; // Перекидываем ошибку, чтобы прервать выполнение
    }
}

function populateList(listElement, items, itemType, buttonClass) {
    items.forEach(item => {
        const listItem = document.createElement('li');
        listItem.textContent = item[itemType];
        listItem.innerHTML += `<button class="${buttonClass}">
            <img src="../img/add-icon.png" alt="Add">
        </button>`;
        listElement.appendChild(listItem);
    });
}

document.addEventListener('DOMContentLoaded', async function () {
    try {
        const users = await fetchData('/api/User/getAllUsers');
        if (users.length > 0) {
            populateList(userList, users, 'login', 'add-button');
        }

        const friends = await fetchData('/api/User/getFriends');
        if (friends.length > 0) {
            populateList(friendList, friends, 'name', 'delete-button');
        }
    } catch (error) {
        // Дополнительная обработка ошибок, если нужно
        console.error('Error occurred:', error);
        // Перенаправляем на страницу входа
        window.location.href = '../pages/auth.html';
    }
});