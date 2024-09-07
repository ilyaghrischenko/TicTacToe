'use strict';

const form = document.getElementById('regForm');

form.addEventListener('submit', async (e) => {
    e.preventDefault();
    
    const email = document.getElementById('email').value;
    const login = document.getElementById('username').value;
    const password = document.getElementById('password').value;
    const confirmPassword = document.getElementById('confirm-password').value;
    
    if (password !== confirmPassword) {
        document.getElementById('confirm-password').focus();
        alert('Passwords do not match');
        return;
    }
    
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
    
    if (!response.ok) {
        console.error(`Error while register: ${response.status}`);
    } else {
        console.log(`Successful register: ${response.status}`);
    }
});