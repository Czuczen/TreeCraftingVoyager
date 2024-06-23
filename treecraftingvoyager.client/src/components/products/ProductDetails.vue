<script setup>
    import BackBtn from '@/components/shared/BackBtn.vue';
</script>

<template>
    <div class="container mt-4">
        <div class="d-flex justify-content-center">
            <h4 class="header-entry px-5 glow-effect">Szczegóły produktu</h4>
        </div>
        <div v-if="isLoading" class="loader"></div>
        <div v-if="product" class="card">
            <div class="card-body">
                <h5 class="card-title">{{ product.name }}</h5>
                <p class="card-text">{{ product.description }}</p>
                <p class="card-text">{{ product.price }} zł</p>
                <p class="card-text">{{ formattedDate(product.expirationDate) }}</p>
                <p class="card-text">Kategoria: {{ product.categoryName }}</p>
            </div>
        </div>
    </div>

    <BackBtn :returnPath="'/products'" class="me-auto"></BackBtn>
</template>

<script>
    import moment from 'moment';
    import apiClient from '@/api';

    export default {
        name: 'ProductDetails',
        data() {
            return {
                product: null,
                isLoading: false 
            };
        },
        async created() {
            await this.fetchProductDetails();
        },
        methods: {
            async fetchProductDetails() {
                try {
                    this.isLoading = true;
                    const id = this.$route.params.id;
                    apiClient.get(`Products/Details/${id}`)
                        .then(r => r.data)
                        .then(json => {
                            this.product = json;
                        })
                        .finally(() => {
                            this.isLoading = false;
                        });
                } catch (error) {
                    console.error('Fetching error:', error);
                    alert("Coś poszło nie tak. Spróbuj ponownie.");
                }
            },
            formattedDate(expirationDate) {
                return expirationDate ? moment(expirationDate).format('DD/MM/YYYY HH:mm') : '';
            }
        },
    };
</script>
