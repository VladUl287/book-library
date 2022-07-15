<template>
    <div class="auth col-9 row mx-auto">
        <form class="g-3 needs-validation col-5" @submit="onLogin" novalidate>
            <div class="col-12">
                <input type="text" v-model="loginForm.email" class="form-control" id="validationCustom01" required
                    placeholder="email...">
            </div>
            <div class="col-12">
                <input type="text" v-model="loginForm.password" class="form-control" id="validationCustom02" required
                    placeholder="пароль...">
            </div>
            <div class="col-12">
                <button class="btn btn-primary" type="submit">Submit form</button>
            </div>
        </form>
        <form class="g-3 needs-validation col-5" @submit="onRegister" novalidate>
            <div class="col-12">
                <input type="text" v-model="registerForm.email" class="form-control" id="validationCustom01" required
                    placeholder="email...">
            </div>
            <div class="col-12">
                <input type="text" v-model="registerForm.password" class="form-control" id="validationCustom02" required
                    placeholder="пароль...">
            </div>
            <div class="col-12">
                <input type="text" v-model="registerForm.confirmPassword" class="form-control" id="validationCustom02"
                    required placeholder="подтверждение пароль...">
            </div>
            <div class="col-12">
                <button class="btn btn-primary" type="submit">Submit form</button>
            </div>
        </form>
    </div>
</template>

<script setup lang="ts">
import store from '@/store'
import { ref } from '@vue/reactivity';
import { useRouter } from 'vue-router';
import { LoginForm, RegisterForm } from '@/common/contracts';
import { AuthActions } from '@/store/common/enums';

const router = useRouter();

const loginForm = ref<LoginForm>({
    email: '',
    password: ''
})

const registerForm = ref<RegisterForm>({
    email: '',
    password: '',
    confirmPassword: ''
})

const onLogin = async (e: Event) => {
    e.preventDefault();

    try {
        await store.dispatch(AuthActions.LOGIN, loginForm.value);
        router.push("/");
    } catch (error) {
        console.log();
    } finally {
        console.log();
    }
}

const onRegister = async (e: Event) => {
    e.preventDefault();

    try {
        await store.dispatch("Register", registerForm.value);
        router.push("/");
    } catch (error) {
        console.log();
    } finally {
        console.log();
    }
}
</script>