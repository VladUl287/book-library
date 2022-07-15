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
import { useStore } from '@/store';
import { BookmarkActions } from '@/store/common/enums';

const store = useStore();
const books = computed(() => store.getters.getBookmarks);

onMounted(() => {
  store.dispatch(BookmarkActions.GET_BOOKMARKS);
});

</script>

<style>
</style>