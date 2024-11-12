'use strict';

const changePasswordForm = document.querySelector('#change-password-form');
const passwordButton = document.querySelector('#change-password-button');

changePasswordForm.addEventListener('submit', async (e) => {
    e.preventDefault();

    const token = sessionStorage.getItem('token');
    if (!token) {
        window.location.href = '../pages/auth.html';
        return;
    }

    passwordButton.disabled = true;
    passwordButton.innerText = 'Saving...';
    passwordButton.style.backgroundColor = 'black';
    passwordButton.style.cursor = 'wait';
    
    try {
        const newPassword = {
            ConfirmPassword: document.getElementById('ConfirmPassword').value,
            PasswordInput: document.getElementById('PasswordInput').value
        }


        const response = await fetch('/api/Settings/changePassword', {
            method: 'POST',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(newPassword),
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
    } finally {
        passwordButton.disabled = false;
        passwordButton.style.backgroundColor = 'var(--button-color)';
        passwordButton.innerText = 'Save';
        passwordButton.style.cursor = 'pointer';
    }
});

function showTooltip(inputField, message) {
    const tooltip = document.createElement('div');
    tooltip.className = 'error-tooltip';
    tooltip.innerText = message;

    inputField.parentNode.appendChild(tooltip);
    inputField.classList.add('input-error');
}
