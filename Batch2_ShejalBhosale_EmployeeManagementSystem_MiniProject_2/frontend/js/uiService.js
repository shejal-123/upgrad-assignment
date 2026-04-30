var uiService = {

  getAvatar: function (emp) {
    var initials = (emp.firstName[0] || "") + (emp.lastName[0] || "");
    return '<div class="emp-avatar">' + initials.toUpperCase() + "</div>";
  },

  formatSalary: function (salary) {
    return "₹" + Number(salary).toLocaleString("en-IN");
  },

  formatDate: function (dateStr) {
    return new Date(dateStr).toLocaleDateString("en-IN");
  },

  renderEmployeeTable: function (result) {
    var list = result.data || [];
    var html = "";

    if (list.length === 0) {
      $("#employeeTableBody").html(
        '<tr><td colspan="10" class="text-center">No data</td></tr>'
      );
      return;
    }

    for (var i = 0; i < list.length; i++) {
      var e = list[i];

      html += `<tr>
        <td>${e.id}</td>
        <td>${this.getAvatar(e)}</td>
        <td>${e.firstName} ${e.lastName}</td>
        <td>${e.email}</td>
        <td>${e.department}</td>
        <td>${e.designation}</td>
        <td>${this.formatSalary(e.salary)}</td>
        <td>${this.formatDate(e.joinDate)}</td>
        <td>
          <span class="${e.status === 'Active' ? 'badge-status-active' : 'badge-status-inactive'}">
            ${e.status}
          </span>
        </td>
        <td>
          <button class="view-btn btn btn-sm btn-info text-white" data-id="${e.id}">View</button>
          <button class="edit-btn btn btn-sm btn-warning" data-id="${e.id}">Edit</button>
          <button class="delete-btn btn btn-sm btn-danger" data-id="${e.id}">Delete</button>
        </td>
      </tr>`;
    }

    $("#employeeTableBody").html(html);

    const start = (result.page - 1) * result.pageSize + 1;
    const end = start + list.length - 1;

    $("#employeeCount").text(`${start}-${end} of ${result.totalCount}`);

    this.renderPagination(result);
  },

  renderPagination: function (res) {
    let html = "";

    for (let i = 1; i <= res.totalPages; i++) {
      html += `<button class="page-btn btn btn-sm ${i === res.page ? "btn-primary" : "btn-light"}" data-page="${i}">${i}</button>`;
    }

    $("#pagination").html(html);
  },

  renderDashboardCards: function (data) {
    $("#statTotal").text(data.totalEmployees);
    $("#statActive").text(data.activeEmployees);
    $("#statInactive").text(data.inactiveEmployees);
    $("#statDepts").text(data.departmentBreakdown.length);
  },

  renderDepartmentBreakdown: function (list) {
    let html = "";

    const total = list.reduce((a, b) => a + b.count, 0);

    list.forEach((d) => {
      const percent = total > 0 ? ((d.count / total) * 100).toFixed(1) : 0;

      html += `<tr>
        <td>${d.department}</td>
        <td>${d.count}</td>
        <td>
          <div class="progress">
            <div class="progress-bar bg-primary" style="width:${percent}%"></div>
          </div>
        </td>
        <td>${percent}%</td>
      </tr>`;
    });

    $("#deptBreakdownBody").html(html);
  },

  renderRecentEmployees: function (list) {
    let html = "";

    list.forEach((e) => {
      html += `
        <div class="d-flex justify-content-between border-bottom py-2">
          <div>${e.firstName} ${e.lastName}</div>
          <small>${this.formatDate(e.joinDate)}</small>
        </div>
      `;
    });

    $("#recentEmployeesList").html(html);
  },

  applyRoleUI: function () {
    if (AuthService.isAdmin()) $(".admin-only").show();
    else $(".admin-only").hide();
  },

  showToast: function (msg, type = "success") {
    const toast = $("#appToast");

    toast.removeClass("text-bg-success text-bg-danger text-bg-warning");
    toast.addClass(`text-bg-${type}`);

    toast.find(".toast-body").text(msg);

    new bootstrap.Toast(toast[0]).show();
  },

};