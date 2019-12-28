const requestUrls = {
    account: {
        getAccount: "Account/GetAccount",
    },
    accountValidation: {
        validateRegistration: "AccountValidation/ValidateRegistration",
    },
    friends: {
        getAllFriends: "Friends/GetAllFriends",
    },
    profile: {
        getProfilePicturePath: "Profile/GetProfilePicturePath",
        uploadProfilePicture: "Profile/UploadProfilePicture",
    },
    logOn: {
        login: "Logon/Login",
        logOff: "Logon/LogOff",
        register: "Logon/Register",
    },
    search: {
        searchForQuery: "Search/SearchForQuery"
    }
}

export default requestUrls;