<script setup>
    import BackBtn from '@/components/shared/BackBtn.vue';
    import SaveBtn from '@/components/shared/SaveBtn.vue';
</script>

<template>
    <div class="d-flex bd-highlight">
        <div class="bd-highlight me-auto">
            <BackBtn :returnPath="returnPath"></BackBtn>
        </div>
        <div class="bd-highlight">
            <SaveBtn></SaveBtn>
        </div>
    </div>
</template>

<script>
    export default {
        props: {
            returnPath: {
                type: String
            }
        },
        
    };
</script>
