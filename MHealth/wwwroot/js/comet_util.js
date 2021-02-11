function login(token){
var authToken = token;

CometChat.login(authToken).then(
  User => {
        console.log("Login successfully:", { User });
        $("#btn_call").removeAttr("disabled");
    // User loged in successfully.
        call_listener();
  },
  error => {
    console.log("Login failed with exception:", { error });
    // User login failed, check error and take appropriate action.
  }
);
}

function logout(){
    CometChat.logout().then(V=>{
        //Logout completed successfully
        console.log("Logout completed successfully");
    }, error => {
        //Logout failed with exception
        console.log("Logout failed with exception:", { error });
    });
}

function initiateCall(reciever_id){
    var receiverID = reciever_id;
    var callType = CometChat.CALL_TYPE.VIDEO;
    var receiverType = CometChat.RECEIVER_TYPE.USER;

    var call = new CometChat.Call(receiverID, callType, receiverType);

    CometChat.initiateCall(call).then(
      outGoingCall => {
        console.log("Call initiated successfully:", outGoingCall);
        // perform action on success. Like show your calling screen.
            sessionID = outGoingCall.sessionId;
      },
      error => {
        console.log("Call initialization failed with exception:", error);
      }
    );
}


function call_listener() {
    var listnerID = "UNIQUE_LISTENER_ID";
    CometChat.addCallListener(
        listnerID,
        new CometChat.CallListener({
            onIncomingCallReceived(call) {
                console.log("Incoming call:", call);
                // Handle incoming call
                $("#btn_accept").removeAttr("disabled");
                sessionID = call.sessionId;
            },
            onOutgoingCallAccepted(call) {
                console.log("Outgoing call accepted:", call);
                // Outgoing Call Accepted
                start_call();
            },
            onOutgoingCallRejected(call) {
                console.log("Outgoing call rejected:", call);
                // Outgoing Call Rejected
            },
            onIncomingCallCancelled(call) {
                console.log("Incoming call calcelled:", call);
            }
        })
    );
}

function accept_call(){

//var sessionID = "SESSION_ID";

CometChat.acceptCall(sessionID).then(
  call => {
    console.log("Call accepted successfully:", call);
    // start the call using the startCall() method
        start_call();
  },
  error => {
    console.log("Call acceptance failed with error", error);
    // handle exception
  }
);

}

function start_call(){
//var sessionID = "SESSION_ID";

CometChat.startCall(
  sessionID,
  document.getElementById("callScreen"),
  new CometChat.OngoingCallListener({
    onUserJoined: user => {
      /* Notification received here if another user joins the call. */
      console.log("User joined call:", user);
      /* this method can be use to display message or perform any actions if someone joining the call */
    },
    onUserLeft: user => {
      /* Notification received here if another user left the call. */
      console.log("User left call:", user);
      /* this method can be use to display message or perform any actions if someone leaving the call */
    },
    onCallEnded: call => {
      /* Notification received here if current ongoing call is ended. */
      console.log("Call ended:", call);
      /* hiding/closing the call screen can be done here. */
    }
  })
);
}



function end_call() {
    var sessionID = "SESSION_ID";
    var status = CometChat.CALL_STATUS.REJECTED;

    CometChat.rejectCall(sessionID, status).then(
        call => {
            console.log("Call rejected successfully", call);
        },
        error => {
            console.log("Call rejection failed with error:", error);
        });
}
