'use strict';

const loginForm = document.querySelector('#change-avatar-form');
const avatarButton = document.querySelector('#avatar-button');

document.getElementById('avatarImage').addEventListener('change', function (event) {
    event.preventDefault();
    
    const file = event.target.files[0];
    if (file) {
        const validImageTypes = ['image/jpeg', 'image/png', 'image/jpg'];
        if (!validImageTypes.includes(file.type)) {
            alert('Please, upload an image in JPEG, PNG or JPG format.');
            event.target.value = '';
        }
    }
});

loginForm.addEventListener('submit', async (e) => {
    e.preventDefault();
    const token = sessionStorage.getItem('token');

    if (!token) {
        window.location.href = '../pages/auth.html';
        return;
    }

    const imageInput = document.getElementById('avatarImage');
    if (imageInput.files.length === 0) {
        alert("Please, select a file!");
        return;
    }

    avatarButton.disabled = true;
    avatarButton.innerText = 'Saving...';
    avatarButton.style.backgroundColor = 'black';
    avatarButton.style.cursor = 'wait';

    const file = imageInput.files[0];

    const formData = new FormData();
    formData.append('avatar', file);
    
    try {
        await fetch('/api/Settings/changeAvatar', {
            method: 'POST',
            headers: {
                'Authorization': `Bearer ${token}`
            },
            body: formData
        })
            .then(response => {
                if (!response.ok) {
                    if (response.status === 403) {
                        window.history.back();
                        return;
                    }
                    
                    console.error('Error changing avatar:', response.status);
                }
                else {
                    window.location.reload();
                }
            })
            .catch(error => {
                console.error('Error:', error);
            });
    } catch (error) {
        alert('Network exception occurred');
        console.error('Network error:', error);
        window.location.href = '../pages/auth.html';
    }
});
