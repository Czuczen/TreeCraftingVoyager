<template>
    <button class="btn btn-secondary m-1" @click="goBack">Wróć</button>
</template>

<script>
    export default {
        methods: {
            goBack() {
                this.$router.go(-1);
            }
        }
    };
</script>
