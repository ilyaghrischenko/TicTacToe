// Получаем элементы
const reportModal = document.getElementById("reportModal");
const reportBtn = document.getElementById("reportBtn");
const closeModal = document.getElementsByClassName("close")[0];
const sendReportBtn = document.getElementById("sendReportBtn");

// Открыть модальное окно при нажатии на кнопку "!"
reportBtn.onclick = function() {
    reportModal.style.display = "block";
}

// Закрыть модальное окно при нажатии на крестик
closeModal.onclick = function() {
    reportModal.style.display = "none";
}

// Закрыть модальное окно при нажатии вне окна
window.onclick = function(event) {
    if (event.target == reportModal) {
        reportModal.style.display = "none";
    }
}

// Обработка отправки жалобы (в данный момент просто скрываем окно)
sendReportBtn.onclick = function() {
    const reportText = document.getElementById("reportText").value;
    if (reportText.trim() !== "") {
        alert("Report sent: " + reportText); // Здесь можно добавить логику отправки данных
        reportModal.style.display = "none"; // Закрыть окно после отправки
        document.getElementById("reportText").value = ""; // Очистить поле ввода
    } else {
        alert("Please enter a report message.");
    }
}
