<template>
    <div class="d-flex justify-content-center align-items-center min-vh-100">
        <div class="card w-50">
            <div class="card-body">
                <h5 class="card-title text-center">{{ $t('login') }}</h5>
                <Form @submit="login">
                    <div class="mb-3">
                        <label for="email" class="form-label">{{ $t('email') }}</label>
                        <Field name="email" type="email" class="form-control" id="email" rules="required|email" />
                        <ErrorMessage name="email" class="text-danger" />
                    </div>
                    <div class="mb-3">
                        <label for="password" class="form-label">{{ $t('password') }}</label>
                        <Field name="password" type="password" class="form-control" id="password" rules="required" />
                        <ErrorMessage name="password" class="text-danger" />
                    </div>
                    <button type="submit" class="btn btn-primary w-100">{{ $t('login') }}</button>
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

            const login = async () => {
                try {
                    await store.dispatch('login', { email: email.value, password: password.value });
                    router.push('/');
                } catch (error) {
                    console.error(error);
                    alert('Logowanie nie powiodło się');
                }
            };

            return {
                email,
                password,
                errors,
                login
            };
        }
    };
</script>
