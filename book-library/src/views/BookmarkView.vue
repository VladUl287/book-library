<template>
  <div>
    <div v-for="book in books" :key="book.id.toString()">
      <BookmarkCard :book="book" />
    </div>
  </div>
</template>

<script setup lang="ts">
import { useStore } from 'vuex';
import { onMounted } from 'vue';
import { computed } from '@vue/reactivity';
import BookmarkCard from '@/components/BookmarkCard.vue';

const store = useStore();

const books = computed(() => store.getters.Bookmarks);

onMounted(() => {
  store.dispatch('GetBookmarks');
});

</script>

<style>


</style>