<template>
    <div class="d-flex justify-content-center align-items-center min-vh-100">
        <div class="card w-50">
            <div class="card-body">
                <h5 class="card-title text-center">{{ $t('register') }}</h5>
                <Form @submit="register">
                    <div class="mb-3">
                        <label for="email" class="form-label">{{ $t('email') }}</label>
                        <Field name="email" type="email" class="form-control" id="email" rules="required|email" />
                        <ErrorMessage name="email" class="text-danger" />
                    </div>
                    <div class="mb-3">
                        <label for="password" class="form-label">{{ $t('password') }}</label>
                        <Field name="password" type="password" class="form-control" id="password" rules="required|min:6" />
                        <ErrorMessage name="password" class="text-danger" />
                    </div>
                    <div class="mb-3">
                        <label for="confirmPassword" class="form-label">{{ $t('confirmPassword') }}</label>
                        <Field name="confirmPassword" type="password" class="form-control" id="confirmPassword" rules="required|confirmed:password" />
                        <ErrorMessage name="confirmPassword" class="text-danger" />
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

    export default {
        setup() {
            const store = useStore();
            const { errors } = useForm();
            const email = ref('');
            const password = ref('');
            const confirmPassword = ref('');

            const register = async () => {
                try {
                    await store.dispatch('register', { email: email.value, password: password.value });
                    router.push('/');
                } catch (error) {
                    console.error(error);
                    alert('Rejestracja nie powiodła się');
                }
            };

            return {
                email,
                password,
                confirmPassword,
                errors,
                register
            };
        }
    };
</script>
