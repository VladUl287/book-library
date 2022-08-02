<template>
  <div class="header">
    <p>Все книги</p>
  </div>
  <div class="main-wrap">
    <BookFilters />
    <div class="books">
      <BookList :books="booksModule.books"/>
    </div>
  </div>
</template>

<script setup lang="ts">
import BookList from '@/components/BookList.vue';
import { booksModule } from '@/store/modules/books';
import BookFilters from '@/components/BookFilters.vue';

</script>

<style scoped>
.header {
  display: flex;
  user-select: none;
  justify-content: center;
}
.header p {
  margin: 30px 0 50px 0;
  font-size: xx-large;
}
.main-wrap {
  max-width: 90%;
  margin: 0 auto;
}
</style>