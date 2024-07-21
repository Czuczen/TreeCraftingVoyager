import { createStore } from 'vuex';
import AuthService from '@/services/authService';

export default createStore({
    state: {
        token: document.cookie.replace(/(?:(?:^|.*;\s*)token\s*=\s*([^;]*).*$)|^.*$/, "$1") || '',
        isAuthenticated: false,
        userEmail: '',
        userId: '',
        userClaims: [],
        userRoles: []
    },
    mutations: {
        setUserId(state, id) {
            state.userId = id;
        },
        setAuthentication(state, isAuthenticated) {
            state.isAuthenticated = isAuthenticated;
        },
        setToken(state, token) {
            state.token = token;
            if (token) {
                document.cookie = `token=${token};path=/; secure; SameSite=Strict`;
            } else {
                document.cookie = 'token=; Max-Age=0; path=/; secure; SameSite=Strict';
            }
        },
        setUserEmail(state, email) {
            state.userEmail = email;
        },
        setUserClaims(state, claims) {
            state.userClaims = claims;
        },
        setUserRoles(state, roles) {
            state.userRoles = roles;
        }
    },
    actions: {
        async login({ commit }, credentials) {
            const response = await AuthService.login(credentials);
            if (response.token) {
                commit('setToken', response.token);
                commit('setAuthentication', true);
                commit('setUserEmail', response.email);
                commit('setUserId', response.id);
                // Set user claims and roles if available in the response
            }
        },
        async register({ dispatch }, userData) {
            const response = await AuthService.register(userData);
            if (response.data.result.includes("User created")) {
                await dispatch('login', { email: userData.email, password: userData.password });
            }
        },
        async confirmEmail({ commit }, { userId, code }) {
            const response = await AuthService.confirmEmail(userId, code);
            return response;
        },
        async checkAuth({ commit }) {
            try {
                const response = await AuthService.checkAuth();
                if (response.isAuthenticated) {
                    commit('setAuthentication', true);
                    commit('setUserEmail', response.email);
                    commit('setUserId', response.id);
                    // Set user claims and roles if available in the response
                } else {
                    commit('setAuthentication', false);
                    commit('setToken', '');
                    commit('setUserEmail', '');
                    commit('setUserId', '');
                }
            } catch (error) {
                commit('setAuthentication', false);
                commit('setToken', '');
                commit('setUserEmail', '');
                commit('setUserId', '');
            }
        },
        logout({ commit }) {
            AuthService.logout();
            commit('setAuthentication', false);
            commit('setToken', '');
            commit('setUserEmail', '');
            commit('setUserId', '');
        },
    },
    getters: {
        isLoggedIn: state => !!state.token,
        userClaims: state => state.userClaims,
        userRoles: state => state.userRoles
    }
});
