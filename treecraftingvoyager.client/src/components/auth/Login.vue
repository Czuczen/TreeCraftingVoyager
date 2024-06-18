<template>
    <div>
        <h1>Login</h1>
        <form @submit.prevent="handleLogin">
            <input v-model="email" type="email" placeholder="Email" required>
            <input v-model="password" type="password" placeholder="Password" required>
            <button type="submit">Login</button>
        </form>
        <p>Nie posiadasz konta? <router-link to="/register">Zarejestruj siê</router-link></p>
    </div>
</template>

<script>
    import { mapActions } from 'vuex';

    export default {
        data() {
            return {
                email: '',
                password: '',
            };
        },
        methods: {
            ...mapActions(['login']),
            async handleLogin() {
                await this.login({ email: this.email, password: this.password });
                this.$emit('login');
                this.$router.push({ name: 'HomePage' });
            },
        },
    };
</script>
