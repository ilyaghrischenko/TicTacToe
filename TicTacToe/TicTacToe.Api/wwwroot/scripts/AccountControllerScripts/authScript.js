'use strict';

const loginForm = document.querySelector('#loginForm');

loginForm.addEventListener('submit', async (e) => {
    e.preventDefault();

    const login = document.getElementById('Login').value;
    const password = document.getElementById('Password').value;
    try {
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

        document.querySelectorAll('.tooltip').forEach(tooltip => tooltip.remove());
        if (!response.ok) {
            const {errors} = await response.json();
            Object.keys(errors).forEach(field => {
                const inputField = document.getElementById(field);
                if (inputField) {
                    showTooltip(inputField, errors[field].join(', '));
                } else {
                    console.warn(`Field with ID "${field}" not found in form`);
                }
            });
        } else {
            const data = await response.json();
            const token = data.access_token;

            sessionStorage.setItem('token', token);
            
            if (data.user_role === 'User') {
                window.location.href = '../pages/main.html';
            }
            else if (data.user_role === 'Admin') {
                window.location.href = '../pages/admin.html';
            }
        }
    } catch (error) {
        alert('User not found\nPlease check your login and password');
    }
});


function showTooltip(inputField, message) {
    const tooltip = document.createElement('div');
    tooltip.className = 'error-tooltip';
    tooltip.innerText = message;

    inputField.parentNode.appendChild(tooltip);
    inputField.classList.add('input-error');
}
