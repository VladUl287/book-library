<template>
    <div class="book-mark-card">
        <div class="book-image-wrap">
            <img :src="book.image" />
        </div>
        <div class="book-image-info">
            <p>{{ book.name }}</p>
            <p v-for="author in book.authors" :key="author.id.toString()">{{ author.name }}</p>
            <p>{{ book.description }}</p>
            <p>{{ book.pagesCount }}</p>
        </div>
    </div>
</template>
<script setup lang="ts">
import { Guid } from 'guid-typescript';
import { defineProps, PropType } from 'vue';
import { useStore } from 'vuex';
import { Book } from '@/common/contracts';

const props = defineProps({
    book: {
        type: Object as PropType<Book>,
        required: true
    }
});

const store = useStore();

const removeBookmark = async (id: Guid) => {
    try {
        await store.dispatch('RemoveBookmark', id);
        let book = props.book;
        book.bookmark = false;
        store.commit('updateBook', book);
    } catch (error) {
        console.log();
    }
}

</script>

<style>
.book-mark-card {
    display: flex;
    color: #f1f1f1;
    margin: 10px 0 0 0;
}

.book-mark-card img {
    max-width: 180px;
}

.book-image-info {
    width: 100%;
    padding: 10px 0 0 10px;
}

.book-image-info p {
    margin: 0;
    text-align: left;
}
</style>