'use strict';

async function showUserReports(userId) {
    const token = sessionStorage.getItem('token');

    if (!token) {
        window.location.href = '../pages/auth.html';
        return;
    }

    try {
        const response = await fetch("/api/Admin/getUserReports", {
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
            } else if (response.status === 401) {
                window.location.href = '../pages/main.html';
                return;
            }

            const errorText = await response.text();
            throw new Error(`Ошибка при получении отчётов: ${errorText}`);
        }

        const data = await response.json();

        const modal = document.getElementById('reportModal');
        const reportList = document.getElementById('report-list');
        reportList.innerHTML = '';

        data.forEach(report => {
            const li = document.createElement('li');
            const date = new Date(report.createdAt);
            const formattedDate =
                `${date.toLocaleDateString('ru-RU')} ${date.toLocaleTimeString('ru-RU', {
                    hour: '2-digit',
                    minute: '2-digit'
                })}`;
            // li.textContent = `${formattedDate}: ${report.message}`;

            li.innerHTML = `
                <span class="report-date">${formattedDate}</span>
                <span class="report-message">${report.message}</span>
            `;

            reportList.appendChild(li);
        });

        modal.style.display = 'block';
    } catch (error) {
        console.error('Ошибка:', error.message);
    }
}

const modal = document.getElementById('reportModal');
const closeBtn = document.querySelector('.close');

closeBtn.onclick = () => {
    modal.style.display = 'none';
};

window.onclick = event => {
    if (event.target === modal) {
        modal.style.display = 'none';
    }
};