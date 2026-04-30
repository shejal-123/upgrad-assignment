var employees = JSON.parse(JSON.stringify(employeesData));

var storageService = {

    getAll: function() {
        return employees;
    },

    getById: function(id) {
        for (var i = 0; i < employees.length; i++) {
            if (employees[i].id == id) {
                return employees[i];
            }
        }
        return null;
    },

    add: function(emp) {
        employees.push(emp);
    },

    update: function(id, newData) {
        for (var i = 0; i < employees.length; i++) {
            if (employees[i].id == id) {
                employees[i] = Object.assign({}, employees[i], newData);
                break;
            }
        }
    },

    remove: function(id) {
        var newList = [];
        for (var i = 0; i < employees.length; i++) {
            if (employees[i].id != id) {
                newList.push(employees[i]);
            }
        }
        employees = newList;
    },

    nextId: function() {
        if (employees.length === 0) return 1;
        var maxId = 0;
        for (var i = 0; i < employees.length; i++) {
            if (employees[i].id > maxId) {
                maxId = employees[i].id;
            }
        }
        return maxId + 1;
    }

};
