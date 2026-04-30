var currentSearch = "";
var currentDept = "All";
var currentStatus = "All";
var currentSortField = "name";
var currentSortDir = "asc";
var currentSection = "login";

function showSection(section) {
    currentSection = section;

    $("#loginSection").addClass("d-none");
    $("#signupSection").addClass("d-none");
    $("#dashboardSection").addClass("d-none");
    $("#employeeSection").addClass("d-none");

    if (section === "login" || section === "signup") {
        $("#mainNavbar").addClass("d-none");
    } else {
        $("#mainNavbar").removeClass("d-none");
    }

    if (section === "login") {
        $("#loginSection").removeClass("d-none");
    } else if (section === "signup") {
        $("#signupSection").removeClass("d-none");
    } else if (section === "dashboard") {
        $("#dashboardSection").removeClass("d-none");
        refreshDashboard();
        $("#navDashboard").addClass("active");
        $("#navEmployees").removeClass("active");
    } else if (section === "employees") {
        $("#employeeSection").removeClass("d-none");
        uiService.fillDeptFilter();
        refreshEmployeeList();
        $("#navEmployees").addClass("active");
        $("#navDashboard").removeClass("active");
    }
}

function refreshDashboard() {
    var summary = dashboardService.getSummary();
    uiService.renderDashboardCards(summary);

    var breakdown = dashboardService.getDepartmentBreakdown();
    uiService.renderDepartmentBreakdown(breakdown);

    var recent = dashboardService.getRecentEmployees(5);
    uiService.renderRecentEmployees(recent);
}

function refreshEmployeeList() {
    var list = employeeService.applyFilters(currentSearch, currentDept, currentStatus);
    list = employeeService.sortBy(list, currentSortField, currentSortDir);
    uiService.renderEmployeeTable(list);
}

function refreshAll() {
    refreshDashboard();
    if (currentSection === "employees") {
        refreshEmployeeList();
    }
}

