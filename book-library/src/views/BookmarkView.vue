<template>
  <div>
    <div v-for="(book, i) in books" :key="i">
      <BookmarkCard :book="book" />
    </div>
  </div>
</template>

<script setup lang="ts">
import { onMounted } from 'vue';
import { computed } from '@vue/reactivity';
import BookmarkCard from '@/components/BookmarkCard.vue';
import { bookmarksModule } from '@/store/modules/bookmarks';

const books = computed(() => bookmarksModule.bookmarks);

onMounted(() => {
  bookmarksModule.getBookmarks();
});

</script>

<style>
</style>