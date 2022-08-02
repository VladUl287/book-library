<template>
    <div class="filtets-wrap">
        <div class="input" @click="() => modalVisible = !modalVisible">
            <p v-if="selectedItems.length === 0">Жанры</p>
            <div v-else class="input-items">
                <p v-for="(genre, i) of selectedItems" :key="i">{{ genre.name }}</p>
            </div>
            <i class="bi bi-caret-down-fill"></i>
        </div>
        <div class="modal-multi-select" v-if="modalVisible">
            <div class="modal-items-list">
                <div>
                    <h4>Жанры</h4>
                </div>
                <div class="list-wrap">
                    <label v-for="(item, i) of items" :key="i" class="modal-item">
                        <input type="checkbox" :checked="selectedItemsHalfe.findIndex(x => x.name === item.name) > -1"
                            @change="select(item)" class="form-check-input m-0">
                        {{ item.name }}
                    </label>
                </div>
                <div class="modal-controls">
                    <button @click="reset">Сброс</button>
                    <button @click="cancelHandle">Отмена</button>
                    <button @click="okHandle">Ок</button>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import { Genre } from '@/common/contracts';
import { defineProps, onMounted, PropType, ref } from 'vue'

type ExtendedItem = {
    selected: boolean
}

const props = defineProps({
    items: Object as PropType<Array<Genre & ExtendedItem>>,
    change: Function
})

const modalVisible = ref(false);
const selectedItems = ref<Genre[]>([]);
const selectedItemsHalfe = ref<Genre[]>([]);

// onMounted(() => {
//     console.log(props.items);
// })

const okHandle = () => {
    if (props.change) {
        selectedItems.value = [...selectedItemsHalfe.value];
        props.change(selectedItems.value);
    }
    modalVisible.value = false;
}

const cancelHandle = () => {
    selectedItemsHalfe.value = [...selectedItems.value];
    modalVisible.value = false;
}

const select = (item: any) => {
    const index = selectedItemsHalfe.value.findIndex(x => x.name === item.name);
    if (index > -1) {
        selectedItemsHalfe.value.splice(index, 1);
    } else {
        selectedItemsHalfe.value.push(item);
    }
}

const reset = () => {
    selectedItemsHalfe.value = [];
}
</script>

<style scoped>
.filtets-wrap .input {
    display: flex;
    flex-wrap: nowrap;
    border-radius: 4px;
    padding: .3em .5em;
    border: 1px solid gray;
}

.filtets-wrap .input-items {
    display: flex;
    gap: .5em;
}

.filtets-wrap .input-items p {
    color: #f1f1f1;
    user-select: none;
    padding: .1em .5em;
    border-radius: .5em;
    background-color: gray;
}

.filtets-wrap .input p {
    margin: 0;
}

.filtets-wrap .input i {
    margin-left: auto;
}

.modal-multi-select {
    position: fixed;
    left: 0;
    top: 0;
    width: 100%;
    height: 100%;
    z-index: 5000;
    color: #f1f1f1;
    background-color: rgba(0, 0, 0, 0.9);
}

.modal-items-list {
    position: absolute;
    top: 30px;
    left: 50%;
    transform: translateX(-50%);
    z-index: 5001;
    padding: 15px;
    min-width: 280px;
    max-height: 80%;
    border: 1px solid #f1f1f167;
    background-color: #212529;
    display: grid;
    row-gap: .6em;
    border-radius: 5px;
    grid-template-rows: auto auto 1fr;
}

.modal-items-list h4 {
    margin: 0;
    user-select: none;
}

.modal-items-list .list-wrap {
    overflow-y: auto;
}

.modal-items-list .modal-item {
    display: flex;
    padding: 0 5px;
    font-size: 17px;
    align-items: center;
    user-select: none;
    column-gap: .3em;
    margin: 0 0 .8em 0;
}

.modal-items-list .modal-controls {
    display: flex;
    column-gap: .4em;
    user-select: none;
    align-items: flex-end;
}

.modal-items-list .modal-controls button:first-child {
    margin: 0 auto 0 0;
    display: block;
}

.modal-items-list .modal-controls button {
    color: #f1f1f1;
    border-radius: 5px;
    border: 1px solid gray;
    background-color: transparent;
}
</style>