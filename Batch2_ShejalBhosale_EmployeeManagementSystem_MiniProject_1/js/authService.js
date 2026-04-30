var registeredAdmins = [
    { username: adminCredentials.username, password: adminCredentials.password }
];

var currentUser = null; 

var authService = {

    signup: function(username, password) {
        for (var i = 0; i < registeredAdmins.length; i++) {
            if (registeredAdmins[i].username === username) {
                return { success: false, error: "Username already exists. Please choose another." };
            }
        }
        registeredAdmins.push({ username: username, password: password });
        return { success: true, error: null };
    },

    login: function(username, password) {
        for (var i = 0; i < registeredAdmins.length; i++) {
            if (registeredAdmins[i].username === username && registeredAdmins[i].password === password) {
                currentUser = username;
                return true;
            }
        }
        return false;
    },

    logout: function() {
        currentUser = null;
    },

    isLoggedIn: function() {
        return currentUser !== null;
    },

    getCurrentUser: function() {
        return currentUser;
    }

};
