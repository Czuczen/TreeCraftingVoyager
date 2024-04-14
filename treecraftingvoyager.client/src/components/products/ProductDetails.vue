<script setup>
    import BackBtn from '@/components/shared/BackBtn.vue';
</script>

<template>
    <div class="container mt-4">
        <h2 class="text-center mb-3">Szczegóły produktu</h2>
        <div v-if="isLoading" class="loader"></div>
        <div v-if="product" class="card">
            <div class="card-body">
                <h5 class="card-title">{{ product.name }}</h5>
                <p class="card-text">{{ product.description }}</p>
                <p class="card-text">{{ product.price }} zł</p>
                <p class="card-text">{{ formattedDate(product.expirationDate) }}</p>
                <!--ładowanie include?-->
                <p class="card-text">Kategoria: {{ product.categoryId }}</p>
            </div>
        </div>
    </div>

    <BackBtn></BackBtn>
</template>

<script>
    import moment from 'moment';
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
                    fetch(`/api/Products/Get/${id}`)
                        .then(r => r.json())
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
                return expirationDate ? moment(expirationDate).format('DD/MM/YYYY') : '';
            }
        },
    };
</script>
