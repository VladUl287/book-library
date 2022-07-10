<template>
    <div class="books-filters-wrap">
        <input type="text" class="books-input" placeholder="название книги...">
        <button type="button" data-bs-toggle="modal" data-bs-target="#exampleModal">
            <i class="bi bi-filter"></i>
        </button>
        <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
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
        </div>
    </div>
</template>

<script setup lang="ts">

</script>

<style>
.books-filters-wrap {
    display: flex;
    column-gap: .4em;
    align-items: center;
    padding: .5em 16px 0 16px;
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
</style>