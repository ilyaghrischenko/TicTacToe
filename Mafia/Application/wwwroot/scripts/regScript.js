'use strict';

const form = document.getElementById('regForm');

form.addEventListener('submit', async (e) => {
    e.preventDefault();

    const email = document.getElementById('email').value;
    const login = document.getElementById('username').value;
    const password = document.getElementById('password').value;
    const confirmPassword = document.getElementById('confirm-password').value;

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

    const errorDiv = document.getElementById('errors');
    errorDiv.innerHTML = '';

    if (!response.ok) {
        const data = await response.json();
        const errors = data.errors;

        Object.keys(errors).forEach(field => {
            const errorMessages = errors[field];
            errorMessages.forEach(message => {
                const errorElement = document.createElement('div');
                errorElement.className = 'error';
                errorElement.innerText = `${field}: ${message}`;
                errorDiv.appendChild(errorElement);
            });
        });

        console.error(`Error while registering: ${response.status}`);
    } else {
        console.log(`Successful registration: ${response.status}`);
        window.location.href = '../pages/auth.html';
    }
});
