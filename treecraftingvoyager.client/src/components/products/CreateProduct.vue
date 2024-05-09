<script setup>
    import FormBtns from '@/components/shared/FormBtns.vue';
</script>

<template>
    <div class="container">
        <div class="d-flex justify-content-center">
            <h4 class="header-entry px-5 glow-effect">Dodaj nowy produkt</h4>
        </div>
        <div v-if="isLoading" class="loader"></div>
        <form @submit.prevent="submitProduct" @keydown.enter.prevent="submitProduct" class="text-white">
            <div class="mb-3">
                <label for="productName" class="form-label">Nazwa produktu</label>
                <input type="text" class="form-control" id="productName" v-model="product.name">
                <div v-if="errors.Name && errors.Name.length" class="alert alert-danger">
                    {{ errors.Name[0] }}
                </div>
            </div>
            <div class="mb-3">
                <label for="productDescription" class="form-label">Opis</label>
                <input type="text" class="form-control" id="productDescription" v-model="product.description">
                <div v-if="errors.Description && errors.Description.length" class="alert alert-danger">
                    {{ errors.Description[0] }}
                </div>
            </div>
            <div class="mb-3">
                <label for="productPrice" class="form-label">Cena</label>
                <input type="number" class="form-control" id="productPrice" v-model.number="product.price" step="0.01">
                <div v-if="errors.Price && errors.Price.length" class="alert alert-danger">
                    {{ errors.Price[0] }}
                </div>
            </div>
            <div class="mb-3">
                <label for="productExpirationDate" class="form-label">Data ważności</label>
                <input type="datetime-local" class="form-control" id="productExpirationDate" v-model="formattedExpirationDate">
                <div v-if="errors.ExpirationDate && errors.ExpirationDate.length" class="alert alert-danger">
                    {{ errors.ExpirationDate[0] }}
                </div>
            </div>
            <div class="mb-3">
                <label for="productCategoryId" class="form-label">Kategoria</label>
                <select class="form-control" id="productCategoryId" v-model="product.categoryId">
                    <option value="0">Wybierz kategorię</option>
                    <option v-for="category in categories" :value="category.id" :key="category.id">
                        {{ category.name }}
                    </option>
                </select>
                <div v-if="errors.CategoryId && errors.CategoryId.length" class="alert alert-danger">
                    {{ errors.CategoryId[0] }}
                </div>
            </div>

            <FormBtns :returnPath="'/products'"></FormBtns>
        </form>
    </div>
</template>

<script>
    import moment from 'moment';

    export default {
        data() {
            return {
                product: {
                    name: '',
                    description: '',
                    price: 0.0,
                    expirationDate: null,
                    categoryId: 0
                },
                categories: [],
                errors: {
                    Name: [],
                    Description: [],
                    ExpirationDate: [],
                    Price: [],
                    CategoryId: []
                },
                isLoading: false
            };
        },
        computed: {
            formattedExpirationDate: {
                get() {
                    return this.product.expirationDate ? moment(this.product.expirationDate).format('YYYY-MM-DDTHH:mm') : '';
                },
                set(newValue) {
                    this.product.expirationDate = newValue ? moment(newValue, 'YYYY-MM-DDTHH:mm').toISOString() : null;
                }
            }
        },
        methods: {
            async fetchCategories() {
                try {
                    this.isLoading = true;
                    const response = await fetch('/api/Categories/Get');
                    this.isLoading = false;

                    if (!response.ok) throw new Error('Failed to fetch categories');
                    this.categories = await response.json();
                } catch (error) {
                    console.error('Fetching error:', error);
                    alert("Nie udało się wczytać kategorii. Spróbuj ponownie.");
                }
            },
            async submitProduct() {
                this.errors = {};
                
                try {
                    this.isLoading = true;
                    const response = await fetch('/api/Products/Create', {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json',
                        },
                        body: JSON.stringify(this.product)
                    });

                    this.isLoading = false;

                    if (response.status === 204) {
                        this.$router.push('/products');
                        return;
                    }

                    const responseData = await response.json();

                    if (!response.ok) {
                        this.errors = responseData.errors;
                        throw new Error('Validation failed');
                    }

                    this.$router.push('/products');
                } catch (error) {
                    console.error('Submission error:', error);
                    if (!this.errors || Object.keys(this.errors).length === 0) {
                        alert("Coś poszło nie tak przy dodawaniu produktu. Spróbuj ponownie.");
                    }
                }
            }
        },
        mounted() {
            this.fetchCategories();
        }
    };
</script>
