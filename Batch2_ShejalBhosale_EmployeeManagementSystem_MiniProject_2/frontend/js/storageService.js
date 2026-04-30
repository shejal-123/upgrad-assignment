const StorageService = (() => {

    function headers() {
        const token = AuthService.getToken();
        const h = { "Content-Type": "application/json" };
        if (token) h["Authorization"] = `Bearer ${token}`;
        return h;
    }

    async function getAll(params) {
        const query = new URLSearchParams(params).toString();

        const res = await fetch(`${API_BASE_URL}/employees?${query}`, {
            headers: headers()
        });

        if (!res.ok) throw new Error("Failed to fetch employees");

        return await res.json();
    }

    async function getById(id) {
        const res = await fetch(`${API_BASE_URL}/employees/${id}`, {
            headers: headers()
        });

        if (!res.ok) throw new Error("Failed to fetch employee");

        return await res.json();
    }

    async function add(data) {
        const res = await fetch(`${API_BASE_URL}/employees`, {
            method: "POST",
            headers: headers(),
            body: JSON.stringify(data)
        });

        if (res.status === 409) throw new Error("Email already exists");
        if (!res.ok) throw new Error("Failed to add employee");

        return await res.json();
    }

    async function update(id, data) {
        const res = await fetch(`${API_BASE_URL}/employees/${id}`, {
            method: "PUT",
            headers: headers(),
            body: JSON.stringify(data)
        });

        if (res.status === 409) throw new Error("Email already exists");
        if (!res.ok) throw new Error("Failed to update employee");

        return await res.json();
    }

    async function remove(id) {
        const res = await fetch(`${API_BASE_URL}/employees/${id}`, {
            method: "DELETE",
            headers: headers()
        });

        if (!res.ok) throw new Error("Failed to delete employee");

        return true;
    }

    return {
        getAll,
        getById,
        add,
        update,
        remove
    };

})();