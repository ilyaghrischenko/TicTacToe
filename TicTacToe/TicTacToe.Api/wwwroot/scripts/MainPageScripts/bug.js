function renderBugModal() {
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

    // Выпадающий список для TriggeredAction
    const actionSelect = document.createElement("select");
    actionSelect.id = "triggeredAction";
    const actionOptions = ["Action 1", "Action 2", "Action 3"];
    actionOptions.forEach(action => {
        const option = document.createElement("option");
        option.value = action;
        option.textContent = action;
        actionSelect.appendChild(option);
    });

    // Поле для ввода описания
    const descriptionTextarea = document.createElement("textarea");
    descriptionTextarea.id = "description";
    descriptionTextarea.placeholder = "Enter your description...";

    // Выпадающий список для приоритетности
    const prioritySelect = document.createElement("select");
    prioritySelect.id = "priority";
    const priorityOptions = ["High", "Medium", "Low"];
    priorityOptions.forEach(priority => {
        const option = document.createElement("option");
        option.value = priority;
        option.textContent = priority;
        prioritySelect.appendChild(option);
    });

    // Кнопка отправки
    const sendBugBtn = document.createElement("button");
    sendBugBtn.id = "sendBugBtn";
    sendBugBtn.classList.add("btn", "bug-send-button");
    sendBugBtn.textContent = "Send";

    // Добавление всех элементов в модальное окно
    bugModalContent.appendChild(closeModal);
    bugModalContent.appendChild(bugModalHeader);
    bugModalContent.appendChild(actionSelect);
    bugModalContent.appendChild(descriptionTextarea);
    bugModalContent.appendChild(prioritySelect);
    bugModalContent.appendChild(sendReportBtn);

    bugModal.appendChild(bugModalContent);
    document.getElementById('central-panel').appendChild(bugModal);

    bugModal.style.display = "block";

    // Закрытие модального окна
    closeModal.onclick = function() {
        document.getElementById('central-panel').removeChild(bugModal);
    };

    // Обработка отправки
    sendBugBtn.onclick = function() {
        const triggeredAction = actionSelect.value;
        const description = descriptionTextarea.value;
        const priority = prioritySelect.value;

        if (description.trim() !== "") {
            // Логика отправки данных на сервер
            

            // Закрытие модального окна после отправки
            bugModal.style.display = "none";
            document.getElementById('central-panel').removeChild(bugModal);
        } else {
            console.error("Please enter a description.");
        }
    };

    // Закрытие модального окна при клике вне окна
    window.onclick = function(event) {
        if (event.target === bugModal) {
            document.getElementById('central-panel').removeChild(bugModal);
        }
    };
}
