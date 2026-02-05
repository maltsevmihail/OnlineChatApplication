import { WaitingRoom } from "./Components/WaitingRoom";
import { HubConnectionBuilder } from "@microsoft/signalr";
import { useState } from "react";
import { Chat } from "./Components/Chat";

function App() {
  const [connection, setConnection] = useState();
  const [chatName, setChatName] = useState("");
  const [messages, setMessages] = useState([]);

  const joinChat = async (userName, chatName) => {
    var connection = new HubConnectionBuilder()
      .withUrl("http://localhost:5229/chatHub")
      .withAutomaticReconnect()
      .build();

      connection.on("RecieveMessage", (userName, message) => {
        setMessages((messages) => [...messages, { userName, message }]);
      });

      try {
        await connection.start();
        await connection.invoke("JoinChat", { userName, chatName });

        setConnection(connection);
        setChatName(chatName);
      } catch (error) {
        console.log(error)
      }
  }

  const sendMessage = (message) => {
    connection.invoke("SendMessage", message);
  };

  const closeChat = async () => {
    await connection.stop();
    setConnection(null);
  }

  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-100">
      {connection ? (
        <Chat 
          messages={messages} 
          chatName={chatName} 
          sendMessage={sendMessage} 
          closeChat={closeChat}
        /> 
      ) : (
        <WaitingRoom 
          joinChat={joinChat}
        />
      )}
    </div>
  );
}

export default App;
