<template>
    <div class="container container-flex-auto">
        <header>
            <nav class="navbar rounded-bottom navbar-expand-sm navbar-toggleable-sm navbar-light border-bottom shadow-lg mb-3">
                <div class="container-fluid">
                    <router-link class="navbar-brand" to="/">TCV</router-link>
                    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                            aria-expanded="false" aria-label="Toggle navigation">
                        <span class="navbar-toggler-icon"></span>
                    </button>
                    <div class="navbar-collapse collapse d-sm-inline-flex justify-content-between">
                        <div>
                            <router-link class="nav-link text-dark ms-2" to="/">Strona główna</router-link>
                        </div>
                        <ul class="navbar-nav flex-grow-1 justify-content-center">
                            <li><router-link class="nav-link text-dark me-4" to="/categories">Kategorie</router-link></li>
                            <li class="nav-link text-dark">
                                <CategoriesTree></CategoriesTree>
                            </li>
                            <li><router-link class="nav-link text-dark" to="/products">Produkty</router-link></li>
                        </ul>
                        <div>
                            <router-link class="nav-link text-dark me-2" to="/about">O mnie</router-link>
                        </div>
                        <div>
                            <router-link class="nav-link text-dark ms-2" to="/logs">Logi</router-link>
                        </div>
                        <div v-if="isAuthenticated">
                            <span class="nav-link text-dark">Welcome, {{ userEmail }}!</span>
                            <button @click="logout" class="btn btn-link nav-link text-dark">Wyloguj</button>
                        </div>
                        <div v-else>
                            <router-link class="nav-link text-dark ms-2" to="/login">Zaloguj</router-link>
                            <router-link class="nav-link text-dark ms-2" to="/register">Zarejestruj</router-link>
                        </div>
                    </div>
                </div>
            </nav>
        </header>

        <div class="container container-flex-auto">
            <router-view></router-view>
        </div>

        <footer class="border-top footer text-muted rounded-top shadow-lg mt-4">
            <div class="container d-block">
                &copy; 2024 - Tree Crafting Voyager - <router-link to="/privacy">Prywatność</router-link>
            </div>
        </footer>
    </div>
</template>

<script>
    import { mapActions, mapState } from 'vuex';
    import CategoriesTree from '@/components/categories/CategoriesTree.vue';

    export default {
        components: {
            CategoriesTree
        },
        computed: {
            ...mapState(['isAuthenticated', 'userEmail'])
        },
        methods: {
            ...mapActions(['logout'])
        },
        mounted() {
            this.$store.dispatch('checkAuth');
        }
    };
</script>
