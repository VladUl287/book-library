<template>
  <div class="main-wrap">
    <BookFilters />
    <div class="books">
      <BookList />
    </div>
  </div>
</template>

<script setup lang="ts">
import BookList from '@/components/BookList.vue';
import BookFilters from '@/components/BookFilters.vue';

</script>

<style>
.main-wrap {
  max-width: 60%;
  margin: 0 auto;
}
</style>