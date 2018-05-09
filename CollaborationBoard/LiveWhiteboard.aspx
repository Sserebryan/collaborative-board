<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LiveWhiteboard.aspx.cs" Inherits="LiveWhiteBoard.LiveWhiteboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Live WhiteBoard</title>
    <script src="Scripts/jquery-1.12.4.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery-ui-1.12.1.min.js"></script>
    <script src="Scripts/jquery.signalR-2.2.2.js"></script>
    <script src="<%: ResolveClientUrl("~/signalr/hubs") %>"></script> 
    <script type="text/javascript" src="Scripts/CollaborationBoard/canvas.js"> </script>
    <script type="text/javascript" src="Scripts/CollaborationBoard/whiteboardHub.js"> </script>
    <link href="Content/style.css" rel="stylesheet" />
    <link href="Content/themes/base/all.css" rel="stylesheet" />
</head>
<body onload="init()"  >
    <form id="form1" runat="server">
   <div id="dialog-form" hidden="hidden">
    <div id="divJoinMeeting" style="font-size: medium">
        </div>
       
    <hr />
    <br />
    <div style="font-size: small">
        <i>Name to display:</i>
    </div>
    <table width="100%">
        <tr>
            <td>
                <input type="text" id="name" class="text ui-widget-content ui-corner-all" maxlength="25" />
            </td>
            <tr>
                <td align="right">
                    <input id="btnJoin" type="button" value="Join">
                </td>
            </tr>
    </table>
</div>
    <div id="error" ></div>
    <div>
        <div class="leftside">
            <canvas id="sketchboard" width="400" height="400" ></canvas>
            <div class="color-pallette">
                <div>Choose Color</div>
                <div class="color-box" style="background:green;" id="green" onclick="color(this)"></div>
                <div class="color-box" style="background:blue;" id="blue" onclick="color(this)"></div>
                <div class="color-box" style="background:red;" id="red" onclick="color(this)"></div>
                <div class="color-box" style="background:yellow;" id="yellow" onclick="color(this)"></div>
                <div class="color-box" style="background:orange;" id="orange" onclick="color(this)"></div>
                <div class="color-box" style="background:black;" id="black" onclick="color(this)"></div>
                <div class="tools">
                <div>Tools</div>
                <div id="white" onclick="color(this)"><img id="imgeraser" src="Images/eraser.jpg" title="erase" /></div>
                <img id="imgclear" src="Images/clear.jpg" title="clear canvas" onclick="erase()" />
                <br />
                <img id="imgshare" src="Images/share.jpg" title="Share" onclick="shareLink()" />
                <br />
                <img id="imgundo" src="Images/undo.png" title="Undo" onclick="undoSketch()" />
                </div>
            </div>
            
        </div>

        <div id="divShare" class="rightside" hidden="hidden">
                <table style="table-layout: fixed;">
                    <colgroup>
                        <col />
                    </colgroup>
                    <tbody>
                        <tr>
                            <td>
                                <div class="invite">
                                <span><i><b>Invite : </b>Share the whiteboard and collaborate with others using below link </i>
                                    <br />
                                    <a id="sharedurl" target="_blank" ></a>                                   
                                </span>
                                    </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        <br />
        <div id="divTextShare" class="rightside">
            <div id="divMessage">
            </div>
            <textarea rows="2" cols="25" id="txtMessage"></textarea>
            <input type="button" id="btnSend" value="Send" />
        </div>
    </div>
        <input  type="hidden" id="groupName"  />
        <input  type="hidden" id="userName" />
 </form>
</body>
</html>
