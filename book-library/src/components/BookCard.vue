<template>
    <div class="book-card">
        <img :src="book?.image" />
        <div>{{ book?.name }}</div>
    </div>
</template>
<script setup lang="ts">
import { defineProps } from 'vue';

defineProps({
    book: Object
});
</script>

<style scoped>
.book-card {
    width: 100px;
}

.book-card img {
    width: 100%;
}
</style>