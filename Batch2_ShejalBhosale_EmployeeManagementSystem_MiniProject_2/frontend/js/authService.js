const AuthService = (() => {

    let _session = JSON.parse(localStorage.getItem("session")) || null;

    async function login(username, password) {
        const res = await fetch(`${API_BASE_URL}/auth/login`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ username, password })
        });

        const data = await res.json();

        if (!res.ok) {
            return { success: false, message: data.message || "Login failed" };
        }

        _session = {
            username: data.username,
            role: data.role,
            token: data.token
        };

        localStorage.setItem("session", JSON.stringify(_session));

        return { success: true };
    }

    async function signup(username, password) {
        const res = await fetch(`${API_BASE_URL}/auth/register`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ username, password })
        });

        const data = await res.json();

        if (!res.ok) {
            return { success: false, message: data.message || "Signup failed" };
        }

        return { success: true };
    }

    function logout() {
        _session = null;
        localStorage.removeItem("session");
    }

    function getToken() {
        return _session?.token || null;
    }

    function getRole() {
        return _session?.role || null;
    }

    function isAdmin() {
        return getRole() === "Admin";
    }

    function isLoggedIn() {
        return !!_session;
    }

    return {
        login,
        signup,
        logout,
        getToken,
        getRole,
        isAdmin,
        isLoggedIn
    };

})();