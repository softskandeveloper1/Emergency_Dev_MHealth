"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/Chat").build();

//Disable send button until connection is established  
//document.getElementById("sendBtn").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&").replace(/</g, "<").replace(/>/g, ">");
    //var encodedMsg = user + " says " + msg;
    //var li = document.createElement("li");
    //li.textContent = encodedMsg;
    //document.getElementById("ulmessages").appendChild(li);

    if ($("#com_id").val() == user) {
        $('<li class="replies"><img src="' + $("#img_" + user).attr("src") + '" alt="" /><p>' + msg + '</p></li>').appendTo($('.messages ul'));
    } else {
        //mark the user on the contact list
    }
});

//connection.start().then(function () {
//    document.getElementById("sendBtn").disabled = false;
//}).catch(function (err) {
//    return console.error(err.toString());
//});

//document.getElementById("sendBtn").addEventListener("click", function (event) {
//    var message = document.getElementById("message_input").value;
//    var com_id = document.getElementById("com_id").value;
//    connection.invoke("SendMessage",com_id, message).catch(function (err) {
//        return console.error(err.toString());
//    });
//    event.preventDefault();
//}); 


connection.start().then(function () {
    connection.invoke('join');
    //document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});




function load_conversations(user_id, cntrl) {
    $(".contact").removeClass("active");
    $(cntrl).addClass("active");

    $("#current_name").text($("#name_" + user_id).text());
    $("#current_img").attr("src", $("#img_" + user_id).attr("src"));
    $("#com_id").val(user_id);

    $.ajax({
        url: "/conversation/GetUserConversations?contact_id=" + user_id,
        method: "GET",
        success: function (data) {
            $(".messages > ul").empty();

            for (var i = 0; i < data.length; i++) {
                var cls = "sent";
                if (data[i].from == user_id) {
                    cls = "replies";
                }

                $('<li class="' + cls + '"><img src="' + $("#img_" + user_id).attr("src") + '" alt="" /><p>' + data[i].message + '</p></li>').appendTo($('.messages ul'));
            }
            $(".messages").animate({ scrollTop: $(document).height() }, "fast");
        }
    });

}

function newMessage() {
    var message = $(".message-input input").val();
    if ($.trim(message) == '') {
        return false;
    }

    //var message = document.getElementById("message_input").value;
    var com_id = document.getElementById("com_id").value;
    connection.invoke("SendMessage", com_id, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();


    $('<li class="sent"><img src="' + $("#current_img").attr("src")+'" alt="" /><p>' + message + '</p></li>').appendTo($('.messages ul'));
    $('.message-input input').val(null);
    $('.contact.active .preview').html('<span>You: </span>' + message);
    $(".messages").animate({ scrollTop: $(document).height() }, "fast");
};

$('.submit').click(function () {
    newMessage();
});

$(window).on('keydown', function (e) {
    if (e.which == 13) {
        newMessage();
        return false;
    }
});