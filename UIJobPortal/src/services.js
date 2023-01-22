
export async function getOrganizationCoreTypes(inputData) {
    try {
        const response = await fetch("https://localhost:7077/api/LookUpGroup/GetLookupGroupByName/" + inputData, {
            headers: { "Content-Type": "application/json" },
        });
        return await response.json();
    }
    catch (error) {
        return [];
    }
}

export async function getDataService(apiUrl) {
    try {
        const response = await fetch(apiUrl, {
            headers: { "Content-Type": "application/json" },
        });
        return await response.json();
    }
    catch (error) {
        return [];
    }
}
export async function getDepartmentRecord(api) {
    try {
        const response = await fetch(apiUrl, {
            headers: { "Content-Type": "application/json" },
        });
        return await response.json();
    }
    catch (error) {
        return [];
    }
}

export async function userLogin(inputData) {
    debugger
    try {
        const response = await fetch('https://localhost:7077/api/Account/Login', {
            headers: {
                'Content-type': 'application/json; charset=UTF-8',
            },
            method: 'POST',
            body: JSON.stringify(inputData),
        });
        return await response.json();
    }
    catch (error) {
        return [];
    }
}

export async function registerUserDetails(inputData) {

    try {
        const response = await fetch('https://localhost:7077/api/Account/RegisterUser', {
            method: 'POST',
            body: JSON.stringify(inputData),
            headers: {
                'Content-type': 'application/json; charset=UTF-8',
            }
        });
        // .then((response) => response.json())
        // .then((data) => {
        //     console.log(data);
        //     // Handle data
        // })
        // .catch((err) => {
        //     console.log(err.message);
        // }); 
        return await response.json();
    }
    catch (error) {
        return [];
    }
}

export async function saveJobOpening(inputData) {
    try {
        const response = await fetch('https://localhost:7077/api/Job/SaveJobOpening', {
            method: 'POST',
            body: JSON.stringify(inputData),
            headers: {
                'Content-type': 'application/json; charset=UTF-8',
            }
        });
        return await response.json();
    }
    catch (error) {
        return [];
    }
}

export async function saveStudentDetails(inputData) {
    try {
        const response = await fetch('https://localhost:7077/api/Student/SaveStudentDetails', {
            method: 'POST',
            body: JSON.stringify(inputData),
            headers: {
                'Content-type': 'application/json; charset=UTF-8',
            }
        });
        return await response.json();
    }
    catch (error) {
        return [];
    }
}

export async function getAllDepartmentByOrganizationId(organizationId) {
    try {
        const response = await fetch('https://localhost:7077/api/Department/GetAllDepartmentByOrganizationId/' + organizationId, {
            headers: { "Content-Type": "application/json" },
        });
        return await response.json();
    }
    catch (error) {
        return [];
    }
}

export async function addCoreAreasToDepartment(inputData) {
    try {
        const response = await fetch('https://localhost:7077/api/Department/AddCoreAreasToDepartment', {
            method: 'POST',
            body: JSON.stringify(inputData),
            headers: {
                'Content-type': 'application/json; charset=UTF-8',
            }
        });
        return await response.json();
    }
    catch (error) {
        return [];
    }
}
