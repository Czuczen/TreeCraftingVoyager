import { createStore } from 'vuex';
import apiClient from '@/api';

export default createStore({
    state: {
        token: document.cookie.replace(/(?:(?:^|.*;\s*)token\s*=\s*([^;]*).*$)|^.*$/, "$1") || '',
        isAuthenticated: false,
        userEmail: ''
    },
    mutations: {
        setAuthentication(state, isAuthenticated) {
            state.isAuthenticated = isAuthenticated;
        },
        setToken(state, token) {
            state.token = token;
            if (token) {
                document.cookie = `token=${token};path=/;secure;SameSite=Strict`;
            } else {
                document.cookie = 'token=; Max-Age=0; path=/; secure; SameSite=Strict';
            }
        },
        setUserEmail(state, email) {
            state.userEmail = email;
        },
    },
    actions: {
        async login({ commit }, credentials) {
            const response = await apiClient.post('auth/login', credentials);
            if (response.token) {
                commit('setToken', response.token);
                commit('setAuthentication', true);
                commit('setUserEmail', credentials.email);
            }
        },
        async register({ dispatch }, userData) {
            const response = await apiClient.post('auth/register', userData);
            if (response.result === 'User created successfully') {
                await dispatch('login', { email: userData.email, password: userData.password });
            }
        },
        async checkAuth({ commit }) {
            try {
                const response = await apiClient.get('auth/check');
                if (response.isAuthenticated) {
                    commit('setAuthentication', true);
                    const userResponse = await apiClient.get('auth/user');
                    commit('setUserEmail', userResponse.email);
                } else {
                    commit('setAuthentication', false);
                    commit('setToken', '');
                }
            } catch (error) {
                commit('setAuthentication', false);
                commit('setToken', '');
            }
        },
        logout({ commit }) {
            commit('setToken', '');
            commit('setAuthentication', false);
            commit('setUserEmail', '');
        },
    },
    getters: {
        isLoggedIn: state => !!state.token,
    }
});
