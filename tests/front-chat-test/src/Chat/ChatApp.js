import React, { useEffect, useState } from 'react';
import { HubConnectionBuilder } from '@microsoft/signalr';

const ChatApp = () => {
    const [connection, setConnection] = useState(null);
    const [message, setMessage] = useState('');
    const [messages, setMessages] = useState([]);

    useEffect(() => {

        const token = `eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoic3RyaW5nIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiUmVndWxhclVzZXIiLCJleHAiOjE3Mjc1MDE0OTEsImlzcyI6IlBldENvbm5lY3QiLCJhdWQiOiJsb2NhbGhvc3QifQ.sSWGCy4Q7IaWgQq-Si6EiKFoED0BiaBEee3CzfB02fg`; // Получаем токен из локального хранилища
        const connect = new HubConnectionBuilder()
            .withUrl(`http://localhost:3000/hubs/chat`, { accessTokenFactory: () => token }) // Используйте обратные кавычки для шаблонной строки
            .withAutomaticReconnect()
            .build();

        connect.start()
            .then(() => {
                console.log('Connected to SignalR hub');

                connect.on('ReceivedMessage', (chatMessage, chatUser) => {
                    setMessages(prevMessages => [
                        ...prevMessages,
                        { user: chatUser.username, content: chatMessage.content }
                    ]);
                });
            })
            .catch(err => console.error('Connection error: ', err));
          
        setConnection(connect);

        return () => {
            connect.stop();
        };
    }, []);

    const sendMessage = async () => {
        if (connection && message) {
            await connection.invoke('SendMessage', message);
            setMessage('');
        }
    };

    return (
        <div>
            <h2>Chat</h2>
            <div style={{ height: '300px', overflowY: 'scroll', border: '1px solid #ccc', padding: '10px' }}>
                {messages.map((msg, index) => (
                    <div key={index}>
                        <strong>{msg.user}: </strong>{msg.content}
                    </div>
                ))}
            </div>
            <input
                type="text"
                value={message}
                onChange={(e) => setMessage(e.target.value)}
                placeholder="Введите сообщение"
                style={{ width: '80%', marginRight: '10px' }}
            />
            <button onClick={sendMessage}>Отправить</button>
        </div>
    );
};

export default ChatApp;
