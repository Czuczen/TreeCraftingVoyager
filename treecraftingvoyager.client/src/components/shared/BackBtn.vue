<template>
    <button class="btn btn-secondary m-1" @click="goBack">Wróć</button>
</template>

<script>
    export default {
        props: {
            returnPath: {
                type: String,
                default: '/'
            }
        },
        methods: {
            goBack() {
                this.$router.push(this.returnPath);
            }
        }
    };
</script>
