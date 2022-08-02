<template>
  <div class="main-wrap">
    <div class="books">
      <BookList :books="books"/>
    </div>
  </div>
</template>

<script setup lang="ts">
import BookList from '@/components/BookList.vue';
import { Book } from '@/common/contracts';
import { booksModule } from '@/store/modules/books';
import { onMounted, ref } from 'vue';

const books = ref<Book[]>([])

onMounted(async () => {
  books.value = await booksModule.getNoveltiesBooks()
})

</script>

<style>
.main-wrap {
  max-width: 60%;
  margin: 0 auto;
}
</style>