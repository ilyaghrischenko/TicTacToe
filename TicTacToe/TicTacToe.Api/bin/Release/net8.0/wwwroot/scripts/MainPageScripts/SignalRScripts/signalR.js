async function initiateConnection(userId) {
    window.connection = new signalR.HubConnectionBuilder()
        .withUrl(`/gameHub?userId=${encodeURIComponent(userId)}`)
        .build();

    await connection.start().then(() => {
        console.log('SignalR Connected');
    }).catch(err => console.error(err.toString()));
}