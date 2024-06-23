<template>
    <div class="container mt-3 text-center">
        <div class="d-flex justify-content-center">
            <h4 class="header-entry px-5 glow-effect">Kategorie</h4>
        </div>
        <div class="d-flex justify-content-end">
            <button class="btn btn-primary mb-2" @click="createCategory">Dodaj</button>
        </div>
        <div v-if="isLoading" class="loader"></div>
        <div class="table-responsive rounded">
            <table class="table table-bordered table-hover">
                <thead class="table-dark">
                    <tr>
                        <th>#</th>
                        <th @click="sortTable('name')" class="sortable">Nazwa</th>
                        <th @click="sortTable('description')" class="sortable">Opis</th>
                        <th>Akcje</th>
                    </tr>
                </thead>
                <tbody class="text-second">
                    <tr v-for="(category, index) in sortedCategories" :key="category.id" class="table-secondary">
                        <td>{{ index + 1 }}</td>
                        <td>{{ category.name }}</td>
                        <td>{{ category.description }}</td>
                        <td>
                            <button class="btn btn-success btn-sm m-1" @click="showDetails(category.id)">Szczegóły</button>
                            <button class="btn btn-purple btn-sm m-1" @click="editCategory(category.id)">Edytuj</button>
                            <button class="btn btn-danger btn-sm m-1" @click="promptDeleteCategory(category.id, category.name)">Usuń</button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>

        <!-- Delete modal-->
        <div class="modal fade" id="deleteCategoryModal" tabindex="-1" aria-labelledby="deleteCategoryModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="deleteCategoryModalLabel">Usuń kategorię</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        Czy na pewno chcesz usunąć kategorię "{{ categoryNameToDelete }}"?
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Anuluj</button>
                        <button type="button" class="btn btn-danger" @click="deleteCategory">Usuń</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import { Modal } from 'bootstrap';
    import apiClient from '@/api';

    export default {
        name: "IndexCategories",
        data() {
            return {
                categories: [],
                categoryIdToDelete: null,
                categoryNameToDelete: '',
                deleteModal: null,
                currentSort: 'name',
                currentSortDir: 'asc',
                isLoading: false 
            };
        },
        computed: {
            sortedCategories() {
                return this.categories.sort((a, b) => {
                    let modifier = 1;
                    if (this.currentSortDir === 'desc') modifier = -1;
                    if (a[this.currentSort] < b[this.currentSort]) return -1 * modifier;
                    if (a[this.currentSort] > b[this.currentSort]) return 1 * modifier;
                    return 0;
                });
            }
        },
        methods: {
            fetchCategories() {
                try {
                    this.isLoading = true;
                    apiClient.get('Categories/Get')
                        .then(r => r.data)
                        .then(json => {
                            this.categories = json;
                        })
                        .finally(() => {
                            this.isLoading = false;
                        });
                } catch (error) {
                    console.error('Fetching error:', error);
                    alert("Coś poszło nie tak. Spróbuj ponownie.");
                }
            },
            createCategory() {
                this.$router.push('/create-category');
            },
            showDetails(id) {
                this.$router.push({ name: 'CategoryDetails', params: { id: id } });
            },
            editCategory(id) {
                this.$router.push({ name: 'EditCategory', params: { id: id } });
            },
            promptDeleteCategory(id, name) {
                this.categoryIdToDelete = id;
                this.categoryNameToDelete = name;

                const modalElement = document.getElementById('deleteCategoryModal');
                this.deleteModal = new Modal(modalElement);
                this.deleteModal.show();
            },
            deleteCategory() {
                if (!this.categoryIdToDelete) return;

                this.isLoading = true;
                apiClient.delete(`Categories/Delete/${this.categoryIdToDelete}`)
                .then(() => {
                    this.fetchCategories();
                })
                .finally(() => {
                    this.deleteModal.hide();
                    this.categoryIdToDelete = null;
                    this.categoryNameToDelete = '';
                    this.isLoading = false;
                });
            },
            sortTable(sortKey) {
                if (this.currentSort === sortKey) {
                    this.currentSortDir = this.currentSortDir === 'asc' ? 'desc' : 'asc';
                } else {
                    this.currentSort = sortKey;
                    this.currentSortDir = 'asc';
                }
            }
        },
        mounted() {
            this.fetchCategories();
        }
    };
</script>

<style>
    .btn-purple {
        background-color: #6f42c1;
        border-color: #6f42c1;
        color: white;
    }

    .btn-purple:hover {
        background-color: #5a35b1;
        border-color: #512da8;
    }

    .sortable {
        color: blue;
        cursor: pointer;
        text-decoration: underline;
    }
</style>
