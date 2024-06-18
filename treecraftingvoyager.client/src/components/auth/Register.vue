<template>
    <div>
        <h1>Register</h1>
        <form @submit.prevent="handleRegister">
            <input v-model="email" type="email" placeholder="Email" required>
            <input v-model="password" type="password" placeholder="Password" required>
            <button type="submit">Register</button>
        </form>
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
            ...mapActions(['register']),
            async handleRegister() {
                await this.register({ email: this.email, password: this.password });
                this.$router.push({ name: 'HomePage' });
            },
        },
    };
</script>
