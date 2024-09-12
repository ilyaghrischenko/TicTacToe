'use strict';

const loginForm = document.querySelector('#loginForm');

loginForm.addEventListener('submit', async (e) => {
    e.preventDefault();
    
    const login = document.querySelector('#login').value;
    const password = document.querySelector('#password').value;
    
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
    
    //TODO: Handle errors and display them
    if (!response.ok) {
        const data = await response.json();
        const errors = data.errors;

        const errorDiv = document.getElementById('errors');
        errorDiv.innerHTML = '';

        Object.keys(errors).forEach(field => {
            const errorMessages = errors[field];
            errorMessages.forEach(message => {
                const errorElement = document.createElement('div');
                errorElement.className = 'error';
                errorElement.innerText = `${field}: ${message}`;
                errorDiv.appendChild(errorElement);
            });
        });
        
        console.error(`Errors while logging in: ${response.status}`);
    } else {
        console.log(`Successful login: ${response.status}`);
        window.location.href = '../pages/main.html';
    }
});