$(document).ready(function() {

    showSection("login");

    $("#navDashboard").on("click", function() {
        showSection("dashboard");
    });

    $("#navEmployees").on("click", function() {
        showSection("employees");
    });

    $("#navAddEmployee, #dashAddBtn").on("click", function() {
        uiService.clearForm();
        uiService.clearFormErrors();
        $("#empModalLabel").text("Add Employee");
        $("#saveEmpBtn").text("Save Employee");
        $("#editEmpId").val("");
        var modal = new bootstrap.Modal(document.getElementById("empModal"));
        modal.show();
    });

    $("#logoutBtn").on("click", function() {
        authService.logout();
        $("#loginForm")[0].reset();
        uiService.clearAuthErrors("login");
        showSection("login");
    });

    $("#loginForm").on("submit", function(e) {
        e.preventDefault();

        var username = $("#loginUsername").val();
        var password = $("#loginPassword").val();

        var errors = validationService.validateAuthForm({ username: username, password: password }, false);
        uiService.clearAuthErrors("login");

        if (Object.keys(errors).length > 0) {
            uiService.showAuthErrors(errors, "login");
            return;
        }

        var success = authService.login(username, password);
        if (success) {
            $("#navbarUsername").text(authService.getCurrentUser());
            showSection("dashboard");
        } else {
            $("#loginErrorMsg").removeClass("d-none").text("Invalid username or password. Please try again.");
        }
    });

    $("#goToSignup").on("click", function() {
        $("#signupForm")[0].reset();
        uiService.clearAuthErrors("signup");
        showSection("signup");
    });

    $("#signupForm").on("submit", function(e) {
        e.preventDefault();

        var username = $("#signupUsername").val();
        var password = $("#signupPassword").val();
        var confirmPassword = $("#signupConfirmPassword").val();

    
        var errors = validationService.validateAuthForm(
            { username: username, password: password, confirmPassword: confirmPassword },
            true
        );
        uiService.clearAuthErrors("signup");

        if (Object.keys(errors).length > 0) {
            uiService.showAuthErrors(errors, "signup");
            return;
        }

        var result = authService.signup(username, password);
        if (result.success) {
            uiService.showToast("Account created! Please login.", "success");
            setTimeout(function() {
                showSection("login");
            }, 1200);
        } else {
            $("#signupErrorMsg").removeClass("d-none").text(result.error);
        }
    });

    $("#goToLogin").on("click", function() {
        $("#loginForm")[0].reset();
        uiService.clearAuthErrors("login");
        showSection("login");
    });

    $("#searchInput").on("input", function() {
        currentSearch = $(this).val();
        refreshEmployeeList();
    });

    $("#deptFilter").on("change", function() {
        currentDept = $(this).val();
        refreshEmployeeList();
    });

    $("#statusAll").on("click", function() {
        currentStatus = "All";
        $("#statusAll, #statusActive, #statusInactive").removeClass("active");
        $(this).addClass("active");
        refreshEmployeeList();
    });

    $("#statusActive").on("click", function() {
        currentStatus = "Active";
        $("#statusAll, #statusActive, #statusInactive").removeClass("active");
        $(this).addClass("active");
        refreshEmployeeList();
    });

    $("#statusInactive").on("click", function() {
        currentStatus = "Inactive";
        $("#statusAll, #statusActive, #statusInactive").removeClass("active");
        $(this).addClass("active");
        refreshEmployeeList();
    });

    $(document).on("click", ".sortable", function() {
        var field = $(this).data("sort");

        if (currentSortField === field) {
            currentSortDir = currentSortDir === "asc" ? "desc" : "asc";
        } else {
            currentSortField = field;
            currentSortDir = "asc";
        }

        $(".sortable .sort-icon").html('<i class="bi bi-arrow-down-up text-muted"></i>');
        if (currentSortDir === "asc") {
            $(this).find(".sort-icon").html('<i class="bi bi-arrow-up text-primary"></i>');
        } else {
            $(this).find(".sort-icon").html('<i class="bi bi-arrow-down text-primary"></i>');
        }

        refreshEmployeeList();
    });

    $("#addEmpBtn").on("click", function() {
        uiService.clearForm();
        uiService.clearFormErrors();
        $("#empModalLabel").text("Add Employee");
        $("#saveEmpBtn").text("Save Employee");
        $("#editEmpId").val("");
        var modal = new bootstrap.Modal(document.getElementById("empModal"));
        modal.show();
    });

    $("#saveEmpBtn").on("click", function() {
        var editId = $("#editEmpId").val() || null;

        var data = {
            firstName:   $("#empFirstName").val().trim(),
            lastName:    $("#empLastName").val().trim(),
            email:       $("#empEmail").val().trim(),
            phone:       $("#empPhone").val().trim(),
            department:  $("#empDepartment").val(),
            designation: $("#empDesignation").val().trim(),
            salary:      Number($("#empSalary").val()),
            joinDate:    $("#empJoinDate").val(),
            status:      $("#empStatus").val()
        };

        var errors = validationService.validateEmployeeForm(data, editId);
        if (Object.keys(errors).length > 0) {
            uiService.showFormErrors(errors);
            return;
        }

        if (editId) {
            employeeService.update(Number(editId), data);
            uiService.showToast(data.firstName + " " + data.lastName + " updated successfully.", "success");
        } else {
            employeeService.add(data);
            uiService.showToast(data.firstName + " " + data.lastName + " added successfully.", "success");
        }

        bootstrap.Modal.getInstance(document.getElementById("empModal")).hide();
        uiService.fillDeptFilter();
        refreshAll();
    });

    $("#empForm").on("input change", ".form-control, .form-select", function() {
        $(this).removeClass("is-invalid");
    });

    $(document).on("click", ".viewBtn", function() {
        var id = $(this).data("id");
        var emp = employeeService.getById(id);
        if (emp) {
            uiService.showViewModal(emp);
        }
    });

    $(document).on("click", ".editBtn", function() {
        var id = $(this).data("id");
        var emp = employeeService.getById(id);
        if (emp) {
            uiService.clearFormErrors();
            uiService.populateForm(emp);
            $("#editEmpId").val(emp.id);
            $("#empModalLabel").text("Edit Employee");
            $("#saveEmpBtn").text("Update Employee");
            var modal = new bootstrap.Modal(document.getElementById("empModal"));
            modal.show();
        }
    });

    $(document).on("click", ".deleteBtn", function() {
        var id = $(this).data("id");
        var emp = employeeService.getById(id);
        if (emp) {
            $("#deleteEmpName").text(emp.firstName + " " + emp.lastName);
            $("#confirmDeleteBtn").data("id", emp.id);
            var modal = new bootstrap.Modal(document.getElementById("deleteModal"));
            modal.show();
        }
    });

    $("#confirmDeleteBtn").on("click", function() {
        var id = $(this).data("id");
        var emp = employeeService.getById(id);
        var name = emp ? emp.firstName + " " + emp.lastName : "Employee";

        employeeService.remove(id);
        bootstrap.Modal.getInstance(document.getElementById("deleteModal")).hide();
        uiService.showToast(name + " deleted successfully.", "danger");
        uiService.fillDeptFilter();
        refreshAll();
    });

}); 
