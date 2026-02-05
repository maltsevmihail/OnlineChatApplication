
import { Button, Heading, Input, Text } from "@chakra-ui/react"
import { useState } from "react";

export const WaitingRoom = ({joinChat}) => {
    const [userName, setUserName] = useState();
    const [сhatName, setСhatName] = useState();

    const onSubmit = (e) => {
        e.preventDefault();
        joinChat(userName, сhatName);
    }

    return (
        <form onSubmit= {onSubmit}className="max-w-sm w-full bg-white p-8 rounded shadow-lg">
            <Heading>Онлайн чат</Heading>
            <div className="mb-4">
                <Text fontSize={"sm"}>Имя пользователя</Text>
                <Input 
                    onChange={(e) => setUserName(e.target.value)} 
                    name="userName" 
                    placeholder="Введите ваше имя" 
                />
            </div>
            <div className="mb-4">
                <Text fontSize={"sm"}>Название чата</Text>
                <Input 
                    onChange={(e) => setСhatName(e.target.value)}
                    name="chatName" 
                    placeholder="Введите название чата" 
                />
            </div>
            <Button type="submit" colorScheme="blue">Присоедениться</Button>
        </form>
    );
}