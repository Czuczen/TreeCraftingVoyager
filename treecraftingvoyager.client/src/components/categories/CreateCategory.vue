<script setup>
    import FormBtns from '@/components/shared/FormBtns.vue';
</script>

<template>
    <div class="container">
        <h2>Dodaj nową kategorię</h2>
        <form @submit.prevent="submitCategory">
            <div class="mb-3">
                <label for="categoryName" class="form-label">Nazwa kategorii</label>
                <input type="text" class="form-control" id="categoryName" v-model="category.name">
            </div>
            <div class="mb-3">
                <label for="categoryDescription" class="form-label">Opis</label>
                <input type="text" class="form-control" id="categoryDescription" v-model="category.description">
            </div>

            <div class="mb-3">
                <label for="categoryImageURL" class="form-label">Url</label>
                <input type="text" class="form-control" id="categoryImageURL" v-model="category.imageURL">
            </div>
            <div class="mb-3">
                <label for="categorySeoKeywords" class="form-label">SEO</label>
                <input type="text" class="form-control" id="categorySeoKeywords" v-model="category.seoKeywords">
            </div>
            <div class="mb-3">
                <label for="categoryIsActive" class="form-label">IsActive</label>
                <input type="checkbox" class="form-control form-check-input" id="categoryIsActive" v-model="category.isActive">
            </div>
            <div class="mb-3">
                <label for="categoryDisplayOrder" class="form-label">Kolejność</label>
                <input type="number" class="form-control" id="categoryDisplayOrder" v-model="category.displayOrder">
            </div>

            <FormBtns></FormBtns>
        </form>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                category: {
                    name: '',
                    description: '',
                    imageURL: '',
                    seoKeywords: '',
                    isActive: true,
                    displayOrder: 1
                }
            };
        },
        methods: {
            submitCategory() {
                try {
                    fetch('/api/Categories/CreateCategory', {
                        method: 'post',
                        headers: {
                            'Content-Type': 'application/json', 
                        },
                        body: JSON.stringify(this.category)
                    })
                    .then(response => {
                        if (!response.ok) {
                            throw new Error('Network response was not ok');
                        }
                        return response.json();
                    })
                    .then(data => {
                        console.log(data);
                        this.$router.push('/categories');
                    })
                    .catch(error => {
                        console.error('There has been a problem with your fetch operation:', error);
                    });
                } catch (error) {
                    console.error('Fetching error:', error);
                }
            }
        }
    };
</script>
