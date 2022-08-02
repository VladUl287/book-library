<template>
    <div class="book-card">
        <div class="book-image-wrap">
            <div class="additional-info" :class="{ 'visible': infoVisible }">
                <!-- <div>
                    <p>{{ book.description }}</p>
                </div> -->
                <p>Lorem, ipsum dolor sit amet consectetur adipisicing elit. Praesentium voluptatem nobis,
                    reprehenderit, eaque quidem dolorum doloremque libero magni delectus enim unde a ullam molestiae
                    placeat dignissimos consequuntur sit sequi officia? Lorem ipsum dolor sit amet consectetur
                    adipisicing elit. Reiciendis tenetur iure dolor consectetur beatae quia nemo dicta odio commodi
                    voluptates perferendis, dolorum placeat eum consequatur cumque impedit fugit molestias
                    perspiciatis.
                    Amet, vel, quo sequi modi reiciendis praesentium ut, possimus necessitatibus quasi commodi
                    pariatur numquam velit libero nostrum? Corrupti distinctio nesciunt odio unde quos quidem
                    eligendi consequatur. Explicabo delectus modi ad?</p>
            </div>
            <img :src="book.image" @click="toBookPage" />
        </div>
        <div class="book-info">
            <!-- <div>
                <p class="author">{{ book.authors[0].name }}</p>
                <p class="name" @click="toBookPage">{{ book.name }}</p>
            </div> -->
            <div>
                <p class="author">Фёдор Михайлович Достоевский</p>
                <p class="name">Преступление и наказание</p>
            </div>
            <div class="additional">
                <div class="toggle">
                    <i class="bi bi-menu-app" @click="toogleInfo" v-if="!infoVisible"></i>
                    <i class="bi bi-menu-button-wide" @click="toogleInfo" v-else></i>
                </div>
                <!-- <div v-if="book.rating > 0">
                    <i class="bi bi-star-fill"></i>
                    <p>{{ book.rating }}</p>
                </div> -->
                <div class="book-rate">
                    <i class="bi bi-star-fill"></i>
                    <p>9.00</p>
                </div>
            </div>
        </div>
    </div>
</template>
<script setup lang="ts">
import { Book } from '@/common/contracts';
import { defineProps, PropType, ref } from 'vue';
import { useRouter } from 'vue-router';

const props = defineProps({
    book: {
        type: Object as PropType<Book>,
        required: true
    }
});

const router = useRouter();

const infoVisible = ref(false);

const toogleInfo = () => {
    infoVisible.value = !infoVisible.value;
}

const toBookPage = () => {
    router.push({ name: 'book', params: { bookId: props.book.id.toString() } });
}

</script>

<style scoped>
.book-card {
    width: 100%;
    height: 100%;
    padding: .7em 0;
    max-width: 300px;
    min-width: 200px;
    overflow: hidden;
    position: relative;
    border-radius: .5em;
    border: 1px solid #f1f1f16e;
    background-color: #343a40;
}

.book-card i {
    font-size: 20px;
    cursor: pointer;
}

.book-card .book-image-wrap {
    width: 100%;
    padding: 0 .6em;
    position: relative;
}

.book-image-wrap img {
    width: 100%;
    cursor: pointer;
}

.additional-info {
    top: 0;
    left: 0;
    height: 100%;
    padding: 0 .6em;
    margin: 0 2px 0 0;
    overflow-y: auto;
    overflow-x: hidden;
    color: #f1f1f1;
    position: absolute;
    transform: scaleX(0);
    transform-origin: left;
    background-color: #343a40;
    border-bottom: 1px solid #80808080;
    transition: transform 150ms ease-in-out;
}

.additional-info.visible {
    transform: scaleX(1);
}

.book-card p {
    margin: 0;
    text-align: left;
}

.book-card .book-info {
    display: grid;
    column-gap: .4em;
    row-gap: .3em;
    user-select: none;
    align-items: flex-end;
    padding: .3em .6em 0  .6em;
    grid-template-columns: 1fr auto;
}

.book-card .book-info p {
    margin: 0;
    font-size: 20px;
    text-align: left;
}

.book-card .book-info .name,
.book-card .book-info .author {
    cursor: pointer
}

.book-card .book-info .author {
    font-size: 12px;
    color: #707070;
    margin: 0 0 .2em 0;
}

.book-card .book-info .author:hover {
    text-decoration: underline;
}

.book-card .book-info .name {
    font-size: 17px;
}

.book-info .additional {
    height: 100%;
    display: grid;
    grid-template-rows: auto 1fr;
    align-items: flex-end;
}

.book-info .toggle {
    width: fit-content;
    justify-self: end;
}

.book-info .toggle i {
    font-size: 20px;
}

.book-info .book-rate {
    display: flex;
    column-gap: .2em;
    flex-wrap: nowrap;
}

.book-info .book-rate p,
.book-info .book-rate i {
    font-size: 14px;
}
</style>