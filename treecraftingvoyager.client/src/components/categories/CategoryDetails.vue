<script setup>
    import BackBtn from '@/components/shared/BackBtn.vue';
</script>

<template>
    <div class="container mt-4">
        <div class="d-flex justify-content-center">
            <h4 class="header-entry px-5 glow-effect">Szczegóły kategorii</h4>
        </div>
        <div v-if="isLoading" class="loader"></div>
        <div v-if="category" class="card">
            <img :src="category.imageURL" class="card-img-top" alt="Zdjęcie kategorii" style="object-fit: cover; min-height: 3rem">
            <div class="card-body">
                <h5 class="card-title">{{ category.name }}</h5>
                <p class="card-text">{{ category.description }}</p>
                <p class="card-text">Kategoria nadrzędna: {{ category.parentName ?? "Brak"}}</p>
            </div>
        </div>
    </div>

    <BackBtn :returnPath="'/categories'" class="me-auto"></BackBtn>
</template>

<script>
    import apiClient from '@/api';

    export default {
        name: 'CategoryDetails',
        data() {
            return {
                category: null,
                isLoading: false
            };
        },
        async created() {
            await this.fetchCategoryDetails();
        },
        methods: {
            async fetchCategoryDetails() {
                try {
                    this.isLoading = true;
                    const id = this.$route.params.id;
                    apiClient.get(`Categories/Details/${id}`)
                        .then(r => r.data)
                        .then(json => {
                            this.category = json;
                        })
                        .finally(() => {
                            this.isLoading = false;
                        });
                } catch (error) {
                    console.error('Fetching error:', error);
                    alert("Coś poszło nie tak. Spróbuj ponownie.");
                }
            },
        },
    };
</script>
