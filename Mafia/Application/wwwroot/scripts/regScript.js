'use strict';

const form = document.getElementById('regForm');

form.addEventListener('submit', async (e) => {
    e.preventDefault();

    const email = document.getElementById('Email').value;
    const login = document.getElementById('Login').value;
    const password = document.getElementById('Password').value;
    const confirmPassword = document.getElementById('ConfirmPassword').value;

    const response = await fetch('/api/Account/register', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            email,
            login,
            password,
            confirmPassword
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
        console.log(`Successful registration: ${response.status}`);
        window.location.href = '../pages/auth.html';
    }
});
