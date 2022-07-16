<template>
    <div class="books-create">
        <form class="col-5" @submit="onCreate" novalidate>
            <div class="col-12">
                <input type="text" v-model="bookForm.name" class="form-control" required placeholder="название">
            </div>
            <div class="col-12">
                <input type="text" v-model="bookForm.description" class="form-control" required placeholder="описание">
            </div>
            <div class="col-12">
                <input type="text" v-model="bookForm.description" class="form-control" required>
            </div>
            <div class="col-12">
                <button class="btn btn-primary" type="submit">Submit form</button>
            </div>
        </form>
    </div>
</template>

<script setup lang="ts">
import { ref } from '@vue/reactivity';
import { useRouter } from 'vue-router';

const router = useRouter();

const bookForm = ref<any>({
    name: '',
    description: ''
})

const onCreate = async (e: Event) => {
    e.preventDefault();

    try {
        router.push("/");
    } catch (error) {
        console.log();
    } finally {
        console.log();
    }
}
</script>