<template>
    <div class="book-card">
        <div class="bookmark" @click="removeBookmark(book.id)" v-if="book.bookmark">
            <i class="bi bi-bookmark-dash"></i>
        </div>
        <div class="bookmark" @click="addBookmark(book.id)" v-else>
            <i class="bi bi-bookmark-plus"></i>
        </div>
        <div class="book-image-wrap">
            <div class="additional-info" :class="{ 'visible': infoVisible }">
                <div>
                    <p>{{ book.description }}</p>
                </div>
                <div>
                    <i class="bi bi-star-fill"></i>
                    <p>9.00</p>
                </div>
            </div>
            <img :src="book.image" />
        </div>
        <div class="book-info">
            <div>
                <p class="author">{{ book.authors[0].name }}</p>
                <p class="name">{{ book.name }}</p>
            </div>
            <div class="additional">
                <i class="bi bi-menu-app" @click="toogleInfo" v-if="!infoVisible"></i>
                <i class="bi bi-menu-button-wide" @click="toogleInfo" v-else></i>
            </div>
        </div>
    </div>
</template>
<script setup lang="ts">
import { Guid } from 'guid-typescript';
import { defineProps, PropType, ref } from 'vue';
import { useStore } from 'vuex';
import { Book } from '@/common/contracts';

const props = defineProps({
    book: {
        type: Object as PropType<Book>,
        required: true
    }
});

const store = useStore();

const infoVisible = ref(false);

const toogleInfo = () => {
    infoVisible.value = !infoVisible.value;
}

const addBookmark = async (id: Guid) => {
    try {
        await store.dispatch('AddBookmark', id);
        let book = props.book;
        book.bookmark = true;
        store.commit('pdateBook', book);
    } catch {
        console.log();
    }
}

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
.book-card {
    border-radius: .5em;
    width: 100%;
    height: 100%;
    max-width: 330px;
    min-width: 230px;
    padding: .6em 0;
    position: relative;
    background-color: #edf6f9;
}

.book-card .bookmark {
    top: -7px;
    right: 8px;
    position: absolute;
    z-index: 1000;
}

.book-card i {
    font-size: 24px;
    cursor: pointer;
}

.book-card .book-image-wrap {
    width: 100%;
    padding: 0 .6em;
    position: relative;
}

.additional-info {
    top: 0;
    left: 0;
    width: 0;
    height: 100%;
    overflow: hidden;
    position: absolute;
    display: grid;
    grid-template-rows: 1fr auto;
    background-color: #edf6f9;
    border-bottom: 1px solid #80808080;
    -webkit-transition: width 150ms ease-in-out;
    -moz-transition: width 150ms ease-in-out;
    -o-transition: width 150ms ease-in-out;
    transition: width 150ms ease-in-out;
}

.additional-info.visible {
    width: 100%;
}

.additional-info i {
    color: #ffff00f1;
}

.additional-info div:first-child {
    overflow-y: auto;
    overflow-x: hidden;
    padding: 1em .7em 0 .7em;
}

.additional-info div:last-child {
    display: flex;
    user-select: none;
    font-size: 22px;
    padding: 0 .6em;
    flex-wrap: nowrap;
    align-items: center;
    justify-content: flex-end;
    column-gap: .2em;
}

.book-image-wrap .additional-info p {
    margin: 0;
    text-align: left;
}

.book-image-wrap img {
    width: 100%;
}

.book-info {
    text-align: left;
    user-select: none;
    align-items: center;
    padding: .7em .6em 0 .6em;
    display: grid;
    column-gap: .5em;
    grid-template-columns: 1fr auto;
}

.book-info p {
    margin: 0;
    font-size: 20px;
    text-align: left;
}

.book-info .author {
    font-size: 13px;
    color: #707070;
}
</style>