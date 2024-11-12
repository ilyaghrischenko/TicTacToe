const token = sessionStorage.getItem("token");

if (token) {
    const payloadBase64 = token.split('.')[1];
    const payload = JSON.parse(atob(payloadBase64));

    const userRole = payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    if (userRole !== "Blocked") {
        window.history.back();
    }
} else {
    window.location.href = "../pages/auth.html";
}
