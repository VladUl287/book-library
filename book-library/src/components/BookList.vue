<template>
    <div class="books-list">
        <div v-for="(book, i) in books" :key="i">
            <BookCard :book="book" />
        </div>
    </div>
</template>
<script setup lang="ts">
import { onMounted } from 'vue';
import { computed } from '@vue/reactivity';
import BookCard from './BookCard.vue';
import { Book } from '@/common/contracts';
import { BooksActions } from '@/store/enums';
import { useStore } from '@/store';

const store = useStore();
const books = computed<Book[]>(() => store.getters.getBooks);

onMounted(() => {
    if (books.value.length == 0) {
        store.dispatch(BooksActions.GET_ALL_BOOKS);
    }
})

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