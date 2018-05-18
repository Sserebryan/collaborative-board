# Collaboration board

A web application made with SignalR and ASP.NET. This is old project from 2014.

ASP.NET is an open-source server-side web application framework designed for web development to produce dynamic web pages. It was developed by Microsoft to allow programmers to build dynamic web sites, web applications and web services.

SignalR is a software library for Microsoft ASP.NET that allows server code to send asynchronous notifications to client-side web applications. The library includes server-side and client-side JavaScript components.

jQuery is a cross-platform JavaScript library designed to simplify the client-side scripting of HTML. It is free, open-source software using the permissive MIT License.

## Tests

For tests using XUnit library

Tests is in CollaborationBoard.Tests project(UnitTest1.cs file).

xUnit.net is a free, open source, community-focused unit testing tool for the .NET Framework. Written by the original inventor of NUnit v2, xUnit.net is the latest technology for unit testing C#, F#, VB.NET and other .NET languages

### list of methods that have been tested

1. PublishDraw
2. PublishChatMesssage
3. ClearCanvas
4. JoinGroup
5. UndoCanvasSketchTest

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
