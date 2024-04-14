<template>
    <div class="container mt-4">
        <h2 class="text-center mb-3">Produkty</h2>
        <div v-for="product in products" :key="product.id" class="card mb-2">
            <div class="card-body">
                <h5 class="card-title">{{ product.name }}</h5>
                <p class="card-text">{{ product.description }}</p>
                <p class="card-text">{{ product.price }} zł</p>
                <p class="card-text">{{ formattedDate(product.expirationDate) }}</p>
                <!--ładowanie include?-->
                <p class="card-text">Kategoria: {{ product.categoryId }}</p>

            </div>
            <button class="btn btn-primary">Kup</button>
        </div>
    </div>
</template>

<script>
    import moment from 'moment';

    export default {
        name: 'CategoryProducts',
        data() {
            return {
                products: [],
            };
        },
        watch: {
            '$route.params.id': {
                immediate: true,
                handler(newId, oldId) {
                    this.fetchProductsByCategory(newId);
                }
            }
        },
        methods: {
            async fetchProductsByCategory() {
                try {
                    const id = this.$route.params.id;
                    fetch(`/api/Categories/GetByCategory/${id}`)
                        .then(r => r.json())
                        .then(json => {
                            this.products = json;
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
