<script setup>
    import BackBtn from '@/components/shared/BackBtn.vue';
</script>

<template>
    <div class="container mt-4">
        <h2 class="text-center mb-3">Szczegóły kategorii</h2>
        <div v-if="category" class="card">
            <img :src="category.ImageURL" class="card-img-top" alt="Zdjęcie kategorii" style="object-fit: cover; min-height: 3rem">
            <div class="card-body">
                <h5 class="card-title">{{ category.name }}</h5>
                <p class="card-text">{{ category.description }}</p>
                <p class="card-text">Kategoria nadrzędna: {{ category.parent ? category.parent.name : 'Brak' }}</p>
                <h6>Kategorie podrzędne:</h6>
                <ul>
                    <li v-for="child in category.Childrens" :key="child.Id">{{ child.name }}</li>
                </ul>
            </div>
        </div>
        <div v-else class="text-center">
            <p>Ładowanie szczegółów kategorii...</p>
        </div>
    </div>

    <BackBtn></BackBtn>
</template>

<script>
    export default {
        name: 'CategoryDetails',
        data() {
            return {
                category: null,
            };
        },
        async created() {
            await this.fetchCategoryDetails();
        },
        methods: {
            async fetchCategoryDetails() {
                try {
                    const id = this.$route.params.id;
                    fetch(`/api/Categories/Get/${id}`)
                        .then(r => r.json())
                        .then(json => {
                            this.category = json;
                        });
                } catch (error) {
                    console.error('Fetching error:', error);
                     alert("Coś poszło nie tak. Spróbuj ponownie.");
                }
            },
        },
    };
</script>
