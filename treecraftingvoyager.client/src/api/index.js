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

    //if (error.response) {
    //    if (error.response.status === 400 && error.response.data && error.response.data.errors) {
    //        return error.response; 
    //    }
    //}

    console.error(error);
    return Promise.reject(error);
});

export default apiClient;
