var validationService = {

    validateEmployeeForm: function(data) {
        var errors = {};

        if (!data.firstName) errors.firstName = "First name is required";
        if (!data.lastName) errors.lastName = "Last name is required";

        if (!data.email) {
            errors.email = "Email is required";
        } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(data.email)) {
            errors.email = "Invalid email";
        }

        if (!data.phone) {
            errors.phone = "Phone is required";
        } else if (!/^\d{10}$/.test(data.phone)) {
            errors.phone = "Phone must be 10 digits";
        }

        if (!data.department) errors.department = "Select department";
        if (!data.designation) errors.designation = "Designation required";

        if (!data.salary || data.salary <= 0) {
            errors.salary = "Invalid salary";
        }

        if (!data.joinDate) errors.joinDate = "Join date required";
        if (!data.status) errors.status = "Select status";

        return errors;
    },

    validateAuthForm: function(data, isSignup) {
        var errors = {};

        if (!data.username) errors.username = "Username required";

        if (!data.password) {
            errors.password = "Password required";
        } else if (isSignup && data.password.length < 6) {
            errors.password = "Min 6 characters";
        }

        if (isSignup) {
            if (!data.confirmPassword) {
                errors.confirmPassword = "Confirm password required";
            } else if (data.password !== data.confirmPassword) {
                errors.confirmPassword = "Passwords do not match";
            }
        }

        return errors;
    }

};