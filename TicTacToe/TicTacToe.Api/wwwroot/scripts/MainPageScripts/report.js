function renderReportModal() {
    const reportModal = document.createElement("div");
    reportModal.id = "reportModal";
    reportModal.classList.add("modal");

    const reportModalContent = document.createElement("div");
    reportModalContent.classList.add("modal-content");

    const closeModal = document.createElement("span");
    closeModal.classList.add("close");
    closeModal.innerHTML = "&times;";

    const reportModalHeader = document.createElement("h2");
    reportModalHeader.textContent = "Report";

    const reportModalTextarea = document.createElement("textarea");
    reportModalTextarea.id = "reportText";
    reportModalTextarea.placeholder = "Enter your report...";

    const reportModalBtn = document.createElement("button");
    reportModalBtn.id = "sendReportBtn";
    reportModalBtn.classList.add("btn");
    reportModalBtn.textContent = "Send";

    reportModalContent.appendChild(closeModal);
    reportModalContent.appendChild(reportModalHeader);
    reportModalContent.appendChild(reportModalTextarea);
    reportModalContent.appendChild(reportModalBtn);

    reportModal.appendChild(reportModalContent);
    document.getElementById('central-panel').appendChild(reportModal);

    reportModal.style.display = "block";

    closeModal.onclick = function() {
        document.getElementById('central-panel').removeChild(reportModal);
    };

    reportModalBtn.onclick = function() {
        const reportText = reportModalTextarea.value;
        if (reportText.trim() !== "") {
            connection.invoke("SendReport", currentGameId, reportText).catch(function (err) {
                console.error("Error in sending report:", err.toString());
            });
            reportModal.style.display = "none";
            document.getElementById('central-panel').removeChild(reportModal);
        } else {
            console.error("Пожалуйста, введите сообщение для отчета.");
        }
    };

    window.onclick = function(event) {
        if (event.target === reportModal) {
            document.getElementById('central-panel').removeChild(reportModal);
        }
    };
}