'use strict';

const changeLoginForm = document.querySelector('#change-login-form');

changeLoginForm.addEventListener('submit', async (e) => {
    e.preventDefault();

    const token = sessionStorage.getItem('token');
    if (!token) {
        window.location.href = '../pages/auth.html';
        return;
    }

    try {

        const newLogin = {
            LoginInput: document.getElementById('LoginInput').value
        }

        const response = await fetch('/api/Settings/changeLogin', {
            method: 'POST',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(newLogin),
        });

        document.querySelectorAll('.tooltip').forEach(tooltip => tooltip.remove());
        if (!response.ok) {
            if (response.status === 403) {
                window.history.back();
                return;
            }
            
            const {errors} = await response.json();
            console.error('Error while changing login:', errors);
            Object.keys(errors).forEach(field => {
                const inputField = document.getElementById(field);
                if (inputField) {
                    showTooltip(inputField, errors[field].join(', '));
                } else {
                    console.warn(`Field with ID "${field}" not found in form`);
                }
            });
        } else {
            alert('Login successfully changed');
            window.location.reload();
        }
    } catch (error) {
        console.error('Error during request:', error);
    }
});

function showTooltip(inputField, message) {
    const tooltip = document.createElement('div');
    tooltip.className = 'error-tooltip';
    tooltip.innerText = message;

    inputField.parentNode.appendChild(tooltip);
    inputField.classList.add('input-error');
}
