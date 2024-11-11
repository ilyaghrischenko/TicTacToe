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

const descriptionTextarea = document.createElement("textarea");
descriptionTextarea.id = "description";
descriptionTextarea.placeholder = "Enter your description...";

const prioritySelect = document.createElement("select");
prioritySelect.id = "priority";
const priorityOptions = ["High", "Medium", "Low"];

priorityOptions.forEach(priority => {
    const option = document.createElement("option");
    option.value = priority;
    option.textContent = priority;
    prioritySelect.appendChild(option);
});

const sendBugBtn = document.createElement("button");
sendBugBtn.id = "sendBugBtn";
sendBugBtn.classList.add("btn", "bug-send-button");
sendBugBtn.textContent = "Send";

bugModalContent.appendChild(closeModal);
bugModalContent.appendChild(bugModalHeader);
bugModalContent.appendChild(actionSelect);
bugModalContent.appendChild(descriptionTextarea);
bugModalContent.appendChild(prioritySelect);
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

    if (description.trim() !== "") {
        await sendBug(actionEnumValue, description, priorityEnumValue);
        bugModal.style.display = "none";
    } else {
        console.error("Please enter a description.");
    }
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

        if (!response.ok) {
            console.error("Error in sending bug:", response.status);
        } else {
            console.log("Bug sent successfully");
            await response.json();
        }
    } catch (error) {
        console.error("Error in sending bug:", error.toString());
    }
}
