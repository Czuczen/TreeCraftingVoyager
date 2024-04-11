<template>
    <div class="dropdown me-4">
        <a class="dropdown-item dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
            Sklep
        </a>
        <ul class="dropdown-menu">
            <li v-for="category in categories" :key="category.id">
                <a class="dropdown-item" href="#">{{ category.name }}</a>
                <!-- Rekurencyjne wstawianie komponentu dla podkategorii -->
                <CategoriesTree v-if="category.childrens && category.childrens.length" :initialCategories="category.childrens" />
            </li>
        </ul>
    </div>
</template>

<script>
    export default {
        name: 'CategoriesTree',
        props: {
            initialCategories: {
                type: Array,
                default: () => [],
            },
        },
        data() {
            return {
                categories: [],
            };
        },
        methods: {
            fetchCategories() {
                try {
                    fetch('/api/Categories/GetHierarchy')
                        .then(r => r.json())
                        .then(json => {
                            this.categories = json;
                        });
                } catch (error) {
                    console.error('Fetching error:', error);
                    alert("Coś poszło nie tak. Spróbuj ponownie.");
                }
            },
        },
        mounted() {
            if (!this.initialCategories.length) {
                this.fetchCategories();
            } else {
                this.categories = this.initialCategories;
            }
        },
    };
</script>
