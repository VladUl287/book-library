<template>
    <div class="books-filters-wrap">
        <div class="filters-header">
            <input type="text" class="books-input" placeholder="название книги..." v-model="searchName"
                @change="search">
            <button type="button" data-bs-toggle="modal" data-bs-target="#exampleModal">
                <i class="bi bi-filter"></i>
            </button>
        </div>
        <div class="filters.visible">
        </div>
        <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title modal-text" id="exampleModalLabel">
                            Фильтры
                        </h5>
                        <button type="button" class="btn-close modal-text" data-bs-dismiss="modal"
                            aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div>
                            <MultiSelect :items="options" :change="change"/>
                            <!-- <label for="customRange1" class="filter-label">
                                Рейтинг {{ minValueRef }} с по {{ maxValueRef }}
                            </label>
                            <MultiRangeSlider :min="0" :max="10" :ruler="false" :label="false" :minValue="minValueRef"
                                :maxValue="maxValueRef" @input="yearSelect" /> -->
                        </div>
                        <!-- Годы
                        с по
                        <select name="" id=""></select> -->
                        <!--
                        Сортировка
                        Дата добавления
                        По годам -->
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="modal-text">Принять</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { Guid } from 'guid-typescript';
import { Genre } from '@/common/contracts';
import { booksModule } from '@/store/modules/books';
import MultiSelect from '@/components/MultiSelect.vue';
import MultiRangeSlider from "multi-range-slider-vue";

const value: Genre[] = [];
const searchName = ref<string>('');

const minValueRef = ref<number>(0);
const maxValueRef = ref<number>(10);

const options: Genre[] = [
    {
        id: Guid.create(),
        name: 'Vue.js'
    },
    {
        id: Guid.create(),
        name: 'Adonis'
    },
];

const change = (items: any) => {
    console.log(items);
}

const search = async () => {
    booksModule.setFilters({ name: searchName.value });
    await booksModule.getBooks();
}

const yearSelect = ({ minValue, maxValue }: { minValue: number, maxValue: number }) => {
    minValueRef.value = minValue;
    maxValueRef.value = maxValue;
}

</script>

<style scoped>
.books-filters-wrap {
    padding: .5em 16px 0 16px;
}

.books-filters-wrap .filters-header {
    display: flex;
    align-items: center;
    column-gap: .5em;
}

.books-filters-wrap .books-input {
    min-width: 250px;
    width: 400px;
    border: none;
    position: relative;
    padding: .3em .5em;
    border-radius: 4px;
    background-color: #f1f1f1;
}

.books-filters-wrap .filters-header button {
    color: #fff;
    font-size: 20px;
    border-radius: 4px;
    border: 1px solid #fff;
    background-color: transparent;
    transition: all 150ms linear;
}

.books-filters-wrap .filters-header button:hover {
    color: red;
    border: 1px solid red;
    box-shadow: 0 0 7px 1px red;
}

.books-filters-wrap .filters.visible {
    display: block;
}

.books-filters-wrap .modal-text,
.books-filters-wrap .modal-body {
    color: #000;
}

.books-filters-wrap .filter-label {
    width: 100%;
    display: block;
    text-align: left;
}

.books-filters-wrap .bar-inner {
    border: none;
    box-shadow: none;
    background-color: red;
}

.books-filters-wrap .thumb::before {
    border: none;
    box-shadow: none;
    margin: -7px -10px;
    background-color: red;
}
</style>