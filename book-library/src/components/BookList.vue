<template>
    <div class="books-list">
        <div v-for="(book, i) in books" :key="i">
            <BookCard :book="book" />
        </div>
    </div>
</template>
<script setup lang="ts">
import { onMounted } from 'vue';
import { useStore } from 'vuex';
import { reactive, ref } from '@vue/reactivity';
import BookCard from './BookCard.vue';
import { Book } from '@/common/contracts';

const store = useStore();

onMounted(() => {
    store.dispatch('GetAll');
})

const books = reactive<Book[]>(store.getters.Books);

</script>

<style scoped>
.books-list {
    display: grid;
    grid-template-columns: repeat(auto-fit, minmax(240px, 1fr));
    column-gap: 1.5em;
    row-gap: 1em;
    padding: 1em;
}
</style>