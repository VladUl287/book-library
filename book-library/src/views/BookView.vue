<template>
    <div class="book-wrap" v-if="book">
        <div class="book-img">
            <img :src="book.image" />
        </div>
        <div class="book-info">
            <p>{{ book.name }}</p>
            <p>{{ book.description }}</p>
            <p>{{ book.pagesCount }}</p>
            <p v-for="(genre, i) in book.genres" :key="i">{{ genre.name }}</p>
            <p v-for="(author, i) in book.authors" :key="i">{{ author.name }}</p>
            <button @click="addBookmark">Добавить в закладки</button>
            <button>Поделиться</button>
            <button>Цены</button>
            <button>Карта</button>
            <button>Пометить как прочитанную</button>
        </div>
    </div>
    <div class="reviews-wrap">
        <div>
            Рецензии
        </div>
        <div>
            <input type="number" name="" id="" v-model="rating">
            <textarea name="" id="" cols="30" rows="10" v-model="text"></textarea>
            <button @click="createReview">Create</button>
        </div>
    </div>
</template>

<script setup lang="ts">
import { useRoute } from 'vue-router';
import { Guid } from 'guid-typescript';
import { Book } from '@/common/contracts';
import { onMounted, ref, watch } from 'vue';
import { booksModule } from '@/store/modules/books';
import { bookmarksModule } from '@/store/modules/bookmarks';

const route = useRoute()
const book = ref<Book | undefined>(booksModule.selectedBook)
const rating = ref<number>();
const text = ref<string>();

onMounted(async () => {
    await getBook()
})

watch(
    () => route.params.bookId,
    async () => {
        await getBook();
    }
)

const getBook = async (): Promise<void> => {
    const bookId = Guid.parse(route.params.bookId as string)
    await booksModule.getBookById(bookId)
}

const createReview = async (): Promise<void> => {

}

const addBookmark = async (): Promise<void> => {
    if (book.value) {
        await bookmarksModule.addBookmark(Guid.parse(book.value.id.toString()));
    }
}

</script>

<style scoped>
.book-wrap {
    display: grid;
    grid-template-columns: auto 1fr;
    margin: 0 auto;
    width: 70%;
    padding: 15px 0;
}

.book-wrap .book-img img {
    width: 250px;
}

.book-wrap .book-info {
    padding: 0 10px;
}

.book-wrap .book-info p {
    text-align: left;
}
</style>