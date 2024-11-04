'use strict';

const changeEmailForm = document.querySelector('#change-email-form');

changeEmailForm.addEventListener('submit', async (e) => {
    e.preventDefault();

    const token = sessionStorage.getItem('token');
    if (!token) {
        window.location.href = '../pages/auth.html';
        return;
    }

    try {

        const newEmail = {
            EmailInput: document.getElementById('EmailInput').value
        }


        const response = await fetch('/api/Settings/changeEmail', {
            method: 'POST',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(newEmail),
        });

        document.querySelectorAll('.tooltip').forEach(tooltip => tooltip.remove());
        if (!response.ok) {
            if (response.status === 403) {
                window.history.back();
                return;
            }
            
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
