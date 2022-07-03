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
import { ref } from '@vue/reactivity';
import BookCard from './BookCard.vue';
import { Book } from '@/common/contracts';

const store = useStore();

onMounted(() => {
    store.dispatch('GetAll');
})

const books = ref<Book[]>(store.getters.Books);

</script>

<style scoped>
.books-list {
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(100px, 1fr));
}
</style>