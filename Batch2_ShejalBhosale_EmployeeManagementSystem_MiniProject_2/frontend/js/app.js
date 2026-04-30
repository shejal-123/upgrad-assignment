var currentSearch = "";
var currentDept = "";
var currentStatus = "";
var currentSortField = "firstName";
var currentSortDir = "asc";
var currentSection = "login";
var currentPage = 1;

function showSection(section) {
  currentSection = section;

  $("#loginSection,#signupSection,#dashboardSection,#employeeSection").addClass(
    "d-none",
  );

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
  } else if (section === "employees") {
    $("#employeeSection").removeClass("d-none");
    refreshEmployeeList();
  }
}

async function refreshDashboard() {
  try {
    const data = await dashboardService.getDashboard();
    uiService.renderDashboardCards(data);
    uiService.renderDepartmentBreakdown(data.departmentBreakdown);
    uiService.renderRecentEmployees(data.recentEmployees);
  } catch {
    uiService.showToast("Failed to load dashboard", "danger");
  }
}

async function refreshEmployeeList() {
  try {
    const result = await employeeService.fetchEmployees({
      search: currentSearch,
      department: currentDept,
      status: currentStatus,
      sortBy: currentSortField,
      sortDir: currentSortDir,
      page: currentPage,
    });

    uiService.renderEmployeeTable(result);
    uiService.applyRoleUI();
  } catch {
    uiService.showToast("Failed to load employees", "danger");
  }
}

function loadDepartments() {
  const departments = [
    "Engineering",
    "Marketing",
    "HR",
    "Finance",
    "Operations",
  ];
  const dropdown = $("#deptFilter");

  dropdown.html('<option value="All">All Departments</option>');

  departments.forEach((dep) => {
    dropdown.append(`<option value="${dep}">${dep}</option>`);
  });
}

