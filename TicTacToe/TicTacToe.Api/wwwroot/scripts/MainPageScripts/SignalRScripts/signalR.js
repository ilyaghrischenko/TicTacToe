async function initiateConnection(userLogin) {
    window.connection = new signalR.HubConnectionBuilder()
        .withUrl(`/gameHub?userName=${encodeURIComponent(userLogin)}`)
        .build();

    await connection.start().then(() => {
        console.log('SignalR Connected');
    }).catch(err => console.error(err.toString()));
}