import axios from 'axios';

const apiClient = axios.create({
    baseURL: '/api',
    withCredentials: true, // Included cookies in requests
    headers: {
        'Content-Type': 'application/json'
    }
});

apiClient.interceptors.request.use(config => {
    const token = document.cookie.replace(/(?:(?:^|.*;\s*)token\s*=\s*([^;]*).*$)|^.*$/, "$1") || '';
    if (token) {
        console.log("Sended token with req: " + token);
        config.headers['Authorization'] = `Bearer ${token}`;
    }
    return config;
}, error => {
    console.error(error);
    return Promise.reject(error);
});

apiClient.interceptors.response.use(response => {
    return response;
}, error => {

    // Requests limit exceeded
    if (error.response) {
        if (error.response.status === 429 && error.response.data) {
            alert(error.response.data);
        }
    }

    // Unauthorized
    if (error.response) {
        if (error.response.status === 401) {
            if (error.response.data) {
                alert(error.response.data);
            } else {
                alert("Lack of access");
            }
        }
    }

    // Forbidden
    if (error.response) {
        if (error.response.status === 403) {
            if (error.response.data) {
                alert(error.response.data);
            } else {
                alert("Lack of access");
            }
        }
    }

    console.error(error);
    return Promise.reject(error);
});

export default apiClient;
