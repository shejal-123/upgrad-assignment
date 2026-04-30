const dashboardService = (() => {

    async function getDashboard() {
        const res = await fetch(`${API_BASE_URL}/employees/dashboard`, {
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${AuthService.getToken()}`
            }
        });

        if (!res.ok) throw new Error("Failed to load dashboard");

        return await res.json();
    }

    return {
        getDashboard
    };

})();