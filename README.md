# Collaboration board

A web application made with signalR and ASP.NET.

## Build Setup
You need the following soft to open the application:

Visual Studio 2017 (x86/x64)

Some tests is in CollaborationBoard.Tests project

## API

## API METHODS

Methods accept and return data in a format [JSON](https://developer.mozilla.org/ru/docs/Web/JavaScript/Reference/Global_Objects/JSON)

## WS: PublishDraw
The method draws a point on canvas

```json 
{"x" : "123", "y": "1", "name" :"nname", "groupName": "groupName"}
```

## WS: PublishChatMesssage
The method publish message.

```json 
{
    "name" :"nname", "groupName": "groupName", "message": "message",
}
```

## WS: ClearCanvas
The method clearCanvas

```json 
{
    "name" :"nname", "groupName": "groupName", "message": "message",
}
```

## WS: JoinGroup

The method set id of room

```json 
{
    "groupName": "1",
}
```
