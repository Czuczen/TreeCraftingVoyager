<template>
    <div class="d-flex justify-content-center align-items-center min-vh-100">
        <div class="card w-50">
            <div class="card-body">
                <h5 class="card-title text-center">{{ $t('login') }}</h5>
                <Form @submit="login">
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
                        <Field name="password" type="password" class="form-control" id="password" v-model="password" rules="required" />
                        <ErrorMessage name="password" class="text-danger" />
                        <div v-if="backendErrors.password" class="text-danger">
                            {{ backendErrors.password[0] }}
                        </div>
                    </div>
                    <div class="mb-3 form-check">
                        <input type="checkbox" class="form-check-input" id="rememberMe" v-model="rememberMe">
                        <label class="form-check-label" for="rememberMe">{{ $t('rememberMe') }}</label>
                    </div>
                    <button type="submit" class="btn btn-primary w-100">{{ $t('login') }}</button>
                </Form>
                <div v-if="backendErrors.general" class="text-danger text-center mt-3">
                    {{ backendErrors.general }}
                </div>
                <div class="mt-3">
                    <!--<p>
                        <router-link to="/forgot-password">{{ $t('forgotPassword') }}</router-link>
                    </p>-->
                    <p>
                        <router-link to="/register">{{ $t('register') }}</router-link>
                    </p>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import { ref } from 'vue';
    import { useForm } from 'vee-validate';
    import { useStore } from 'vuex';
    import { useRouter, useRoute } from 'vue-router';

    export default {
        setup() {
            const store = useStore();
            const router = useRouter();
            const route = useRoute();
            const { handleSubmit, errors } = useForm();
            const email = ref('');
            const password = ref('');
            const rememberMe = ref(false);
            const backendErrors = ref({});

            const login = handleSubmit(async () => {
                backendErrors.value = {};
                try {
                    const response = await store.dispatch('login', { email: email.value, password: password.value, rememberMe: rememberMe.value });
                    const returnUrl = route.query.returnUrl || '/';
                    router.push(returnUrl);
                } catch (error) {
                    if (error.errors) {
                        backendErrors.value = error.errors;
                    } else if (error.response?.data?.[""]?.[0]) {
                        backendErrors.value.general = error.response.data[""][0];
                    } else {
                        console.error(error);
                        backendErrors.value.general = 'Login failed';
                    }
                }
            });

            return {
                email,
                password,
                rememberMe,
                errors,
                backendErrors,
                login
            };
        }
    };
</script>
