//This could have been done by binding the form in Razor
//I wanted to add variety and demo an alternative approach

$(function () {
    customer_create.init();
});

var customer_create = {
    init: function () {
        $('#feedback').hide();
        $(document).on('click', '#createCustomer', customer_create.createCustomer);
    },
    createCustomer: function (e) {
        if (!customer_create.formIsValid()) {
            e.preventDefault();
        }
    },
    formIsValid: function (e) {
        var $firstName = $('#firstName');
        var $lastName = $('#lastName');
        var $dob = $('#dob');
        var $email = $('#email');
        var $feedback = $('#feedback');

        if ($firstName.val() === '') {
            $feedback.html("Enter your first name");
            $feedback.show();
            return false;
        }

        if ($lastName.val() === '') {
            $feedback.html("Enter your last name");
            $feedback.show();
            return false;
        }

        if (!customer_create.isEmail($email.val())) {
            $feedback.html("Enter a valid email address");
            $feedback.show();
            return false;
        }

        if (!Date.parse($dob.val())) {
            $feedback.html("Enter your date of birth");
            $feedback.show();
            return false;
        }

        var dob = new Date(Date.parse($dob.val()))
        var age = customer_create.getAge(dob);
        if (age < 18) {
            $feedback.html("You must be over 18 to apply");
            $feedback.show();
            return false;
        }

        return true;
    },

    isEmail: function (email) {
        var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        return regex.test(email);
    },

    getAge: function (dob) {
        var diff_ms = Date.now() - dob.getTime();
        var age_dt = new Date(diff_ms);
        return Math.abs(age_dt.getUTCFullYear() - 1970);
    }
}