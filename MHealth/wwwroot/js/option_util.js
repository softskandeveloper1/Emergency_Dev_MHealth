function load_appointment_activities(parent_ctrl, ctrl) {
    $("#" + ctrl).empty();
    $("#" + ctrl).append("<option>Select</option>");
    $.ajax({
        url: "/api/options/GetAppointmentActivities?parent_id=" + $("#" + parent_ctrl).val(),
        method: "GET",
        success: function (data) {

            for (var i = 0; i < data.length; i++) {
                $("#" + ctrl).append("<option value='" + data[i].id + "'>" + data[i].name + "</option>");
            }
        }

    });
}


function load_appointment_activities_sub(parent_ctrl, ctrl) {
    $("#" + ctrl).empty();
    $("#" + ctrl).append("<option>Select</option>");
    $.ajax({
        url: "/api/options/GetAppointmentSubActivities?parent_id=" + $("#" + parent_ctrl).val(),
        method: "GET",
        success: function (data) {
            // $("#" + ctrl).empty();
            for (var i = 0; i < data.length; i++) {
                $("#" + ctrl).append("<option value='" + data[i].id + "'>" + data[i].name + "</option>");
            }
        }

    });
}


function load_appointment_activities_sub_check(parent_ctrl, ctrl) {
    $("#" + ctrl).empty();
    //$("#" + ctrl).append("<option>Select</option>");
    $.ajax({
        url: "/api/options/GetAppointmentSubActivities?parent_id=" + $("#" + parent_ctrl).val(),
        method: "GET",
        success: function (data) {
            // $("#" + ctrl).empty();
            for (var i = 0; i < data.length; i++) {

                var item = '<div class="form-check" style="margin-left:10px"> <label class="form-check-label">';
                item += '<input class="inp-cbx form-check-input" id="appointment_category_sub_id" name="appointment_category_sub_id" value="' + data[i].id + '" type="checkbox">  ';
                item += data[i].name + '</label></div>';

                $("#" + ctrl).append(item);
                //$("#" + ctrl).append("<option value='" + data[i].id + "'>" + data[i].name + "</option>");
            }
        }

    });
}

function select_appointment(appointment_type_id) {
    $("#appointment_type").val(appointment_type_id);
    load_appointment_activities('appointment_type', 'appointment_category');
}


function check_profile_questionaire_status() {
    $.ajax({
        url: "/api/enrollmentapi/GetQuestionaireStatus?appointment_type=" + $("#appointment_type").val(),
        method: "GET",
        success: function (data) {
            if (data) {
                $(".apt-wizard").hide();
                $("#questionaire_exist").show();
            } else {
                $(".apt-wizard").hide();
                $("#questionaire_not_exist").show();
            }
        }

    });
}

function check_earlier_appointment() {

    $.ajax({
        url: "/ProfileMatch/GetMatch?appointment_type=" + $("#appointment_type").val() + "&appointment_category=" + $("#appointment_category").val() + "&appointment_category_sub=" + $("#appointment_category_sub").val(),
        method: "GET",
        success: function (data) {
            //var json = JSON.parse(data);
            if (data != null) {
                //the member has an appointment already
                $("#step-1").hide();
                $("#appointment_exists").show();
                $("#match_id").val(data);
            } else {
                //check if a user an enrollment
                $(".apt-wizard").hide();
                check_profile_questionaire_status();
            }
        }

    });
    //$.ajax({
    //    url: "/appointment/CheckUserHadAppointment?appointment_type=" + $("#appointment_type").val(),
    //    method: "GET",
    //    success: function (data) {
    //        var json = JSON.parse(data);
    //        if (json.appointments > 0) {
    //            //the member has an appointment already
    //            $("#step-1").hide();
    //            $("#appointment_exists").show();
    //        } else {
    //            //check if a user an enrollment
    //            $(".apt-wizard").hide();
    //            check_profile_questionaire_status();
    //        }
    //    }

    //});
}

function appointment_exists() {
    var earler_appointment = $("#earlier_appointment").val();

    if (earler_appointment == 1) {
        //contine with the last provider
        //$("#frm_new_appointment").attr("action", "/Clinician/Clinicians");
        //$("#frm_new_appointment").submit();
        window.location.href = "/Appointment/NewAppointment?id=" + $("#match_id").val();
    } else {
        check_profile_questionaire_status();
    }
}

function questionaire_exist() {
    var sel_questionaire_exist = $("#sel_questionaire_exist").val();

    if (sel_questionaire_exist == 1) {
        //contine with the existing questionaire response
        $("#frm_new_appointment").attr("action", "/Clinician/Clinicians");
        $("#frm_new_appointment").submit();
        //window.location.href = "/Clinician/Clinicians";
    } else {
        //edit the existing questionaire
        var appointment_type_id = $("#appointment_type").val();
        if (appointment_type_id == 1) {
            $("#frm_new_appointment").attr("action", "/Profile/EditEnrollment");
            $("#frm_new_appointment").submit();
            //window.location.href = "/Profile/EditEnrollment";
        }
        else {
            $("#frm_new_appointment").attr("action", "/Profile/EditQ");
            $("#frm_new_appointment").submit();
            //window.location.href = "/Profile/EditQ";
        }
    }
}


function questionaire_not_exist() {
    var appointment_type_id = $("#appointment_type").val();
    if (appointment_type_id == 1) {
        $("#frm_new_appointment").attr("action", "/Profile/EditEnrollment");
        $("#frm_new_appointment").submit();
        //window.location.href = "/Profile/EditEnrollment";
    }
    else {
        $("#frm_new_appointment").attr("action", "/Profile/EditQ");
        $("#frm_new_appointment").submit();
        //window.location.href = "/Profile/EditQ";
    }
}