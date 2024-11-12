'use strict';

const bugButton = document.getElementById("bugReportButton");

const bugModal = document.createElement("div");
bugModal.id = "bugModal";
bugModal.classList.add("bug-modal");

const bugModalContent = document.createElement("div");
bugModalContent.classList.add("bug-modal-content");

const closeModal = document.createElement("span");
closeModal.classList.add("close");
closeModal.innerHTML = "&times;";

const bugModalHeader = document.createElement("h2");
bugModalHeader.textContent = "Bug Report";

const actionContainer = document.createElement("div");
actionContainer.classList.add("action-container");

const actionSelectLabel = document.createElement("p");
actionSelectLabel.classList.add("action-select-p");
actionSelectLabel.textContent = "Select the event that caused the error:";

const actionSelect = document.createElement("select");
actionSelect.id = "triggeredAction";
const actionOptions = [
    "Log in", "Sign up", "Invite to game", "Accept invite",
    "Reject Invite", "Game", "Statistics update", "Change login",
    "Change password", "Change email", "Change avatar",
    "Add friend", "Delete friend", "Other"
];

actionOptions.forEach(action => {
    const option = document.createElement("option");
    option.value = action;
    option.textContent = action;
    actionSelect.appendChild(option);
});

actionContainer.appendChild(actionSelectLabel);
actionContainer.appendChild(actionSelect);

const descriptionContainer = document.createElement("div");
descriptionContainer.classList.add("description-container");

const descriptionTextarea = document.createElement("textarea");
descriptionTextarea.id = "Description";
descriptionTextarea.placeholder = "Enter your description...";

descriptionContainer.appendChild(descriptionTextarea);

const priorityContainer = document.createElement("div");
priorityContainer.classList.add("priority-container");

const prioritySelectLabel = document.createElement("p");
prioritySelectLabel.classList.add("priority-select-p");
prioritySelectLabel.textContent = "Choose how important and urgent you think it is to solve this problem:";

const prioritySelect = document.createElement("select");
prioritySelect.id = "priority";
const priorityOptions = ["High", "Medium", "Low"];

priorityOptions.forEach(priority => {
    const option = document.createElement("option");
    option.value = priority;
    option.textContent = priority;
    prioritySelect.appendChild(option);
});

priorityContainer.appendChild(prioritySelectLabel);
priorityContainer.appendChild(prioritySelect);

const sendBugBtn = document.createElement("button");
sendBugBtn.id = "sendBugBtn";
sendBugBtn.classList.add("btn", "bug-send-button");
sendBugBtn.textContent = "Send";

bugModalContent.appendChild(closeModal);
bugModalContent.appendChild(bugModalHeader);
bugModalContent.appendChild(actionContainer);
bugModalContent.appendChild(descriptionContainer);
bugModalContent.appendChild(priorityContainer);
bugModalContent.appendChild(sendBugBtn);
bugModal.appendChild(bugModalContent);
document.getElementById('central-panel').appendChild(bugModal);

bugModal.style.display = "none";

bugButton.onclick = () => {
    bugModal.style.display = "block";
};

closeModal.onclick = () => {
    bugModal.style.display = "none";
};

window.onclick = event => {
    if (event.target === bugModal) {
        bugModal.style.display = "none";
    }
};

sendBugBtn.onclick = async () => {
    const description = descriptionTextarea.value;
    const triggeredAction = actionSelect.value;
    const priority = prioritySelect.value;

    const actionEnumValue = actionOptions.indexOf(triggeredAction);
    const priorityEnumValue = priorityOptions.indexOf(priority);

    await sendBug(actionEnumValue, description, priorityEnumValue);
};

async function sendBug(triggeredActionInt, description, importanceInt) {
    const token = sessionStorage.getItem('token');
    try {
        const response = await fetch("/api/Bug/sendBug", {
            method: "POST",
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                action: triggeredActionInt,
                description: description,
                importance: importanceInt
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
            console.log("Bug sent successfully");
            bugModal.style.display = "none";
        }
    } catch (error) {
        console.error("Error in sending bug:", error.toString());
    }
}

function showTooltip(inputField, message) {
    const tooltip = document.createElement('div');
    tooltip.className = 'error-tooltip';
    tooltip.innerText = message;

    inputField.parentNode.appendChild(tooltip);
    inputField.classList.add('input-error');
}
