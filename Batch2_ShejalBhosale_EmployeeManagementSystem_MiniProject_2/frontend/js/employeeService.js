const employeeService = (() => {

    async function fetchEmployees(state) {
        const query = new URLSearchParams({
            search: state.search,
            department: state.department,
            status: state.status,
            sortBy: state.sortBy,
            sortDir: state.sortDir,
            page: state.page,
            pageSize: PAGE_SIZE
        }).toString();

        const res = await fetch(`${API_BASE_URL}/employees?${query}`, {
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${AuthService.getToken()}`
            }
        });

        if (!res.ok) throw new Error("Failed to fetch employees");

        return await res.json();
    }

    async function getById(id) {
        const res = await fetch(`${API_BASE_URL}/employees/${id}`, {
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${AuthService.getToken()}`
            }
        });

        if (!res.ok) throw new Error("Failed to fetch employee");

        return await res.json();
    }

    async function add(data) {
        const res = await fetch(`${API_BASE_URL}/employees`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${AuthService.getToken()}`
            },
            body: JSON.stringify(data)
        });

        if (res.status === 409) throw new Error("Email already exists");
        if (!res.ok) throw new Error("Failed to add employee");

        return await res.json();
    }

    async function update(id, data) {
        const res = await fetch(`${API_BASE_URL}/employees/${id}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${AuthService.getToken()}`
            },
            body: JSON.stringify(data)
        });

        if (res.status === 409) throw new Error("Email already exists");
        if (!res.ok) throw new Error("Failed to update employee");

        return await res.json();
    }

    async function remove(id) {
        const res = await fetch(`${API_BASE_URL}/employees/${id}`, {
            method: "DELETE",
            headers: {
                "Authorization": `Bearer ${AuthService.getToken()}`
            }
        });

        if (!res.ok) throw new Error("Failed to delete employee");

        return true;
    }

    return {
        fetchEmployees,
        getById,
        add,
        update,
        remove
    };

})();