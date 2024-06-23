<template>
    <div class="container mt-3 text-center">
        <div class="d-flex justify-content-center">
            <h4 class="header-entry px-5 glow-effect">Produkty</h4>
        </div>
        <div class="d-flex justify-content-end">
            <button class="btn btn-primary mb-2" @click="createProduct">Dodaj</button>
        </div>
        <div v-if="isLoading" class="loader"></div>
        <div class="table-responsive rounded">
            <table class="table table-bordered table-hover">
                <thead class="table-dark">
                    <tr>
                        <th>#</th>
                        <th @click="sortTable('name')" class="sortable">Nazwa</th>
                        <th @click="sortTable('price')" class="sortable">Cena</th>
                        <th @click="sortTable('expirationDate')" class="sortable">Data Ważności</th>
                        <th>Akcje</th>
                    </tr>
                </thead>
                <tbody class="text-second">
                    <tr v-for="(product, index) in sortedProducts" :key="product.id" class="table-secondary">
                        <td>{{ index + 1 }}</td>
                        <td>{{ product.name }}</td>
                        <td>{{ product.price.toFixed(2) }} zł</td>
                        <td>{{ formattedDate(product.expirationDate) }}</td>
                        <td>
                            <button class="btn btn-success btn-sm m-1" @click="showDetails(product.id)">Szczegóły</button>
                            <button class="btn btn-purple btn-sm m-1" @click="editProduct(product.id)">Edytuj</button>
                            <button class="btn btn-danger btn-sm m-1" @click="promptDeleteProduct(product.id, product.name)">Usuń</button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>

        <!-- Delete modal-->
        <div class="modal fade" id="deleteProductModal" tabindex="-1" aria-labelledby="deleteProductModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="deleteProductModalLabel">Usuń Produkt</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        Czy na pewno chcesz usunąć produkt "{{ productNameToDelete }}"?
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Anuluj</button>
                        <button type="button" class="btn btn-danger" @click="deleteProduct">Usuń</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import { Modal } from 'bootstrap';
    import moment from 'moment';
    import apiClient from '@/api';

    export default {
        name: "IndexProducts",
        data() {
            return {
                products: [],
                productIdToDelete: null,
                productNameToDelete: '',
                deleteModal: null,
                currentSort: 'name',
                currentSortDir: 'asc',
                isLoading: false 
            };
        },
        computed: {
            sortedProducts() {
                return this.products.sort((a, b) => {
                    let modifier = 1;
                    if (this.currentSortDir === 'desc') modifier = -1;
                    if (a[this.currentSort] < b[this.currentSort]) return -1 * modifier;
                    if (a[this.currentSort] > b[this.currentSort]) return 1 * modifier;
                    return 0;
                });
            }
        },
        methods: {
            fetchProducts() {
                try {
                    this.isLoading = true;
                    apiClient.get('Products/Get')
                        .then(r => r.data)
                        .then(json => {
                            this.products = json;
                        })
                        .finally(() => {
                            this.isLoading = false;
                        });
                } catch (error) {
                    console.error('Fetching error:', error);
                    alert("Coś poszło nie tak. Spróbuj ponownie.");
                }
            },
            createProduct() {
                this.$router.push('/create-product');
            },
            showDetails(id) {
                this.$router.push({ name: 'ProductDetails', params: { id: id } });
            },
            editProduct(id) {
                this.$router.push({ name: 'EditProduct', params: { id: id } });
            },
            promptDeleteProduct(id, name) {
                this.productIdToDelete = id;
                this.productNameToDelete = name;

                const modalElement = document.getElementById('deleteProductModal');
                this.deleteModal = new Modal(modalElement);
                this.deleteModal.show();
            },
            deleteProduct() {
                if (!this.productIdToDelete) return;

                this.isLoading = true;
                apiClient.delete(`Products/Delete/${this.productIdToDelete}`)
                .then(() => {
                    this.fetchProducts();
                })
                .finally(() => {
                    this.deleteModal.hide();
                    this.productIdToDelete = null;
                    this.productNameToDelete = '';
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
            },
            formattedDate(expirationDate) {
                return expirationDate ? moment(expirationDate).format('DD/MM/YYYY') : '';
            }
        },
        mounted() {
            this.fetchProducts();
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
