let API_URL;
if (import.meta.env.MODE === 'development') {
    API_URL = 'https://localhost:5173';
} else {
    API_URL = 'https://your-production-server.com';
}

const getHeaders = () => {
    const token = document.cookie.replace(/(?:(?:^|.*;\s*)token\s*=\s*([^;]*).*$)|^.*$/, "$1");
    return {
        'Content-Type': 'application/json',
        ...(token && { 'Authorization': `Bearer ${token}` })
    };
};

const apiClient = {
    get: async (endpoint) => {
        const response = await fetch(`${API_URL}/api/${endpoint}`, {
            method: 'GET',
            headers: getHeaders(),
            credentials: 'include', // To include cookies
        });
        return response.json();
    },
    post: async (endpoint, data) => {
        const response = await fetch(`${API_URL}/api/${endpoint}`, {
            method: 'POST',
            headers: getHeaders(),
            body: JSON.stringify(data),
            credentials: 'include', // To include cookies
        });
        return response.json();
    },
    // Additional methods (PUT, DELETE, etc.) can be added here
};

export default apiClient;
