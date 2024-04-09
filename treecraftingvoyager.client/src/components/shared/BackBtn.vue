<template>
    <div>
        <button class="btn btn-secondary m-1" @click="goBack">Wróć</button>
    </div>
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
