'use strict';

async function getBugs() {
    const token = sessionStorage.getItem('token');
    if (!token) {
        window.location.href = '../pages/auth.html';
        return;
    }
    
    try {
        const response = await fetch('/api/Bug/getBugs', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        });
        
        if (!response.ok) {
            console.error('Error during request:', await response.json());
        } else {
            const bugs = await response.json();
            renderBugs(bugs);
        }
        
    } catch (error) {
        console.error('Error during request:', error);
    }
}

async function getBugsByStatus(status) {
    const token = sessionStorage.getItem('token');
    if (!token) {
        window.location.href = '../pages/auth.html';
        return;
    }
    
    try {
        const response = await fetch('/api/Bug/getBugsByStatus', {
            method: 'POST',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                status: status
            })
        });

        if (!response.ok) {
            console.error('Error during request:', await response.json());
        } else {
            const bugs = await response.json();
            renderBugs(bugs);
        }
    } catch (error) {
        console.error('Error during request:', error);
    }
}

function renderBugs(bugs) {
    const tableBody = document.getElementById("bugsTableBody");
    tableBody.innerHTML = '';

    if (bugs.length === 0) {
        tableBody.innerHTML = '<tr><td colspan="5">No bugs</td></tr>';
        return;
    }
    
    bugs.forEach(bug => {
        const row = document.createElement("tr");
        row.classList.add("bug-item");

        row.innerHTML = `
            <td class="description-column">${bug.description}</td>
            <td class="action-column">${bug.action}</td>
            <td class="importance-column">${bug.importance}</td>
            <td class="status-column">${bug.status || 'Pending'}</td>
            <td class="date-column">${(new Date(bug.createdAt)).toLocaleDateString()}</td>
        `;

        tableBody.appendChild(row);
    });
}

document.addEventListener("DOMContentLoaded", getBugs);
document.getElementById("getBugsButton").onclick = getBugs;
document.getElementById("getUnresolvedBugsButton").onclick = () => getBugsByStatus(0);
document.getElementById("getInProgressBugsButton").onclick = () => getBugsByStatus(1);
document.getElementById("getResolvedBugsButton").onclick = () => getBugsByStatus(2);