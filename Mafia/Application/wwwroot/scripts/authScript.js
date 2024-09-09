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
        const errors = Object.keys(data.errors).map(key=>({
            Field: key, 
            Errors: data.errors[key]
        }));
        const errorDiv = document.getElementById('errors');
        errorDiv.innerHTML = '';
        //
        console.dir(errors);
        //
        errors.forEach(error => {
            const errorElement = document.createElement('div');
            errorElement.className = 'error';
            errorElement.innerText = `${error.field}: ${error.errors.join(', ')}`;
            errorDiv.appendChild(errorElement);
        });
    } else {
        console.log(`Successful login: ${response.status}`);
    }
});