<template>
  <div class="main-wrap">
    <BookFilters />
    <div class="books">
      <BookList :books="books"/>
    </div>
  </div>
</template>

<script setup lang="ts">
import { Book } from '@/common/contracts';
import { computed } from '@vue/reactivity';
import BookList from '@/components/BookList.vue';
import { booksModule } from '@/store/modules/books';
import BookFilters from '@/components/BookFilters.vue';

const books = computed<Book[]>(() => booksModule.books)

</script>

<style>
.main-wrap {
  max-width: 60%;
  margin: 0 auto;
}
</style>