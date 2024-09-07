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
    
    if (!response.ok) {
        console.error(`Error while login: ${response.status}`);
    } else {
        console.log(`Successful login: ${response.status}`);
    }
});