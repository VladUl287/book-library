<template>
    <div class="collection-books-list">
        <div v-for="(book, i) in books" :key="i">
            {{ book.name }}
        </div>
    </div>
</template>

<script setup lang="ts">
import { Book } from '@/common/contracts';
import { onMounted, ref, watch } from 'vue';
import { booksModule } from '@/store/modules/books'
import { useRoute } from 'vue-router';
import { Guid } from 'guid-typescript';

const route = useRoute();
const books = ref<Book[]>([])

watch(
    () => route.params.bookId,
    async () => {
        await getBooks();
    }
)

onMounted(async () => {
    await getBooks();
})

const getBooks = async () => {
    const collectionId = Guid.parse(route.params.collectionId as string);
    books.value = await booksModule.getBooksByCollection(collectionId);
}

</script>

<style scoped>
</style>