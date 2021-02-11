const button = document.getElementById('join_leave');
const container = document.getElementById('container');
const count = document.getElementById('count');
var connected = false;
var muted = false;
var show = true;
var clientFirst = true;
var clinicianFirst = true;
var room;
function addLocalVideo() {
    Twilio.Video.createLocalVideoTrack().then(track => {
        var video = document.getElementById('local').firstChild;
        video.style.transform = 'scale(-1, 1)';
        video.appendChild(track.attach());
    });
};

function connectButtonHandler(event) {

    event.preventDefault();
    if (!connected) {
        addLocalVideo();
        button.disabled = true;
        button.innerHTML = 'Connecting...';
        connect().then(() => {
            //notify the server that the user have connected
            $.ajax({
                url: "/api/appointmentlog/postlog",
                method: "POST",
                processData: false,
                dataType: 'json',
                contentType: 'application/json',
                data: JSON.stringify({ appointment_id: $("#appointment_id").val() })
            });
            button.innerHTML = 'Leave call';
            button.disabled = false;
        }).catch((e) => {
            if ($.trim($('#twilioError').val()) != "") {
                alert($.trim($('#twilioError').val()));
            } else {
                alert('Connection failed. Is webcam connected?');
            }
            button.innerHTML = 'Join call';
            button.disabled = false;
        });
    }
    else {
        disconnect();
        button.innerHTML = 'Join call';
        connected = false;
    }
};

function connect() {
    var appointmentId = new FormData();
    appointmentId.append("appointmentId", $("#appointment_id").val());
    $('#twilioError').val('');
    var promise = new Promise((resolve, reject) => {
        // get a token from the back end
        fetch('/app/post_token', {
            method: 'POST',
            body: appointmentId
        }).then(res => res.json()).then(data => {
            if (data.token == "Appointment was canceled") {
                $('#twilioError').val(data.token);
                return false;
            } else {
                // join video call
                return Twilio.Video.connect(data.token, {
                    //name: "faisal",
                    bandwidthProfile: {
                        video: {
                            // Specify the profile mode. See Understanding mode
                            mode: 'collaboration',

                            // See Understanding maxSubscriptionBitrate
                            maxSubscriptionBitrate: 2400000,

                            // See Understanding dominantSpeakerPriority
                            dominantSpeakerPriority: 'high',

                            // Max number of tracks that are ‘on’. See Understanding maxTracks 
                            maxTracks: 3,

                            // provide a hint for how much bandwidth to allocate for different track resolutions. See Understanding renderDimensions
                            renderDimensions: {
                                high: { width: 1080, height: 720 },
                                standard: { width: 640, height: 480 },
                                low: { width: 320, height: 240 }
                            }
                        }
                    }
                });
            }
        }).then(_room => {
            room = _room;
            room.participants.forEach(participantConnected);
            room.on('participantConnected', participantConnected);
            room.on('participantDisconnected', participantDisconnected);
            connected = true;           
            $("#btn_mute").removeAttr("disabled");
            $("#btn_show").removeAttr("disabled");
            stopwatch.start();
            updateParticipantCount();
            if ($('#roleCall').val() == "client") {
                if (clientFirst) {
                    clientFirst = false;
                    WaitTimer();
                }
                if ($('#appointment_activity_id').val() != 2 && $('#appointment_activity_id').val() != 0) {
                    muteVideo();
                }
            } else {
                if (clinicianFirst) {
                    clientFirst = false;
                    WaitTimer();
                }
            }
            resolve();
        }).catch(() => {
            reject();
        });
    });
    return promise;
};

function updateParticipantCount() {
    if (!connected)
        count.innerHTML = 'Disconnected.';
    else
        count.innerHTML = (room.participants.size + 1) + ' participants online.';
};

function participantConnected(participant) {
    var participant_div = document.createElement('div');
    participant_div.setAttribute('id', participant.sid);
    participant_div.setAttribute('class', 'participant');

    var tracks_div = document.createElement('div');
    participant_div.appendChild(tracks_div);

    var label_div = document.createElement('div');
    label_div.innerHTML = participant.identity;
    participant_div.appendChild(label_div);

    container.appendChild(participant_div);

    participant.tracks.forEach(publication => {
        if (publication.isSubscribed)
            trackSubscribed(tracks_div, publication.track);
    });
    participant.on('trackSubscribed', track => trackSubscribed(tracks_div, track));
    participant.on('trackUnsubscribed', trackUnsubscribed);

    updateParticipantCount();
};

function participantDisconnected(participant) {
    document.getElementById(participant.sid).remove();
    updateParticipantCount();
};

function trackSubscribed(div, track) {
    div.style.transform = 'scale(-1, 1)';
    div.appendChild(track.attach());
};

function trackUnsubscribed(track) {
    track.detach().forEach(element => element.remove());
};

function disconnect() {
    room.disconnect();
    $("#local_video").empty();
    $("#btn_mute").attr("disabled", "disabled");
    $("#btn_show").attr("disabled", "disabled");
    stopwatch.stop();
    //while (container.lastChild.id !== 'local')
    //    container.removeChild(container.lastChild);
    button.innerHTML = 'Join call';
    connected = false;
    var track = room.localParticipant.videoTracks.forEach(publication => {
        publication.stop();
        publication.detach();
        //publication.unpublish();
    });

    var localParticipant = room.localParticipant;
    //localParticipant.unpublishTrack(track);
    updateParticipantCount();
};

//addLocalVideo();
button.addEventListener('click', connectButtonHandler);

function toggle_mute() {
    if (muted === false) {
        muteAudio();
    } else {
        unMuteAudio();
    }
}

function unMuteAudio() {
    room.localParticipant.audioTracks.forEach(function (track) {
        track.enable();
    });
    $("#btn_mute").text("Mute");
    muted = false;
}

function muteAudio() {
    room.localParticipant.audioTracks.forEach(function (track) {
        track.disable();
    });

    $("#btn_mute").text("Un Mute");
    muted = true;
}
function toggle_video() {
    if (show === false) {
        muteVideo();
    } else {
        unMuteVideo();
    }
}

function muteVideo() {
    var localParticipant = room.localParticipant;
    localParticipant.videoTracks.forEach(function (videoTracks) {
        videoTracks.disable();
    });
    $('#video_overlays').show();
    $("#btn_show").text("Show");
    show = true;
    room.participants.forEach(funTrackDisabled);
}

function unMuteVideo() {
    var localParticipant = room.localParticipant;
    localParticipant.videoTracks.forEach(function (videoTracks) {
        videoTracks.enable();
    });
    $('#video_overlays').hide();
    $("#btn_show").text("Hide");
    show = false;
    room.participants.forEach(funTrackEnabled);
}
function funTrackDisabled(participant) {
    console.log('disablaed...')
};

function funTrackEnabled(participant) {
    // alert('trackEnabled');
};
