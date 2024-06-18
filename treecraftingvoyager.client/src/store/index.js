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
        },
        setUserEmail(state, email) {
            state.userEmail = email;
        },
    },
    actions: {
        async login({ commit }, credentials) {
            const response = await apiClient.post('auth/login', credentials);
            if (response.token) {
                document.cookie = `token=${response.token};path=/;secure`;
                commit('setToken', response.Token);
                commit('setAuthentication', true);
                commit('setUserEmail', credentials.email);
            }
        },
        async register({ commit, dispatch }, userData) {
            const response = await apiClient.post('auth/register', userData);
            if (response.result === 'User created successfully') {
                await dispatch('login', { email: userData.email, password: userData.password });
            }
        },
        async checkAuth({ commit }) {
            const response = await apiClient.get('auth/check');
            if (response.isAuthenticated) {
                commit('setAuthentication', true);
                // Zak³adaj¹c, ¿e API zwraca email zalogowanego u¿ytkownika
                const userResponse = await apiClient.get('auth/user');
                commit('setUserEmail', userResponse.email);
            }
        },
        logout({ commit }) {
            document.cookie = 'token=; Max-Age=0; path=/; secure';
            commit('setAuthentication', false);
            commit('setToken', '');
            commit('setUserEmail', '');
        },
    },
    getters: {
        isLoggedIn: state => !!state.token,
    }
});
