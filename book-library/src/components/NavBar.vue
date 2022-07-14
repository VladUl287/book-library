<template>
    <nav>
        <ul>
            <li>
                <router-link to="/" class="nav-item">Главная</router-link>
            </li>
            <li>
                <router-link to="/" class="nav-item">Новинки</router-link>
            </li>
            <li>
                <router-link to="/" class="nav-item">Рекомендации</router-link>
            </li>
            <li>
                <router-link to="/bookmarks" class="nav-item">Закладки</router-link>
            </li>
            <li>
                <router-link to="/" class="nav-item">Коллекции</router-link>
            </li>
            <li class="nav-account">
                <router-link to="/" class="nav-item">ulyanovskiy.01@mail.ru</router-link>
            </li>
        </ul>
    </nav>
</template>

<script setup lang="ts">

</script>

<style>
nav {
    width: 100%;
    height: 54px;
    padding: 0 15px;
    background-color: #e76f51;
}

nav ul {
    margin: 0;
    padding: 0;
    height: 100%;
    display: flex;
    column-gap: .5em;
    align-items: center;
    list-style: none;
}

nav ul .nav-item {
    padding: .5em;
    cursor: pointer;
    color: #f1f1f1;
    user-select: none;
    border-radius: .2em;
    text-decoration: none;
    transition: background-color 150ms ease-in-out, box-shadow 250ms ease-in-out;
}

nav ul .nav-item:hover,
nav ul .nav-item:focus {
    color: #fff;
    background-color: #01b075;
    box-shadow: 0 0 10px 0 #01b075;
}

nav .nav-account {
    margin: 0 0 0 auto;
}
</style>