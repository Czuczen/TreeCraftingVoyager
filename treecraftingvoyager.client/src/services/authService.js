import apiClient from '@/api';

class AuthService {
    login(user) {
        return apiClient.post('auth/login', {
            email: user.email,
            password: user.password,
            rememberMe: user.rememberMe
        }).then(response => response.data);
    }

    register(user) {
        return apiClient.post('auth/register', {
            email: user.email,
            password: user.password,
            confirmPassword: user.confirmPassword
        });
    }

    confirmEmail(userId, code) {
        return apiClient.get(`auth/confirm-email`, {
            params: {
                userId: userId,
                code: code
            }
        }).then(response => response.data);
    }

    checkAuth() {
        return apiClient.get('auth/check').then(response => response.data);
    }

    logout() {
        // Implement logout if necessary
    }
}

export default new AuthService();
