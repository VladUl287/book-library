<template>
  <div class="main-wrap">
    <div class="books">
      <BookList :books="books" />
    </div>
  </div>
</template>

<script setup lang="ts">
import { Book } from '@/common/contracts'
import { computed, onMounted, ref } from 'vue'
import BookList from '@/components/BookList.vue'
import { booksModule } from '@/store/modules/books'

const books = ref<Book[]>([])

onMounted(async () => {
  books.value = await booksModule.getRecommendations()
})

</script>

<style>
.main-wrap {
  max-width: 60%;
  margin: 0 auto;
}
</style>