<template>
    <div class="header">
        <p>Все книги</p>
    </div>
    <div class="main-wrap">
        <BookFilters />
        <div class="books">
            <BookList :books="booksModule.books" />
        </div>
    </div>
</template>

<script setup lang="ts">
import BookList from '@/components/BookList.vue';
import { booksModule } from '@/store/modules/books';
import BookFilters from '@/components/BookFilters.vue';
import { useRoute } from 'vue-router';
import { onMounted, ref, watch } from 'vue';
import { Guid } from 'guid-typescript';
import { Author, Book } from '@/common/contracts';

const route = useRoute()
const books = ref<Book[]>([])
const author = ref<Author | undefined>()

onMounted(() => {
    initilize();
})

watch(
    () => route.params.bookId,
    () => {
        initilize();
    }
)

const initilize = () => {
    const authorPromise = booksModule.getBooksByAuthor(Guid.parse(route.params.authorId as string));
    const booksPromise = booksModule.getBooksByAuthor(Guid.parse(route.params.authorId as string));
    Promise.all([authorPromise, booksPromise]).then(
        (data) => {
            // author.value = data[0];
            books.value = data[1];
        }
    );
}
</script>

<style scoped>
</style>