<template>
    <div class="books-list">
        <div v-for="book in books" :key="book.id.toString()">
            <BookCard :book="book" />
        </div>
    </div>
</template>
<script setup lang="ts">
import BookCard from './BookCard.vue';
import { Book } from '@/common/contracts';
import { booksModule } from '@/store/modules/books';
import { onMounted, defineProps, PropType } from 'vue';

defineProps({
    books: {
        type: Object as PropType<Book[]>,
        required: true
    }
});

onMounted(() => {
    booksModule.getBooks();
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