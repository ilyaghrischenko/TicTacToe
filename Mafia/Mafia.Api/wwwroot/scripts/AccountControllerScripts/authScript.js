'use strict';

const loginForm = document.querySelector('#loginForm');

loginForm.addEventListener('submit', async (e) => {
    e.preventDefault();

    const login = document.getElementById('Login').value;
    const password = document.getElementById('Password').value;

    const response = await fetch('/api/Account/login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            login,
            password
        })
    });

    const previousTooltips = document.querySelectorAll('.tooltip');
    previousTooltips.forEach(tooltip => tooltip.remove());

    if (!response.ok) {
        const data = await response.json();
        const errors = data.errors;

        Object.keys(errors).forEach(field => {
            const errorMessages = errors[field];
            const inputField = document.getElementById(field);

            if (inputField) {
                const tooltip = document.createElement('div');
                tooltip.className = 'tooltip';
                tooltip.innerText = errorMessages.join(', ');

                inputField.parentNode.appendChild(tooltip);
                inputField.classList.add('input-error');
            } else {
                console.warn(`Поле с ID "${field}" не найдено в форме`);
            }
        });

        console.error(`Error while registering: ${response.status}`);
    } else {
        sessionStorage.setItem('token', response.access_token);
        console.log(`Successful login: ${response.status}`);
        window.location.href = '../pages/main.html';
    }
});