'use strict';

const bugsPageBtn = document.getElementById('bugsPageBtn');
bugsPageBtn.addEventListener('click', function() {
    console.log(bugsPageBtn.innerHTML);
    window.location.href = '../pages/bugs.html';
});