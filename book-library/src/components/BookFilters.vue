<template>
    <div class="books-filters-wrap">
        <div class="filters-header">
            <input type="text" class="books-input" placeholder="название книги...">
            <button type="button" data-bs-toggle="modal" data-bs-target="#exampleModal">
                <i class="bi bi-filter"></i>
            </button>
        </div>
        <div class="filters.visible">
        </div>
        <!-- <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Modal title</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        ...
                    </div>
                    <div class="modal-footer">
                        <button type="button" data-bs-dismiss="modal">Close</button>
                        <button type="button">Save changes</button>
                    </div>
                </div>
            </div>
        </div> -->
    </div>
</template>

<script setup lang="ts">
import { Genre } from '@/common/contracts';
import { Guid } from 'guid-typescript';

const value: Genre[] = [];

const options: Genre[] = [
    {
        id: Guid.create(),
        name: 'Vue.js'
    },
    {
        id: Guid.create(),
        name: 'Adonis'
    }
];

</script>

<style>
.books-filters-wrap {
    padding: .5em 16px 0 16px;
}

.books-filters-wrap .filters-header {
    display: flex;
    align-items: center;
    column-gap: .5em;
}

.books-filters-wrap .books-input {
    width: 100%;
    border: none;
    padding: .3em .4em;
    border-radius: 4px;
}

.books-filters-wrap button {
    color: #fff;
    font-size: 20px;
    border-radius: 4px;
    border: 1px solid #fff;
    background-color: transparent;
}

.books-filters-wrap .filters {
    /* display: none; */
}

.books-filters-wrap .filters.visible {
    display: block;
}
</style>