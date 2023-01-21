
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

export async function saveRegistrationDetails(inputData) {
    fetch('http://localhost:5110/api/WorkflowTemplate/Save?orgId=' + _orgId + '&workflowId=' + 1 + '&comments=' + 'Test comment' + '&fileContent=' + inputData, {
        method: 'POST',
        mode: 'no-cors',
        headers: {
            // 'Accept': 'application/json',
            // 'Content-Type': 'application/json',
            'Access-Control-Allow-Origin': '*'
        }
    });
}
