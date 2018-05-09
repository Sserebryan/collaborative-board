var canvas, ctx, flag = false,
    prevX = 0,
    currX = 0,
    prevY = 0,
    currY = 0,
    dot_flag = false;

var sketchColor = "black",
    sketchWidth = 2;

var applicationPath;
var whiteboardHub;
var sketchcanvas;

var mouseMoveEnum = {
    move: 0,
    down: 1,
    outside: 2,
    up: 3
};

function init() {

    controlMouseCursor();
    setGroupName();
    JoinUserToGroup();
    remvoeErrorMsg();

    $("#btnJoin").click(function (e) {
        $.event.trigger({
            type: "JoinClicked",
            message: {},
            time: new Date()
        });
    });

    $("#btnSend").click(
     function () {
         $.event.trigger({
             type: "sendChatClicked",
             message: $("#txtMessage").val().trim(),
             time: new Date()
         });         
     }
 );

    try {

        sketchcanvas = document.getElementById('sketchboard');
        w = sketchcanvas.width;
        h = sketchcanvas.height;

        if (sketchcanvas.getContext)
            ctx = sketchcanvas.getContext("2d");

        if (ctx) {
            sketchcanvas.addEventListener("mousemove", function (e) {
                raiseEvent(mouseMoveEnum.move, e)
            }, false);
            sketchcanvas.addEventListener("mousedown", function (e) {
                raiseEvent(mouseMoveEnum.down, e)
            }, false);
            sketchcanvas.addEventListener("mouseup", function (e) {
                raiseEvent(mouseMoveEnum.up, e)
            }, false);
            sketchcanvas.addEventListener("mouseout", function (e) {
                raiseEvent(mouseMoveEnum.outside, e)
            }, false);
        }
        else {
            document.write("Browser not supported !!!")
        }
    }
    catch (err) {
        notifyError(err);
    }
}


function color(obj) {
    switch (obj.id) {
        case "green":
            sketchColor = "green";
            break;
        case "blue":
            sketchColor = "blue";
            break;
        case "red":
            sketchColor = "red";
            break;
        case "yellow":
            sketchColor = "yellow";
            break;
        case "orange":
            sketchColor = "orange";
            break;
        case "black":
            sketchColor = "black";
            break;
        case "white":
            sketchColor = "white";
            break;
    }
    if (sketchColor == "white") sketchWidth = 14;
    else sketchWidth = 2;

}

function draw() {
    ctx.beginPath();
    ctx.moveTo(prevX, prevY);
    ctx.lineTo(currX, currY);
    ctx.strokeStyle = sketchColor;
    ctx.lineWidth = sketchWidth;
    ctx.stroke();
    ctx.closePath();
}

function raiseEvent(mouseState, e) {
    // raise only if mouse button pressed
    if (e.buttons > 0) {
        $.event.trigger({
            type: "drawSketch",
            message: { drawState: mouseState, currX: e.clientX, currY: e.clientY, color: sketchColor, width: sketchWidth },
            time: new Date()
        });
    }
}

function erase() {
    var m = confirm("Do you want to clear canvas ?");
    if (m) {
        //clearCanvas();
        $.event.trigger({
            type: "clearCanvas"
        });
    }
}

function undoSketch() {
    $.event.trigger({
        type: "undoSketch"
    });
}


function setDrawCordinates(parsedSketchData, isHistory) {
    sketchColor = parsedSketchData.color;
    sketchWidth = parsedSketchData.width;

    if (parsedSketchData.drawState == mouseMoveEnum.down) {
        prevX = currX;
        prevY = currY;
        currX = parsedSketchData.currX - sketchcanvas.offsetLeft;
        currY = parsedSketchData.currY - sketchcanvas.offsetTop;

        flag = true;
        dot_flag = true;
        if (dot_flag) {
            console.log("down");
            ctx.beginPath();
            ctx.fillStyle = sketchColor;
            ctx.fillRect(currX, currY, 2, 2);
            ctx.closePath();
            dot_flag = false;
        }
    }
    if (parsedSketchData.drawState == mouseMoveEnum.up || parsedSketchData.drawState == mouseMoveEnum.outside) {
        flag = false;
    }
    if (parsedSketchData.drawState == mouseMoveEnum.move) {
        if (flag || isHistory) {
            console.log("move");
            prevX = currX;
            prevY = currY;
            currX = parsedSketchData.currX - sketchcanvas.offsetLeft;
            currY = parsedSketchData.currY - sketchcanvas.offsetTop;
            draw();

        }
    }
}

function resetCanvasPen() {
    sketchColor = "black";
    sketchWidth = 2;
}

function controlMouseCursor() {
    $("#sketchboard").bind("mouseover", function () {
        $(this).css('cursor', 'url(images/cross_hair.png),auto')
    }).mouseout(function () {
        $(this).css('cursor', 'auto');
    });
}

function JoinUserToGroup() {
    $("#userName").val("");
    $("#dialog-form").dialog({
        autoOpen: false, width: 350, modal: true, closeOnEscape: false,
        open: function (event, ui) {
            $(".ui-dialog-titlebar-close", ui.dialog | ui).hide();
        }
    });
    $("#dialog-form").dialog("open");
    $("#name").keypress(function (e) {
        if (e && e.keyCode == 13) {
            $("#btnJoin").click();
        }
    });
}

function GetParameterValues(param) {
    var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < url.length; i++) {
        var urlparam = url[i].split('=');
        if (urlparam[0] == param) {
            return urlparam[1];
        }
    }
}

function generateGroupId() {
    var text = "";
    var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    for (var i = 0; i < 5; i++)
        text += possible.charAt(Math.floor(Math.random() * possible.length));

    return text;
}

function setGroupName() {
    var groupId = GetParameterValues("groupID");
    if (groupId == null) {
        $("#groupName").val(generateGroupId());
        $("#lblMeeting").val("Start Liveboard Meeting");
        $("#divJoinMeeting").append("<span>Start Liveboard Meeting</span>");
    } else {
        $("#groupName").val(groupId);
        $("#divJoinMeeting").append("<span>Join Liveboard Meeting</span>");
    }
}

function shareLink() {
    var shareurl = window.location.origin + window.location.pathname + "?groupID=" + $("#groupName").val();
    $("#sharedurl").attr('href', shareurl).text(shareurl);
    $("#divShare").removeAttr("hidden");
}

function notifyError(msg) {
    $('#error').html(msg);
    $("#error").addClass("error");
}

function remvoeErrorMsg() {
    $('#error').html("");
    $("#error").removeClass("error");
}



