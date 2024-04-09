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
                            <button class="btn btn-success btn-sm m-1" @click="showDetails(category.id)">Szczegó³y</button>
                            <button class="btn btn-purple btn-sm m-1" @click="editCategory(category.id)">Edytuj</button>
                            <button class="btn btn-danger btn-sm m-1" @click="deleteCategory(category.id)">Usuñ</button>
                        </td>
                    </tr>
                </tbody>
            </table>
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
            fetchCategories() {
                try {
                    fetch('/api/Categories/GetCategories')
                        .then(r => r.json())
                        .then(json => {
                            this.categories = json;
                        });
                } catch (error) {
                    console.error('Error fetching logs:', error);
                }
            },
            createCategory() {
                // Tutaj logika do otwierania modalu lub przekierowania na stronê z formularzem
                /*alert('Otwieranie formularza dodawania kategorii.');*/
                this.$router.push('/create-category');
            },
            showDetails(id) {
                // Metoda zaœlepka
                alert(`Szczegó³y kategorii ${id}`);
            },
            editCategory(id) {
                // Metoda zaœlepka
                alert(`Edycja kategorii ${id}`);
            },
            deleteCategory(id) {
                // Metoda zaœlepka
                alert(`Usuwanie kategorii ${id}`);
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
