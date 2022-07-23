<template>
    <div class="books-info-list">
        <div v-for="(book, i) in books" :key="i">
            <BookInfoCard :book="book" />
        </div>
    </div>
</template>
<script setup lang="ts">
import { Book } from '@/common/contracts';
import { defineProps, PropType } from 'vue';

defineProps({
    books: {
        type: Object as PropType<Book[]>,
        required: true
    }
});

</script>

<style scoped>
</style>