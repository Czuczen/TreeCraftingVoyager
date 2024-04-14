<template>
    <div v-if="isLoading" class="loader"></div>
    <div id="primaryMenu" v-if="isRoot" class="dropdown me-4">
        <a class="dropdown-item dropdown-toggle" href="javascript:void(0);" @click.prevent="toggleSubmenu(null)">
            Sklep
        </a>
        <ul class="dropdown-menu" :style="{display: rootOpen ? 'block' : 'none'}">
            <li v-for="category in sortedCategories" :key="category.id" class="dropdown-submenu">
                <a class="dropdown-item" href="javascript:void(0);" @click.prevent="showCategoryProducts(category.id)">
                    {{ category.name }}
                </a>
                <span class="toggle-icon" @click.prevent="toggleSubmenu(category.id)">
                    <i>→</i>
                </span>
                <ul v-if="category.childrens && category.childrens.length" class="dropdown-menu" :style="{display: category.open ? 'block' : 'none'}">
                    <CategoriesTree :initialCategories="category.childrens" :isRoot="false" />
                </ul>
            </li>
        </ul>
    </div>

    <template v-if="!isRoot">
        <li v-for="category in sortedCategories" :key="category.id" class="dropdown-submenu">
            <a class="dropdown-item" href="javascript:void(0);" @click.prevent="showCategoryProducts(category.id)">
                {{ category.name }}
            </a>
            <span v-if="category.childrens && category.childrens.length" class="toggle-icon" @click.prevent="toggleSubmenu(category.id)">
                <i>→</i>
            </span>
            <ul v-if="category.childrens && category.childrens.length" class="dropdown-menu" :style="{display: category.open ? 'block' : 'none'}">
                <CategoriesTree :initialCategories="category.childrens" :isRoot="false" />
            </ul>
        </li>
    </template>
</template>

<style>
    .dropdown-submenu {
        position: relative;
        display: flex;
        justify-content: space-between;
        align-items: center;
    }

    .dropdown-submenu .dropdown-menu {
        display: none;
        position: absolute;
        left: 100%; 
        top: 0;
        margin-top: 0;
        margin-left: 0;
        z-index: 1000;
    }

    .toggle-icon {
        cursor: pointer;
        padding: 8px;
    }

    @media (max-width: 768px) {
        .dropdown-submenu .dropdown-menu {
            left: 0; /* Dla urządzeń mobilnych pozostaw rozwijanie w dół */
            top: 100%;
        }
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
                default: true
            }
        },
        data() {
            return {
                categories: [],
                rootOpen: false,
                isLoading: false 
            };
        },
        computed: {
            sortedCategories() {
                return this.categories.sort((a, b) => {
                    return a.displayOrder - b.displayOrder;
                });
            }
        },
        methods: {
            fetchCategories() {
                this.isLoading = true;
                fetch('/api/Categories/GetHierarchy')
                    .then(response => {
                        if (!response.ok) {
                            throw new Error('Network response was not ok');
                        }
                        return response.json();
                    })
                    .then(data => {
                        this.categories = data.map(category => ({
                            ...category,
                            open: false
                        }));
                    })
                    .catch(error => {
                        console.error('Fetching error:', error);
                        alert("Coś poszło nie tak. Spróbuj ponownie. \n\r Lub poczekaj aż backend się załaduje");
                    })
                    .finally(() => {
                        this.isLoading = false;
                    });
            },
            toggleSubmenu(categoryId) {
                if (categoryId === null) { // Handle root category toggle
                    this.rootOpen = !this.rootOpen;
                    if (this.rootOpen) { // Close all other submenus when the root opens
                        this.categories.forEach(cat => cat.open = false);
                    }
                } else {
                    this.categories.forEach(cat => {
                        if (cat.id !== categoryId) {
                            cat.open = false; // Close all other categories
                        }
                    });
                    const category = this.categories.find(c => c.id === categoryId);
                    category.open = !category.open; // Toggle the clicked category
                }
            },
            showCategoryProducts(id) {
                this.$router.push({ name: 'CategoryProducts', params: { id: id } });
            },
            closeMenu() {
                this.rootOpen = false;
                this.categories.forEach(cat => cat.open = false);
            },
            handleOutsideClick(event) {
                const primaryMenu = document.getElementById('primaryMenu');
                let targetElement = event.target; // clicked element

                do {
                    if (targetElement == primaryMenu) {
                        return; // This means the click is inside primaryMenu, stop the function
                    }
                    // Go up the DOM
                    targetElement = targetElement.parentNode;
                } while (targetElement);

                if (!this.$el.contains(event.target) && this.rootOpen) {
                    this.closeMenu(); // Close the menu if the click is outside the component
                }
            }
        },
        mounted() {
            document.addEventListener('click', this.handleOutsideClick);
            if (!this.initialCategories.length) {
                this.fetchCategories();
            } else {
                this.categories = this.initialCategories.map(category => ({
                    ...category,
                    open: false
                }));
            }
        },
        beforeDestroy() {
            document.removeEventListener('click', this.handleOutsideClick);
        },
    };
</script>
