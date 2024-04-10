<template>
    <div class="container mt-3">
        <h4 class="text-center">Kategorie</h4>
        <div class="d-flex justify-content-end">
            <button class="btn btn-primary mb-2" @click="createCategory">Dodaj</button>
        </div>
        <div class="table-responsive">
            <table class="table table-bordered table-hover">
                <thead class="table-info">
                    <tr>
                        <th>#</th>
                        <th>Nazwa</th>
                        <th>Opis</th>
                        <th>Status</th>
                        <th>Akcje</th>
                    </tr>
                </thead>
                <tbody class="text-second">
                    <tr v-for="(category, index) in categories" :key="category.id">
                        <td>{{ index + 1 }}</td>
                        <td>{{ category.name }}</td>
                        <td>{{ category.description }}</td>
                        <td>{{ category.isActive ? 'Aktywna' : 'Nieaktywna' }}</td>
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
    export default {
        name: "IndexCategories",
        data() {
            return {
                categories: [],
                categoryIdToDelete: null,
                categoryNameToDelete: '',
                deleteModal: null
            };
        },
        methods: {
            fetchCategories() {
                try {
                    fetch('/api/Categories/Get')
                        .then(r => r.json())
                        .then(json => {
                            this.categories = json;
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

                fetch(`/api/Categories/Delete/${this.categoryIdToDelete}`, {
                    method: 'DELETE',
                })
                    .then(response => {
                        if (!response.ok) {
                            throw new Error('Network response was not ok');
                        }
                        return;
                    })
                    .then(() => {
                        this.fetchCategories();
                    })
                    .catch(error => {
                        console.error('There has been a problem with your fetch operation:', error);
                        alert("Coś poszło nie tak. Spróbuj ponownie.");
                    })
                    .finally(() => {
                        this.deleteModal.hide();
                        this.categoryIdToDelete = null;
                        this.categoryNameToDelete = '';
                    });
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
</style>
