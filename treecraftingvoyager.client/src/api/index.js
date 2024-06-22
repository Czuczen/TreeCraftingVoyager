const getHeaders = () => {
    const token = document.cookie.replace(/(?:(?:^|.*;\s*)token\s*=\s*([^;]*).*$)|^.*$/, "$1") || '';
    console.log('Token from cookie:', token);
    return {
        'Content-Type': 'application/json',
        ...(token && { 'Authorization': `Bearer ${token}` })
    };
};

const apiClient = {
    get: async (endpoint) => {
        const response = await fetch(`/api/${endpoint}`, {
            method: 'GET',
            headers: getHeaders(),
            credentials: 'include', // To include cookies
        });
        return response.json();
    },
    post: async (endpoint, data) => {
        const response = await fetch(`/api/${endpoint}`, {
            method: 'POST',
            headers: getHeaders(),
            body: JSON.stringify(data),
            credentials: 'include', // To include cookies
        });
        return response.json();
    }
};

export default apiClient;
