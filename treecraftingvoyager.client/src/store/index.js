import { createStore } from 'vuex';
import apiClient from '@/api';

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
            const response = await apiClient.post('auth/login', credentials);
            if (response.data.token) {
                console.log("Token after login req: " + response.data.token);
                commit('setToken', response.data.token);
                commit('setAuthentication', true);
                commit('setUserEmail', credentials.email);
                commit('setUserId', response.data.id);


                //const decodedToken = JSON.parse(atob(response.data.token.split('.')[1]));
                //commit('setUserClaims', decodedToken);
                //const roles = decodedToken.role ? (Array.isArray(decodedToken.role) ? decodedToken.role : [decodedToken.role]) : [];
                //commit('setUserRoles', roles);
            }
        },
        async register({ dispatch }, userData) {
            const response = await apiClient.post('auth/register', userData);
            if (response.data.result === 'User created successfully') {
                await dispatch('login', { email: userData.email, password: userData.password });
            }
        },
        async checkAuth({ commit }) {
            try {
                const response = await apiClient.get('auth/check');
                if (response.data.isAuthenticated) {
                    commit('setAuthentication', true);
                    commit('setUserEmail', response.data.email);
                    commit('setUserId', response.data.id);

                    //const decodedToken = JSON.parse(atob(response.data.token.split('.')[1]));
                    //commit('setUserClaims', decodedToken);
                    //const roles = decodedToken.role ? (Array.isArray(decodedToken.role) ? decodedToken.role : [decodedToken.role]) : [];
                    //commit('setUserRoles', roles);
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
