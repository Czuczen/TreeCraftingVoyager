<template>
    <div class="container">
        <h2>Kategorie</h2>
        <div class="list-group">
            <router-link v-for="category in categories"
                         :key="category.id"
                         :to="{ name: 'category-details', params: { id: category.id }}"
                         class="list-group-item list-group-item-action">
                {{ category.name }}
                <p class="small">{{ category.description }}</p>
            </router-link>
        </div>
    </div>
</template>

<script>
    export default {
        name: "IndexCategories",
        data() {
            return {
                categories: []
            };
        },
        methods: {
            async fetchCategories() {
                try {
                    fetch('/api/categories/GetCategories')
                        .then(response => {
                            if (!response.ok) {
                                throw new Error('Network response was not ok');
                            }
                            return response.json();
                        })
                        .then(json => {
                            this.categories = json;
                        })
                        .catch(error => {
                            console.error('Error fetching categories:', error);
                        });
                } catch (error) {
                    console.error('Error fetching logs:', error);
                }
            }
        },
        mounted() {
            this.fetchCategories();
        }
    };
</script>

<style scoped>
    
</style>
