import React from 'react';

const MessageLog = ({ messages }) => {
    return (
        <div>
            <h2>Messages:</h2>
            <ul>
                {messages.map((msg, index) => (
                    <li key={index}>
                        <strong>{msg.user.Username}:</strong> {msg.message.Content}
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default MessageLog;
