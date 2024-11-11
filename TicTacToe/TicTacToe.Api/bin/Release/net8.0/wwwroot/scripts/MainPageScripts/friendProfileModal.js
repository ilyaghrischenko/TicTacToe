'use strict';

function showProfileModal(avatar, login, statistic) {
    const modal = document.createElement('div');
    modal.onclick = () => modal.remove();
    modal.classList.add('friend-profile-modal');

    const modalContent = document.createElement('div');
    modalContent.classList.add('friend-profile-modal-content');
    
    const friendAvatar = document.createElement('img');
    friendAvatar.src = avatar;
    friendAvatar.alt = 'avatar';
    friendAvatar.classList.add('friend-profile-modal-avatar');
    
    const friendLogin = document.createElement('p');
    friendLogin.innerText = login;
    friendLogin.classList.add('friend-profile-modal-login');
    
    const statisticsContainer = document.createElement('div');
    statisticsContainer.classList.add('friend-profile-modal-statistics-container');
    
    const wins = document.createElement('p');
    wins.innerText = `Wins: ${statistic.wins}`;
    wins.classList.add('friend-profile-modal-statistic-p');
    
    const losses = document.createElement('p');
    losses.innerText = `Losses: ${statistic.losses}`;
    losses.classList.add('friend-profile-modal-statistic-p');
    
    statisticsContainer.appendChild(wins);
    statisticsContainer.appendChild(losses);
    
    modalContent.appendChild(friendAvatar);
    modalContent.appendChild(friendLogin);
    modalContent.appendChild(statisticsContainer);
    modal.appendChild(modalContent);
    document.body.appendChild(modal);
    
    modal.style.display = 'block';
}