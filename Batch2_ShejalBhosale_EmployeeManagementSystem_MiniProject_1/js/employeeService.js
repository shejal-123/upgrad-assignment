var employeeService = {

    getAll: function() {
        return storageService.getAll();
    },

    getById: function(id) {
        return storageService.getById(id);
    },

    add: function(data) {
        data.id = storageService.nextId();
        storageService.add(data);
    },

    update: function(id, data) {
        storageService.update(id, data);
    },

    remove: function(id) {
        storageService.remove(id);
    },

    search: function(query) {
        var q = query.toLowerCase();
        var result = [];
        var all = storageService.getAll();
        for (var i = 0; i < all.length; i++) {
            var fullName = (all[i].firstName + " " + all[i].lastName).toLowerCase();
            var email = all[i].email.toLowerCase();
            if (fullName.indexOf(q) !== -1 || email.indexOf(q) !== -1) {
                result.push(all[i]);
            }
        }
        return result;
    },

    filterByDepartment: function(dept) {
        if (!dept || dept === "All") return storageService.getAll();
        var result = [];
        var all = storageService.getAll();
        for (var i = 0; i < all.length; i++) {
            if (all[i].department === dept) {
                result.push(all[i]);
            }
        }
        return result;
    },

    filterByStatus: function(status) {
        if (!status || status === "All") return storageService.getAll();
        var result = [];
        var all = storageService.getAll();
        for (var i = 0; i < all.length; i++) {
            if (all[i].status === status) {
                result.push(all[i]);
            }
        }
        return result;
    },

    applyFilters: function(searchText, dept, status) {
        var all = storageService.getAll();
        var result = [];
        var q = searchText ? searchText.toLowerCase() : "";

        for (var i = 0; i < all.length; i++) {
            var emp = all[i];

            var fullName = (emp.firstName + " " + emp.lastName).toLowerCase();
            var matchSearch = !q || fullName.indexOf(q) !== -1 || emp.email.toLowerCase().indexOf(q) !== -1;

            var matchDept = !dept || dept === "All" || emp.department === dept;

            var matchStatus = !status || status === "All" || emp.status === status;

            if (matchSearch && matchDept && matchStatus) {
                result.push(emp);
            }
        }
        return result;
    },

    sortBy: function(list, field, direction) {
        var sorted = list.slice(); 
        sorted.sort(function(a, b) {
            var valA, valB;

            if (field === "name") {
                valA = a.lastName.toLowerCase();
                valB = b.lastName.toLowerCase();
            } else if (field === "salary") {
                valA = a.salary;
                valB = b.salary;
            } else if (field === "joinDate") {
                valA = new Date(a.joinDate);
                valB = new Date(b.joinDate);
            } else {
                return 0;
            }

            if (valA < valB) return direction === "asc" ? -1 : 1;
            if (valA > valB) return direction === "asc" ? 1 : -1;
            return 0;
        });
        return sorted;
    },

    getDepartments: function() {
        var all = storageService.getAll();
        var depts = [];
        for (var i = 0; i < all.length; i++) {
            if (depts.indexOf(all[i].department) === -1) {
                depts.push(all[i].department);
            }
        }
        return depts;
    }

};
