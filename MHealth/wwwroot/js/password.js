function validatePassword(password) {

    // Do not show anything when the length of password is zero.
    if (password.length === 0) {
        document.getElementById("msg").innerHTML = "";
        return;
    }
    // Create an array and push all possible values that you want in password
    var matchedCase = new Array();
    matchedCase.push("[$@$!%*#?&]"); // Special Charector
    matchedCase.push("[A-Z]");      // Uppercase Alpabates
    matchedCase.push("[0-9]");      // Numbers
    matchedCase.push("[a-z]");     // Lowercase Alphabates


    var color = "";
    var strength = "";
    // Check the conditions
    if (password.length >= 8) {

        var ctr = 0;
        for (var i = 0; i < matchedCase.length; i++) {
            if (new RegExp(matchedCase[i]).test(password)) {
                ctr++;
            }
        }

        // Display it
      
        switch (ctr) {
            case 0:
            case 1:
            case 2:
                strength = "Very Weak (must contain 0-9,A-Z,a-Z and special character)";
                color = "red";
                $("#btn_submit").hide();
                break;
            case 3:
                strength = "Medium";
                color = "orange";
                $("#btn_submit").show();
                break;
            case 4:
                strength = "Strong";
                color = "green";
                $("#btn_submit").show();
                break;
        }
    } else {
        $("#btn_submit").attr("disabled", "disabled");
        strength = "Very Weak (password must be at least 8 characters)";
        color = "red";
    }

    document.getElementById("msg").innerHTML = strength;
    document.getElementById("msg").style.color = color;
}

function validatePassword_submit() {

    // Do not show anything when the length of password is zero.
    if (password.length === 0) {
        document.getElementById("msg").innerHTML = "";
        return;
    }
    // Create an array and push all possible values that you want in password
    var matchedCase = new Array();
    matchedCase.push("[$@$!%*#?&]"); // Special Charector
    matchedCase.push("[A-Z]");      // Uppercase Alpabates
    matchedCase.push("[0-9]");      // Numbers
    matchedCase.push("[a-z]");     // Lowercase Alphabates


    var color = "";
    var strength = "";
    // Check the conditions
    if (password.length >= 8) {

        var ctr = 0;
        for (var i = 0; i < matchedCase.length; i++) {
            if (new RegExp(matchedCase[i]).test(password)) {
                ctr++;
            }
        }

        // Display it

        switch (ctr) {
            case 0:
            case 1:
            case 2:
                strength = "Very Weak (must contain 0-9,A-Z,a-Z and special character)";
                color = "red";
                $("#btn_submit").attr("disabled", "disabled");
                break;
            case 3:
                strength = "Medium";
                color = "orange";
                $("#btn_submit").removeAttr("disabled");
                break;
            case 4:
                strength = "Strong";
                color = "green";
                $("#btn_submit").removeAttr("disabled");
                break;
        }
    } else {
        $("#btn_submit").attr("disabled", "disabled");
        strength = "Very Weak (password must be at least 8 characters)";
        color = "red";
    }

    document.getElementById("msg").innerHTML = strength;
    document.getElementById("msg").style.color = color;
}

function compare_passwords() {
    var color = "";
    var strength = "";

    if ($("#password").val() === $("#confirm_password").val()) {
        $("#btn_submit").removeAttr("disabled");
        strength = "Passwords match";
        color = "green";
        //validatePassword($("#password").val());
    } else {
        //
        $("#btn_submit").attr("disabled", "disabled");
        strength = "Password must match";
        color = "red";
    }


    document.getElementById("confirm_msg").innerHTML = strength;
    document.getElementById("confirm_msg").style.color = color;
}