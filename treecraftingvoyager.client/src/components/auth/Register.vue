<template>
    <div class="d-flex justify-content-center align-items-center min-vh-100">
        <div class="card w-50">
            <div class="card-body">
                <h5 class="card-title text-center">{{ $t('register') }}</h5>
                <Form @submit="register">
                    <div class="mb-3">
                        <label for="email" class="form-label">{{ $t('email') }}</label>
                        <Field name="email" type="email" class="form-control" id="email" v-model="email" rules="required|email" />
                        <ErrorMessage name="email" class="text-danger" />
                        <div v-if="backendErrors.email" class="text-danger">
                            {{ backendErrors.email[0] }}
                        </div>
                    </div>
                    <div class="mb-3">
                        <label for="password" class="form-label">{{ $t('password') }}</label>
                        <Field name="password" type="password" class="form-control" id="password" v-model="password" rules="required|min:6" />
                        <ErrorMessage name="password" class="text-danger" />
                        <div v-if="backendErrors.password" class="text-danger">
                            {{ backendErrors.password[0] }}
                        </div>
                    </div>
                    <div class="mb-3">
                        <label for="confirmPassword" class="form-label">{{ $t('confirmPassword') }}</label>
                        <Field name="confirmPassword" type="password" class="form-control" id="confirmPassword" v-model="confirmPassword" rules="required|confirmed:@password" />
                        <ErrorMessage name="confirmPassword" class="text-danger" />
                        <div v-if="backendErrors.confirmPassword" class="text-danger">
                            {{ backendErrors.confirmPassword[0] }}
                        </div>
                    </div>
                    <button type="submit" class="btn btn-primary w-100">{{ $t('register') }}</button>
                </Form>
            </div>
        </div>
    </div>
</template>

<script>
    import { ref } from 'vue';
    import { useForm } from 'vee-validate';
    import { useStore } from 'vuex';
    import { useRouter } from 'vue-router';

    export default {
        setup() {
            const store = useStore();
            const router = useRouter();
            const { handleSubmit, errors } = useForm();
            const email = ref('');
            const password = ref('');
            const confirmPassword = ref('');
            const backendErrors = ref({});

            const register = handleSubmit(async () => {
                backendErrors.value = {};
                try {
                    await store.dispatch('register', { email: email.value, password: password.value });
                    router.push('/');
                } catch (error) {
                    if (error.errors) {
                        backendErrors.value = error.errors;
                    } else {
                        console.error(error);
                        alert('Rejestracja nie powiodła się');
                    }
                }
            });

            return {
                email,
                password,
                confirmPassword,
                errors,
                backendErrors,
                register
            };
        }
    };
</script>
