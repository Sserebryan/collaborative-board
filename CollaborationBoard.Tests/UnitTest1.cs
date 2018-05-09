using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNet.SignalR.Hubs;
using Moq;
using CollaborationBoard.Models;
using Microsoft.AspNet.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CollaborationBoard.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void JoinGroupTest()
        {
            // Arrange
            var groupManagerMock = new Mock<IGroupManager>();
            var connectionId = Guid.NewGuid().ToString();
            var groupsJoined = new List<string>();

            groupManagerMock.Setup(g => g.Add(connectionId, It.IsAny<string>()))
                            .Returns(Task.FromResult<object>(null))
                            .Callback<string, string>((cid, groupToJoin) =>
                                groupsJoined.Add(groupToJoin));

            var groupName = "TestGroup";

            var hub = new WhiteboardHub();
            hub.Groups = groupManagerMock.Object;
            hub.Context = new HubCallerContext(request: null,
                                                 connectionId: connectionId);

            // Act
            hub.JoinGroup(groupName);

            // Assert
            groupManagerMock.VerifyAll();
            Assert.AreEqual(1, groupsJoined.Count);
           
        }

        [TestMethod]
        public void JoinChatTest()
        {
            // Arrange.
            var hub = new WhiteboardHub();
            var mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();
            var groups = new Mock<IClientContract>();
            var name = "TestUser";
            var groupName = "TestGroup";

            hub.Clients = mockClients.Object;
            groups.Setup(_ => _.chatJoined(name)).Verifiable();
            mockClients.Setup(_ => _.Group(groupName)).Returns(groups.Object);

            // Act.
            hub.JoinChat(name, groupName);

            // Assert.
            groups.VerifyAll();
        }


        [TestMethod]
        public void PublishChatMessageTest()
        {
            // Arrange.
            var hub = new WhiteboardHub();
            var mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();
            var groups = new Mock<IClientContract>();
            var name = "TestUser";
            var message = "Hi I m Test User";
            var groupName = "TestGroup";

            hub.Clients = mockClients.Object;
            groups.Setup(x => x.broadcastChatMessage(name, message)).Verifiable();
            mockClients.Setup(_ => _.Group(groupName)).Returns(groups.Object);

            // Act.
            hub.PublishChatMesssage(message, name, groupName);

            // Assert.
            groups.VerifyAll();
        }

        [TestMethod]
        public void PublishDrawTest()
        {
            // Arrange.
            var hub = new WhiteboardHub();
            var mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();
            var groups = new Mock<IClientContract>();
            var name = "TestUser";
            var groupName = "TestGroup";
            MetaData linecordinates = new MetaData
            {
                Color = "black",
                CurrX = 10,
                CurrY = 20,
                DrawState = DrawState.move,
                Width = 20
            };

            var returnData = "{\"drawState\":0,\"currX\":10,\"currY\":20,\"color\":\"black\",\"width\":20}";

            hub.Clients = mockClients.Object;
            groups.Setup(x => x.broadcastSketch(returnData)).Verifiable();
            mockClients.Setup(_ => _.Group(groupName)).Returns(groups.Object);

            // Act.
            hub.PublishDraw(linecordinates, name, groupName);

            // Assert.
            groups.VerifyAll();
        }

        [TestMethod]
        public void ClearCanvasTest()
        {
            // Arrange.
            var hub = new WhiteboardHub();
            var mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();
            var groups = new Mock<IClientContract>();
            var name = "TestUser";
            var groupName = "TestGroup";

            hub.Clients = mockClients.Object;
            groups.Setup(x => x.clearCanvas()).Verifiable();
            mockClients.Setup(y => y.Group(groupName)).Returns(groups.Object);

            // Act.
            hub.ClearCanvas(name, groupName);

            // Assert.
            groups.VerifyAll();
        }

        [TestMethod]
        public void UndoCanvasSketchTest()
        {
            // Arrange.
            var hub = new WhiteboardHub();
            var mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();
            var groups = new Mock<IClientContract>();
            var groupManagerMock = new Mock<IGroupManager>();
            var connectionId = Guid.NewGuid().ToString();
            var groupsJoined = new List<string>();

            groupManagerMock.Setup(g => g.Add(connectionId, It.IsAny<string>()))
                            .Returns(Task.FromResult<object>(null))
                            .Callback<string, string>((cid, groupToJoin) =>
                                groupsJoined.Add(groupToJoin));

            
            
            var name = "TestUser";
            var groupName = "TestGroup";
            MetaData linecordinates1 = new MetaData
            {
                Color = "black",
                CurrX = 10,
                CurrY = 20,
                DrawState = DrawState.down,
                Width = 20
            };
            MetaData linecordinates2 = new MetaData
            {
                Color = "black",
                CurrX = 10,
                CurrY = 20,
                DrawState = DrawState.move,
                Width = 20
            };

            var returnData = "[{\"drawState\":0,\"currX\":10,\"currY\":20,\"color\":\"white\",\"width\":21},{\"drawState\":0,\"currX\":10,\"currY\":20,\"color\":\"white\",\"width\":21}]";

            hub.Clients = mockClients.Object;
            hub.Groups = groupManagerMock.Object;
            hub.Context = new HubCallerContext(request: null,connectionId: connectionId);

            groups.Setup(x => x.broadcastUndoCanvas(returnData)).Verifiable();
            mockClients.Setup(_ => _.Group(groupName)).Returns(groups.Object);
            


            // Act.
            hub.JoinGroup(groupName);
            hub.PublishDraw(linecordinates1, name, groupName);
            hub.PublishDraw(linecordinates2, name, groupName);
            hub.UndoCanvasSketch(name, groupName);

            // Assert.
            groups.VerifyAll();
        }
       
    }
}
