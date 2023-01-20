export async function getDataService(apiUrl) {
    try {
        const response = await fetch(apiUrl, {
            headers: { "Content-Type": "application/json"},
        });
        return await response.json();    }
    catch (error) {
        return [];
    }
}
