<script setup>import FormBtns from '@/components/shared/FormBtns.vue';</script>

<template>
    <div class="container">
        <h2>Dodaj nowy produkt</h2>
        <form @submit.prevent="submitProduct">
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
            </div>
            <div class="mb-3">
                <label for="productPrice" class="form-label">Cena</label>
                <input type="number" class="form-control" id="productPrice" v-model.number="product.price" step="0.01">
                <div v-if="errors.Price && errors.Price.length" class="alert alert-danger">
                    {{ errors.Price[0] }}
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
            </div>

            <FormBtns></FormBtns>
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
                    categoryId: null
                },
                categories: [],
                errors: {
                    Name: [],
                    ExpirationDate: []
                }
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
                    const response = await fetch('/api/Categories/Get');
                    if (!response.ok) throw new Error('Failed to fetch categories');

                    this.categories = await response.json();
                } catch (error) {
                    console.error('Fetching error:', error);
                    alert("Nie udało się wczytać kategorii. Spróbuj ponownie.");
                }
            },
            fetchProduct() {
                const productId = this.$route.params.id;
                fetch(`/api/Products/Get/${productId}`)
                    .then(response => response.json())
                    .then(data => {
                        if (data) {
                            this.product = { ...this.product, ...data };
                        }
                    })
                    .catch(error => {
                        console.error('There has been a problem with your fetch operation:', error);
                        alert("Coś poszło nie tak. Spróbuj ponownie.");
                    });
            },
            async submitProduct() {
                this.errors = {}; // Clear errors before submission

                try {
                    const response = await fetch('/api/Products/Update', {
                        method: 'PUT',
                        headers: {
                            'Content-Type': 'application/json',
                        },
                        body: JSON.stringify(this.product)
                    });

                    if (!response.ok) {
                        const responseData = await response.json();
                        this.errors = responseData.errors;
                        throw new Error('Validation failed');
                    }

                    this.$router.push('/products');
                } catch (error) {
                    console.error('Submission error:', error);
                    if (!this.errors || Object.keys(this.errors).length === 0) {
                        alert("Coś poszło nie tak przy aktualizowaniu kategorii. Spróbuj ponownie.");
                    }
                }
            },
        },
        mounted() {
            this.fetchProduct();
            this.fetchCategories();
        }
    };
</script>
