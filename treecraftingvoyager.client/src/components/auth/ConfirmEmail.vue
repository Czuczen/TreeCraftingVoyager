<template>
    <div class="d-flex justify-content-center align-items-center">
        <div class="card w-50">
            <div class="card-body">
                <h5 class="card-title text-center">{{ message }}</h5>
            </div>
        </div>
    </div>
</template>

<script>
import { ref, onMounted } from 'vue';
import { useRoute } from 'vue-router';
import { useStore } from 'vuex';

export default {
    setup() {
        const route = useRoute();
        const store = useStore();
        const message = ref('');

        onMounted(async () => {
            const { userId, code } = route.query;
            try {
                const response = await store.dispatch('confirmEmail', { userId, code });
                message.value = response;
            } catch (error) {
                message.value = 'Error confirming your email.';
                console.error(error);
            }
        });

        return {
            message
        };
    }
};
</script>
