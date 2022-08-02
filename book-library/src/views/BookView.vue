<template>
    <div class="book-wrap" v-if="book">
        <div class="book-img">
            <img :src="book.image" />
        </div>
        <div class="book-info">
            <!-- <p>{{ book.name }}</p> -->
            <!-- <p>{{ book.description }}</p> -->
            <!-- <p>{{ book.pagesCount }}</p> -->
            <!-- <p v-for="(genre, i) in book.genres" :key="i">{{ genre.name }}</p> -->
            <!-- <p v-for="(author, i) in book.authors" :key="i">{{ author.name }}</p> -->
            <h4>
                Преступление и наказание
                <i class="bi bi-bookmark-plus" @click="addBookmark" v-if="book.bookmark"></i>
                <i class="bi bi-bookmark-dash" v-else></i>
            </h4>
            <p>Lorem, ipsum dolor sit amet consectetur adipisicing elit. Praesentium voluptatem nobis,
                reprehenderit, eaque quidem dolorum doloremque libero magni delectus enim unde a ullam molestiae
                placeat dignissimos consequuntur sit sequi officia? Lorem ipsum dolor sit amet consectetur
                adipisicing elit. Reiciendis tenetur iure dolor consectetur beatae quia nemo dicta odio commodi
                voluptates perferendis, dolorum placeat eum consequatur cumque impedit fugit molestias
                perspiciatis.
                Amet, vel, quo sequi modi reiciendis praesentium ut, possimus necessitatibus quasi commodi
                pariatur numquam velit libero nostrum? Corrupti distinctio nesciunt odio unde quos quidem
                eligendi consequatur. Explicabo delectus modi ad?</p>
            <div class="book-additional">
                <div class="info-list">
                    <p>Авторы:</p>
                    <router-link to="#">Фёдор Михайлович Достоевский,</router-link>
                    <router-link to="#">Фёдор Михайлович Достоевский</router-link>
                </div>
                <div class="info-list">
                    <p>Жанры:</p>
                    <router-link to="#">Ужасы,</router-link>
                    <router-link to="#">Проза</router-link>
                </div>
                <p>Дата: 01.01.2001</p>
                <p>Страниц: 300</p>
            </div>
            <div class="book-controls">
                <button @click="markAsRead">
                    <i class="bi bi-plus" v-if="!book.read"></i>
                    <i class="bi bi-dash" v-else></i>
                    Отметить как прочитанную
                </button>
                <button>
                    <i class="bi bi-share"></i>
                    Поделиться
                </button>
                <button>
                    <i class="bi bi-tags"></i>
                    Цены
                </button>
                <button>
                    <i class="bi bi-geo-alt"></i>
                    Карта
                </button>
            </div>
        </div>
    </div>
    <div class="reviews-wrap">
        <div class="reviews-header">
            <h5>Рецензии: 183</h5>
            <select name="" id="">
                <option value="">Сначала новые</option>
                <option value="">Сначала популярные</option>
            </select>
            <button>
                <i class="bi bi-plus-square"></i>
            </button>
        </div>
        <!-- <div>
            <input type="number" name="" id="" v-model="rating">
            <textarea name="" id="" cols="30" rows="10" v-model="text"></textarea>
            <button @click="createReview">Create</button>
        </div> -->
        <div class="reviews-list">
            <div class="review">
                <div class="review-user">
                    <p>ulyanovskiy.01@mail.ru</p>
                    <p>01.01.2001</p>
                </div>
                <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Ad eius tenetur, quas reiciendis dolorem
                    praesentium architecto cum iste enim itaque quidem amet recusandae deserunt tempora error, explicabo
                    perferendis. In, explicabo.
                    Possimus ipsa sapiente tenetur architecto qui deleniti hic laudantium ad, fugiat nam, provident
                    commodi nemo, dignissimos doloremque ex laboriosam placeat dolores iusto consequuntur esse! Cumque
                    recusandae saepe aut. Quod, accusamus?
                    Exercitationem distinctio vero obcaecati! Sit voluptates corrupti rerum, repellendus labore amet
                    nostrum eligendi voluptas facilis dicta magni nemo aliquid enim deserunt. Repellat voluptate facere
                    perspiciatis vel cum cupiditate ratione facilis?</p>
                <button>
                    <i class="bi bi-hand-thumbs-up"></i>
                    1293
                </button>
            </div>
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
    console.log("");
}

const addBookmark = async (): Promise<void> => {
    if (book.value) {
        await bookmarksModule.addBookmark(Guid.parse(book.value.id.toString()));
    }
}

const markAsRead = async (): Promise<void> => {
    if (book.value) {
        await booksModule.markAsRead(Guid.parse(book.value.id.toString()));
        book.value.read = true;
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

.book-wrap .book-img {
    position: relative;
}

.book-wrap .book-img img {
    width: 250px;
}

.book-wrap .book-info {
    padding: 0 10px;
}

.book-info h4 i {
    cursor: pointer;
}

.book-info .book-additional {
    margin: 0 0 1em 0;
}

.book-additional .info-list {
    display: flex;
    column-gap: .3em;
    margin-bottom: .3em;
}

.book-info .book-additional p,
.book-info .book-additional a {
    margin: 0;
    font-size: 14px;
    text-align: left;
}

.book-info .book-additional > p {
    margin: 0 0 .3em 0;
}

.book-info .book-controls {
    display: flex;
    column-gap: 5px;
}

.book-info .book-controls button {
    border: none;
    border-radius: 4px;
    padding: .3em .5em;
}

.book-wrap .book-info p,
.book-wrap .book-info h4 {
    text-align: left;
}

.reviews-wrap {
    width: 50%;
    margin: 0 auto;
    padding: 10px 0;
}

.reviews-header {
    display: flex;
    column-gap: 15px;
    align-items: center;
    user-select: none;
}

.reviews-header h5 {
    margin: 0;
}

.reviews-header button {
    border: none;
    padding: 0;
    color: #f1f1f1;
    font-size: 22px;
    margin-left: auto;
    height: fit-content;
    border-radius: 4px;
    background-color: transparent;
}

.reviews-header select {
    border: none;
    cursor: pointer;
    color: #f1f1f1;
    background-color: transparent;
}

.reviews-header select option {
    color: #000;
}

.reviews-wrap textarea {
    width: 100%;
}

.reviews-list {
    padding: 1em 0;
}

.reviews-list .review p {
    margin: 0;
    text-align: left;
}

.reviews-list .review-user {
    margin-bottom: 7px;
}

.reviews-list .review-user p:last-child {
    color: rgba(128, 128, 128, 0.856);
    font-size: 14px;
}

.reviews-list .review button {
    border: none;
    display: block;
    color: #f1f1f1;
    margin: 15px auto 0 0;
    background-color: transparent;
}
</style>