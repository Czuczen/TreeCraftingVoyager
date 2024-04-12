<template>
    <div v-if="isRoot" class="dropdown me-4">
        <a class="dropdown-item dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
            Sklep
        </a>
        <ul class="dropdown-menu">
            <li v-for="category in categories" :key="category.id" class="dropdown-submenu">
                <a class="dropdown-item" 
                   :class="{'dropdown-toggle': category.childrens && category.childrens.length}" 
                   :href="`/Products/GetByCategory/${category.id}`">{{ category.name }}
                </a>
                

                <CategoriesTree v-if="category.childrens && category.childrens.length" :initialCategories="category.childrens" :isRoot="false" />
            </li>
        </ul>
    </div>
    
    <ul v-else class="dropdown-menu submenu">
        <li v-for="category in categories" :key="category.id" class="dropdown-submenu">
            <a class="dropdown-item"
               :class="{'dropdown-toggle': category.childrens && category.childrens.length}"
               :href="`/Products/GetByCategory/${category.id}`">
                {{ category.name }}
            </a>
            
            <CategoriesTree v-if="category.childrens && category.childrens.length" :initialCategories="category.childrens" :isRoot="false" />
        </li>
    </ul>
</template>

<style>
.dropdown-submenu {
    position: relative;
}

.dropdown-submenu .dropdown-menu {
    display: none;
    position: absolute;
    left: 100%; /* Pozycjonuj menu po prawej stronie elementu nadrzędnego */
    top: 0;
    margin-top: 0;
    margin-left: 0;
}

.dropdown-submenu:hover .dropdown-menu {
    display: block; /* Pokaż menu podrzędne gdy najedzie się na element nadrzędny */
}
</style>

<script>
    export default {
        name: 'CategoriesTree',
        props: {
            initialCategories: {
                type: Array,
                default: () => [],
            },
            isRoot: {
                type: Boolean,
                default: true // Domyślnie ustawiamy na true dla głównego wywołania
            }
        },
        data() {
            return {
                categories: [],
            };
        },
        methods: {
            fetchCategories() {
                fetch('/api/Categories/GetHierarchy')
                    .then(response => {
                        if (!response.ok) {
                            throw new Error('Network response was not ok');
                        }
                        return response.json();
                    })
                    .then(data => {
                        this.categories = data;
                    })
                    .catch(error => {
                        console.error('Fetching error:', error);
                        alert("Coś poszło nie tak. Spróbuj ponownie.");
                    });
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
