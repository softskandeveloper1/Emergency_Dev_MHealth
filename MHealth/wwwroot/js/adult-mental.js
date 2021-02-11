$().ready(function () {
    var mental_health_container = $('div.mental-health-container');
    // validate the form when it is submitted
    $("#mental_health_form").validate({
        errorContainer: mental_health_container,
        errorLabelContainer: $("ol", mental_health_container),
        wrapper: 'li',
        messages: {
            depression: "Please select an answer for Depression",
            anxiety: "Please select an answer for Anxiety",
            problem: "You have not said What brought you to Counselling",
            mood_swing: "Please select an answer for Mood Swing",
            appetite_changes: "Please select an answer for appetite change",
            sleep_changes: "Please select an answer for sleep change",
            hallucinations: "Please select an answer for hallucinations",
            work_problems: "Please select an answer for work problems",
            racing_thoughts: "Please select an answer for racing thoughts",
            memory_problems: "Please select an answer for memory problems",
            loss_interest: "Please select an answer for loss of interest",
            irritability: "Please select an answer for irritability",
            excessive_worry: "Please select an answer for excessive worry",
            suicidal_ideation: "Please select an answer for suicidal ideation",
            relationship_issues: "Please select an answer for relationship issues",
            low_energy: "Please select an answer for low energy",
            confusion: "Please select an answer for confusion",
            panic_attacks: "Please select an answer for panic attacks",
            obsessive_thoughts: "Please select an answer for obsessive thoughts",
            ritualistic_behaviour: "Please select an answer for ritualistic behaviour",
            checking: "Please select an answer for checking",
            counting: "Please select an answer for counting",
            self_injury: "Please select an answer for self injury",
            difficulty_concentrating: "Please select an answer for difficulty concentrating",
            hyperactivity: "Please select an answer for hyperactivity",
            history: "Please select an answer for history",
            effect_symptons: "Please select an answer for effect symptons",
            mental_problem: "Please select an answer for mental problem",
            mental_hospitalization: "Please select an answer for mental hospitalization"
        }
    });
    var medical_history_container = $('div.medical-history-container');
    // validate the form when it is submitted
    $("#medical_history_form").validate({
        // errorContainer: medical_history_container,
        // errorLabelContainer: $("ol", medical_history_container),
        // wrapper: 'li',
        // messages: {

        // }
    });
    var marital_relationship_container = $('div.marital-relationship-container');
    // validate the form when it is submitted
    $("#marital_relationship_form").validate({
        // errorContainer: marital_relationship_container,
        // errorLabelContainer: $("ol", marital_relationship_container),
        // wrapper: 'li',
        // messages: {

        // }
    });
    var family_history_container = $('div.family-history-container');
    $("#family_history_form").validate({});
    var education_history_container = $('div.education-history-container');
    $("#education_history_form").validate({});
    var work_history_container = $('div.work-history-container');
    $("#work_history_form").validate({});
    var substance_use_form_contianer = $('div.substance-use-container');
    $("#substance_use_form").validate({});
});

$(document).on('click', '#mental_health_submit', function () {
    var is_valid_form = $('#mental_health_form').valid();
    if (is_valid_form) {
        $.ajax({
            type: "POST",
            url: "/Psychosocial/Adult/",
            data: $("#mental_health_form").serialize(),
            success: function (data) {
                if (data === "success") {
                    enableNextTab('#mental-health', '#mental-health-link', '#medical-history', '#medical-history-link')
                }
            }
        });
        return false;
    }
});
$(document).on('click', '#medical_history_submit', function () {
    var is_valid_form = $('#medical_history_form').valid();
    if (is_valid_form) {
        $.ajax({
            type: "POST",
            url: "/Psychosocial/Medication/",
            data: $("#medical_history_form").serialize(),
            success: function (data) {
                if (data === "success") {
                    enableNextTab('#medical-history', '#medical-history-link', '#marital-relationship', '#marital-relationship-link');
                }
            }
        });
        return false;
    }
});

