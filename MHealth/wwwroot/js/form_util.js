function get_appointment_forms(appointment_id) {
    $("#div_" + appointment_id).empty();
    $.ajax({
        url: "/api/form/GetAppointmentForms?appointment_id=" + appointment_id,
        method: "GET",
        success: function (data) {

            for (var i = 0; i < data.length; i++) {
                var frm = "<a href='" + data[i].view_url + "' class='btn btn-info' style='margin:5px'>" + data[i].form_name + "</a>";
                $("#div_" + appointment_id).append(frm);
            }
        }
    });

}