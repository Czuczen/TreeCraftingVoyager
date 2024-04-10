<script setup>
    import FormBtns from '@/components/shared/FormBtns.vue';
</script>

<template>
    <div class="container">
        <h2>Edytuj kategorię</h2>
        <form @submit.prevent="submitCategory">
            <input type="hidden" id="categoryId" v-model="category.id">
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
            <div class="mb-3">
                <label for="categoryParentId" class="form-label">Rodzic</label>
                <input type="number" class="form-control" id="categoryParentId" v-model="category.parentId">
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
                    id: null,
                    name: '',
                    description: '',
                    imageURL: '',
                    seoKeywords: '',
                    displayOrder: 1,
                    parentId: null
                }
            };
        },
        methods: {
            fetchCategory() {
                const categoryId = this.$route.params.id;
                fetch(`/api/Categories/Get/${categoryId}`)
                    .then(response => {
                        if (!response.ok) {
                            throw new Error('Network response was not ok');
                        }
                        return response.json();
                    })
                    .then(data => {
                        this.category = data;
                    })
                    .catch(error => {
                        console.error('There has been a problem with your fetch operation:', error);
                        alert("Coś poszło nie tak. Spróbuj ponownie.");
                    });
            },
            submitCategory() {
                fetch('/api/Categories/Update', {
                    method: 'PUT',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify(this.category)
                })
                .then(response => {
                    if (!response.ok) {
                        throw new Error('Network response was not ok');
                    }
                    return;
                })
                .then(() => {
                    this.$router.push('/categories');
                })
                .catch(error => {
                    console.error('There has been a problem with your fetch operation:', error);
                    alert("Coś poszło nie tak. Spróbuj ponownie.");
                });
            }
        },
        mounted() {
            this.fetchCategory();
        }
    };
</script>
