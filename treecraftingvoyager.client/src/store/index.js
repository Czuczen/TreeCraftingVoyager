import { createStore } from 'vuex';
import AuthService from '@/services/authService';
import * as jwtDecode from 'jwt-decode'; // named import

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
                const decodedToken = jwtDecode.default(response.token);
                commit('setToken', response.token);
                commit('setAuthentication', true);
                commit('setUserEmail', decodedToken.email);
                commit('setUserId', decodedToken.sub);
                // Set user claims and roles if available in the token
            }
        },
        async register({ dispatch }, userData) {
            const response = await AuthService.register(userData);
            if (response.data.result === 'User created successfully') {
                await dispatch('login', { email: userData.email, password: userData.password });
            }
        },
        async checkAuth({ commit }) {
            try {
                const response = await AuthService.checkAuth();
                if (response.isAuthenticated) {
                    const token = document.cookie.replace(/(?:(?:^|.*;\s*)token\s*=\s*([^;]*).*$)|^.*$/, "$1");
                    const decodedToken = jwtDecode.default(token);
                    commit('setAuthentication', true);
                    commit('setUserEmail', decodedToken.email);
                    commit('setUserId', decodedToken.sub);
                    // Set user claims and roles if available in the token
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
