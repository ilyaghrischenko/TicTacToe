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
            body: JSON.stringify(status)
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

        const statusOptions = ['Unresolved', 'In Progress', 'Resolved'];
        const statusSelect = document.createElement("select");
        statusSelect.classList.add("status-select");
        statusSelect.value = statusOptions[bug.status];

        statusOptions.forEach((option, index) => {
            const optionElement = document.createElement("option");
            optionElement.value = index;
            optionElement.textContent = option;
            if (index === bug.status) {
                optionElement.selected = true;
            }
            statusSelect.appendChild(optionElement);
        });

        statusSelect.addEventListener('change', async () => {
            const token = sessionStorage.getItem('token');
            if (!token) {
                window.location.href = '../pages/auth.html';
                return;
            }
            
            const newStatus = parseInt(statusSelect.value);
            const requestBody = {
                id: bug.id,
                status: newStatus
            };
            
            try {
                const response = await fetch(`/api/Bug/changeBugStatus`, {
                    method: 'POST',
                    headers: {
                        'Authorization': `Bearer ${token}`,
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(requestBody)
                });

                if (response.ok) {
                    console.log(`Bug ${bug.id} status updated to ${newStatus}`);
                    await getBugs();
                } else {
                    console.error('Failed to update status:', await response.json());
                }
            } catch (error) {
                console.error('Error during request:', error);
            }
        });

        row.innerHTML = `
            <td class="description-column">${bug.description}</td>
            <td class="action-column">${bug.action}</td>
            <td class="importance-column">${bug.importance}</td>
            <td class="status-column"></td>
            <td class="date-column">${(new Date(bug.createdAt)).toLocaleDateString()}</td>
        `;

        row.querySelector('.status-column').appendChild(statusSelect);
        tableBody.appendChild(row);
    });
}

document.addEventListener("DOMContentLoaded", getBugs);
document.getElementById("getBugsButton").onclick = getBugs;
document.getElementById("getUnresolvedBugsButton").onclick = () => getBugsByStatus(0);
document.getElementById("getInProgressBugsButton").onclick = () => getBugsByStatus(1);
document.getElementById("getResolvedBugsButton").onclick = () => getBugsByStatus(2);