$(document).on('click', '#marital_relationship_submit', function () {
    var is_valid_form = $('#marital_relationship_form').valid();
    if (is_valid_form) {
        $.ajax({
            type: "POST",
            url: "/Psychosocial/MaritalRelationship/",
            data: $("#marital_relationship_form").serialize(),
            success: function (data) {
                if (data === "success") {
                    enableNextTab('#marital-relationship', '#marital-relationship-link', '#family-history', '#family-history-link');
                }
            }
        });
        return false;
    }
});

$(document).on('click', '#family_history_submit', function () {
    var is_valid_form = $('#family_history_form').valid();
    if (is_valid_form) {
        $.ajax({
            type: "POST",
            url: "/Psychosocial/FamilyHistory/",
            data: $("#family_history_form").serialize(),
            success: function (data) {
                if (data === "success") {
                    enableNextTab('#family-history', '#family-history-link', '#education-history', '#education-history-link');
                }
            }
        });
        return false;
    }
});

$(document).on('click', '#education_history_submit', function () {
    var is_valid_form = $('#education_history_form').valid();
    if (is_valid_form) {
        $.ajax({
            type: "POST",
            url: "/Psychosocial/EducationHistory/",
            data: $("#education_history_form").serialize(),
            success: function (data) {
                if (data === "success") {
                    enableNextTab('#education-history', '#education-history-link', '#work-history', '#work-history-link');
                }
            }
        });
        return false;
    }
});
$(document).on('click', '#work_history_submit', function () {
    var is_valid_form = $('#work_history_form').valid();
    if (is_valid_form) {
        $.ajax({
            type: "POST",
            url: "/Psychosocial/WorkHistory/",
            data: $("#work_history_form").serialize(),
            success: function (data) {
                if (data === "success") {
                    enableNextTab('#work-history', '#work-history-link', '#substance-use', '#substance-use-link');
                }
            }
        });
        return false;
    }
});

$(document).on('click', '#substance_use_submit', function () {
    var is_valid_form = $('#substance_use_form').valid();
    if (is_valid_form) {
        $.ajax({
            type: "POST",
            url: "/Psychosocial/SubstanceUse/",
            data: $("#substance_use_form").serialize(),
            success: function (data) {
                if (data === "success") {
                    //TODO redirect to another page
                    alert("Form Saved Successfully")
                }
            }
        });
        return false;
    }
});


function enableNextTab(currentTab, currentTabLink, nextTab, nextTabLink) {
    $(currentTab).removeClass('active');
    $(currentTabLink).removeClass('active');
    $(nextTab).addClass('active');
    $(nextTab).addClass('show');
    $(nextTabLink).addClass('active');
    $(nextTabLink).addClass('show');
    $(nextTabLink).attr('href', nextTab);
    $(nextTabLink).attr('data-toggle', 'tab');
}

function add_row(tb) {

    var $tableBody = $('#' + tb).find("tbody"),
        $trLast = $tableBody.find("tr:last"),
        $trNew = $trLast.clone();

    $trNew.find('input').each(function () {
        $(this).val("");
    });
    $trLast.after($trNew);
}

function delete_row(row, tb) {
    var rows = $("#" + tb + " tbody tr").length;
    if (rows > 1) {
        $(row).parent().parent().remove();
    } else {
        clear_row(row);
    }
}

function clear_row(row) {
    $(row).parent().parent().find("input").each(function () {
        $(this).val("");
    });
    $(row).parent().parent().find("select").each(function () {
        $(this).val("");
    });
}

function enableDisable(inputId, value) {
    if (value === '1')
        $("#" + inputId).attr("disabled", false);
    else
        $("#" + inputId).attr("disabled", true);
}

function enableDisableTable(inputId, value) {
    if (value === '1')
        $("#" + inputId).find("input,button,textarea,select").attr("disabled", false);
    else
        $("#" + inputId).find("input,button,textarea,select").attr("disabled", true);
}

function enableDisableMore(inputIds, value) {
    var ids = inputIds.split(',');
    ids.forEach(element => {
        if (value === '1')
            $("#" + element).attr("disabled", false);
        else
            $("#" + element).attr("disabled", true);
    });
}