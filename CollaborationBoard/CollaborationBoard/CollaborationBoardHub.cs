using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using CollaborationBoard.Models;
using System.Configuration;

namespace CollaborationBoard
{
    public class WhiteboardHub : Hub
    {
        private static Dictionary<string, DrawCommannd> _groupWiseSketchCommand = new Dictionary<string, DrawCommannd>();
        int capacity = Convert.ToInt32(ConfigurationManager.AppSettings["StackCapacity"]);
        public void JoinGroup(string groupName)
        {
            try
            {
                if (!_groupWiseSketchCommand.ContainsKey(groupName))
                {
                    _groupWiseSketchCommand.Add(groupName, new DrawCommannd(new Stack<MetaData>(capacity)));
                }
                Groups.Add(Context.ConnectionId, groupName);
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
            }
        }
        public void JoinChat(string name, string groupName)
        {
            Clients.Group(groupName).chatJoined(name);
        }

        public void PublishChatMesssage(string message, string name, string groupName)
        {
            Clients.Group(groupName).broadcastChatMessage(name, message);
        }

        public void PublishDraw(MetaData linecordinates, string name, string groupName)
        {
            try
            {
                if (_groupWiseSketchCommand.ContainsKey(groupName))
                {
                    _groupWiseSketchCommand[groupName].sketch = linecordinates;
                    _groupWiseSketchCommand[groupName].Do();
                }

                var returnData = JsonConvert.SerializeObject(linecordinates);
                Clients.Group(groupName).broadcastSketch(returnData);
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
            }
        }

        public void ClearCanvas(string name, string groupName)
        {
            Clients.Group(groupName).clearCanvas();
        }

        public void UndoCanvasSketch(string name, string groupName)
        {
            try
            {
                if (_groupWiseSketchCommand.ContainsKey(groupName))
                {
                    var returnData = JsonConvert.SerializeObject(_groupWiseSketchCommand[groupName].UnDo());
                    Clients.Group(groupName).broadcastUndoCanvas(returnData);
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
            }
        }

    }
}