$(document).ready(function () {
  $(document).on("click", "#goToSignup", () => showSection("signup"));
  $(document).on("click", "#goToLogin", () => showSection("login"));

  if (AuthService.isLoggedIn()) {
    uiService.applyRoleUI();
    showSection("dashboard");
  } else {
    showSection("login");
  }

  loadDepartments();

  $(document).on("click", ".view-btn", async function () {
    const id = $(this).data("id");
    const emp = await employeeService.getById(id);

    $("#viewModalBody").html(`
      <p><b>Name:</b> ${emp.firstName} ${emp.lastName}</p>
      <p><b>Email:</b> ${emp.email}</p>
      <p><b>Department:</b> ${emp.department}</p>
      <p><b>Designation:</b> ${emp.designation}</p>
      <p><b>Salary:</b> ₹${emp.salary}</p>
      <p><b>Status:</b> ${emp.status}</p>
    `);

    new bootstrap.Modal(document.getElementById("viewModal")).show();
  });

  $(document).on("click", ".edit-btn", async function () {
    const id = $(this).data("id");
    const emp = await employeeService.getById(id);

    $("#editEmpId").val(emp.id);
    $("#empFirstName").val(emp.firstName);
    $("#empLastName").val(emp.lastName);
    $("#empEmail").val(emp.email);
    $("#empPhone").val(emp.phone);
    $("#empDepartment").val(emp.department);
    $("#empDesignation").val(emp.designation);
    $("#empSalary").val(emp.salary);
    $("#empJoinDate").val(emp.joinDate.split("T")[0]);
    $("#empStatus").val(emp.status);

    new bootstrap.Modal(document.getElementById("empModal")).show();
  });

  let deleteId = null;

  $(document).on("click", ".delete-btn", async function () {
    deleteId = $(this).data("id");

    const emp = await employeeService.getById(deleteId);
    $("#deleteEmpName").text(emp.firstName + " " + emp.lastName);

    new bootstrap.Modal(document.getElementById("deleteModal")).show();
  });

  $("#confirmDeleteBtn").click(async function () {
    await employeeService.remove(deleteId);

    bootstrap.Modal.getInstance(document.getElementById("deleteModal")).hide();

    uiService.showToast("Employee deleted");
    refreshEmployeeList();
    refreshDashboard();
  });

  $("#navDashboard").click(() => showSection("dashboard"));
  $("#navEmployees").click(() => showSection("employees"));

  $("#logoutBtn").click(function () {
    AuthService.logout();
    showSection("login");
  });

  $("#loginForm").submit(async function (e) {
    e.preventDefault();

    const result = await AuthService.login(
      $("#loginUsername").val(),
      $("#loginPassword").val(),
    );

    if (result.success) {
      uiService.applyRoleUI();
      showSection("dashboard");
    } else {
      $("#loginErrorMsg").removeClass("d-none").text(result.message);
    }
  });

  $("#signupForm").submit(async function (e) {
    e.preventDefault();

    const result = await AuthService.signup(
      $("#signupUsername").val(),
      $("#signupPassword").val(),
    );

    if (result.success) {
      uiService.showToast("Account created", "success");
      showSection("login");
    } else {
      $("#signupErrorMsg").removeClass("d-none").text(result.message);
    }
  });

  $("#searchInput").on("input", function () {
    currentSearch = $(this).val();
    currentPage = 1;
    refreshEmployeeList();
  });

  $("#deptFilter").change(function () {
    currentDept = $(this).val() === "All" ? "" : $(this).val();
    currentPage = 1;
    refreshEmployeeList();
  });

  $("#statusAll,#statusActive,#statusInactive").click(function () {
    $("#statusAll,#statusActive,#statusInactive").removeClass("active");
    $(this).addClass("active");

    const val = $(this).text().trim();
    currentStatus = val === "All" ? "" : val;

    currentPage = 1;
    refreshEmployeeList();
  });

  $(document).on("click", ".page-btn", function () {
    currentPage = Number($(this).data("page"));
    refreshEmployeeList();
  });

  $(document).on("click", ".sortable", function () {
    const field = $(this).data("sort");

    if (currentSortField === field) {
      currentSortDir = currentSortDir === "asc" ? "desc" : "asc";
    } else {
      currentSortField = field;
      currentSortDir = "asc";
    }

    refreshEmployeeList();
  });

  $("#navAddEmployee").click(function () {
    $("#empForm")[0].reset();
    $("#editEmpId").val("");

    new bootstrap.Modal(document.getElementById("empModal")).show();
  });

  $("#saveEmpBtn").click(async function () {
    const data = {
      firstName: $("#empFirstName").val(),
      lastName: $("#empLastName").val(),
      email: $("#empEmail").val(),
      phone: $("#empPhone").val(),
      department: $("#empDepartment").val(),
      designation: $("#empDesignation").val(),
      salary: Number($("#empSalary").val()),
      joinDate: $("#empJoinDate").val(),
      status: $("#empStatus").val(),
    };

    const errors = validationService.validateEmployeeForm(data);

    $(".invalid-feedback").text("");
    $(".form-control").removeClass("is-invalid");

    Object.keys(errors).forEach((key) => {
      const input = $("#emp" + key.charAt(0).toUpperCase() + key.slice(1));
      input.addClass("is-invalid");
      input.next(".invalid-feedback").text(errors[key]);
    });

    if (Object.keys(errors).length > 0) {
      uiService.showToast("Please fix form errors", "danger");
      return;
    }

    const id = $("#editEmpId").val();

    try {
      if (id) {
        await employeeService.update(id, data);
        uiService.showToast("Employee updated");
      } else {
        await employeeService.add(data);
        uiService.showToast("Employee added");
      }

      bootstrap.Modal.getInstance(document.getElementById("empModal")).hide();

      refreshEmployeeList();
      refreshDashboard();
    } catch (err) {
      uiService.showToast(err.message, "danger");
    }
  });
});
