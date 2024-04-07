<template>
    <div>
        <form @submit.prevent="submitForm">
            <div class="form-group">
                <label for="name">Name</label>
                <input v-model="category.name" type="text" class="form-control" id="name" required>
            </div>
            <!-- Dodatkowe pola -->
            <button type="submit" class="btn btn-primary">Submit</button>
        </form>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                category: {
                    name: '',
                    imageURL: '',
                    SEOKeywords: '',
                    isActive: true
                }
            };
        },
        methods: {
            submitForm() {

            }
        }
    }
</script>
