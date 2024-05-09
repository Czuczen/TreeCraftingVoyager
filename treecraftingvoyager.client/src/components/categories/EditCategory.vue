<script setup>
    import FormBtns from '@/components/shared/FormBtns.vue';
</script>

<template>
    <div class="container">
        <div class="d-flex justify-content-center">
            <h4 class="header-entry px-5 glow-effect">Edytuj kategorię</h4>
        </div>
        <div v-if="isLoading" class="loader"></div>
        <form @submit.prevent="submitCategory" @keydown.enter.prevent="submitCategory" class="text-white">
            <input type="hidden" id="categoryId" v-model="category.id">
            <div class="mb-3">
                <label for="categoryName" class="form-label">Nazwa kategorii</label>
                <input type="text" class="form-control" id="categoryName" v-model="category.name">
                <div v-if="errors.Name && errors.Name.length" class="alert alert-danger">
                    {{ errors.Name[0] }}
                </div>
            </div>
            <div class="mb-3">
                <label for="categoryDescription" class="form-label">Opis</label>
                <input type="text" class="form-control" id="categoryDescription" v-model="category.description">
                <div v-if="errors.Description && errors.Description.length" class="alert alert-danger">
                    {{ errors.Description[0] }}
                </div>
            </div>
            <div class="mb-3">
                <label for="categoryImageURL" class="form-label">Url obrazka</label>
                <input type="text" class="form-control" id="categoryImageURL" v-model="category.imageURL">
                <div v-if="errors.ImageURL && errors.ImageURL.length" class="alert alert-danger">
                    {{ errors.ImageURL[0] }}
                </div>
            </div>
            <div class="mb-3">
                <label for="categoryDisplayOrder" class="form-label">Kolejność wyświetlania</label>
                <input type="number" class="form-control" id="categoryDisplayOrder" v-model="category.displayOrder">
                <div v-if="errors.DisplayOrder && errors.DisplayOrder.length" class="alert alert-danger">
                    {{ errors.DisplayOrder[0] }}
                </div>
            </div>
            <div class="mb-3">
                <label for="categoryParentId" class="form-label">Rodzic</label>
                <select class="form-control" id="categoryParentId" v-model="category.parentId">
                    <option :value=null>Brak rodzica</option>
                    <option v-for="parent in parents" :value="parent.id" :key="parent.id">
                        {{ parent.name }}
                    </option>
                </select>
                <div v-if="errors.ParentId && errors.ParentId.length" class="alert small alert-danger">
                    {{ errors.ParentId[0] }}
                </div>
            </div>

            <FormBtns :returnPath="'/categories'"></FormBtns>
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
                    displayOrder: 0,
                    parentId: null
                },
                parents: [],
                errors: {
                    Name: [],
                    Description: [],
                    ImageURL: [],
                    SeoKeywords: [],
                    IsActive: [],
                    DisplayOrder: [],
                    ParentId: []
                },
                isLoading: false
            };
        },
        methods: {
            async fetchParents() {
                try {
                    this.isLoading = true;
                    const categoryId = this.$route.params.id;
                    const response = await fetch('/api/Categories/Get');

                    this.isLoading = false;
                    if (!response.ok) throw new Error('Failed to fetch parent categories');

                    let categories = await response.json();
                    this.parents = categories.filter(category => category.id != categoryId);
                    if (categoryId)
                        this.category = categories.find(category => category.id == categoryId) || this.category;

                } catch (error) {
                    console.error('Fetching error:', error);
                    alert("Nie udało się wczytać kategorii rodzica. Spróbuj ponownie.");
                }
            },
            async submitCategory() {
                this.errors = {};

                try {
                    this.isLoading = true;
                    const response = await fetch('/api/Categories/Update', {
                        method: 'PUT',
                        headers: {
                            'Content-Type': 'application/json',
                        },
                        body: JSON.stringify(this.category)
                    });

                    this.isLoading = false;

                    if (!response.ok) {
                        const responseData = await response.json();
                        this.errors = responseData.errors;
                        throw new Error('Validation failed');
                    }

                    this.$router.push('/categories');
                } catch (error) {
                    console.error('Submission error:', error);
                    if (!this.errors || Object.keys(this.errors).length === 0) {
                        alert("Coś poszło nie tak przy aktualizowaniu kategorii. Spróbuj ponownie.");
                    }
                }
            }
        },
        mounted() {
            this.fetchParents();
        }
    };
</script>
